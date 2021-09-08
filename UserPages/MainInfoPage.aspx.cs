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
                this.txtChat.Visible = false;
                this.btnSaveChat.Visible = false;
                this.btnUIE.Visible = false;
                this.btnLogOut.Visible = false;

                this.aBackToLogin.Text = "前往登入";
            }

            if (userLV == 0)
            {
                this.btnLvChange.Visible = true;
            }

            string runnerText = "歡迎各位NBA粉絲來此交流！登入才可以盡情發言喔！                                     ";
            runnerText += "發言時請遵守社會準則，請勿在聊天室引戰、談政治、無關緊要的話題、洗版等等，請尊重社會聊天準則                                      ";
            string runner = "<MARQUEE style='font-weight: 700; background-color: whitesmoke;'>" + runnerText + "</MARQUEE>";
            ltRunner.Text = runner;

            if (!this.IsPostBack)
            {
                string about = GetPageQuery();
                int id = Convert.ToInt32(GetIDQuery()); 
                var list = UserPersonalHelper.GetUserChatListByString(about, "Type");

                //產生留言
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

                this.ltlChatBoard.Text = "";
                foreach (var item in list)
                {
                    string guid = item.UserID.ToString();
                    string UserName = LoginHelper.GetUserInfoByString(guid, "GUID").Name;
                    this.ltlChatBoard.Text += "<div class='card text-center'>" +
                                                $"<div class='card-body'><p class='card-text'>{item.Chat}</p><table align='center'><tr><td>" +
                                                $"<input type='hidden' class='hfIDC' value='{item.ID}'/>";

                    int textLV = LoginHelper.GetUserInfoByString(guid, "GUID").UserLevel;

                    if (textLV == 1) this.ltlChatBoard.Text += $"<h6 class='card-subtitle mb-2 text-muted'>來自管理員：{UserName}</h6>";
                    else if (textLV == 0) this.ltlChatBoard.Text += $"<h6 class='card-subtitle mb-2 text-muted'>來自超級管理員：{UserName}</h6>";
                    else this.ltlChatBoard.Text += $"<h6 class='card-subtitle mb-2 text-muted'>來自：{UserName}</h6>";

                    this.ltlChatBoard.Text += $"<div class='card-footer text-muted'>留言日期：{item.Date.ToString("g")}</div>";

                    if (userLV < 2 || guid.CompareTo(correctUserGUID) == 0)
                    {
                        this.ltlChatBoard.Text += "<button type='button' class='btnChatDel'>刪除留言</button>";
                    }
                    this.ltlChatBoard.Text += "</td></tr></table></div></div>";
                }

                //產生基礎資訊
                this.ltlInfo.Text = "<div class='card shadow mb-4'>";
                if (about == "Team") 
                {
                    //單純table
                    var rowInfo = ManageHelper.GetTeamByID(id);
                    this.ltlInfo.Text += $"<div class='card-header py-3'><h6 class='m-0 font-weight-bold text-primary'>{rowInfo.TeamName}</h6>";
                    this.ltlInfo.Text += "<div class='card-body'><div class='table-responsive'><table class='table table-bordered' id='dataTable' width='100%' cellspacing='0'>";
                    this.ltlInfo.Text += "<thead><tr> <th>國籍</th> <th>球員數</th> <th>擁有者</th> <th>主場</th> <th>隊色</th> </tr></thead>";
                    this.ltlInfo.Text += $"<tbody><tr> <td>{rowInfo.Local}</td> <td>{rowInfo.BallerCount}</td> <td>{rowInfo.Owner}</td> <td>{rowInfo.HomeCourt}</td> <td>{rowInfo.TeamColor}</td> </tr></tbody>";
                    this.ltlInfo.Text += "</table></div>";
                }
                if (about == "Race")
                {
                    //條狀圖
                    var rowInfo = ManageHelper.GetRaceByID(id);
                    string date = rowInfo.Date.ToString("yyyyMMdd");
                    this.ltlInfo.Text += $"<div class='card-header py-3'><h6 class='m-0 font-weight-bold text-primary'>第{rowInfo.RaceNum}場-{rowInfo.TeamName}-{date}</h6></div>";
                    this.ltlInfo.Text += "<div class='card-body'>";
                    double shootRate = rowInfo.Shoot;
                    string barColor = GetBarColor(shootRate);
                    this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>投球率<span class='float-right'>{shootRate}%</span></h4>";
                    this.ltlInfo.Text += $"<div class='progress mb-4'><div class='{barColor}' role='progressbar' style='width: {shootRate}%' aria-valuenow='{shootRate}' aria-valuemin='0' aria-valuemax='100'></div></div>";
                    double threeRate = rowInfo.ThreePoint;
                    barColor = GetBarColor(threeRate);
                    this.ltlInfo.Text += $"<h4 class='small font-weight-bold'>三分率<span class='float-right'>{threeRate}%</span></h4>";
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
                if (about == "Baller") 
                {
                    var rowInfo = ManageHelper.GetBallerByID(id);
                    string date = rowInfo.Birth.ToString("yyyyMMdd");
                    this.ltlInfo.Text += $"<div class='card-header py-3'><h6 class='m-0 font-weight-bold text-primary'>{rowInfo.Name}</h6>";
                    this.ltlInfo.Text += "<div class='card-body'><div class='table-responsive'><table class='table table-bordered' id='dataTable' width='100%' cellspacing='0'>";
                    this.ltlInfo.Text += "<thead><tr> <th>隸屬</th> <th>位置</th> <th>背號</th> <th>生日</th> <th>大學</th> </tr></thead>";
                    this.ltlInfo.Text += $"<tbody><tr> <td>{rowInfo.TeamName}</td> <td>{rowInfo.Position}</td> <td>{date}</td> <td>{rowInfo.University}</td> </tr></tbody>";
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

                this.ltlInfo.Text += "</div></div>";

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
                        if (item.PicTitle != null || !string.IsNullOrWhiteSpace(item.PicTitle)) this.ltlInfo.Text += $"<h5 class='card-title'>{item.PicTitle}</h5>";
                        if (item.PicText != null || !string.IsNullOrWhiteSpace(item.PicText)) this.ltlInfo.Text += $"<p class='card-text'>{item.PicText}</p>";
                        if (item.HyperLink != null || !string.IsNullOrWhiteSpace(item.HyperLink)) this.ltlInfo.Text += $"<a href='{item.HyperLink}' class='btn btn-primary'>前往內容連結</a>";
                        this.ltlInfo.Text += $"<input type='hidden' class='hfPDC' value='{item.ID}'/>";
                        this.ltlInfo.Text += $"<input type='hidden' class='hfPath' value='~/FileDownload/{item.About}_pic/{item.Pic}'/>";
                        if (userLV < 2)
                        {
                            this.ltlInfo.Text += "<button type='button' class='btnPicDel'>刪除圖片</button>";
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
                if (correctUserGUID != null || correctUserGUID != "traveler")
                {
                    var userTaste = ManageHelper.GetUsersTasteByGUID(correctUserGUID);
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
                        }
                        else if (about == "Race")
                        {
                            if (userTaste.FavoriteRaceID == id) this.UTLike();
                            else this.UTUnlike();
                        }
                    }
                    else
                    {
                        this.UTUnlike();
                    }
                }
                else 
                {
                    this.UTAllFalse();
                }

            }
        }

        private void UTAllFalse()
        {
            this.btnHeartHole.Visible = false;
            this.btnHeart.Visible = false;
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
            if (string.IsNullOrWhiteSpace(id) || id == null) return "1";

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
                //btnSaveChat.Attributes.Add("OnClientClick", "alert('留言發布失敗！')");
                //btnSaveChat.Attributes.Remove("OnClientClick");
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
            //btnSaveChat.Attributes.Add("OnClientClick", "alert('留言發布成功！')");
            //btnSaveChat.Attributes.Remove("OnClientClick");
            Response.Redirect(Request.RawUrl);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (this.fuInfo.HasFile && FileHelper.ValidFileUpload(this.fuInfo, out List<string> tempList))
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
                string pTitle = this.txtPTitle.Text;
                string pHref = this.txtPHref.Text;
                if (pText != null || string.IsNullOrWhiteSpace(pText)) picture.PicText = pText;
                if (pTitle != null || string.IsNullOrWhiteSpace(pTitle)) picture.PicTitle = pTitle;
                if (pHref != null || string.IsNullOrWhiteSpace(pHref)) picture.HyperLink = pHref;

                ManageHelper.CreatePictureData(picture);
                Response.Redirect(Request.RawUrl);
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
                    var ut = ManageHelper.GetUsersTasteByGUID(guidText);
                    string about = this.GetPageQuery();
                    int id = Convert.ToInt32(this.GetIDQuery());
                    if (ut != null)
                    {
                        if (about == "Team") ut.FavoriteTeamID = id;
                        if (about == "Baller") ut.FavoriteBallerID = id;
                        if (about == "Race") ut.FavoriteRaceID = id;
                        ManageHelper.UpdateUserTaste(ut);
                        this.UTLike();
                    }
                    else
                    {
                        //第一次喜歡
                        UsersTaste newUt = new UsersTaste();
                        newUt.UserID = user.ID;
                        if (about == "Team") newUt.FavoriteTeamID = id;
                        if (about == "Baller") newUt.FavoriteBallerID = id;
                        if (about == "Race") newUt.FavoriteRaceID = id;
                        ManageHelper.CreateUserTaste(newUt);
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
                    var ut = ManageHelper.GetUsersTasteByGUID(guidText);
                    string about = this.GetPageQuery();
                    if (ut != null)
                    {
                        if (about == "Team") ut.FavoriteTeamID = null;
                        if (about == "Baller") ut.FavoriteBallerID = null;
                        if (about == "Race") ut.FavoriteRaceID = null;
                        ManageHelper.UpdateUserTaste(ut);
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
    }
}