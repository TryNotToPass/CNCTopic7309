using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.DBModels;
using System.IO;
using System.Web;

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
                         orderby item.CreateDate descending
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
        /// 獲取球隊單
        /// </summary>
        /// <returns></returns>
        public static List<Baller> GetBallerList()
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Ballers
                         orderby item.ID
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
        /// 獲取隊伍名單
        /// </summary>
        /// <returns></returns>
        public static List<Team> GetTeamList()
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Teams
                         orderby item.ID
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
        /// 獲取賽場資料
        /// </summary>
        /// <returns></returns>
        public static List<Race> GetRaceList()
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Races
                         orderby item.ID
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
        /// 由ID獲取隊伍資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Team GetTeamByID(int id)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Teams
                         where item.ID == id
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
        /// 藉由ID獲取球員資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Baller GetBallerByID(int id)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Ballers
                         where item.ID == id
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
        /// 藉由ID獲取賽場資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Race GetRaceByID(int id)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Races
                         where item.ID == id
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
        /// <summary>
        /// 更新使用者等級(超級管理員)
        /// </summary>
        /// <param name="userinfo"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        public static bool UpdateUserLv(UserInfo userinfo ,int act)
        {
            //字串應在按下案件前確認，故這裡不確認
            try
            {
                if (act > 0 && act < 3 )
                {
                    using (ContextModel context = new ContextModel())
                    {
                        var dbObj = context.UserInfoes.Where(o => o.ID == userinfo.ID).FirstOrDefault();
                        dbObj.UserLevel = act;
                        context.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
                return false;
            }
        }

        /// <summary>
        /// 建立球隊資料(管理員)
        /// </summary>
        /// <param name="teamData"></param>
        public static void CreateTeamData(Team data)
        {

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.Teams.Add(data);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }

        /// <summary>
        /// 建立球員資料
        /// </summary>
        /// <param name="data"></param>
        public static void CreateBallerData(Baller data)
        {

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.Ballers.Add(data);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }
        /// <summary>
        /// 建立賽場資料
        /// </summary>
        /// <param name="data"></param>
        public static void CreateRaceData(Race data)
        {

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.Races.Add(data);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }

        /// <summary>
        /// 建立聊天資料
        /// </summary>
        /// <param name="data"></param>
        public static void CreateChatDB(UserChat data)
        {

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    data.Date = DateTime.Now;
                    context.UserChats.Add(data);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }

        /// <summary>
        /// 更新球隊資料(管理員)
        /// </summary>
        /// <param name="teamData"></param>
        /// <returns></returns>
        public static bool UpdateTeamData(Team teamData)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbObject = context.Teams.Where(obj => obj.ID == teamData.ID).FirstOrDefault();
                    dbObject.TeamName = teamData.TeamName;
                    dbObject.Local = teamData.Local;
                    dbObject.BallerCount = teamData.BallerCount;
                    dbObject.Owner = teamData.Owner;
                    dbObject.HomeCourt = teamData.HomeCourt;
                    dbObject.TeamColor = teamData.TeamColor;
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
        /// <summary>
        /// 更新球員資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateBallerData(Baller data)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbObject = context.Ballers.Where(obj => obj.ID == data.ID).FirstOrDefault();
                    dbObject.TeamName = data.TeamName;
                    dbObject.Position = data.Position;
                    dbObject.Number = data.Number;
                    dbObject.Name = data.Name;
                    dbObject.Height = data.Height;
                    dbObject.Weight = data.Weight;
                    dbObject.Birth = data.Birth;
                    dbObject.University = data.University;
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
        /// <summary>
        /// 更新賽場資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateRaceData(Race data)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbObject = context.Races.Where(obj => obj.ID == data.ID).FirstOrDefault();
                    dbObject.RaceNum = data.RaceNum;
                    dbObject.Date = data.Date;
                    dbObject.TeamName = data.TeamName;
                    dbObject.Shoot = data.Shoot;
                    dbObject.ThreePoint = data.ThreePoint;
                    dbObject.Penalty = data.Penalty;
                    dbObject.BackBoard = data.BackBoard;
                    dbObject.Assistance = data.Assistance;
                    dbObject.Block = data.Block;
                    dbObject.Steal = data.Steal;
                    dbObject.Miss = data.Miss;
                    dbObject.RestrictedArea = data.RestrictedArea;
                    dbObject.Foul = data.Foul;
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

        /// <summary>
        /// 資訊類資料刪除(管理員)
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type">T,B,R,Chat</param>
        public static void DeleteData(int ID, string type)
        {
            try
            {
                if (type == "T")
                {
                    using (ContextModel context = new ContextModel())
                    {
                        var dbo = context.Teams.Where(o => o.ID == ID).FirstOrDefault();
                        if (dbo != null)
                        {
                            context.Teams.Remove(dbo);
                            context.SaveChanges();
                        }
                    }
                    //DeleteSameTypePic(ID, "Team");
                }
                else if ( type == "B")
                {
                    using (ContextModel context = new ContextModel())
                    {
                        var dbo = context.Ballers.Where(o => o.ID == ID).FirstOrDefault();
                        if (dbo != null)
                        {
                            context.Ballers.Remove(dbo);
                            context.SaveChanges();
                        }
                    }
                    //DeleteSameTypePic(ID, "Baller");
                } 
                else if (type == "R")
                {
                    using (ContextModel context = new ContextModel())
                    {
                        var dbo = context.Races.Where(o => o.ID == ID).FirstOrDefault();
                        if (dbo != null)
                        {
                            context.Races.Remove(dbo);
                            context.SaveChanges();
                        }
                    }
                    //DeleteSameTypePic(ID, "Race");
                }
                else if (type == "Chat")
                {
                    using (ContextModel context = new ContextModel())
                    {
                        var dbo = context.UserChats.Where(o => o.ID == ID).FirstOrDefault();
                        if (dbo != null)
                        {
                            context.UserChats.Remove(dbo);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }

        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <param name="picture"></param>
        public static void CreatePictureData(Picture picture)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.Pictures.Add(picture);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }

        /// <summary>
        /// 藉由type跟ID獲取圖片單
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<Picture> GetPicListByString(int ID, String t)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Pictures
                            where item.About == t && item.InfoID == ID
                            orderby item.ID
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
        /// 刪除圖片
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="path"></param>
        public static void DeletePic(int ID, string path)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbo = context.Pictures.Where(o => o.ID == ID).FirstOrDefault();
                    if (dbo != null)
                    {
                        context.Pictures.Remove(dbo);
                        context.SaveChanges();
                    }
                }
                //由於路徑問題會失敗
                File.Delete(path);
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }

        public static void DeleteSameTypePic(int infoID, string type)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbo = context.Pictures.Where(o => o.InfoID == infoID && o.About == type);
                    if (dbo != null)
                    {
                        foreach (var item in dbo)
                        {
                            context.Pictures.Remove(item);
                            context.SaveChanges();
                            File.Delete(HttpContext.Current.Server.MapPath($"~/FileDownload/{type}_pic/{item.Pic}"));
                        }
                    }
                }
                //由於路徑問題會失敗
            }
            catch (Exception ex)
            {
                LoginHelper.WriteLog(ex);
            }
        }

    }
}
