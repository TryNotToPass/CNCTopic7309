using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManager;

namespace CNCTopic7309.UserPages
{
    public partial class VoteInfoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = LoginHelper.GetCurrentUserInfo();
            string guid = user.ID.ToString();
            var voteList = UserPersonalHelper.GetUsersTasteByGUID(guid);

            #region 建置參數
            string fvBallerName, fvTeamName, fvRaceNum, btBallerName, fkBallerName;
            string fvBallerLink, fvTeamLink, fvRaceLink, btBallerLink, fkBallerLink;
            if (voteList.FavoriteBallerID != null)
            {
                int id = Convert.ToInt32(voteList.FavoriteBallerID);
                fvBallerName = ManageHelper.GetBallerByID(id).Name;
                fvBallerLink = $"<td><a href='/UserPages/MainInfoPage.aspx?ID=${voteList.FavoriteBallerID}&Type=Baller'>{fvBallerName}</a></td>";
            }
            else fvBallerLink = "<td>尚無 <a href='/UserPages/MainInfoPage.aspx?ID=1&Type=Baller'>現在立刻去找</a></td>";
            if (voteList.FavoriteTeamID != null)
            {
                int id = Convert.ToInt32(voteList.FavoriteTeamID);
                fvTeamName = ManageHelper.GetTeamByID(id).TeamName;
                fvTeamLink = $"<td><a href='/UserPages/MainInfoPage.aspx?ID={voteList.FavoriteTeamID}&Type=Team'>{fvTeamName}</a></td>";
            }
            else fvTeamLink = "<td>尚無 <a href='/UserPages/MainInfoPage.aspx?ID=1&Type=Team'>現在立刻去找</a></td>";
            if (voteList.FavoriteRaceID != null)
            {
                int id = Convert.ToInt32(voteList.FavoriteRaceID);
                fvRaceNum ="第 " + ManageHelper.GetRaceByID(id).RaceNum.ToString() + " 場賽事";
                fvRaceLink = $"<td><a href='/UserPages/MainInfoPage.aspx?ID={voteList.FavoriteRaceID}&Type=Race'>{fvRaceNum}</a></td>";
            }
            else fvRaceLink = "<td>尚無 <a href='/UserPages/MainInfoPage.aspx?ID=1&Type=Race'>現在立刻去找</a></td>";
            if (voteList.BadTemperBallerID != null)
            {
                int id = Convert.ToInt32(voteList.BadTemperBallerID);
                btBallerName = ManageHelper.GetBallerByID(id).Name;
                btBallerLink = $"<td><a href='/UserPages/MainInfoPage.aspx?ID={voteList.BadTemperBallerID}&Type=Baller'>{btBallerName}</a></td>";
            }
            else btBallerLink = "<td>尚無 <a href='/UserPages/MainInfoPage.aspx?ID=1&Type=Baller'>現在立刻去找</a></td>";
            if (voteList.FoulKingBallerID != null)
            {
                int id = Convert.ToInt32(voteList.FoulKingBallerID);
                fkBallerName = ManageHelper.GetBallerByID(id).Name;
                fkBallerLink = $"<td><a href='/UserPages/MainInfoPage.aspx?ID={voteList.FoulKingBallerID}&Type=Baller'>{fkBallerName}</a></td>";
            }
            else fkBallerLink = "<td>尚無 <a href='/UserPages/MainInfoPage.aspx?ID=1&Type=Baller'>現在立刻去找</a></td>";
            #endregion
            //產生列表
            this.ltVoteTable.Text = "<table class='table table-primary' style='margin: auto; border:solid 2px;'>";
            this.ltVoteTable.Text += "<thead><th>最喜歡的球員</th> <th>最喜歡的隊伍</th> <th>最喜歡的賽事</th> <th>脾氣最差的球員</th> <th>最會犯規的球員</th></thead>";
            this.ltVoteTable.Text += $"<tbody><tr>";
            this.ltVoteTable.Text += fvBallerLink;
            this.ltVoteTable.Text += fvTeamLink;
            this.ltVoteTable.Text += fvRaceLink;
            this.ltVoteTable.Text += btBallerLink;
            this.ltVoteTable.Text += fkBallerLink;
            this.ltVoteTable.Text += "</tr></tbody>";
            this.ltVoteTable.Text += "</table>";

        }
    }
}