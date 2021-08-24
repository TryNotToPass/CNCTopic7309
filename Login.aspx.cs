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

        }

        protected void btnTraveler_Click(object sender, EventArgs e)
        {

        }
    }
}