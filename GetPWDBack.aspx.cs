using ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManager;

namespace CNCTopic7309
{
    public partial class GetPWDBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void AutoChangePWD(HttpContext context, Guid id, Guid pwGuid)
        {
            //this.lblMsg.Text = id;
            string fakePWD = GetRandomString(8);
            this.lblMsg.Text = fakePWD;
            UserInfo model = new UserInfo()
            {
                ID = id,
                PWD = fakePWD
            };
            LoginHelper.UpdateUserInfo(model);

            LoginHelper.DeleteForgotPWDInfo(pwGuid);

            this.phAcessSucess.Visible = true;
            this.phInput.Visible = false;
        }

        public static string GetRandomString(int length)
        {
            var str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var next = new Random();
            var builder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                builder.Append(str[next.Next(0, str.Length)]);
            }
            return builder.ToString();
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            HttpContext context = this.Context;
            string idText = context.Request.QueryString["ID"];
            string guidText = context.Request.QueryString["GUID"];
            Guid id;
            Guid guid;
            string account = this.txtAcc.Text;
            if (string.IsNullOrWhiteSpace(idText) || string.IsNullOrWhiteSpace(guidText) || !Guid.TryParse(idText, out id)
                || !Guid.TryParse(guidText, out guid))
            {
                Response.Redirect("ErrorPages/404.html");
                return;
            }
            if (string.IsNullOrWhiteSpace(account))
            {
                this.lblMiss.Text = "帳號未輸入";
                return;
            }
            var user = LoginHelper.GetUserInfoByString(idText, "GUID");
            var pwdInfo = LoginHelper.GetForgetPWDInfo(guid);
            if (user == null || pwdInfo == null || pwdInfo.ExpireDate < DateTime.Now)
            {
                Response.Redirect("ErrorPages/404.html");
                return;
            }

            if (pwdInfo.WrongTime >= 3)
            {
                LoginHelper.DeleteForgotPWDInfo(guid);
                this.lblMiss.Text = "帳號輸入失敗超過三次，請再次去系統請求忘記密碼";
                return;
            }

            if (user.Account.CompareTo(account) != 0) 
            {
                pwdInfo.WrongTime += 1;
                LoginHelper.UpdateFPWD_WrongTimes(pwdInfo);
                this.lblMiss.Text = $"帳號確認失敗，若失敗超過三次，本次取回密碼將被取消。目前失敗：{pwdInfo.WrongTime} 次";
                return;
            }

            AutoChangePWD(context, id, guid);
        }

        protected void txtAcc_TextChanged(object sender, EventArgs e)
        {
            this.lblMiss.Text = string.Empty;
        }
    }
}