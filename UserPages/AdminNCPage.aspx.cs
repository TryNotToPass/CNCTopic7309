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

        protected void gvBaller_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = row.FindControl("lblBirth") as Label;

                var rowData = row.DataItem as Baller;
                string date = rowData.Birth.ToString("yyyy-MM-dd");

                if (date != null)
                {
                    lbl.Text = date;
                }
                else
                {
                    lbl.Text = "無法獲取資料";
                }
            }
        }

        protected void gvRace_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = row.FindControl("lblRaceDate") as Label;

                var rowData = row.DataItem as Race;
                string date = rowData.Date.ToString("yyyy-MM-dd");

                if (date != null)
                {
                    lbl.Text = date;
                }
                else
                {
                    lbl.Text = "無法獲取資料";
                }
            }
        }
    }
}