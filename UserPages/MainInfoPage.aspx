<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainInfoPage.aspx.cs" Inherits="CNCTopic7309.UserPages.MainInfoPage" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>NBA台灣賽事分析網</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <script src="../js/bootstrap.js"></script>
    <%--<script src="../js/popper.min.js"></script>--%>
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
                        url: "/Handlers/AdminNCHandle.ashx?Act=delete&Type=Chat",
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
                        url: "/Handlers/AdminNCHandle.ashx?Act=delete&Type=Pic",
                        type: "POST",
                        data: {
                            "ID": id,
                            "Path": path
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
                    alert("沒成功QQ");
                }
            });

            //獲取列表
            $.ajax({
                url: "/Handlers/AdminNCHandle.ashx?Act=list&Type=Team",
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
                },
                error: function (jqXHR, exception) {
                    var msg = jqXHR.responseText;
                    alert(msg);
                }
            });
            $.ajax({
                url: "/Handlers/AdminNCHandle.ashx?Act=list&Type=Baller",
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
                },
                error: function (jqXHR, exception) {
                    var msg = jqXHR.responseText;
                    alert(msg);
                }
            });
            $.ajax({
                url: "/Handlers/AdminNCHandle.ashx?Act=list&Type=Race",
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
                },
                error: function (jqXHR, exception) {
                    var msg = jqXHR.responseText;
                    alert(msg);
                }
            });

            //回到頂部
            var $win = $(window);
            var $backToTop = $('.js-back-to-top');
            $backToTop.hide();
            $win.scroll(function () {
                if ($win.scrollTop() > 100) {
                    $backToTop.show();
                } else {
                    $backToTop.hide();
                }
            });
            $backToTop.click(function () {
                $('html, body').animate({ scrollTop: 0 }, 10);
                $backToTop.hide();
            });


        });
    </script>
    <style>
        .float_card {
            float: left;
        }
        body {
            background-color: #3AB0FA;
            overflow-x: hidden;
        }
        .light_btn{
            transition: 500ms;
        }
        .light_btn:hover{
            border-radius: 20px;
            box-shadow: 0px 0px 6px 1px #FA9A89, inset 10em 10em #FA9A89;
            transition: 200ms;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="ltAlert" runat="server" Text=""/>
        <div class="container-fliud">

            <%--送信小視窗--%>
            <table>
                <tr>
                    <td colspan="2" align="center">
                        <!-- Modal -->
                        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">聯絡超級管理員</h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="input-group mb-3">
                                            <span class="input-group-text" id="new-addon2">信件標題</span>
                                            <asp:TextBox runat="server" ID="txtTitle" class="form-control" placeholder="請輸入標題" aria-describedby="new-addon2"/>
                                        </div>
                                        <div class="input-group mb-3">
                                            <span class="input-group-text" id="new-addon1">信件種類</span>
                                            <asp:DropDownList ID="DropDownList1" runat="server" aria-describedby="new-addon1">
                                                <asp:ListItem Selected="True" Value="疑難雜症"> 疑難雜症 </asp:ListItem>
                                                <asp:ListItem Value="會員糾紛"> 會員糾紛 </asp:ListItem>
                                                <asp:ListItem Value="問題回報"> 問題回報 </asp:ListItem>
                                                <asp:ListItem Value="應徵版主"> 應徵版主 </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="input-group mb-3">
                                            <span class="input-group-text" id="new-addon3">信件內容</span>
                                            <asp:TextBox runat="server" ID="txtContent" class="form-control" TextMode="MultiLine" placeholder="欲傳達之事項" aria-describedby="new-addon3"/>
                                        </div>
                                        <div class="input-group mb-3">
                                            <span class="input-group-text" id="new-addon5">信箱</span>
                                            <asp:TextBox runat="server" ID="txtMail" TextMode="Email" class="form-control" placeholder="欲接收回信的信箱，可以不填" aria-describedby="new-addon5"/>
                                        </div>

                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button Text="確認送出" ID="btnSendMail" CssClass="btn btn-primary" runat="server" OnClick="btnSendMail_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </td>
                </tr>
            </table>

            <div class="row sticky-top justify-content-center">
                <asp:Literal Text="跑嗎燈位置" runat="server" ID="ltRunner"/>
                <button type="button" class="js-back-to-top btn btn-secondary w-25" id="btnToTop">回到頂部</button>
            </div>
            <%--折疊式選單--%>
            <div class="row">
                <nav class="navbar navbar-expand-md navbar-light bg-light">
                    <a class="navbar-brand">
                        &nbsp
                        <img src="../images/NBA_logo.jpg" alt="" width="100" height="50" />
                    </a>

                    <div class="collapse navbar-collapse" id="navbarNavDropdown">
                        <%--<ul class="navbar-nav">--%>
                        <ul class="navbar-nav" style="--bs-scroll-height: 100px;">
                            <li class="nav-item">
                                <asp:Button Text="個資編輯" runat="server" ID="btnUIE" OnClick="btnUIE_Click" CssClass="btn btn-outline-primary m-1"/>
                            </li>
                            <li class="nav-item">
                                <asp:Button Text="編輯查詢資料" runat="server" ID="btnTIE" Visible="false" OnClick="btnTIE_Click" CssClass="btn btn-outline-primary m-1"/>
                            </li>
                            <li class="nav-item">
                                <asp:Button Text="權限變更" runat="server" ID="btnLvChange" OnClick="btnLvChange_Click" Visible="false" CssClass="btn btn-outline-danger m-1"/>
                            </li>
                            <li class="nav-item">
                                <asp:Button Text="檢視投票" runat="server" ID="btnVote" OnClick="btnVote_Click" CssClass="btn btn-outline-info m-1"/>
                                <%--<a class="btn btn-outline-info" href="/UserPages/MainInfoPage.aspx?Type=Vote">檢視投票</a>--%>
                            </li>
                            <asp:PlaceHolder ID="sendMailtoSA" runat="server">
                                <li class="nav-item">
                                    <button type="button" class="btn btn-outline-info m-1" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                        聯絡超級管理員
                                    </button>
                                </li>
                            </asp:PlaceHolder>
                            <li class="nav-item">
                                <a href="../Login.aspx"><asp:Label Text="" runat="server" ID="aBackToLogin" CssClass="btn btn-outline-info" Visible="false"/></a>
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
                    <div class="d-flex me-4">
                        <asp:Button Text="登出" ID="btnLogOut" runat="server" OnClick="btnLogOut_Click" CssClass="btn btn-outline-secondary"/>
                    </div>
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
                        data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false"
                        aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                </nav>

            </div>

            <div class="row">
                <div class="col-md-1 col-sm-12"></div>
                
                <div class="col-md-5 col-sm-12">
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
                            <asp:FileUpload ID="fuInfo" runat="server" CssClass="form-control"/>
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
                            <asp:ImageButton ImageUrl="~/images/heartBtn.png" runat="server" ID="btnHeartHole" Width="25px" Height="25px" OnClick="btnHeartHole_Click" CssClass="light_btn" ToolTip="成為最愛"/>
                            <asp:ImageButton ImageUrl="~/images/heartBtnFilled.png" runat="server" ID="btnHeart" Width="25px" Height="25px" OnClick="btnHeart_Click" CssClass="light_btn" ToolTip="取消最愛"/>
                            <asp:ImageButton ImageUrl="~/images/badTemp.png" runat="server" ID="btnBadTemp" Width="25px" Height="25px" OnClick="btnBadTemp_Click" CssClass="light_btn" ToolTip="脾氣最壞"/>
                            <asp:ImageButton ImageUrl="~/images/badTempFilled.png" runat="server" ID="btnBadTempFilled" Width="25px" Height="25px" OnClick="btnBadTempFilled_Click" CssClass="light_btn" ToolTip="取消壞脾氣"/>
                            <asp:ImageButton ImageUrl="~/images/FoulKing.png" runat="server" ID="btnFoulKing" Width="30px" Height="30px" OnClick="btnFoulKing_Click" CssClass="light_btn" ToolTip="犯規之王"/>
                            <asp:ImageButton ImageUrl="~/images/FoulKingFilled.png" runat="server" ID="btnFoulKingFilled" Width="30px" Height="30px" OnClick="btnFoulKingFilled_Click" CssClass="light_btn" ToolTip="取消犯規王名號"/>
                        </div>
                    </div>
                    <asp:Literal Text="" runat="server" ID="ltBallerListByTeam"/>
                </div>
                <div class="col-md-5 col-sm-12">
                    <asp:Literal Text="" ID="ltlChatBoard" runat="server"/>
                    <hr />

                    <uc1:ucPager runat="server" ID="ucPager" PageSize="5" CurrentPage="1" TotalSize="2" Url="/UserPages/MainInfoPage.aspx"/>

                    <asp:PlaceHolder ID="phChat" runat="server">
                        <div class="input-group mb-2">
                            <span class="input-group-text" id="basic-addon-chat">留言</span>
                            <asp:TextBox runat="server" ID="txtChat" TextMode="MultiLine" CssClass="form-control" placeholder="請輸入留言"/>
                        </div>
                        <div class="d-grid col-6 mx-auto">
                            <asp:Button Text="發布留言" runat="server" ID="btnSaveChat" OnClick="btnSaveChat_Click" CssClass="btn btn-dark"/>
                        </div>
                        <br />
                    </asp:PlaceHolder>
                </div>
                <div class="col-md-1 col-sm-12"></div>
            </div>
        </div>
    </form>
</body>
</html>
