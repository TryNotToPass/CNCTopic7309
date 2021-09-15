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
                //this.Literal1.Text = userinfo.ID.ToString();
                this.txtAccount.Text = userinfo.Account;
                this.txtName.Text = userinfo.Name;
                this.txtEmail.Text = userinfo.Email;
                this.txtOldPWD.Text = string.Empty;
                this.txtNewPWD.Text = string.Empty;
                this.txtAgnPWD.Text = string.Empty;
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

            if (!this.ph_changePWD.Visible)
            {
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
                this.ltlMsg.Text = "資料更新完畢";
            }
            else 
            {
                string oldPWD = userinfo.PWD;

                if (oldPWD.CompareTo(this.txtOldPWD.Text) == 0)
                {
                    if (this.txtNewPWD.Text.CompareTo(this.txtAgnPWD.Text) == 0)
                    {
                        string pwd = this.txtAgnPWD.Text;

                        UserInfo model = new UserInfo()
                        {
                            ID = userinfo.ID,
                            UserLevel = userinfo.UserLevel,
                            PWD = pwd
                        };
                        LoginHelper.UpdateUserInfo(model);
                        this.ltlMsg.Text = "密碼變更完畢";
                        this.txtOldPWD.Text = string.Empty;
                        this.txtNewPWD.Text = string.Empty;
                        this.txtAgnPWD.Text = string.Empty;
                    }
                    else
                    {
                        this.ltlMsg.Text = "兩次密碼輸入不一致";
                        return;
                    }
                }
                else
                {
                    this.ltlMsg.Text = "原密碼輸入錯誤";
                    return;
                }
            }
        }

        protected void btnPWDChange_Click(object sender, EventArgs e)
        {
            this.ltlMsg.Text = string.Empty;
            if (!this.ph_changePWD.Visible)
            {
                this.ph_changePWD.Visible = true;
                this.ph_changeACC.Visible = false;
            }
            else 
            {
                this.ph_changePWD.Visible = false;
                this.ph_changeACC.Visible = true;
            }
        }

        private bool CheckInput(out List<string> msgList)
        {
            List<string> list = new List<string>();

            //確認是否空自串
            if (this.ph_changeACC.Visible)
            {
                if (string.IsNullOrWhiteSpace(this.txtAccount.Text) || string.IsNullOrWhiteSpace(this.txtName.Text) || string.IsNullOrWhiteSpace(this.txtEmail.Text))
                {
                    list.Add("帳號\\暱稱\\信箱處為必填");
                    msgList = list;
                    return false;
                }
            }
            else
            {
                int g = this.txtOldPWD.Text.Length;

                if (string.IsNullOrWhiteSpace(this.txtOldPWD.Text) || string.IsNullOrWhiteSpace(this.txtNewPWD.Text) ||
                    string.IsNullOrWhiteSpace(this.txtAgnPWD.Text) || this.txtNewPWD.Text.Length < 8)
                {
                    list.Add("原密碼\\新密碼\\確認密碼處為必填，且密碼長度需大於八碼");
                    msgList = list;
                    return false;
                }
                    
            }

            msgList = list;
            return true;
        }
    }
}