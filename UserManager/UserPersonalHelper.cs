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

        /// <summary>
        /// 投票公布結果
        /// </summary>
        /// <returns></returns>
        public static List<PopViewModel> GetPopBallerList(string type)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    if (type == "Baller")
                    {
                        var query =
                            (from item in context.UsersTastes
                             where item.FavoriteBallerID != null
                             join baller in context.Ballers
                         on item.FavoriteBallerID equals baller.ID
                             group item by new { item.FavoriteBallerID, baller.Name }
                                into tempGroup
                             let Cnt = tempGroup.Count()
                             orderby Cnt
                             select new
                             {
                                 tempGroup.Key.Name,
                                 PopCount = tempGroup.Count()
                             });
                        var list = query.ToList();
                        if (list == null) return null;

                        List<PopViewModel> popList = new List<PopViewModel>();
                        foreach (var obj in list)
                        {
                            PopViewModel model = new PopViewModel();
                            model.Name = obj.Name;
                            model.PopCount = obj.PopCount;
                            popList.Add(model);
                        }
                        return popList;
                    }
                    else if (type == "Team") 
                    {
                        var query =
                            (from item in context.UsersTastes
                             where item.FavoriteTeamID != null
                             join team in context.Teams
                         on item.FavoriteTeamID equals team.ID
                             group item by new { item.FavoriteTeamID, team.TeamName }
                                into tempGroup
                             let Cnt = tempGroup.Count()
                             orderby Cnt
                             select new
                             {
                                 tempGroup.Key.TeamName,
                                 PopCount = tempGroup.Count()
                             });
                        var list = query.ToList();
                        if (list == null) return null;

                        List<PopViewModel> popList = new List<PopViewModel>();
                        foreach (var obj in list)
                        {
                            PopViewModel model = new PopViewModel();
                            model.Name = obj.TeamName;
                            model.PopCount = obj.PopCount;
                            popList.Add(model);
                        }
                        return popList;
                    }
                    else if (type == "Race") 
                    {
                        var query =
                            (from item in context.UsersTastes
                             where item.FavoriteRaceID != null
                             join race in context.Races
                         on item.FavoriteRaceID equals race.ID
                             group item by new { item.FavoriteRaceID, race.RaceNum }
                                into tempGroup
                             let Cnt = tempGroup.Count()
                             orderby Cnt
                             select new
                             {
                                 tempGroup.Key.RaceNum,
                                 PopCount = tempGroup.Count()
                             });
                        var list = query.ToList();
                        if (list == null) return null;

                        List<PopViewModel> popList = new List<PopViewModel>();
                        foreach (var obj in list)
                        {
                            PopViewModel model = new PopViewModel();
                            model.Name = "第" + obj.RaceNum.ToString() + "場賽事";
                            model.PopCount = obj.PopCount;
                            popList.Add(model);
                        }
                        return popList;
                    }
                    else if (type == "BBadTemp") 
                    {
                        var query =
                            (from item in context.UsersTastes
                             where item.BadTemperBallerID != null
                             join baller in context.Ballers
                                on item.BadTemperBallerID equals baller.ID
                             group item by new { item.BadTemperBallerID, baller.Name }
                                into tempGroup
                             let Cnt = tempGroup.Count()
                             orderby Cnt
                             select new
                             {
                                 tempGroup.Key.Name,
                                 PopCount = tempGroup.Count()
                             });
                        var list = query.ToList();
                        if (list == null) return null;

                        List<PopViewModel> popList = new List<PopViewModel>();
                        foreach (var obj in list)
                        {
                            PopViewModel model = new PopViewModel();
                            model.Name = obj.Name;
                            model.PopCount = obj.PopCount;
                            popList.Add(model);
                        }
                        return popList;
                    }
                    else if (type == "BFoul")
                    {
                        var query =
                            (from item in context.UsersTastes
                             where item.FoulKingBallerID != null
                             join baller in context.Ballers
                                on item.FoulKingBallerID equals baller.ID
                             group item by new { item.FoulKingBallerID, baller.Name }
                                into tempGroup
                             let Cnt = tempGroup.Count()
                             orderby Cnt
                             select new
                             {
                                 tempGroup.Key.Name,
                                 PopCount = tempGroup.Count()
                             });
                        var list = query.ToList();
                        if (list == null) return null;

                        List<PopViewModel> popList = new List<PopViewModel>();
                        foreach (var obj in list)
                        {
                            PopViewModel model = new PopViewModel();
                            model.Name = obj.Name;
                            model.PopCount = obj.PopCount;
                            popList.Add(model);
                        }
                        return popList;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return null;
            }
        }


        /// <summary>
        /// 藉由GUID取喜好
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static UsersTaste GetUsersTasteByGUID(string Str)
        {
            try
            {
                Guid guid = new Guid(Str);
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UsersTastes
                         where item.UserID == guid
                         select item);
                    var obj = query.FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 登入喜好使用者
        /// </summary>
        /// <param name="data"></param>
        public static void CreateUserTaste(UsersTaste data)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.UsersTastes.Add(data);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }

        /// <summary>
        /// 更新喜好
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateUserTaste(UsersTaste data)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbObject = context.UsersTastes.Where(obj => obj.UserID == data.UserID).FirstOrDefault();
                    dbObject.FavoriteBallerID = data.FavoriteBallerID;
                    dbObject.FavoriteRaceID = data.FavoriteRaceID;
                    dbObject.FavoriteTeamID = data.FavoriteTeamID;

                    dbObject.BadTemperBallerID = data.BadTemperBallerID;
                    dbObject.FoulKingBallerID = data.FoulKingBallerID;
                    context.SaveChanges();

                }
                return true;
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return false;
            }

        }
    }
}
