using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace CNCTopic7309
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //登入驗證統一在這裡做
            var request = HttpContext.Current.Request;
            var responce = HttpContext.Current.Response;
            string path = request.Url.PathAndQuery;

            //限制某資料夾的東西全部都要登入驗證
            if (path.StartsWith("/UserPages", StringComparison.InvariantCultureIgnoreCase))
            {
                bool isAuth = HttpContext.Current.Request.IsAuthenticated;
                var user = HttpContext.Current.User;
                if (!isAuth || user == null)
                {
                    responce.StatusCode = 403;
                    responce.End();
                    return;
                }

                var identity = HttpContext.Current.User.Identity as FormsIdentity;
                if (identity == null)
                {
                    responce.StatusCode = 403;
                    responce.End();
                    return;
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var error = HttpContext.Current.Error;

            string path = "C:\\Log\\Log.log";

            object locker = new object();
            lock (locker)
            {
                System.IO.File.WriteAllText(path, error.Message);
            }

        }
    }
}