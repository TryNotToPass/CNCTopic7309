using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using UserManager;

namespace CNCTopic7309.UserPages
{
    public partial class PermissionChangePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //確認管理員權限
                if (LoginHelper.GetUserType() != 0)
                {
                    Response.Redirect("MainInfoPage.aspx");
                    return;
                }

                var list = ManageHelper.GetUserList();
                if (list.Count > 0)
                {
                    this.GridView1.DataSource = list;
                    this.GridView1.DataBind();
                }
                else
                {
                    this.GridView1.Visible = false;
                }
            }

        }

        //用command替代click以讀取CommandArgument內容
        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            this.label1.Text = e.CommandArgument.ToString();
            //Guid guid = new Guid(e.CommandArgument.ToString());
            //ManageHelper.DeleteUser(guid);
            //Response.Redirect(Request.RawUrl);
        }
    }
}