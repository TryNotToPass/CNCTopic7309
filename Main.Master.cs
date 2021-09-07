using CNCTopic7309.Models;
using CNCTopic7309.UserPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManager;

namespace CNCTopic7309
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //測試_使用主板統一控制權限_可以將下文放入global
            if (this.Page is AdminPageBase)
            {
                var currentUser = LoginHelper.GetCurrentUserInfo();
                if (currentUser == null) 
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                //一般頁面 public partial class Login : System.Web.UI.Page
                //套用AdminPageBase public partial class Login : AdminPageBase => 類別AdminPageBase.cs必須繼承System.Web.UI.Page，詳看該文件
                var adminpage = (AdminPageBase)this.Page;

                //要在需要的頁面class裡放置public override string[] RequiredLevel = new string[]{somthing, somthing};
                if (adminpage.RequiredLevel == UserLevelEnum.Admin && currentUser.UserLevel != (int)UserLevelEnum.Admin) 
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }
            }
        }
    }
}