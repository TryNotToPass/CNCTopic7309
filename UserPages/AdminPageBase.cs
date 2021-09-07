using CNCTopic7309.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CNCTopic7309.UserPages
{
    public class AdminPageBase : System.Web.UI.Page
    {
        public UserLevelEnum RequiredLevel { get; set; } = UserLevelEnum.Regular;
        public virtual string[] RequiredRoles { get; set; } = null;
    }
}