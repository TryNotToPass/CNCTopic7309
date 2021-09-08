<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainInfoPage.aspx.cs" Inherits="CNCTopic7309.UserPages.MainInfoPage" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>NBA資訊主頁</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <script src="../js/bootstrap.js"></script>
    <script src="../js/popper.min.js"></script>
    <script src="../js/jQuery360.js"></script>
    <script>
        $(function () {
            //alert("JQ成功");
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
                    var table = '';

                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var htmlText =
                            `<li><a class="dropdown-item" href="/UserPages/MainInfoPage.aspx?ID=${obj.ID}&Type=Team">${obj.TeamName}隊</a></li>`;
                        table += htmlText;
                    }
                    $("#liTeamList").append(table);
                }
            });
            $.ajax({
                url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=list&Type=Baller",
                type: "GET",
                data: {},
                success: function (result) {
                    var table = '';

                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var htmlText =
                            `<li><a class="dropdown-item" href="/UserPages/MainInfoPage.aspx?ID=${obj.ID}&Type=Baller">球員：${obj.Name}</a></li>`;
                        table += htmlText;
                    }
                    $("#liBallerList").append(table);
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
                            `<li><a class="dropdown-item" href="/UserPages/MainInfoPage.aspx?ID=${obj.ID}&Type=Race">第${obj.RaceNum}場-${obj.TeamName}隊</a></li>`;
                        table += htmlText;
                    }
                    $("#liRaceList").append(table);
                }
            });
        });
    </script>
    <style>
        .float_card {
            float: left;
        }
        body {
            background-color: #3AB0FA;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fliud">
            <%--折疊式選單--%>
            <div class="row sticky-top">
                <nav class="navbar navbar-expand-md navbar-light bg-light">
                    <a class="navbar navbar-brand">
                        <img src="../images/NBA_logo.jpg" alt="" width="100" height="50" />
                        <%--<asp:Literal Text="TEST" ID="ltlTest" runat="server" />--%>
                    </a>

                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
                        data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false"
                        aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>

                    <div class="collapse navbar-collapse" id="navbarNavDropdown">
                        <ul class="navbar-nav">
                            <li class="nav-item">
                                <asp:Button Text="個資編輯" runat="server" ID="btnUIE" OnClick="btnUIE_Click" CssClass="btn btn-outline-primary"/>
                            </li>
                            <li class="nav-item">
                                <asp:Button Text="冠軍賽資訊編輯" runat="server" ID="btnTIE" Visible="false" OnClick="btnTIE_Click" CssClass="btn btn-outline-primary"/>
                            </li>
                            <li class="nav-item">
                                <asp:Button Text="權限變更(SuperAdmin)" runat="server" ID="btnLvChange" OnClick="btnLvChange_Click" Visible="false" CssClass="btn btn-outline-danger"/>
                            </li>                        
                            <li class="nav-item">
                                <asp:Button Text="登出" ID="btnLogOut" runat="server" OnClick="btnLogOut_Click" CssClass="btn btn-outline-secondary"/>
                            </li>
                            <li class="nav-item">
                                <a href="../Login.aspx"><asp:Label Text="" runat="server" ID="aBackToLogin"/></a>
                            </li>
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" role="button"
                                    data-bs-toggle="dropdown" aria-expanded="false">
                                    球隊
                                </a>
                                <ul class="dropdown-menu" id="liTeamList" aria-labelledby="navbarDropdownMenuLink">
                                </ul>
                            </li>
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink2" role="button"
                                    data-bs-toggle="dropdown" aria-expanded="false">
                                    球員
                                </a>
                                <ul class="dropdown-menu" id="liBallerList" aria-labelledby="navbarDropdownMenuLink2">
                                </ul>
                            </li>
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink3" role="button"
                                    data-bs-toggle="dropdown" aria-expanded="false">
                                    賽事
                                </a>
                                <ul class="dropdown-menu" id="liRaceList" aria-labelledby="navbarDropdownMenuLink3">
                                </ul>
                            </li>
                        </ul>
                    </div>
                </nav>
                <asp:Literal Text="跑嗎燈位置" runat="server" ID="ltRunner"/>
            </div>

            <div class="row">
                <div class="col-md-2 col-sm-12 col-lg-3">
                </div>
                <div class="col-md-8 col-sm-12 col-lg-6">

                    <asp:PlaceHolder ID="ph_admin" runat="server"  Visible="false">
                        <div class="input-group mb-1">
                            <span class="input-group-text" id="basic-addon1">標題</span>
                            <asp:TextBox runat="server" ID="txtPTitle" CssClass="form-control" placeholder="可以不輸入內容"/>
                        </div>
                        <div class="input-group mb-1">
                            <span class="input-group-text" id="basic-addon2">內容</span>
                            <asp:TextBox runat="server" ID="txtPText" TextMode="MultiLine" CssClass="form-control" placeholder="可以不輸入內容"/>
                        </div>
                        <div class="input-group mb-1">
                            <span class="input-group-text" id="basic-addon3">分享連結</span>
                            <asp:TextBox runat="server" ID="txtPHref" CssClass="form-control" placeholder="可以不輸入內容"/>
                        </div>
                        <div class="input-group mb-2">
                            <asp:FileUpload ID="fuInfo" runat="server" CssClass="form-control" />
                        </div>
                        <div class="d-grid col-6 mx-auto">
                            <asp:Button Text="上傳文章或圖片" runat="server" ID="btnUpload" OnClick="btnUpload_Click" CssClass="btn btn-light"/>
                        </div>
                    </asp:PlaceHolder>
                    <hr />
                    
                    <asp:Literal Text="資訊圖片等放置(最先的內容以隊伍ID1)" ID="ltlInfo" runat="server" />
                    <%--我的最愛--%>
                    <div class="card">
                        <div class="card-body">
                            <asp:ImageButton ImageUrl="~/images/heartBtn.png" runat="server" ID="btnHeartHole" Width="25px" Height="25px" OnClick="btnHeartHole_Click"/>
                            <asp:ImageButton ImageUrl="~/images/heartBtnFilled.png" runat="server" ID="btnHeart" Width="25px" Height="25px" OnClick="btnHeart_Click"/>
                        </div>
                    </div>
                    <hr />
                    <asp:Literal Text="" ID="ltlChatBoard" runat="server"/>
                    <hr />

                    <uc1:ucPager runat="server" ID="ucPager" PageSize="5" CurrentPage="1" TotalSize="2" Url="/UserPages/MainInfoPage.aspx"/>

                    <div class="input-group mb-2">
                        <span class="input-group-text" id="basic-addon-chat">留言</span>
                        <asp:TextBox runat="server" ID="txtChat" TextMode="MultiLine" CssClass="form-control" placeholder="請輸入留言"/>
                    </div>
                    <div class="d-grid col-6 mx-auto">
                        <asp:Button Text="發布留言" runat="server" ID="btnSaveChat" OnClick="btnSaveChat_Click" CssClass="btn btn-dark"/>
                    </div>
                </div>
                <div class="col-md-2 col-sm-12 col-lg-3">
                </div>
            </div>

        </div>
    </form>
</body>
</html>
