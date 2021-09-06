using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace UserManager
{
    public class FileHelper
    {
        private const int _mbs = 10;
        private const int _maxLength = _mbs * 1024 * 1024;
        private static string[] _allowFileExt = { ".jpg", ".png" };

        /// <summary> 取得新檔名 </summary>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        public static string GetNewFileName(FileUpload fileUpload)
        {
            string seqText = new Random((int)DateTime.Now.Ticks).Next(0, 1000).ToString().PadLeft(3, '0');

            string orgFileName = fileUpload.FileName;
            string ext = System.IO.Path.GetExtension(orgFileName);
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssFFFFFF") + seqText + ext;
            return newFileName;
        }

        /// <summary> 驗證上傳的檔案 </summary>
        /// <param name="fileUpload"></param>
        /// <param name="msgList">回傳的錯誤訊息</param>
        /// <returns></returns>
        public static bool ValidFileUpload(FileUpload fileUpload, out List<string> msgList)
        {
            msgList = new List<string>();

            if (!ValidFileExt(fileUpload.FileName))
                msgList.Add("僅允許jpg或png");

            if (!ValidFileLength(fileUpload.FileBytes))
                msgList.Add("僅允許 " + _mbs + "MB");

            if (msgList.Any())
                return false;
            else
                return true;
        }


        /// <summary> 驗證檔案長度 </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        private static bool ValidFileLength(byte[] fileContent)
        {
            if (fileContent.Length > _maxLength)
                return false;
            else
                return true;
        }


        /// <summary> 驗證副檔名 </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool ValidFileExt(string fileName)
        {
            // 含有 .
            string ext = System.IO.Path.GetExtension(fileName);

            if (!_allowFileExt.Contains(ext.ToLower()))
                return false;
            else
                return true;
        }

    }
}
