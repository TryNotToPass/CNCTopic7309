<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainInfoPage.aspx.cs" Inherits="CNCTopic7309.UserPages.MainInfoPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>NBA資訊主頁</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <script src="../js/bootstrap.js"></script>
    <script src="../js/jQuery360.js"></script>
    <script>
        $(function () {
            //alert("JQ成功");
            $("#divBallerList").hide();
            $("#divTeamList").hide();
            $("#divRaceList").hide();
            //刪除留言
            $(".btnChatDel").click(function () {
                var td = $(this).closest("td");
                var hf = td.find("input.hfIDC");
                var id = hf.val();
                if (id) {
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=delete&Type=Chat",
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
                    alert("沒成功QQ");
                }
            });

            $(".btnPicDel").click(function () {
                var td = $(this).closest("td");
                var hf = td.find("input.hfPDC");
                var hfp = td.find("input.hfPath");

                var id = hf.val();
                var path = hfp.val();
                if (id && path) {
                    $.ajax({
                        url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=delete&Type=Pic",
                        type: "POST",
                        data: {
                            "ID": id,
                            "Path": path
                        },
                        success: function (result) {
                            alert("刪除成功");
                            window.location.reload();
                        }
                    });
                }
                else {
                    alert("沒成功QQ");
                }
            });

            //獲取列表
            $.ajax({
                url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=list&Type=Team",
                type: "GET",
                data: {},
                success: function (result) {
                    var table = '<table border="1">';

                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var htmlText =
                            `<tr> 
                                <td>
                                    <a href="/UserPages/MainInfoPage.aspx?ID=${obj.ID}&Type=Team">${obj.TeamName}隊</a>
                                </td>
                            </tr>`;
                        table += htmlText;
                    }

                    table += "</table>";
                    $("#divTeamList").append(table);
                }
            });
            $.ajax({
                url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=list&Type=Baller",
                type: "GET",
                data: {},
                success: function (result) {
                    var table = '<table border="1">';

                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var htmlText =
                            `<tr> 
                                <td>
                                    <a href="/UserPages/MainInfoPage.aspx?ID=${obj.ID}&Type=Baller">球員：${obj.Name}</a>
                                </td>
                            </tr>`;
                        table += htmlText;
                    }

                    table += "</table>";
                    $("#divBallerList").append(table);
                }
            });
            $.ajax({
                url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=list&Type=Race",
                type: "GET",
                data: {},
                success: function (result) {
                    var table = '<table border="1">';

                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var htmlText =
                            `<tr> 
                                <td>
                                    <a href="/UserPages/MainInfoPage.aspx?ID=${obj.ID}&Type=Race">第${obj.RaceNum}場-${obj.TeamName}隊</a>
                                </td>
                            </tr>`;
                        table += htmlText;
                    }

                    table += "</table>";
                    $("#divRaceList").append(table);
                }
            });

            //顯示列表
            $("#btnTeamList").click(function () {
                $("#divTeamList").toggle(300);
            });
            $("#btnBallerList").click(function () {
                $("#divBallerList").toggle(300);
            });
            $("#btnRaceList").click(function () {
                $("#divRaceList").toggle(300);
            });


        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1">
            <tr>
                <td colspan="2">
                    <asp:Literal Text="跑嗎燈位置" runat="server" ID="ltRunner"/>
                    <br />
                    <asp:Literal Text="TEST" ID="ltlTest" runat="server" />
                    <asp:Button Text="個資編輯" runat="server" ID="btnUIE" OnClick="btnUIE_Click" CssClass="btn btn-primary"/> &nbsp
                    <asp:Button Text="冠軍賽資訊編輯(Admin)" runat="server" ID="btnTIE" Visible="false" OnClick="btnTIE_Click"/> &nbsp
                    <asp:Button Text="權限變更(SuperAdmin)" runat="server" ID="btnLvChange" OnClick="btnLvChange_Click" Visible="false"/> &nbsp
                    <asp:Button Text="LogOut" ID="btnLogOut" runat="server" OnClick="btnLogOut_Click"/>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <button type="button" id="btnTeamList">隊伍列表</button>
                    <div id="divTeamList"></div>
                    <br />
                    <button type="button" id="btnBallerList">球員列表</button>
                    <div id="divBallerList"></div>
                    <br />
                    <button type="button" id="btnRaceList">賽事列表</button>
                    <br />
                    <div id="divRaceList"></div>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                標題：<asp:TextBox runat="server" ID="txtPTitle" Visible="false"/>
                                <br />
                                內容：<asp:TextBox runat="server" ID="txtPText" TextMode="MultiLine" Visible="false"/>
                                <br />
                                分享連結：<asp:TextBox runat="server" ID="txtPHref" Visible="false"/>
                                <br />
                                <asp:FileUpload ID="fuInfo" runat="server" Visible="false"/>
                                <br />
                                主要資訊
                                <asp:Button Text="上傳文章或圖片" runat="server" ID="btnUpload" Visible="false" OnClick="btnUpload_Click"/>
                                <br />
                                <asp:Literal Text="資訊圖片等放置(最先的內容以隊伍ID1)" ID="ltlInfo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal Text="" ID="ltlChatBoard" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="txtChat" TextMode="MultiLine"/>
                                <br />
                                <asp:Button Text="發布留言" runat="server" ID="btnSaveChat" OnClick="btnSaveChat_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
