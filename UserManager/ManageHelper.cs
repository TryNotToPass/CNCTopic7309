using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.DBModels;

namespace UserManager
{
    public class ManageHelper
    {
        /// <summary>
        /// 獲取使用者表單(限定超級管理)
        /// </summary>
        /// <returns></returns>
        public static List<UserInfo> GetUserList()
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.UserLevel > 0
                         orderby item.UserLevel
                         select item);

                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 刪除使用者(限定超級管理)
        /// </summary>
        /// <param name="ID"></param>
        public static void DeleteUser(Guid ID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbo = context.UserInfoes.Where(o => o.ID == ID).FirstOrDefault();
                    if (dbo != null)
                    {
                        context.UserInfoes.Remove(dbo);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }
    }
}
