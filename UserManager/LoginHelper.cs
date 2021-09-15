using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using ORM.DBModels;
using System.Web.Security;
using System.Security.Principal;
using System.Net.Mail;

namespace UserManager
{
    public class LoginHelper
    {
        /// <summary>
        /// 記錄錯誤
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLog(Exception ex) 
        {
            string msg = $@"{DateTime.Now.ToString("G")}{ex.ToString()}";
            string logPath = "C:\\Log\\Log.log";
            string folderPath = System.IO.Path.GetDirectoryName(logPath);

            if (!System.IO.Directory.Exists(folderPath)) System.IO.Directory.CreateDirectory(folderPath);
            if (!System.IO.File.Exists(logPath)) System.IO.File.Create(logPath);
            System.IO.File.AppendAllText(logPath, msg);
            throw ex;
        }

        /// <summary>
        /// 藉由GUID或Account獲取使用者資訊(ORM使用需要EntityFrameWork，web.config那邊也要注意連線字串)
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfoByString(String Str, String t)
        {
            try 
            {
                if (t.CompareTo("GUID") == 0)
                {
                    Guid guid = new Guid();
                    if (!Guid.TryParse(Str, out guid)) return null;
                    //需要安裝entityFrameWork
                    using (ContextModel context = new ContextModel())
                    {
                        var query =
                            (from item in context.UserInfoes
                             where item.ID == guid
                             select item);
                        var obj = query.FirstOrDefault();
                        return obj;
                    }
                }
                else if (t.CompareTo("ACC") == 0)
                {
                    using (ContextModel context = new ContextModel())
                    {
                        var query =
                            (from item in context.UserInfoes
                             where item.Account == Str
                             select item);
                        var obj = query.FirstOrDefault();
                        return obj;
                    }
                }
                else return null;

            }
            catch (Exception ex) 
            {
                WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 直接獲取當前使用者完整資料
        /// </summary>
        /// <returns></returns>
        public static UserInfo GetCurrentUserInfo()
        {
            try
            {
                bool isAuth = HttpContext.Current.Request.IsAuthenticated;
                var user = HttpContext.Current.User;

                if (isAuth && user != null)
                {
                    var identity = HttpContext.Current.User.Identity as FormsIdentity;
                    var infodata = GetUserInfoByString(identity.Ticket.UserData, "GUID");
                    return infodata;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return null;
            }
        }
        /// <summary>
        /// 獲取當前使用者等級
        /// </summary>
        /// <returns></returns>
        public static int GetUserType()
        {
            try
            {
                bool isAuth = HttpContext.Current.Request.IsAuthenticated;
                var user = HttpContext.Current.User;

                if (isAuth && user != null)
                {
                    var identity = HttpContext.Current.User.Identity as FormsIdentity; //獲取Identity裡的ticket並轉型
                    var infodata = GetUserInfoByString(identity.Ticket.UserData, "GUID");
                    return infodata.UserLevel;
                }
                else
                {
                    return 3;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return 3;
            }
        }

        /// <summary>
        /// 更新使用者資訊
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(UserInfo userinfo) 
        {
            //字串應在按下案件前確認，故這裡不確認
            try
            {
                if (string.IsNullOrEmpty(userinfo.PWD))
                {
                    using (ContextModel context = new ContextModel())
                    {
                        var dbObj = context.UserInfoes.Where(o => o.ID == userinfo.ID).FirstOrDefault();
                        dbObj.Account = userinfo.Account;
                        dbObj.Name = userinfo.Name;
                        dbObj.Email = userinfo.Email;
                        context.SaveChanges();
                    }
                }
                else
                {
                    using (ContextModel context = new ContextModel())
                    {
                        var dbObj = context.UserInfoes.Where(o => o.ID == userinfo.ID).FirstOrDefault();
                        dbObj.PWD = userinfo.PWD;
                        context.SaveChanges();
                    }
                }

                return true;
            }
            catch(Exception ex) 
            {
                WriteLog(ex);
                return false;
            }
        }

        /// <summary>
        /// 登入方法
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static bool TryLogin(string account, string pwd, out string errorMsg)
        {
            //檢查
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "帳號或密碼不得為空";
                return false;
            }

            var infodata = GetUserInfoByString(account, "ACC");

            if (infodata == null) 
            {
                errorMsg = "帳號不存在";
                return false;
            }

            if (string.Compare(infodata.Account.ToString(), account, true) == 0 &&
                string.Compare(infodata.PWD.ToString(), pwd, true) == 0)
            {
                //string account = "Admin";
                //string userID = "kindOfUID";
                string[] roles = { "SuperAdmin" };
                int version = 0; //存入使用者等級參數
                bool isPersistance = false;

                //設定系統預設票證
                FormsAuthentication.SetAuthCookie(account, isPersistance);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        version,
                        account,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(60),
                        isPersistance,
                        infodata.ID.ToString()
                    );
                //票證存入Identity
                FormsIdentity identity = new FormsIdentity(ticket);
                HttpCookie cookie = new HttpCookie(
                        FormsAuthentication.FormsCookieName,
                        FormsAuthentication.Encrypt(ticket)
                    );
                cookie.HttpOnly = true;

                //Identity存入GP並放入httpcontext
                GenericPrincipal gp = new GenericPrincipal(identity, roles);
                HttpContext.Current.User = gp;
                HttpContext.Current.Response.Cookies.Add(cookie);

                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "登入失敗，請檢察帳號或密碼";
                return false;
            }

        }

        /// <summary>
        /// 創帳號
        /// </summary>
        /// <param name="user"></param>
        public static void CreateUser(UserInfo user)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    user.ID = Guid.NewGuid();
                    user.UserLevel = 2;
                    context.UserInfoes.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }

        /// <summary>
        /// 寄信
        /// </summary>
        /// <param name="eMail"></param>
        /// <param name="ID"></param>
        public static void SendGmail(string eMail, Guid ID)
        {
            MailMessage mail = new MailMessage();
            //前面是發信email後面是顯示的名稱
            mail.From = new MailAddress("wuyukagura@gmail.com", "NBA冠軍賽系統");

            //收信者email
            mail.To.Add(eMail);

            //設定優先權
            mail.Priority = MailPriority.Normal;

            //標題
            mail.Subject = "忘記密碼信件-此信件由NBA冠軍賽系統自動寄出";

            string idText = ID.ToString();
            //byte[] idByte = Encoding.Default.GetBytes(idText);
            string add = "http://localhost:8082/GetPWDBack.aspx?ID=" + idText;
            //內容
            mail.Body = "<h1>你好，若你沒有忘記NBA冠軍賽系統之密碼，那請無視這張郵件</h1> <br/> <p>請點擊以下連結取回密碼</p> <br/>" +
                "<a href='"+ add + "'>取回密碼連結</a>";

            //內容使用html
            mail.IsBodyHtml = true;

            //設定gmail的smtp (這是google的)
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

            //您在gmail的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("wuyukagura@gmail.com", "9kirisameKagura8");

            //開啟ssl
            MySmtp.EnableSsl = true;

            //發送郵件
            MySmtp.Send(mail);

            //放掉宣告出來的MySmtp
            MySmtp = null;

            //放掉宣告出來的mail
            mail.Dispose();
        }
    }
}
