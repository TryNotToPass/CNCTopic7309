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
            HttpContext context = this.Context;
            AutoChangePWD(context);
        }
        public void AutoChangePWD(HttpContext context)
        {
            if (!this.IsPostBack) 
            {
                string id = context.Request.QueryString["ID"];
                Guid guid = new Guid(id);
                //this.lblMsg.Text = id;
                string fakePWD = GetRandomString(8);
                this.lblMsg.Text = fakePWD;
                UserInfo model = new UserInfo()
                {
                    ID = guid,
                    PWD = fakePWD
                };
                LoginHelper.UpdateUserInfo(model);
            }

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
    }
}