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

namespace CNCTopic7309.UserPages
{
    public partial class MainInfoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //確認權限啟用功能
            this.ltlTest.Text = LoginHelper.GetUserType().ToString();
            int userLV = LoginHelper.GetUserType();
            if (userLV < 2)
            {
                this.btnTIE.Visible = true;
                this.btnUpload.Visible = true;
                this.fuInfo.Visible = true;
                this.txtPText.Visible = true;
                this.txtPTitle.Visible = true;
                this.txtPHref.Visible = true;
            }
            else this.PageInit();

            if (userLV == 0)
            {
                this.btnLvChange.Visible = true;
            }

            string runnerText = "跑馬燈跑起來！";
            string runner = "<MARQUEE>" + runnerText + "</MARQUEE>";
            ltRunner.Text = runner;

            if (!this.IsPostBack)
            {
                string about = GetPageQuery();
                int id = Convert.ToInt32(GetIDQuery()); 
                var list = UserPersonalHelper.GetUserChatListByString(about, "Type");

                //產生留言
                string correctUserGUID = LoginHelper.GetCurrentUserInfo().ID.ToString();
                foreach (var item in list)
                {
                    string guid = item.UserID.ToString();
                    string UserName = LoginHelper.GetUserInfoByString(guid, "GUID").Name;
                    this.ltlChatBoard.Text += "<div class='card' style='width: 18rem;'>" +
                                                "<div class='card-body'><table><tr><td>" +
                                                    $"<input type='hidden' class='hfIDC' value='{item.ID}'/>" +
                                                    $"<p class='card-text'>{item.Chat}</p>";

                    int textLV = LoginHelper.GetUserInfoByString(guid, "GUID").UserLevel;

                    if (textLV == 1) this.ltlChatBoard.Text += $"<h6 class='card-subtitle mb-2 text-muted'>來自管理員：{UserName}</h6>";
                    else if (textLV == 0) this.ltlChatBoard.Text += $"<h6 class='card-subtitle mb-2 text-muted'>來自超級管理員：{UserName}</h6>";
                    else this.ltlChatBoard.Text += $"<h6 class='card-subtitle mb-2 text-muted'>來自：{UserName}</h6>";

                    if (userLV < 2 || guid.CompareTo(correctUserGUID) == 0)
                    {
                        this.ltlChatBoard.Text += "<button type='button' class='btnChatDel'>刪除留言</button>";
                    }
                    this.ltlChatBoard.Text += "</td></tr></table></div></div><br />";
                }

                //產生圖片
                var picList = ManageHelper.GetPicListByString(id, about);
                this.ltlInfo.Text = "";
                try
                {
                    foreach (var item in picList)
                    {
                        this.ltlInfo.Text += "<div class='card' style='width: 18rem; '>";
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
                        this.ltlInfo.Text += "</td></tr></table></div></div>";
                    }
                }
                catch (Exception ex)
                {
                    LoginHelper.WriteLog(ex);
                }
            }
        }

        private void PageInit() 
        {
            this.txtPText.Text = String.Empty;
            this.txtPTitle.Text = String.Empty;
            this.txtPHref.Text = String.Empty;
            this.btnTIE.Visible = false;
            this.btnUpload.Visible = false;
            this.fuInfo.Visible = false;
            this.txtPText.Visible = false;
            this.txtPTitle.Visible = false;
            this.txtPHref.Visible = false;
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
    }
}