using ORM.DBModels;
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
                    var pagedlist = this.GetPageDataOfUserinfo(list);
                    this.GridView1.DataSource = pagedlist;
                    this.GridView1.DataBind();

                    this.ucPager.TotalSize = list.Count;
                    this.ucPager.Bind();

                    //this.GridView1.DataSource = list;
                    //this.GridView1.DataBind();
                }
                else
                {
                    this.ucPager.UCCloser();
                    this.GridView1.Visible = false;
                    this.plcNoData.Visible = true;
                }
            }

        }

        private List<UserInfo> GetPageDataOfUserinfo(List<UserInfo> list) 
        {
            int ps = this.ucPager.PageSize;
            //阻止前面資料的顯示
            int startIndex = (this.ucPager.GetCurrentPage() - 1) * ps;
            return list.Skip(startIndex).Take(ps).ToList();
        }

        //用command替代click以讀取CommandArgument內容
        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            //this.label1.Text = e.CommandArgument.ToString();
            Guid guid = new Guid(e.CommandArgument.ToString());
            ManageHelper.DeleteUser(guid);
            Response.Redirect(Request.RawUrl);
        }

        protected void btnUDG_Command(object sender, CommandEventArgs e)
        {
            var userinfo = LoginHelper.GetUserInfoByString(e.CommandArgument.ToString(), "GUID");
            int lv = userinfo.UserLevel;
            if (lv == 2)
            {
                if (ManageHelper.UpdateUserLv(userinfo, 1)) this.lblMsg.Text = "升級成功！";
                else this.lblMsg.Text = "升級失敗！";
            }
            else
            {
                if (ManageHelper.UpdateUserLv(userinfo, 2)) this.lblMsg.Text = "降級成功！";
                else this.lblMsg.Text = "降級失敗！";
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}