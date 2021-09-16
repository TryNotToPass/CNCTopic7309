using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using UserManager;
using ORM.DBModels;
using System.IO;
using CNCTopic7309.Models;

namespace CNCTopic7309.UserPages
{
    public partial class MainInfoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //確認權限啟用功能
            //this.ltlTest.Text = LoginHelper.GetUserType().ToString();
            int userLV = LoginHelper.GetUserType();
            if (userLV < 2)
            {
                this.btnTIE.Visible = true;
                this.btnUpload.Visible = true;
                this.fuInfo.Visible = true;
                this.ph_admin.Visible = true;
            }
            else this.PageInit();

            if (userLV > 2)
            {
                //封鎖訪客不應該看到的東西
                this.phChat.Visible = false;
                this.btnUIE.Visible = false;
                this.btnLogOut.Visible = false;
                this.btnVote.Visible = false;
                this.sendMailtoSA.Visible = false;

                this.aBackToLogin.Visible = true;
                this.aBackToLogin.Text = "前往登入";
            }

            if (userLV == 0)
            {
                this.btnLvChange.Visible = true;
                this.sendMailtoSA.Visible = false;
            }

            #region 跑馬燈
            string runnerText = "歡迎各位NBA粉絲來此交流！登入才可以發言、投票喔！";
            runnerText += "發言時請遵守社會準則，請勿在聊天室引戰、論政治、無關緊要的話題、洗版等等。";
            List<PopViewModel> popBallerList = UserPersonalHelper.GetPopBallerList("Baller");
            List<PopViewModel> popTeamList = UserPersonalHelper.GetPopBallerList("Team");
            List<PopViewModel> popRaceList = UserPersonalHelper.GetPopBallerList("Race");
            List<PopViewModel> popBBadTempList = UserPersonalHelper.GetPopBallerList("BBadTemp");
            List<PopViewModel> popBFoulKingList = UserPersonalHelper.GetPopBallerList("BFoul");
            if (popBallerList != null && popBallerList.Count > 0) 
            {
                runnerText += "★☆★☆★☆★☆★☆★☆★☆★☆──────最受喜愛球星投票！";
                //foreach (var item in popBallerList)
                //{
                //    runnerText += $"球星 {item.Name}：獲得{item.PopCount}人喜愛。";
                //}
                runnerText += $"球星 {popBallerList.Last().Name}：獲得{popBallerList.Last().PopCount}人喜愛。";
            }
            if (popTeamList != null && popTeamList.Count > 0)
            {
                runnerText += "★☆★☆★☆★☆★☆★☆★☆★☆──────最受喜愛球隊投票！";
                //foreach (var item in popTeamList)
                //{
                //    runnerText += $"球隊 {item.Name}：獲得{item.PopCount}人喜愛。";
                //}
                runnerText += $"球隊 {popTeamList.Last().Name}：獲得{popTeamList.Last().PopCount}人喜愛。";
            }
            if (popRaceList != null && popRaceList.Count > 0)
            {
                runnerText += "★☆★☆★☆★☆★☆★☆★☆★☆──────萬眾矚目賽事投票！";
                runnerText += $"{popRaceList.Last().Name}：獲得{popRaceList.Last().PopCount}人讚揚。";
                //foreach (var item in popRaceList)
                //{
                //    runnerText += $"{item.Name}：獲得{item.PopCount}人讚揚。";
                //}
            }
            if (popBFoulKingList != null && popBFoulKingList.Count > 0)
            {
                runnerText += "★☆★☆★☆★☆★☆★☆★☆★☆──────究竟誰才是犯規之鬼？";
                runnerText += $"球星 {popBFoulKingList.Last().Name}是多數人心中的犯規瘋狗，共有{popBFoulKingList.Last().PopCount}人認同。";
            }
            if (popBBadTempList != null && popBBadTempList.Count > 0)
            {
                runnerText += "★☆★☆★☆★☆★☆★☆★☆★☆──────被打手就暴怒整場！脾氣最差的球員是......";
                runnerText += $"球星 {popBBadTempList.Last().Name}是多數人心中的統神，共有{popBBadTempList.Last().PopCount}人認同。";
            }
            string runner = "<MARQUEE style='font-weight: 700; background-color: whitesmoke;'>" + runnerText + "</MARQUEE>";
            ltRunner.Text = runner;
            #endregion

            if (!this.IsPostBack)
            {
                string about = GetPageQuery();
                int id = Convert.ToInt32(GetIDQuery()); 
                var list = UserPersonalHelper.GetUserChatListByString(about, "Type");
                string correctUserGUID;
                if (LoginHelper.GetCurrentUserInfo() != null)
                {
                    correctUserGUID = LoginHelper.GetCurrentUserInfo().ID.ToString();
                }
                else
                {
                    //允許訪客
                    correctUserGUID = "traveler";
                }
                //產生留言
                if (list.Count > 0)
                {
                    //套用分頁
                    var ucpagerChatList = this.GetPageDataOfChat(list);
                    this.ucPager.TotalSize = list.Count;
                    this.ucPager.Bind();

                    this.ltlChatBoard.Text = "";
                    foreach (var item in ucpagerChatList)
                    {
                        string guid = item.UserID.ToString();
                        string UserName = LoginHelper.GetUserInfoByString(guid, "GUID").Name;
                        this.ltlChatBoard.Text += "<div class='card text-center'>" +
                                                    $"<div class='card-body'><p class='card-text'>{item.Chat}</p><table align='center'><tr><td>" +
                                                    $"<input type='hidden' class='hfIDC' value='{item.ID}'/>";

                        int textLV = LoginHelper.GetUserInfoByString(guid, "GUID").UserLevel;

                        if (textLV == 1) this.ltlChatBoard.Text += $"<h6 class='card-subtitle mb-2 text-muted'>來自版主：{UserName}</h6>";
                        else if (textLV == 0) this.ltlChatBoard.Text += $"<h6 class='card-subtitle mb-2 text-muted'>來自超級管理員：{UserName}</h6>";
                        else this.ltlChatBoard.Text += $"<h6 class='card-subtitle mb-2 text-muted'>來自：{UserName}</h6>";

                        this.ltlChatBoard.Text += $"<div class='card-footer text-muted'>留言日期：{item.Date.ToString("g")}</div>";

                        if (userLV < 2 || guid.CompareTo(correctUserGUID) == 0)
                        {
                            this.ltlChatBoard.Text += "<button type='button' class='btnChatDel btn btn-outline-dark'>刪除留言</button>";
                        }
                        this.ltlChatBoard.Text += "</td></tr></table></div></div>";
                    }
                }
                else 
                {
                    this.ucPager.UCCloser();
                }


                //產生基礎資訊
                this.ltlInfo.Text = "<div class='card shadow mb-4'>";
                if (about == "Team") 
                {
                    //單純table
                    var rowInfo = ManageHelper.GetTeamByID(id);
                    if (rowInfo != null)
                    {
                        this.ltlInfo.Text += $"<div class='card-header py-3'><h6 class='m-0 font-weight-bold text-primary'>{rowInfo.TeamName}</h6></div>";
                        this.ltlInfo.Text += "<div class='card-body'><div class='table-responsive'><table class='table table-bordered' id='dataTable' width='100%' cellspacing='0'>";
                        this.ltlInfo.Text += "<thead><tr> <th>國籍</th> <th>球員數</th> <th>擁有者</th> <th>主場</th> <th>隊色</th> </tr></thead>";
                        this.ltlInfo.Text += $"<tbody><tr> <td>{rowInfo.Local}</td> <td>{rowInfo.BallerCount}</td> <td>{rowInfo.Owner}</td> <td>{rowInfo.HomeCourt}</td> <td>{rowInfo.TeamColor}</td> </tr></tbody>";
                        this.ltlInfo.Text += "</table></div></div>";
                    }
                    else 
                    {
                        this.ltlInfo.Text += "<div class='card-body'><div class='table-responsive'>";
                        this.ltlInfo.Text += "資料不存在，請選擇其他資料！</div></div>";
                    }
                    var ballerByTeam = ManageHelper.GetBallerListByTeam(rowInfo.TeamName);
                    if (ballerByTeam != null && ballerByTeam.Count > 0)
                    {
                        this.ltBallerListByTeam.Text += "<div class='list-group mt-2 mb-4'>";
                        this.ltBallerListByTeam.Text += "<li class='list-group-item'>球員列表</li>";
                        foreach (var item in ballerByTeam)
                        {
                            this.ltBallerListByTeam.Text += $"<a href='/UserPages/MainInfoPage.aspx?ID={item.ID}&Type=Baller' class='list-group-item list-group-item-action' aria-current='true'>{item.Name}</a>";
                        }
                        this.ltBallerListByTeam.Text += "</div>";
                    }
                    else
                    {
                        this.ltBallerListByTeam.Text += "<div class='list-group'>";
                        this.ltBallerListByTeam.Text += "<li class='list-group-item'>球員列表</li>";
                        this.ltBallerListByTeam.Text += "<li class='list-group-item disabled'>尚無登入球員資料</li>";
                        this.ltBallerListByTeam.Text += "</div>";
                    }
                }
                if (about == "Race")
                {
                    //條狀圖
                    var rowInfo = ManageHelper.GetRaceByID(id);
                    
                    if (rowInfo != null)
                    {
                        string date = rowInfo.Date.ToString("yyyyMMdd");
                        this.ltlInfo.Text += $"<div class='card-header py-3'><h6 class='m-0 font-weight-bold text-primary'>第{rowInfo.RaceNum}場-{rowInfo.TeamName}-{date}</h6></div>";
                        this.ltlInfo.Text += "<div class='card-body'>";
                        double sr = Convert.ToDouble(rowInfo.Score);
                        double srate = sr / 2;
                        string barColor = GetBarColor(srate);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>得分<span class='float-right'>{rowInfo.Score}</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {srate}%' aria-valuenow='{srate}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        double shootRate = rowInfo.Shoot;
                        barColor = GetBarColor(shootRate);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>投球命中率<span class='float-right'>{shootRate}%</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {shootRate}%' aria-valuenow='{shootRate}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        double threeRate = rowInfo.ThreePoint;
                        barColor = GetBarColor(threeRate);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>三分命中率<span class='float-right'>{threeRate}%</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {threeRate}%' aria-valuenow='{threeRate}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        double penaltyRate = rowInfo.Penalty;
                        barColor = GetBarColor(penaltyRate);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>罰球命中率<span class='float-right'>{penaltyRate}%</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {penaltyRate}%' aria-valuenow='{penaltyRate}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        double bBCnt = rowInfo.BackBoard;
                        barColor = GetBarColor(bBCnt);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>籃板數<span class='float-right'>{bBCnt}</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {bBCnt}%' aria-valuenow='{bBCnt}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        double assisCnt = rowInfo.Assistance;
                        barColor = GetBarColor(assisCnt);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>助攻數<span class='float-right'>{assisCnt}</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {assisCnt}%' aria-valuenow='{assisCnt}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        double blockCnt = rowInfo.Block;
                        barColor = GetBarColor(blockCnt);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>阻截數<span class='float-right'>{blockCnt}</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {blockCnt}%' aria-valuenow='{blockCnt}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        double stealCnt = rowInfo.Steal;
                        barColor = GetBarColor(stealCnt);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>抄球數<span class='float-right'>{stealCnt}</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {stealCnt}%' aria-valuenow='{stealCnt}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        double missCnt = rowInfo.Miss;
                        barColor = GetBarColor(missCnt);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>失誤數<span class='float-right'>{missCnt}</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {missCnt}%' aria-valuenow='{missCnt}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        double raCnt = rowInfo.RestrictedArea;
                        barColor = GetBarColor(raCnt);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>禁區得分<span class='float-right'>{raCnt}</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {raCnt}%' aria-valuenow='{raCnt}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        double foulCnt = rowInfo.Foul;
                        barColor = GetBarColor(foulCnt);
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>犯規數<span class='float-right'>{foulCnt}</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {foulCnt}%' aria-valuenow='{foulCnt}' aria-valuemin='0' aria-valuemax='100'></div></div>";

                    }
                    else 
                    {
                        this.ltlInfo.Text += "<div class='card-body'><div class='table-responsive'>";
                        this.ltlInfo.Text += "資料不存在，請選擇其他資料！</div>";
                    }
                    this.ltlInfo.Text += "</div>";
                }
                if (about == "Baller") 
                {
                    var rowInfo = ManageHelper.GetBallerByID(id);
                    if (rowInfo != null)
                    {
                        string date = rowInfo.Birth.ToString("yyyy/MM/dd");
                        this.ltlInfo.Text += $"<div class='card-header py-3'><h6 class='m-0 font-weight-bold text-primary'>{rowInfo.Name}</h6></div>";
                        this.ltlInfo.Text += "<div class='card-body'><div class='table-responsive'><table class='table table-bordered' id='dataTable' width='100%' cellspacing='0'>";
                        this.ltlInfo.Text += "<thead><tr> <th>隸屬</th> <th>位置</th> <th>背號</th> <th>生日</th> <th>大學</th> </tr></thead>";
                        this.ltlInfo.Text += $"<tbody><tr> <td>{rowInfo.TeamName}</td> <td>{rowInfo.Position}</td> <td>{rowInfo.Number}</td> <td>{date}</td> <td>{rowInfo.University}</td> </tr></tbody>";
                        this.ltlInfo.Text += "</table></div>";
                        int num = rowInfo.Height;
                        double percent = ((double)num / 250) * 100;
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>身高<span class='float-right'>{rowInfo.Height}cm</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='progress-bar bg-info' role='progressbar' style='width: {percent}%' aria-valuenow='{percent}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                        num = rowInfo.Weight;
                        percent = ((double)num / 150) * 100;
                        this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>體重<span class='float-right'>{rowInfo.Weight}kg</span></h4>";
                        this.ltlInfo.Text += $"<div class='progress mb-4'><div class='progress-bar' role='progressbar' style='width: {percent}%' aria-valuenow='{percent}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                    }
                    else
                    {
                        this.ltlInfo.Text += "<div class='card-body'><div class='table-responsive'>";
                        this.ltlInfo.Text += "資料不存在，請選擇其他資料！</div>";
                    }
                    this.ltlInfo.Text += "</div>";
                }

                this.ltlInfo.Text += "</div>";

                //產生管理員PO的文章
                var picList = ManageHelper.GetPicListByString(id, about);
                try
                {
                    this.ltlInfo.Text += "<div class='row row-cols-1 row-cols-md-2'>";
                    foreach (var item in picList)
                    {
                        this.ltlInfo.Text += "<div class='col'><div class='card' style='max-width: 18rem;'>";
                        this.ltlInfo.Text += $"<img src='../FileDownload/{item.About}_pic/{item.Pic}' class='card-img-top'/>";
                        this.ltlInfo.Text += "<div class='card-body'><table><tr><td>";
                        if (item.PicTitle != null && !string.IsNullOrWhiteSpace(item.PicTitle)) this.ltlInfo.Text += $"<h5 class='card-title'>{item.PicTitle}</h5>";
                        if (item.PicText != null && !string.IsNullOrWhiteSpace(item.PicText)) this.ltlInfo.Text += $"<p class='card-text'>{item.PicText}</p>";
                        if (item.HyperLink != null && !string.IsNullOrWhiteSpace(item.HyperLink)) this.ltlInfo.Text += $"<a href='{item.HyperLink}' class='btn btn-outline-info'>前往內容連結</a>";
                        this.ltlInfo.Text += $"<input type='hidden' class='hfPDC' value='{item.ID}'/>";
                        //this.ltlInfo.Text += $"<input type='text' class='hfPath' value='{GetFilePath(item.Pic)}'/>";
                        this.ltlInfo.Text += $"<input type='hidden' class='hfPath' value='/FileDownload/{item.About}_pic/{item.Pic}'/>";
                        if (userLV < 2)
                        {
                            this.ltlInfo.Text += "<button type='button' class='btnPicDel btn btn-outline-dark'>刪除圖片</button>";
                        }
                        this.ltlInfo.Text += "</td></tr></table></div></div></div>";
                    }
                    this.ltlInfo.Text += "</div>";
                }
                catch (Exception ex)
                {
                    LoginHelper.WriteLog(ex);
                }

                //個性標籤確認

                //球員才會顯示得先鎖
                this.btnBadTemp.Visible = false;
                this.btnBadTempFilled.Visible = false;
                this.btnFoulKing.Visible = false;
                this.btnFoulKingFilled.Visible = false;

                if (correctUserGUID != null && correctUserGUID != "traveler")
                {
                    var userTaste = UserPersonalHelper.GetUsersTasteByGUID(correctUserGUID);
                    if (userTaste != null)
                    {
                        if (about == "Team")
                        {
                            if (userTaste.FavoriteTeamID == id) this.UTLike();
                            else this.UTUnlike();
                        }
                        else if (about == "Baller")
                        {
                            if (userTaste.FavoriteBallerID == id) this.UTLike();
                            else this.UTUnlike();

                            this.btnBadTemp.Visible = true;
                            if (userTaste.BadTemperBallerID == id) this.UTbadTemp();
                            else this.UTUnbadTemp();

                            this.btnFoulKing.Visible = true;
                            if (userTaste.FoulKingBallerID == id) this.UTFoulKing();
                            else this.UTUnFoulKing();
                        }
                        else if (about == "Race")
                        {
                            if (userTaste.FavoriteRaceID == id) this.UTLike();
                            else this.UTUnlike();
                        }
                    }
                    else
                    {
                        if (about == "Baller") 
                        {
                            this.UTUnFoulKing();
                            this.UTUnbadTemp();
                        }
                        this.UTUnlike();
                    }
                }
                else 
                {
                    this.UTAllFalse();
                }

            }
        }

        private List<UserChat> GetPageDataOfChat(List<UserChat> list)
        {
            int ps = this.ucPager.PageSize;
            //阻止前面資料的顯示
            int startIndex = (this.ucPager.GetCurrentPage() - 1) * ps;
            return list.Skip(startIndex).Take(ps).ToList();
        }

#region 快速情緒按鈕顯示方法集
        private void UTAllFalse()
        {
            this.btnHeartHole.Visible = false;
            this.btnHeart.Visible = false;
            this.btnBadTemp.Visible = false;
            this.btnBadTempFilled.Visible = false;
            this.btnFoulKing.Visible = false;
            this.btnFoulKingFilled.Visible = false;
        }
        private void UTUnFoulKing()
        {
            this.btnFoulKing.Visible = true;
            this.btnFoulKingFilled.Visible = false;
        }

        private void UTFoulKing()
        {
            this.btnFoulKing.Visible = false;
            this.btnFoulKingFilled.Visible = true;
        }

        private void UTUnbadTemp()
        {
            this.btnBadTemp.Visible = true;
            this.btnBadTempFilled.Visible = false;
        }
        private void UTbadTemp()
        {
            this.btnBadTemp.Visible = false;
            this.btnBadTempFilled.Visible = true;
        }
        private void UTUnlike()
        {
            this.btnHeartHole.Visible = true;
            this.btnHeart.Visible = false;
        }

        private void UTLike()
        {
            this.btnHeartHole.Visible = false;
            this.btnHeart.Visible = true;
        }
#endregion

        private static string GetBarColor(double shootRate)
        {
            string barColor = "progress-bar bg-success";
            if (shootRate <= 25)
            {
                barColor = "progress-bar bg-danger";
            }
            else if (shootRate > 25 && shootRate < 60)
            {
                barColor = "progress-bar bg-warning";
            }

            return barColor;
        }

        private void PageInit() 
        {
            this.txtPText.Text = String.Empty;
            this.txtPTitle.Text = String.Empty;
            this.txtPHref.Text = String.Empty;
            this.btnTIE.Visible = false;
            this.btnUpload.Visible = false;
            this.fuInfo.Visible = false;
            this.ph_admin.Visible = false;
            this.btnLvChange.Visible = false;
        }

        private string GetPageQuery() 
        {
            HttpContext context = this.Context;
            string page = context.Request.QueryString["Type"];
            if (string.IsNullOrWhiteSpace(page) || page == null) return "Team";

            return page;
        }
        private string GetIDQuery()
        {
            HttpContext context = this.Context;
            string id = context.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(id) || id == null || !int.TryParse(id, out int temp)) return "1";

            return id;
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            cookie.Expires = DateTime.Now.AddHours(-5);
            Response.Cookies.Add(cookie);

            HttpContext.Current.User = null;

            Response.Redirect("~/Login.aspx");
        }

        protected void btnUIE_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfoEditor.aspx");
        }

        protected void btnLvChange_Click(object sender, EventArgs e)
        {
            if (LoginHelper.GetUserType() == 0)
            {
                Response.Redirect("PermissionChangePage.aspx");
            }

        }

        protected void btnTIE_Click(object sender, EventArgs e)
        {
            if (LoginHelper.GetUserType() < 2)
            {
                Response.Redirect("AdminNCPage.aspx");
            }
        }

        protected void btnSaveChat_Click(object sender, EventArgs e)
        {
            string page = GetPageQuery();
            string chatText = this.txtChat.Text;
            if (chatText == null || string.IsNullOrWhiteSpace(chatText))
            {
                this.ltAlert.Text = "<script>alert('請輸入文字！');</script>";
                return;
            }
            if (chatText.Length > 2000)
            {
                this.ltAlert.Text = "<script>alert('內容過長！');</script>";
                return;
            }
            var user = LoginHelper.GetCurrentUserInfo();
            if (user == null) 
            {
                Response.Redirect(Request.RawUrl);
                return;
            }

            Guid guid = LoginHelper.GetCurrentUserInfo().ID;
            UserChat userChat = new UserChat()
            {
                UserID = guid,
                Chat = chatText,
                About = page
            }; 
            ManageHelper.CreateChatDB(userChat);
            Response.Redirect(Request.RawUrl);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (this.fuInfo.HasFile && FileHelper.ValidFileUpload(this.fuInfo, out msgList))
            {
                string saveFileName = FileHelper.GetNewFileName(this.fuInfo);
                string filePath = Path.Combine(this.GetSaveFolderPath(), saveFileName);
                this.fuInfo.SaveAs(filePath);

                Picture picture = new Picture()
                {
                    Pic = saveFileName,
                    InfoID = Convert.ToInt32(GetIDQuery()),
                    About = GetPageQuery()
                };

                string pText = this.txtPText.Text;
                if (pText.Length > 2000) 
                {
                    this.ltAlert.Text = "<script>alert('文章過長！');</script>";
                    return;
                }
                string pTitle = this.txtPTitle.Text;
                if (pText.Length > 50)
                {
                    this.ltAlert.Text = "<script>alert('標題過長！');</script>";
                    return;
                }
                string pHref = this.txtPHref.Text;
                if (pText.Length > 1000)
                {
                    this.ltAlert.Text = "<script>alert('連結過長！');</script>";
                    return;
                }
                if (pText != null || string.IsNullOrWhiteSpace(pText)) picture.PicText = pText;
                if (pTitle != null || string.IsNullOrWhiteSpace(pTitle)) picture.PicTitle = pTitle;
                if (pHref != null || string.IsNullOrWhiteSpace(pHref)) picture.HyperLink = pHref;

                ManageHelper.CreatePictureData(picture);
                Response.Redirect(Request.RawUrl);
            }
            else 
            {
                if (msgList.Any())
                {
                    this.ltAlert.Text = "<script>alert('";
                    foreach (var item in msgList)
                    {
                        this.ltAlert.Text += item + "。";
                    }
                    this.ltAlert.Text += "')</script>";
                }
                else this.ltAlert.Text = "<script>alert('必須含有圖片！');</script>";
            }
            
        }
        private string GetSaveFolderPath()
        {
            string page = GetPageQuery();
            return Server.MapPath($"~/FileDownload/{page}_pic");
        }

        //第一次喜歡、再喜歡
        protected void btnHeartHole_Click(object sender, ImageClickEventArgs e)
        {
            try 
            {
                var user = LoginHelper.GetCurrentUserInfo();
                if (user != null)
                {
                    //再喜歡
                    string guidText = user.ID.ToString();
                    var ut = UserPersonalHelper.GetUsersTasteByGUID(guidText);
                    string about = this.GetPageQuery();
                    int id = Convert.ToInt32(this.GetIDQuery());
                    if (ut != null)
                    {
                        if (about == "Team") ut.FavoriteTeamID = id;
                        if (about == "Baller") ut.FavoriteBallerID = id;
                        if (about == "Race") ut.FavoriteRaceID = id;
                        UserPersonalHelper.UpdateUserTaste(ut);
                        this.UTLike();
                        if (about == "Team") 
                        {
                            var data = ManageHelper.GetTeamByID(id);
                            List<PopViewModel> popTeamList = UserPersonalHelper.GetPopBallerList("Team");
                            if (popTeamList != null && popTeamList.Count > 0)
                            {
                                foreach (var item in popTeamList)
                                {
                                    if (data.TeamName.CompareTo(item.Name) == 0)
                                    {
                                        this.ltAlert.Text = $"<script>alert('目前共有{item.PopCount}人喜歡 {item.Name}');</script>";
                                        break;
                                    }
                                    
                                }
                            }
                        }
                        if (about == "Baller")
                        {
                            var data = ManageHelper.GetBallerByID(id);
                            List<PopViewModel> popBallerList = UserPersonalHelper.GetPopBallerList("Baller");
                            if (popBallerList != null && popBallerList.Count > 0)
                            {
                                foreach (var item in popBallerList)
                                {
                                    if (data.Name.CompareTo(item.Name) == 0)
                                    {
                                        this.ltAlert.Text = $"<script>alert('目前共有{item.PopCount}人喜歡 {item.Name}');</script>";
                                        break;
                                    }

                                }
                            }
                        }
                        
                    }
                    else
                    {
                        //第一次喜歡
                        UsersTaste newUt = new UsersTaste();
                        newUt.UserID = user.ID;
                        if (about == "Team") newUt.FavoriteTeamID = id;
                        if (about == "Baller") newUt.FavoriteBallerID = id;
                        if (about == "Race") newUt.FavoriteRaceID = id;
                        UserPersonalHelper.CreateUserTaste(newUt);
                        this.UTLike();
                    }
                    //Response.Redirect(Request.RawUrl);
                }

            } 
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return;
            }

        }

        //取消喜歡
        protected void btnHeart_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var user = LoginHelper.GetCurrentUserInfo();
                if (user != null)
                {
                    string guidText = user.ID.ToString();
                    var ut = UserPersonalHelper.GetUsersTasteByGUID(guidText);
                    string about = this.GetPageQuery();
                    if (ut != null)
                    {
                        if (about == "Team") ut.FavoriteTeamID = null;
                        if (about == "Baller") ut.FavoriteBallerID = null;
                        if (about == "Race") ut.FavoriteRaceID = null;
                        UserPersonalHelper.UpdateUserTaste(ut);
                        this.UTUnlike();
                    }
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return;
            }
        }

        protected void btnBadTemp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var user = LoginHelper.GetCurrentUserInfo();
                if (user != null)
                {
                    //再喜歡
                    string guidText = user.ID.ToString();
                    var ut = UserPersonalHelper.GetUsersTasteByGUID(guidText);
                    string about = this.GetPageQuery();
                    int id = Convert.ToInt32(this.GetIDQuery());
                    if (ut != null)
                    {
                        if (about == "Baller") ut.BadTemperBallerID = id;
                        UserPersonalHelper.UpdateUserTaste(ut);
                        this.UTbadTemp();
                    }
                    else
                    {
                        //第一次喜歡
                        UsersTaste newUt = new UsersTaste();
                        newUt.UserID = user.ID;
                        if (about == "Baller") newUt.BadTemperBallerID = id;
                        UserPersonalHelper.CreateUserTaste(newUt);
                        this.UTbadTemp();
                    }
                    //Response.Redirect(Request.RawUrl);
                }

            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return;
            }
        }

        protected void btnBadTempFilled_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var user = LoginHelper.GetCurrentUserInfo();
                if (user != null)
                {
                    string guidText = user.ID.ToString();
                    var ut = UserPersonalHelper.GetUsersTasteByGUID(guidText);
                    string about = this.GetPageQuery();
                    if (ut != null)
                    {
                        if (about == "Baller") ut.BadTemperBallerID = null;
                        UserPersonalHelper.UpdateUserTaste(ut);
                        this.UTUnbadTemp();
                    }
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return;
            }
        }

        protected void btnFoulKing_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var user = LoginHelper.GetCurrentUserInfo();
                if (user != null)
                {
                    //再喜歡
                    string guidText = user.ID.ToString();
                    var ut = UserPersonalHelper.GetUsersTasteByGUID(guidText);
                    string about = this.GetPageQuery();
                    int id = Convert.ToInt32(this.GetIDQuery());
                    if (ut != null)
                    {
                        if (about == "Baller") ut.FoulKingBallerID = id;
                        UserPersonalHelper.UpdateUserTaste(ut);
                        this.UTFoulKing();

                    }
                    else
                    {
                        //第一次喜歡
                        UsersTaste newUt = new UsersTaste();
                        newUt.UserID = user.ID;
                        if (about == "Baller") newUt.FoulKingBallerID = id;
                        UserPersonalHelper.CreateUserTaste(newUt);
                        this.UTFoulKing();
                    }
                    //Response.Redirect(Request.RawUrl);
                }

            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return;
            }
        }

        protected void btnFoulKingFilled_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var user = LoginHelper.GetCurrentUserInfo();
                if (user != null)
                {
                    string guidText = user.ID.ToString();
                    var ut = UserPersonalHelper.GetUsersTasteByGUID(guidText);
                    string about = this.GetPageQuery();
                    if (ut != null)
                    {
                        if (about == "Baller") ut.FoulKingBallerID = null;
                        UserPersonalHelper.UpdateUserTaste(ut);
                        this.UTUnFoulKing();
                    }
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return;
            }
        }

        protected void btnVote_Click(object sender, EventArgs e)
        {
            if (LoginHelper.GetUserType() < 3) 
            {
                Response.Redirect("VoteInfoPage.aspx");
            }
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            var user = LoginHelper.GetCurrentUserInfo();
            string title = this.txtTitle.Text;
            string content = this.txtContent.Text;
            string userEmail = this.txtMail.Text;
            string type = this.DropDownList1.Text;
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(type)) 
            {
                this.ltAlert.Text = "<script>alert('所有送信填寫欄位不得為空！');</script>";
                return;
            }
            bool ok = LoginHelper.SendGmailToAdmin("fakeemail.@hueoe.com", user.Name, user.Account, userEmail, content, title, type);
            if (ok) this.ltAlert.Text = "<script>alert('送信完成');</script>";  
            else this.ltAlert.Text = "<script>alert('送信失敗');</script>";
        }
    }
}