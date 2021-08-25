using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNCTopic7309
{
    public partial class CssTemp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) 
            {
                string email =  Request.Form["txtEH"];
                string desc =  Request.Form["txtDH"];

                this.ltlMsg.Text = $"E:{email}  D:{desc}";
            }

        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            this.ltlMsg.Text = "OK";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.ltlMsg.Text = $"E:{this.txtEmail} + {this.txtDesc}";
        }
    }
}