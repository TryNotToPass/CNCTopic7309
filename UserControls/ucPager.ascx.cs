using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNCTopic7309.UserControls
{
    public partial class ucPager : System.Web.UI.UserControl
    {
        /// <summary>鎖定頁面URL</summary>
        public string Url { get; set; }
        /// <summary>總筆數</summary>
        public int TotalSize { get; set; }
        /// <summary>總頁數</summary>
        public int PageSize { get; set; }
        /// <summary>目前頁數</summary>
        public int CurrentPage { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Bind() 
        {
            int totalPages = this.GetTotalPages();
            int cp = GetCurrentPage();
            int firstP = cp - 1;

            this.aLinkF.HRef = $"{this.Url}?page=1";
            this.aLinkL.HRef = $"{this.Url}?page={totalPages}";

            this.ltlMsg.Text = $"共 {this.TotalSize} 筆，共 {totalPages} 頁，目前在第 {cp} 頁。";
            for (var i = firstP; i <= cp + 1; i++) 
            {
                if (i == firstP + 1) this.ltlPager.Text += $"&nbsp {i} &nbsp";
                else if (i <= 0) cp += 1;
                else if (i > totalPages) break;
                else this.ltlPager.Text += $"<a href='{this.Url}?page={i}'>{i}</a> &nbsp;";
            }
        
        }

        public int GetCurrentPage() 
        {
            string pageText = Request.QueryString["Page"];
            if (string.IsNullOrWhiteSpace(pageText)) return 1;
            int page;
            if (!int.TryParse(pageText, out page)) return 1;
            if (page <= 0) return 1;

            return page;
        }

        private int GetTotalPages() 
        {
            int pagers = this.TotalSize / this.PageSize;
            if ((this.TotalSize % this.PageSize) > 0) pagers += 1;
            return pagers;
        }

        public void UCCloser()
        {
            this.aLinkF.Visible = false;
            this.aLinkL.Visible = false;
        }

    }
}