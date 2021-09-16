<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminNCPage.aspx.cs" Inherits="CNCTopic7309.UserPages.AdminNCPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>資訊管理</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <script src="../js/bootstrap.js"></script>
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
                if (id) {
                    $.ajax({
                        url: "/Handlers/AdminNCHandle.ashx?Act=update&Type=Team",
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
                            //alert(result);
                            alert("更新成功");
                            window.location.reload();
                        },
                        error: function (jqXHR, exception) {
                            var msg = jqXHR.responseText;
                            alert(msg);
                        }
                    });
                }
                else {
                    $.ajax({
                        url: "/Handlers/AdminNCHandle.ashx?Act=create&Type=Team",
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
                        },
                        error: function (jqXHR, exception) {
                            var msg = jqXHR.responseText;
                            alert(msg);
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
                if (id) {
                    $.ajax({
                        url: "/Handlers/AdminNCHandle.ashx?Act=update&Type=Baller",
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
                        },
                        error: function (jqXHR, exception) {
                            var msg = jqXHR.responseText;
                            alert(msg);
                        }
                    });
                }
                else {
                    $.ajax({
                        url: "/Handlers/AdminNCHandle.ashx?Act=create&Type=Baller",
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
                        },
                        error: function (jqXHR, exception) {
                            var msg = jqXHR.responseText;
                            alert(msg);
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
                var score = $("#txtScore").val();
                if (id) {
                    $.ajax({
                        url: "/Handlers/AdminNCHandle.ashx?Act=update&Type=Race",
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
                            "Foul": foul,
                            "Score":score
                        },
                        success: function (result) {
                            alert("更新成功");
                            window.location.reload();
                        },
                        error: function (jqXHR, exception) {
                            var msg = jqXHR.responseText;
                            alert(msg);
                        }
                    });
                }
                else {
                    $.ajax({
                        url: "/Handlers/AdminNCHandle.ashx?Act=create&Type=Race",
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
                            "Foul": foul,
                            "Score": score
                        },
                        success: function (result) {
                            alert("新增成功");
                            window.location.reload();
                        },
                        error: function (jqXHR, exception) {
                            var msg = jqXHR.responseText;
                            alert(msg);
                        }
                    });
                }
            });

            //刪除
            $("#btnTeamDel").click(function () {
                var id = $("#hfIDT").val();
                if (id) {
                    $.ajax({
                        url: "/Handlers/AdminNCHandle.ashx?Act=delete&Type=Team",
                        type: "POST",
                        data: {
                            "ID": id
                        },
                        success: function (result) {
                            alert("刪除成功");
                            window.location.reload();
                        },
                        error: function (jqXHR, exception) {
                            var msg = jqXHR.responseText;
                            alert(msg);
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
                        url: "/Handlers/AdminNCHandle.ashx?Act=delete&Type=Baller",
                        type: "POST",
                        data: {
                            "ID": id
                        },
                        success: function (result) {
                            alert("刪除成功");
                            window.location.reload();
                        },
                        error: function (jqXHR, exception) {
                            var msg = jqXHR.responseText;
                            alert(msg);
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
                        url: "/Handlers/AdminNCHandle.ashx?Act=delete&Type=Race",
                        type: "POST",
                        data: {
                            "ID": id
                        },
                        success: function (result) {
                            alert("刪除成功");
                            window.location.reload();
                        },
                        error: function (jqXHR, exception) {
                            var msg = jqXHR.responseText;
                            alert(msg);
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

                $("#divTeamEditor").toggle(300);
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

                $("#divBallerEditor").toggle(300);
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
                $("#txtScore").val('');
                $("#txtFoul").val('');

                $("#divRaceEditor").toggle(300);
            });

            $("#btnGvTeam").click(function () {
                $("#divgvTeam").toggle(200);
            });
            $("#btnGvRace").click(function () {
                $("#divgvRace").toggle(200);
            });
            $("#btnGvBaller").click(function () {
                $("#divgvBaller").toggle(200);
            });

            //資料列
            $(".btnClassEditgvTeam").click(function () {
                var td = $(this).closest("td");
                var hf = td.find("input.hfRowID");

                var rowID = hf.val();

                $.ajax({
                    url: "/Handlers/AdminNCHandle.ashx?Act=query&Type=Team",
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
                    },
                    error: function (jqXHR, exception) {
                        var msg = jqXHR.responseText;
                        alert(msg);
                    }
                });
            });

            $(".btnClassEditgvBaller").click(function () {
                var td = $(this).closest("td");
                var hf = td.find("input.hfRowID");

                var rowID = hf.val();

                $.ajax({
                    url: "/Handlers/AdminNCHandle.ashx?Act=query&Type=Baller",
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
                        var m = new Date(result["Birth"]);
                        var dateString = m.getFullYear() + "/" + (m.getMonth() + 1) + "/" + m.getDate();
                        $("#txtBirth").val(dateString);
                        $("#txtUniversity").val(result["University"]);

                        $("#divBallerEditor").show(300);
                    },
                    error: function (jqXHR, exception) {
                        var msg = jqXHR.responseText;
                        alert(msg);
                    }
                });
            });

            $(".btnClassEditgvRace").click(function () {
                var td = $(this).closest("td");
                var hf = td.find("input.hfRowID");

                var rowID = hf.val();

                $.ajax({
                    url: "/Handlers/AdminNCHandle.ashx?Act=query&Type=Race",
                    type: "POST",
                    data: {
                        "ID": rowID,
                    },
                    success: function (result) {
                        $("#hfIDR").val(result["ID"]);
                        $("#txtRaceNum").val(result["RaceNum"]);
                        var m = new Date(result["Date"]);
                        var dateString = m.getFullYear() + "/" + (m.getMonth() + 1) + "/" + m.getDate();
                        $("#txtDate").val(dateString);
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
                        $("#txtScore").val(result["Score"]);

                        $("#divRaceEditor").show(300);
                    },
                    error: function (jqXHR, exception) {
                        var msg = jqXHR.responseText;
                        alert(msg);
                    }
                });
            });

            $("#btnToTop").click(function () {
                $('html, body').animate({ scrollTop: 0 }, 10);
            });

        });
    </script>
    <style>
        .followYou {
            position: fixed;
            bottom: 10px;
            right: -80px;
            transition: 500ms;
        }
        .followYou:hover {
            right: 10px;
            transition: 500ms;
        }
        .followYou2 {
            position: fixed;
            bottom: 60px;
            right: -80px;
            transition: 500ms;
        }
        .followYou2:hover {
            right: 10px;
            transition: 500ms;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hfIDT" />
        <input type="hidden" id="hfIDB" />
        <input type="hidden" id="hfIDR" />
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-4 col-sm-12">

                    <div id="divTeamEditor">
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="team-addon1">名稱</span>
                            <input type="text" id="txtTTName" class="form-control" aria-describedby="team-addon1" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="team-addon2">地區</span>
                            <input type="text" id="txtLocal" class="form-control" aria-describedby="team-addon2" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="team-addon3">球員人數</span>
                            <input type="text" id="txtBallerCount" class="form-control" aria-describedby="team-addon3" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="team-addon4">持有者</span>
                            <input type="text" id="txtOwner" class="form-control" aria-describedby="team-addon4" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="team-addon5">主場</span>
                            <input type="text" id="txtHomeCourt" class="form-control" aria-describedby="team-addon5" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="team-addon6">隊伍顏色</span>
                            <input type="text" id="txtTeamColor" class="form-control" aria-describedby="team-addon6" />
                        </div>
                        <div class="btn-group" role="group" aria-label="Basic example">
                            <button type="button" id="btnTeamSave" class="btn btn-secondary">儲存隊伍</button>
                            <button type="button" id="btnTeamDel" class="btn btn-secondary">刪除隊伍</button>
                        </div>
                    </div>
                    <button type="button" id="btnTeamCancel" class="btn btn-secondary m-3">關閉/開啟隊伍欄位</button>
                </div>
                <div class="col-md-4 col-sm-12">

                    <div id="divBallerEditor">
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="baller-addon1">隸屬球隊</span>
                            <input type="text" id="txtBTName" class="form-control" aria-describedby="baller-addon1" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="baller-addon2">位置</span>
                            <input type="text" id="txtPosition" class="form-control" aria-describedby="baller-addon2" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="baller-addon3">背號</span>
                            <input type="text" id="txtNumber" class="form-control" aria-describedby="baller-addon3" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="baller-addon4">姓名</span>
                            <input type="text" id="txtName" class="form-control" aria-describedby="baller-addon4" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="baller-addon5">身高</span>
                            <input type="text" id="txtHeight" class="form-control" aria-describedby="baller-addon5" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="baller-addon6">體重</span>
                            <input type="text" id="txtWeight" class="form-control" aria-describedby="baller-addon6" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="baller-addon7">生日</span>
                            <input type="text" id="txtBirth" class="form-control" aria-describedby="baller-addon7" />
                        </div>
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="baller-addon8">畢業</span>
                            <input type="text" id="txtUniversity" class="form-control" aria-describedby="baller-addon8" />
                        </div>
                        <div class="btn-group" role="group" aria-label="Basic example">
                            <button type="button" id="btnBallerSave" class="btn btn-secondary">儲存球員</button>
                            <button type="button" id="btnBallerDel" class="btn btn-secondary">刪除球員</button>
                        </div>
                    </div>
                    <button type="button" id="btnBallerCancel" class="btn btn-secondary m-3">關閉/開啟球員欄位</button>
                </div>
                <div class="col-md-4 col-sm-12">

                    <div id="divRaceEditor">
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon1">場號</span>
                            <input type="text" id="txtRaceNum" class="form-control" aria-describedby="race-addon1" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon2">日期</span>
                            <input type="text" id="txtDate" class="form-control" aria-describedby="race-addon2" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addonEx">得分</span>
                            <input type="text" id="txtScore" class="form-control" aria-describedby="race-addonEx" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon3">隊伍名稱</span>
                            <input type="text" id="txtRTName" class="form-control" aria-describedby="race-addon3" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon4">投籃率</span>
                            <input type="text" id="txtShoot" class="form-control" aria-describedby="race-addon4" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon5">三分命中率</span>
                            <input type="text" id="txtThreePoint" class="form-control" aria-describedby="race-addon5" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon6">罰球命中率</span>
                            <input type="text" id="txtPenalty" class="form-control" aria-describedby="race-addon6" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon7">籃板數</span>
                            <input type="text" id="txtBackBoard" class="form-control" aria-describedby="race-addon7" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon8">助攻數</span>
                            <input type="text" id="txtAssistance" class="form-control" aria-describedby="race-addon8" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon9">阻攻數</span>
                            <input type="text" id="txtBlock" class="form-control" aria-describedby="race-addon9" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon10">抄截數</span>
                            <input type="text" id="txtSteal" class="form-control" aria-describedby="race-addon10" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon11">失誤數</span>
                            <input type="text" id="txtMiss" class="form-control" aria-describedby="race-addon11" />
                        </div>
                        <div class="input-group">
                            <span class="input-group-text" id="race-addon12">禁區得分數</span>
                            <input type="text" id="txtRestrictedArea" class="form-control" aria-describedby="race-addon12" />
                        </div>
                        <div class="input-group mb-1">
                            <span class="input-group-text" id="race-addon13">犯規數</span>
                            <input type="text" id="txtFoul" class="form-control" aria-describedby="race-addon13" />
                        </div>
                        <div class="btn-group" role="group" aria-label="Basic example">
                            <button type="button" id="btnRaceSave" class="btn btn-secondary">儲存賽事</button>
                            <button type="button" id="btnRaceDel" class="btn btn-secondary">刪除賽事</button>
                        </div>
                    </div>
                    <button type="button" id="btnRaceCancel" class="btn btn-secondary m-3">關閉/開啟賽事欄位</button>
                </div>

            </div>

            <div class="row">
                <div class="col-12">

                    <div class="card text-center">
                        <div class="card-header">
                            隊伍GRID
                        </div>
                        <div class="card-body">
                            <div id="divgvTeam">
                                <asp:GridView ID="gvTeam" runat="server" AutoGenerateColumns="False" CssClass="table table-sm">
                                    <Columns>
                                        <asp:TemplateField HeaderText="行為">
                                            <ItemTemplate>
                                                <%--<a href="#">編輯</a>--%>
                                                <button type="button" class="btnClassEditgvTeam btn btn-outline-dark">EDIT</button>
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
                            </div>
                            <button type="button" id="btnGvTeam" class="btn btn-secondary">關閉或開啟隊伍一覽</button>
                        </div>
                    </div>
                </div>

                <div class="col-12">
                    <div class="card text-center">
                        <div class="card-header">
                            球員GRID
                        </div>
                        <div class="card-body">
                            <div id="divgvBaller">

                                <asp:GridView ID="gvBaller" runat="server" AutoGenerateColumns="False" CssClass="table table-sm" OnRowDataBound="gvBaller_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="行為">
                                            <ItemTemplate>
                                                <button type="button" class="btnClassEditgvBaller btn btn-outline-dark">EDIT</button>
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
                                        <asp:TemplateField HeaderText="生日">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBirth"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="University" HeaderText="大學" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <button type="button" id="btnGvBaller" class="btn btn-secondary">關閉或開啟球員一覽</button>
                        </div>
                    </div>

                </div>

                <div class="col-12">

                    <div class="card text-center">
                        <div class="card-header">
                            賽場GRID
                        </div>
                        <div class="card-body">
                            <div id="divgvRace">

                                <asp:GridView ID="gvRace" runat="server" AutoGenerateColumns="False" CssClass="table table-sm" OnRowDataBound="gvRace_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="行為">
                                            <ItemTemplate>
                                                <button type="button" class="btnClassEditgvRace btn btn-outline-dark">EDIT</button>
                                                <input type="hidden" class="hfRowID" value='<%# Eval("ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                        <asp:BoundField DataField="RaceNum" HeaderText="場號" />
                                        <%--<asp:BoundField DataField="Date" HeaderText="日期" />--%>
                                        <asp:TemplateField HeaderText="日期">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblRaceDate"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TeamName" HeaderText="隊伍名稱" />
                                        <asp:BoundField DataField="Score" HeaderText="得分" />
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
                            <button type="button" id="btnGvRace" class="btn btn-secondary">關閉或開啟賽事一覽</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <a href="MainInfoPage.aspx" class="btn btn-outline-dark followYou">★返回資訊總頁</a>
        <button type="button" class="btn btn-outline-dark followYou2" id="btnToTop">★回到頂部=>=></button>
    </form>
</body>
</html>
