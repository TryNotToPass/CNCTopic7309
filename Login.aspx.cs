using ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using UserManager;

namespace CNCTopic7309
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //如果已經登入，就導進主畫面
            var user = LoginHelper.GetCurrentUserInfo();
            if (user != null) Response.Redirect("UserPages/MainInfoPage.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string eMsg;
            //if ("aaa" == this.txtAccount.Text && "111" == this.txtPassword.Text)
            if (LoginHelper.TryLogin(this.txtAccount.Text, this.txtPassword.Text, out eMsg))
            {
                Response.Redirect("UserPages/MainInfoPage.aspx");
            }
            else 
            {
                this.ltlMsg.Text = eMsg;
            }
        }

        protected void btnForget_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtAccount.Text)) 
            {
                this.ltlMsg.Text = "請將帳號輸入於上方帳號欄位中";
                return;
            }

            var list = LoginHelper.GetUserInfoByString(this.txtAccount.Text, "ACC");
            if (list == null) 
            {
                this.ltlMsg.Text = "已經將取回密碼的信件寄送至你的信箱，請注意查收";
                return; 
            }

            string mail = list.Email;
            //LoginHelper.SendGmail("markzmarkz725@gmail.com", list.ID);
            LoginHelper.SendGmail(mail, list.ID);
            this.ltlMsg.Text = "已經將取回密碼的信件寄送至你的信箱，請注意查收";
        }

        protected void btnTraveler_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserPages/MainInfoPage.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltlMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            //資料鍵入
            UserInfo user = new UserInfo()
            {
                Account = this.txtAccCreate.Text,
                PWD = this.txtPWCreate.Text,
                Email = this.txtMail.Text,
                Name = this.txtName.Text
            };
            LoginHelper.CreateUser(user);
            this.ltlMsg.Text = "帳號創建成功，請重新登入";

        }
        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();
            //檢查漏填
            if (string.IsNullOrWhiteSpace(this.txtAccCreate.Text) ||
                string.IsNullOrWhiteSpace(this.txtPWCreate.Text) ||
                string.IsNullOrWhiteSpace(this.txtPWCheck.Text) ||
                string.IsNullOrWhiteSpace(this.txtMail.Text) ||
                string.IsNullOrWhiteSpace(this.txtName.Text))
                msgList.Add("所有欄位皆為必填！");

            //檢查mail重複
            var list = ManageHelper.GetUserList();
            foreach (var item in list)
            {
                if (this.txtMail.Text.CompareTo(item.Email) == 0) 
                {
                    msgList.Add("該信箱已經註冊！");
                    break;
                }
            }

            //檢查密碼
            if (this.txtPWCreate.Text.Length < 8 || this.txtPWCreate.Text.Length > 12)
            {
                msgList.Add("密碼最少要8碼，且小於13碼");
            }
            else if (this.txtPWCreate.Text.CompareTo(this.txtPWCheck.Text) != 0) 
            {
                msgList.Add("密碼輸入不一致");
            }

            errorMsgList = msgList;
            if (msgList.Count == 0)
                return true;
            else
                return false;
        }
    }
}