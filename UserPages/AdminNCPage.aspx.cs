using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManager;

namespace CNCTopic7309.UserPages
{
    public partial class AdminNCPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //確認管理員權限
            if (LoginHelper.GetUserType() > 1)
            {
                Response.Redirect("MainInfoPage.aspx");
                return;
            }
            if (!this.IsPostBack)
            {
                var blist = ManageHelper.GetBallerList();
                var tlist = ManageHelper.GetTeamList();
                var rlist = ManageHelper.GetRaceList();
                if (blist.Count > 0)
                {
                    this.gvBaller.DataSource = blist;
                    this.gvBaller.DataBind();

                }
                else
                {
                    this.gvBaller.Visible = false;
                }

                if (tlist.Count > 0)
                {
                    this.gvTeam.DataSource = tlist;
                    this.gvTeam.DataBind();

                }
                else
                {
                    this.gvTeam.Visible = false;
                }

                if (rlist.Count > 0)
                {
                    this.gvRace.DataSource = rlist;
                    this.gvRace.DataBind();

                }
                else
                {
                    this.gvRace.Visible = false;
                }
            }
        }

    }
}