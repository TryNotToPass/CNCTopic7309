<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminNCPage.aspx.cs" Inherits="CNCTopic7309.UserPages.AdminNCPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../js/jQuery360.js"></script>
    <script>
        $(function () {
            $("#divTeamEditor").hide();
            $("#divBallerEditor").hide();
            $("#divRaceEditor").hide();
            //alert("JQ成功");
            //新增、更新
            $("#btnTeamSave").click(function () {

                var id = $("#hfIDT").val();
                var teamName = $("#txtTTName").val();
                var local = $("#txtLocal").val();
                var ballerCount = $("#txtBallerCount").val();
                var owner = $("#txtOwner").val();
                var homeCourt = $("#txtHomeCourt").val();
                var teamColor = $("#txtTeamColor").val();
                //if (!teamName || !local || !ballerCount || !teamColor) {
                //    alert("必填資料尚未填寫");
                //    return;
                //}
                if (id) {
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=update&Type=Team",
                        type: "POST",
                        data: {
                            "ID": id,
                            "TeamName": teamName,
                            "Local": local,
                            "BallerCount": ballerCount,
                            "Owner": owner,
                            "HomeCourt": homeCourt,
                            "TeamColor": teamColor
                        },
                        success: function (result) {
                            alert("更新成功");
                            window.location.reload();
                        }
                    });
                }
                else {
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=create&Type=Team",
                        type: "POST",
                        data: {
                            "TeamName": teamName,
                            "Local": local,
                            "BallerCount": ballerCount,
                            "Owner": owner,
                            "HomeCourt": homeCourt,
                            "TeamColor": teamColor
                        },
                        success: function (result) {
                            alert("新增成功");
                            window.location.reload();
                        }
                    });
                }
            });

            $("#btnBallerSave").click(function () {

                var id = $("#hfIDB").val();
                var teamName = $("#txtBTName").val();
                var position = $("#txtPosition").val();
                var number = $("#txtNumber").val();
                var name = $("#txtName").val();
                var height = $("#txtHeight").val();
                var weight = $("#txtWeight").val();
                var birth = $("#txtBirth").val();
                var university = $("#txtUniversity").val();
                //if (!teamName || !local || !ballerCount || !teamColor) {
                //    alert("必填資料尚未填寫");
                //    return;
                //}
                if (id) {
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=update&Type=Baller",
                        type: "POST",
                        data: {
                            "ID": id,
                            "TeamName": teamName,
                            "Position": position,
                            "Number": number,
                            "Name": name,
                            "Height": height,
                            "Weight": weight,
                            "Birth": birth,
                            "University": university
                        },
                        success: function (result) {
                            alert("更新成功");
                            window.location.reload();
                        }
                    });
                }
                else {
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=create&Type=Baller",
                        type: "POST",
                        data: {
                            "TeamName": teamName,
                            "Position": position,
                            "Number": number,
                            "Name": name,
                            "Height": height,
                            "Weight": weight,
                            "Birth": birth,
                            "University": university
                        },
                        success: function (result) {
                            alert("新增成功");
                            window.location.reload();
                        }
                    });
                }
            });

            $("#btnRaceSave").click(function () {

                var id = $("#hfIDR").val();
                var teamName = $("#txtRTName").val();
                var raceNum = $("#txtRaceNum").val();
                var date = $("#txtDate").val();
                var shoot = $("#txtShoot").val();
                var threePoint = $("#txtThreePoint").val();
                var penalty = $("#txtPenalty").val();
                var backBoard = $("#txtBackBoard").val();
                var assistance = $("#txtAssistance").val();
                var block = $("#txtBlock").val();
                var steal = $("#txtSteal").val();
                var miss = $("#txtMiss").val();
                var ra = $("#txtRestrictedArea").val();
                var foul = $("#txtFoul").val();
                //if (!teamName || !local || !ballerCount || !teamColor) {
                //    alert("必填資料尚未填寫");
                //    return;
                //}
                if (id) {
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=update&Type=Race",
                        type: "POST",
                        data: {
                            "ID": id,
                            "TeamName": teamName,
                            "RaceNum": raceNum,
                            "Date": date,
                            "Shoot": shoot,
                            "ThreePoint": threePoint,
                            "Penalty": penalty,
                            "BackBoard": backBoard,
                            "Assistance": assistance,
                            "Block": block,
                            "Steal": steal,
                            "Miss": miss,
                            "RestrictedArea": ra,
                            "Foul": foul
                        },
                        success: function (result) {
                            alert("更新成功");
                            window.location.reload();
                        }
                    });
                }
                else {
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=create&Type=Race",
                        type: "POST",
                        data: {
                            "TeamName": teamName,
                            "RaceNum": raceNum,
                            "Date": date,
                            "Shoot": shoot,
                            "ThreePoint": threePoint,
                            "Penalty": penalty,
                            "BackBoard": backBoard,
                            "Assistance": assistance,
                            "Block": block,
                            "Steal": steal,
                            "Miss": miss,
                            "RestrictedArea": ra,
                            "Foul": foul
                        },
                        success: function (result) {
                            alert("新增成功");
                        }
                    });
                }
            });

            //刪除
            $("#btnTeamDel").click(function () {
                var id = $("#hfIDT").val();
                if (id){
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=delete&Type=Team",
                        type: "POST",
                        data: {
                            "ID": id
                        },
                        success: function (result) {
                            alert("刪除成功");
                            window.location.reload();
                        }
                    });
                }
                else {
                    alert("請點選要刪除的資料之編輯按鍵！");
                }
            });

            $("#btnBallerDel").click(function () {
                var id = $("#hfIDB").val();
                if (id) {
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=delete&Type=Baller",
                        type: "POST",
                        data: {
                            "ID": id
                        },
                        success: function (result) {
                            alert("刪除成功");
                            window.location.reload();
                        }
                    });
                }
                else {
                    alert("請點選要刪除的資料之編輯按鍵！");
                }
            });

            $("#btnRaceDel").click(function () {
                var id = $("#hfIDR").val();
                if (id) {
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=delete&Type=Race",
                        type: "POST",
                        data: {
                            "ID": id
                        },
                        success: function (result) {
                            alert("刪除成功");
                            window.location.reload();
                        }
                    });
                }
                else {
                    alert("請點選要刪除的資料之編輯按鍵！");
                }
            });

            //關閉填寫欄位
            $("#btnTeamCancel").click(function () {
                $("#hfIDT").val('');
                $("#txtTTName").val('');
                $("#txtLocal").val('');
                $("#txtBallerCount").val('');
                $("#txtOwner").val('');
                $("#txtHomeCourt").val('');
                $("#txtTeamColor").val('');

                $("#divTeamEditor").hide(300);
            });

            $("#btnBallerCancel").click(function () {
                $("#hfIDB").val('');
                $("#txtBTName").val('');
                $("#txtName").val('');
                $("#txtPosition").val('');
                $("#txtNumber").val('');
                $("#txtHeight").val('');
                $("#txtWeight").val('');
                $("#txtBirth").val('');
                $("#txtUniversity").val('');

                $("#divBallerEditor").hide(300);
            });

            $("#btnRaceCancel").click(function () {
                $("#hfIDR").val('');
                $("#txtRaceNum").val('');
                $("#txtDate").val('');
                $("#txtRTName").val('');
                $("#txtShoot").val('');
                $("#txtThreePoint").val('');
                $("#txtPenalty").val('');
                $("#txtBackBoard").val('');
                $("#txtAssistance").val('');
                $("#txtBlock").val('');
                $("#txtSteal").val('');
                $("#txtMiss").val('');
                $("#txtRestrictedArea").val('');
                $("#txtFoul").val('');

                $("#divRaceEditor").hide(300);
            });

            //資料列
            $(".btnClassEditgvTeam").click(function () {
                var td = $(this).closest("td");
                var hf = td.find("input.hfRowID");

                var rowID = hf.val();

                $.ajax({
                    url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=query&Type=Team",
                    type: "POST",
                    data: {
                        "ID": rowID,
                    },
                    success: function (result) {
                        $("#hfIDT").val(result["ID"]);
                        $("#txtTTName").val(result["TeamName"]);
                        $("#txtLocal").val(result["Local"]);
                        $("#txtBallerCount").val(result["BallerCount"]);
                        $("#txtOwner").val(result["Owner"]);
                        $("#txtHomeCourt").val(result["HomeCourt"]);
                        $("#txtTeamColor").val(result["TeamColor"]);

                        $("#divTeamEditor").show(300);
                    }
                });
            });

            $(".btnClassEditgvBaller").click(function () {
                var td = $(this).closest("td");
                var hf = td.find("input.hfRowID");

                var rowID = hf.val();

                $.ajax({
                    url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=query&Type=Baller",
                    type: "POST",
                    data: {
                        "ID": rowID,
                    },
                    success: function (result) {
                        $("#hfIDB").val(result["ID"]);
                        $("#txtBTName").val(result["TeamName"]);
                        $("#txtName").val(result["Name"]);
                        $("#txtPosition").val(result["Position"]);
                        $("#txtNumber").val(result["Number"]);
                        $("#txtHeight").val(result["Height"]);
                        $("#txtWeight").val(result["Weight"]);
                        $("#txtBirth").val(result["Birth"]);
                        $("#txtUniversity").val(result["University"]);

                        $("#divBallerEditor").show(300);
                    }
                });
            });

            $(".btnClassEditgvRace").click(function () {
                var td = $(this).closest("td");
                var hf = td.find("input.hfRowID");

                var rowID = hf.val();

                $.ajax({
                    url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=query&Type=Race",
                    type: "POST",
                    data: {
                        "ID": rowID,
                    },
                    success: function (result) {
                        $("#hfIDR").val(result["ID"]);
                        $("#txtRaceNum").val(result["RaceNum"]);
                        $("#txtDate").val(result["Date"]);
                        $("#txtRTName").val(result["TeamName"]);
                        $("#txtShoot").val(result["Shoot"]);
                        $("#txtThreePoint").val(result["ThreePoint"]);
                        $("#txtPenalty").val(result["Penalty"]);
                        $("#txtBackBoard").val(result["BackBoard"]);
                        $("#txtAssistance").val(result["Assistance"]);
                        $("#txtBlock").val(result["Block"]);
                        $("#txtSteal").val(result["Steal"]);
                        $("#txtMiss").val(result["Miss"]);
                        $("#txtRestrictedArea").val(result["RestrictedArea"]);
                        $("#txtFoul").val(result["Foul"]);

                        $("#divRaceEditor").show(300);
                    }
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hfIDT" />
        <input type="hidden" id="hfIDB" />
        <input type="hidden" id="hfIDR" />
        <div id="divTeamEditor">
            <table>
                <tr>
                    <td>
                        <!--這裡放主要內容-->
                        名稱：
                        <input type="text" id="txtTTName" />
                        <br />
                        國家：
                        <input type="text" id="txtLocal" />
                        <br />
                        球員人數： 
                        <input type="text" id="txtBallerCount" />
                        <br />
                        持有者：
                        <input type="text" id="txtOwner" />
                        <br />
                        主場：
                        <input type="text" id="txtHomeCourt" />
                        <br />
                        隊伍顏色：
                        <input type="text" id="txtTeamColor" />
                        <br />
                    </td>
                </tr>
            </table>
            <button type="button" id="btnTeamSave">儲存隊伍</button> &nbsp
            <button type="button" id="btnTeamDel">刪除隊伍</button> &nbsp
            <button type="button" id="btnTeamCancel">關閉本填寫欄位</button>
        </div>

        <div id="divBallerEditor">
            <table>
                <tr>
                    <td>
                        <!--這裡放主要內容-->
                        隸屬球隊：
                        <input type="text" id="txtBTName" />
                        <br />
                        位置：
                        <input type="text" id="txtPosition" />
                        <br />
                        背號： 
                        <input type="text" id="txtNumber" />
                        <br />
                        稱呼：
                        <input type="text" id="txtName" />
                        <br />
                        身高：
                        <input type="text" id="txtHeight" />
                        <br />
                        體重：
                        <input type="text" id="txtWeight" />
                        <br />
                        生日：
                        <input type="text" id="txtBirth" />
                        <br />
                        大學：
                        <input type="text" id="txtUniversity" />
                        <br />
                    </td>
                </tr>
            </table>
            <button type="button" id="btnBallerSave">儲存球員</button> &nbsp
            <button type="button" id="btnBallerDel">刪除球員</button>
            <button type="button" id="btnBallerCancel">關閉本填寫欄位</button>
        </div>

        <div id="divRaceEditor">
            <table>
                <tr>
                    <td>
                        <!--這裡放主要內容-->
                        場號：
                        <input type="text" id="txtRaceNum" />
                        <br />
                        日期：
                        <input type="text" id="txtDate" />
                        <br />
                        隊伍名稱： 
                        <input type="text" id="txtRTName" />
                        <br />
                        投籃率：
                        <input type="text" id="txtShoot" />
                        <br />
                        三分命中：
                        <input type="text" id="txtThreePoint" />
                        <br />
                        罰球命中：
                        <input type="text" id="txtPenalty" />
                        <br />
                        籃板：
                        <input type="text" id="txtBackBoard" />
                        <br />
                        助攻：
                        <input type="text" id="txtAssistance" />
                        <br />
                        阻攻：
                        <input type="text" id="txtBlock" />
                        <br />
                        抄截：
                        <input type="text" id="txtSteal" />
                        <br />
                        失誤：
                        <input type="text" id="txtMiss" />
                        <br />
                        禁區得分：
                        <input type="text" id="txtRestrictedArea" />
                        <br />
                        犯規：
                        <input type="text" id="txtFoul" />
                        <br />
                    </td>
                </tr>
            </table>
            <button type="button" id="btnRaceSave">儲存賽事</button>
            <button type="button" id="btnRaceDel">刪除賽事</button>
            <button type="button" id="btnRaceCancel">關閉本填寫欄位</button>
        </div>
        <br />
        <div>
            隊伍GRID：
            <asp:GridView ID="gvTeam" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="行為">
                        <ItemTemplate>
                            <%--<a href="#">編輯</a>--%>
                            <button type="button" class="btnClassEditgvTeam">EDIT</button>
                            <input type="hidden" class="hfRowID" value='<%# Eval("ID") %>' />
                        </ItemTemplate>                        
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="TeamName" HeaderText="名稱" />
                    <asp:BoundField DataField="Local" HeaderText="國家" />
                    <asp:BoundField DataField="BallerCount" HeaderText="球員人數" />
                    <asp:BoundField DataField="Owner" HeaderText="持有者" />
                    <asp:BoundField DataField="HomeCourt" HeaderText="主場" />
                    <asp:BoundField DataField="TeamColor" HeaderText="隊色" />
                </Columns>
            </asp:GridView>
            <br />
            球員GRID：
            <asp:GridView ID="gvBaller" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="行為">
                        <ItemTemplate>
                            <button type="button" class="btnClassEditgvBaller">EDIT</button>
                            <input type="hidden" class="hfRowID" value='<%# Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="TeamName" HeaderText="隸屬" />
                    <asp:BoundField DataField="Position" HeaderText="位置" />
                    <asp:BoundField DataField="Number" HeaderText="背號" />
                    <asp:BoundField DataField="Name" HeaderText="稱呼" />
                    <asp:BoundField DataField="Height" HeaderText="身高" />
                    <asp:BoundField DataField="Weight" HeaderText="體重" />
                    <asp:BoundField DataField="Birth" HeaderText="生日" />
                    <asp:BoundField DataField="University" HeaderText="大學" />
                </Columns>
            </asp:GridView>
            <br />
            賽場GRID：
            <asp:GridView ID="gvRace" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="行為">
                        <ItemTemplate>
                            <button type="button" class="btnClassEditgvRace">EDIT</button>
                            <input type="hidden" class="hfRowID" value='<%# Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="RaceNum" HeaderText="場號" />
                    <asp:BoundField DataField="Date" HeaderText="日期" />
                    <asp:BoundField DataField="TeamName" HeaderText="隊伍名稱" />
                    <asp:BoundField DataField="Shoot" HeaderText="投籃率" />
                    <asp:BoundField DataField="ThreePoint" HeaderText="三分命中率" />
                    <asp:BoundField DataField="Penalty" HeaderText="罰球命中率" />
                    <asp:BoundField DataField="BackBoard" HeaderText="籃板數" />
                    <asp:BoundField DataField="Assistance" HeaderText="助攻數" />
                    <asp:BoundField DataField="Block" HeaderText="阻攻數" />
                    <asp:BoundField DataField="Steal" HeaderText="抄截數" />
                    <asp:BoundField DataField="Miss" HeaderText="失誤數" />
                    <asp:BoundField DataField="RestrictedArea" HeaderText="禁區得分" />
                    <asp:BoundField DataField="Foul" HeaderText="犯規數" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
