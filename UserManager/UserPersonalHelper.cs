using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.DBModels;

namespace UserManager
{
    public class UserPersonalHelper
    {
        /// <summary>
        /// 藉由字串獲取聊天資訊
        /// </summary>
        /// <param name="Str">(Type=>Race, Baller, Team)</param>
        /// <param name="t">(Type, GUID)</param>
        /// <returns></returns>
        public static List<UserChat> GetUserChatListByString(String Str, String t)
        {
            try
            {
                if (t.CompareTo("GUID") == 0)
                {
                    Guid guid = new Guid();
                    if (!Guid.TryParse(Str, out guid)) return null;
                    //需要安裝entityFrameWork
                    using (ContextModel context = new ContextModel())
                    {
                        var query =
                            (from item in context.UserChats
                             where item.UserID == guid
                             select item);

                        var list = query.ToList();
                        return list;
                    }
                }
                else if (t.CompareTo("Type") == 0)
                {
                    using (ContextModel context = new ContextModel())
                    {
                        var query =
                            (from item in context.UserChats
                             where item.About == Str
                             select item);
                        var list = query.ToList();
                        return list;
                    }
                }
                else return null;

            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return null;
            }
        }
    }
}
