using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using UserManager;
using ORM.DBModels;

namespace CNCTopic7309.UserPages
{
    public partial class UserInfoEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var userinfo = LoginHelper.GetCurrentUserInfo();
                this.Literal1.Text = userinfo.ID.ToString();
                this.txtAccount.Text = userinfo.Account;
                this.txtName.Text = userinfo.Name;
                this.txtEmail.Text = userinfo.Email;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltlMsg.Text = string.Join("<br/>", msgList);
                return;
            }
            var userinfo = LoginHelper.GetCurrentUserInfo();

            //UserInfoModel model = new UserInfoModel()
            //{
            //    ID = userinfo.ID,
            //    UserLevel = userinfo.UserLevel,
            //    Account = this.txtAccount.Text,
            //    Name = this.txtName.Text,
            //    Email = this.txtEmail.Text
            //};
            string acc = this.txtAccount.Text;
            string name = this.txtName.Text;
            string email = this.txtEmail.Text;

            UserInfo model = new UserInfo()
            {
                ID = userinfo.ID,
                UserLevel = userinfo.UserLevel,
                Account = acc,
                Name = name,
                Email = email
            };
            LoginHelper.UpdateUserInfo(model);
            Response.Redirect(Request.RawUrl);

        }

        private bool CheckInput(out List<string> msgList)
        {
            List<string> list = new List<string>();
            list.Add("now testing");

            msgList = list;
            return true;
        }
    }
}