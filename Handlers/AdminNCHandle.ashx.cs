using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ORM.DBModels;
using UserManager;

namespace CNCTopic7309.Handlers
{
    /// <summary>
    /// AdminNCHandle 的摘要描述
    /// </summary>
    public class AdminNCHandle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string actionName = context.Request.QueryString["Act"];
            string typeName = context.Request.QueryString["Type"];

            if (string.IsNullOrEmpty(actionName) || string.IsNullOrEmpty(typeName))
            {
                this.ProcessError(context, "無法獲取資料");
                return;
            }

            if (actionName == "query")
            {
                string idText = context.Request.Form["ID"];
                int id;
                int.TryParse(idText, out id);

                if (typeName == "Team")
                {
                    var list = ManageHelper.GetTeamByID(id);
                    if (list == null)
                    {
                        this.ProcessError(context, "無法獲取該ID的資料");
                        return;
                    }
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                else if (typeName == "Baller")
                {
                    var list = ManageHelper.GetBallerByID(id);
                    if (list == null)
                    {
                        this.ProcessError(context, "無法獲取該ID的資料");
                        return;
                    }
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                else if (typeName == "Race")
                {
                    var list = ManageHelper.GetRaceByID(id);
                    if (list == null)
                    {
                        this.ProcessError(context, "無法獲取該ID的資料");
                        return;
                    }
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
            }
            else if (actionName == "create")
            {
                if (typeName == "Team")
                {
                    string teamName = context.Request.Form["TeamName"];
                    string local = context.Request.Form["Local"];
                    string ballerCountText = context.Request.Form["BallerCount"];
                    string owner = context.Request.Form["Owner"];
                    string homeCourt = context.Request.Form["HomeCourt"]; //可null
                    string teamColor = context.Request.Form["TeamColor"]; //可null

                    // 必填檢查
                    if (string.IsNullOrWhiteSpace(teamName) ||
                        string.IsNullOrWhiteSpace(local) ||
                        string.IsNullOrWhiteSpace(ballerCountText) ||
                        string.IsNullOrWhiteSpace(owner))
                    {
                        this.ProcessError(context, "資料漏填");
                        return;
                    }

                    // 轉型
                    int ballerCount;
                    if (!int.TryParse(ballerCountText, out ballerCount))
                    {
                        this.ProcessError(context, "球員數量出錯！");
                        return;
                    }

                    try
                    {
                        Team teamData = new Team()
                        {
                            TeamName = teamName,
                            Local = local,
                            BallerCount = ballerCount,
                            Owner = owner,
                            HomeCourt = homeCourt,
                            TeamColor = teamColor
                        };

                        ManageHelper.CreateTeamData(teamData);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("ok");
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 503;
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("Error");
                    }
                }

                if (typeName == "Race")
                {
                    string teamName = context.Request.Form["TeamName"];
                    string raceNumText = context.Request.Form["RaceNum"];
                    string dateText = context.Request.Form["Date"];
                    string shootText = context.Request.Form["Shoot"];
                    string threePointText = context.Request.Form["ThreePoint"];
                    string penaltyText = context.Request.Form["Penalty"];
                    string backBoardText = context.Request.Form["BackBoard"];
                    string assistanceText = context.Request.Form["Assistance"];
                    string blockText = context.Request.Form["Block"];
                    string stealText = context.Request.Form["Steal"];
                    string missText = context.Request.Form["Miss"];
                    string restrictedAreaText = context.Request.Form["RestrictedArea"];
                    string foulText = context.Request.Form["Foul"];

                    // 必填檢查
                    if (string.IsNullOrWhiteSpace(teamName) ||
                        string.IsNullOrWhiteSpace(raceNumText) ||
                        string.IsNullOrWhiteSpace(dateText) ||
                        string.IsNullOrWhiteSpace(shootText) ||
                        string.IsNullOrWhiteSpace(threePointText) ||
                        string.IsNullOrWhiteSpace(penaltyText) ||
                        string.IsNullOrWhiteSpace(backBoardText) ||
                        string.IsNullOrWhiteSpace(assistanceText) ||
                        string.IsNullOrWhiteSpace(blockText) ||
                        string.IsNullOrWhiteSpace(stealText) ||
                        string.IsNullOrWhiteSpace(restrictedAreaText) ||
                        string.IsNullOrWhiteSpace(foulText))
                    {
                        this.ProcessError(context, "資料漏填");
                        return;
                    }

                    // 轉型
                    DateTime date;
                    double shoot, threePoint, penalty;
                    int raceNum, backBoard, assistance, block, steal, miss, ra, foul;
                    if (!double.TryParse(shootText, out shoot) ||
                        !double.TryParse(threePointText, out threePoint) ||
                        !double.TryParse(penaltyText, out penalty) ||
                        !int.TryParse(raceNumText, out raceNum) ||
                        !int.TryParse(backBoardText, out backBoard) ||
                        !int.TryParse(assistanceText, out assistance) ||
                        !int.TryParse(blockText, out block) ||
                        !int.TryParse(stealText, out steal) ||
                        !int.TryParse(restrictedAreaText, out ra) ||
                        !int.TryParse(foulText, out foul) ||
                        !int.TryParse(missText, out miss) ||
                        !DateTime.TryParse(dateText, out date))
                    {
                        this.ProcessError(context, "鍵入資料出錯");
                        return;
                    }

                    try
                    {
                        Race data = new Race()
                        {
                            TeamName = teamName,
                            Shoot = shoot,
                            ThreePoint = threePoint,
                            Penalty = penalty,
                            RaceNum = raceNum,
                            BackBoard = backBoard,
                            Assistance = assistance,
                            Block = block,
                            Steal = steal,
                            RestrictedArea = ra,
                            Miss = miss,
                            Date = date,
                            Foul = foul
                        };

                        ManageHelper.CreateRaceData(data);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("ok");
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 503;
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("Error");
                    }
                }

                if (typeName == "Baller")
                {
                    string teamName = context.Request.Form["TeamName"];
                    string position = context.Request.Form["Position"];
                    string numberText = context.Request.Form["Number"];
                    string name = context.Request.Form["Name"];
                    string heightText = context.Request.Form["Height"];
                    string weightText = context.Request.Form["Weight"];
                    string birthText = context.Request.Form["Birth"];
                    string university = context.Request.Form["University"];

                    // 必填檢查
                    if (string.IsNullOrWhiteSpace(teamName) ||
                        string.IsNullOrWhiteSpace(position) ||
                        string.IsNullOrWhiteSpace(numberText) ||
                        string.IsNullOrWhiteSpace(name) ||
                        string.IsNullOrWhiteSpace(heightText) ||
                        string.IsNullOrWhiteSpace(weightText) ||
                        string.IsNullOrWhiteSpace(birthText) ||
                        string.IsNullOrWhiteSpace(university))
                    {
                        this.ProcessError(context, "資料漏填");
                        return;
                    }

                    // 轉型
                    int height;
                    int weight;
                    int number;
                    DateTime birth;
                    if (!int.TryParse(heightText, out height) ||
                        !int.TryParse(weightText, out weight) ||
                        !int.TryParse(numberText, out number) ||
                        !DateTime.TryParse(birthText, out birth))
                    {
                        this.ProcessError(context, "鍵入資料出錯");
                        return;
                    }

                    try
                    {
                        Baller data = new Baller()
                        {
                            TeamName = teamName,
                            Position = position,
                            Number = number,
                            Name = name,
                            Height = height,
                            Weight = weight,
                            Birth = birth,
                            University = university
                        };

                        ManageHelper.CreateBallerData(data);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("ok");
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 503;
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("Error");
                    }
                }
            }
            else if (actionName == "update")
            {
                if (typeName == "Team")
                {
                    int id = Convert.ToInt32(context.Request.Form["ID"]);
                    string teamName = context.Request.Form["TeamName"];
                    string local = context.Request.Form["Local"];
                    string ballerCountText = context.Request.Form["BallerCount"];
                    string owner = context.Request.Form["Owner"];
                    string homeCourt = context.Request.Form["HomeCourt"]; //可null
                    string teamColor = context.Request.Form["TeamColor"]; //可null

                    // 必填檢查
                    if (string.IsNullOrWhiteSpace(teamName) ||
                        string.IsNullOrWhiteSpace(local) ||
                        string.IsNullOrWhiteSpace(ballerCountText) ||
                        string.IsNullOrWhiteSpace(owner))
                    {
                        this.ProcessError(context, "資料漏填");
                        return;
                    }

                    // 轉型
                    int ballerCount;
                    if (!int.TryParse(ballerCountText, out ballerCount))
                    {
                        this.ProcessError(context, "球員數量出錯！");
                        return;
                    }

                    try
                    {
                        Team teamData = new Team()
                        {
                            ID = id,
                            TeamName = teamName,
                            Local = local,
                            BallerCount = ballerCount,
                            Owner = owner,
                            HomeCourt = homeCourt,
                            TeamColor = teamColor
                        };

                        ManageHelper.UpdateTeamData(teamData);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("ok");
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 503;
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("Error");
                    }
                }

                if (typeName == "Baller")
                {
                    int id = Convert.ToInt32(context.Request.Form["ID"]);
                    string teamName = context.Request.Form["TeamName"];
                    string position = context.Request.Form["Position"];
                    string numberText = context.Request.Form["Number"];
                    string name = context.Request.Form["Name"];
                    string heightText = context.Request.Form["Height"];
                    string weightText = context.Request.Form["Weight"];
                    string birthText = context.Request.Form["Birth"];
                    string university = context.Request.Form["University"];

                    // 必填檢查
                    if (string.IsNullOrWhiteSpace(teamName) ||
                        string.IsNullOrWhiteSpace(position) ||
                        string.IsNullOrWhiteSpace(numberText) ||
                        string.IsNullOrWhiteSpace(name) ||
                        string.IsNullOrWhiteSpace(heightText) ||
                        string.IsNullOrWhiteSpace(weightText) ||
                        string.IsNullOrWhiteSpace(birthText) ||
                        string.IsNullOrWhiteSpace(university))
                    {
                        this.ProcessError(context, "資料漏填");
                        return;
                    }

                    // 轉型
                    int height;
                    int weight;
                    int number;
                    DateTime birth;
                    if (!int.TryParse(heightText, out height) ||
                        !int.TryParse(weightText, out weight) ||
                        !int.TryParse(numberText, out number) ||
                        !DateTime.TryParse(birthText, out birth))
                    {
                        this.ProcessError(context, "鍵入資料出錯");
                        return;
                    }

                    try
                    {
                        Baller data = new Baller()
                        {
                            ID = id,
                            TeamName = teamName,
                            Position = position,
                            Number = number,
                            Name = name,
                            Height = height,
                            Weight = weight,
                            Birth = birth,
                            University = university
                        };

                        ManageHelper.UpdateBallerData(data);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("ok");
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 503;
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("Error");
                    }
                }

                if (typeName == "Race")
                {
                    int id = Convert.ToInt32(context.Request.Form["ID"]);
                    string teamName = context.Request.Form["TeamName"];
                    string raceNumText = context.Request.Form["RaceNum"];
                    string dateText = context.Request.Form["Date"];
                    string shootText = context.Request.Form["Shoot"];
                    string threePointText = context.Request.Form["ThreePoint"];
                    string penaltyText = context.Request.Form["Penalty"];
                    string backBoardText = context.Request.Form["BackBoard"];
                    string assistanceText = context.Request.Form["Assistance"];
                    string blockText = context.Request.Form["Block"];
                    string stealText = context.Request.Form["Steal"];
                    string missText = context.Request.Form["Miss"];
                    string restrictedAreaText = context.Request.Form["RestrictedArea"];
                    string foulText = context.Request.Form["Foul"];

                    // 必填檢查
                    if (string.IsNullOrWhiteSpace(teamName) ||
                        string.IsNullOrWhiteSpace(raceNumText) ||
                        string.IsNullOrWhiteSpace(dateText) ||
                        string.IsNullOrWhiteSpace(shootText) ||
                        string.IsNullOrWhiteSpace(threePointText) ||
                        string.IsNullOrWhiteSpace(penaltyText) ||
                        string.IsNullOrWhiteSpace(backBoardText) ||
                        string.IsNullOrWhiteSpace(assistanceText) ||
                        string.IsNullOrWhiteSpace(blockText) ||
                        string.IsNullOrWhiteSpace(stealText) ||
                        string.IsNullOrWhiteSpace(restrictedAreaText) ||
                        string.IsNullOrWhiteSpace(foulText))
                    {
                        this.ProcessError(context, "資料漏填");
                        return;
                    }

                    // 轉型
                    DateTime date;
                    double shoot, threePoint, penalty;
                    int raceNum, backBoard, assistance, block, steal, miss, ra, foul;
                    if (!double.TryParse(shootText, out shoot) ||
                        !double.TryParse(threePointText, out threePoint) ||
                        !double.TryParse(penaltyText, out penalty) ||
                        !int.TryParse(raceNumText, out raceNum) ||
                        !int.TryParse(backBoardText, out backBoard) ||
                        !int.TryParse(assistanceText, out assistance) ||
                        !int.TryParse(blockText, out block) ||
                        !int.TryParse(stealText, out steal) ||
                        !int.TryParse(restrictedAreaText, out ra) ||
                        !int.TryParse(foulText, out foul) ||
                        !int.TryParse(missText, out miss) ||
                        !DateTime.TryParse(dateText, out date))
                    {
                        this.ProcessError(context, "鍵入資料出錯");
                        return;
                    }

                    try
                    {
                        Race data = new Race()
                        {
                            ID = id,
                            TeamName = teamName,
                            Shoot = shoot,
                            ThreePoint = threePoint,
                            Penalty = penalty,
                            RaceNum = raceNum,
                            BackBoard = backBoard,
                            Assistance = assistance,
                            Block = block,
                            Steal = steal,
                            RestrictedArea = ra,
                            Miss = miss,
                            Date = date,
                            Foul = foul
                        };

                        ManageHelper.UpdateRaceData(data);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("ok");
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 503;
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("Error");
                    }
                }

            }
            else if (actionName == "delete")
            {
                if (context.Request.Form["ID"] == null || string.IsNullOrWhiteSpace(context.Request.Form["ID"]))
                {
                    this.ProcessError(context, "哪裡出錯了");
                    return;
                }
                int id = Convert.ToInt32(context.Request.Form["ID"]);

                if (id == 0)
                {
                    this.ProcessError(context, "哪裡出錯了");
                    return;
                }

                if (typeName == "Team")
                {
                    ManageHelper.DeleteData(id, "T");
                }
                if (typeName == "Baller")
                {
                    ManageHelper.DeleteData(id, "B");
                }
                if (typeName == "Race")
                {
                    ManageHelper.DeleteData(id, "R");
                }
                if (typeName == "Chat")
                {
                    ManageHelper.DeleteData(id, "Chat");
                }
                if (typeName == "Pic")
                {
                    string path = context.Request.Form["Path"];
                    if (path == null || string.IsNullOrWhiteSpace(path))
                    {
                        this.ProcessError(context, "哪裡出錯了");
                        return;
                    }
                    ManageHelper.DeletePic(id, path);
                }
            }
            else if (actionName == "list") 
            {
                if (typeName == "Team")
                {
                    var list = ManageHelper.GetTeamList();
                    if (list == null)
                    {
                        this.ProcessError(context, "無法獲取資料");
                        return;
                    }
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                if (typeName == "Baller")
                {
                    var list = ManageHelper.GetBallerList();
                    if (list == null)
                    {
                        this.ProcessError(context, "無法獲取資料");
                        return;
                    }
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                if (typeName == "Race")
                {
                    var list = ManageHelper.GetRaceList();
                    if (list == null)
                    {
                        this.ProcessError(context, "無法獲取資料");
                        return;
                    }
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
            }

        }

        private void ProcessError(HttpContext context, string msg)
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
            context.Response.End();
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}