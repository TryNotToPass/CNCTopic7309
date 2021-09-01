using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNCTopic7309
{
    public partial class TryUpload : System.Web.UI.Page
    {
        private static string[] allowFileExt = {".jpg",".png" };
        private const int mbs = 10;
        private const int maxLength = mbs * 1024 * 1024;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string GetSaveFolderPath() 
        {
            return Server.MapPath("~/FileDownload");
        }

        private bool VaildFileExt(string fileName) 
        {
            string ext = System.IO.Path.GetExtension(fileName);
            if (!allowFileExt.Contains(ext.ToLower()))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private bool VaildFileLength(byte[] fileContent) 
        {
            if (fileContent.Length > maxLength) return false;
            return true;
        }

        private bool VaildFileUpload(FileUpload fileUpload, out List<string> msgList)
        {
            msgList = new List<string>();

            if (!VaildFileExt(fileUpload.FileName))
            {
                this.lblMsg.Text = "you got fucked.";
                msgList.Add("only jpg, png");
            }

            if (!VaildFileLength(fileUpload.FileBytes))
            {
                this.lblMsg.Text = "too big, bro";
                msgList.Add("only allow" + mbs +"MB");
            }

            if (msgList.Any()) return false;
            else return true;

        }

        private string GetNewFileName(FileUpload fileUpload) 
        {
            //方法1睡著防止重名
            //System.Threading.Thread.Sleep(10);

            //方法2亂數防止重名
            string seqText = new Random((int)DateTime.Now.Ticks).Next(0, 1000).ToString().PadLeft(3, '0');

            string oriFileName = fileUpload.FileName;
            string ext = System.IO.Path.GetExtension(oriFileName);
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssFFFFFF") + seqText + ext;
            return newFileName;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            this.lblMsg.Text = "";
            if (this.FileUpload1.HasFile && !this.VaildFileUpload(this.FileUpload1, out List<string> tempMsgList))
            {
                string msg = string.Join("<br/>", tempMsgList);
                this.lblMsg.Text += msg;
                return;
            }
            else 
            {
                string newFileName1 = GetNewFileName(this.FileUpload1);
                string path = System.IO.Path.Combine(this.GetSaveFolderPath(), newFileName1);
                this.FileUpload1.SaveAs(path);
            }

            if (this.FileUpload2.HasFile && !this.VaildFileUpload(this.FileUpload2, out List<string> tempMsgList2))
            {
                string msg = string.Join("<br/>", tempMsgList2);
                this.lblMsg.Text += msg;
                return;
            }
            else 
            {
                string newFileName2 = GetNewFileName(this.FileUpload2);
                string path = System.IO.Path.Combine(this.GetSaveFolderPath(), newFileName2);
                this.FileUpload2.SaveAs(path);
            }

            if (this.FileUpload3.HasFile && !this.VaildFileUpload(this.FileUpload3, out List<string> tempMsgList3))
            {
                string msg = string.Join("<br/>", tempMsgList3);
                this.lblMsg.Text += msg;
                return;
            }
            else 
            {
                string newFileName3 = GetNewFileName(this.FileUpload3);
                string path = System.IO.Path.Combine(this.GetSaveFolderPath(), newFileName3);
                this.FileUpload1.SaveAs(path);
            }

            this.lblMsg.Text = "我可以改變世界，改變自己";

        }
    }
}