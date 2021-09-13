<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoteInfoPage.aspx.cs" Inherits="CNCTopic7309.UserPages.VoteInfoPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>投票資訊</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <script src="../js/bootstrap.js"></script>
    <script src="../js/jQuery360.js"></script>
    <script src="../js/excanvas.min.js"></script>
    <script src="../js/jquery.flot.min.js"></script>
    <script src="../js/jquery.flot.pie.min.js"></script>
    <script type="text/javascript">

        $(function () {
            var dataSetB = [];
            var dataSetT = [];
            var dataSetR = [];
            var dataSetBBT = [];
            var dataSetBFK = [];
            var options = {
                series: {
                    pie: {
                        show: true,
                        innerRadius: 0.5,
                        label: {
                            show: true
                        },
                        label: {
                            show: false
                        }
                            
                    }
                }
            };
            function getRandomColor() {
                var letters = '0123456789ABCDEF'.split('');
                var color = '#';
                for (var i = 0; i < 6; i++) {
                    color += letters[Math.floor(Math.random() * 16)];
                }
                return color;
            }
            //產生圖表資訊與圖表
            $.ajax({
                url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=vote&Type=Baller",
                type: "GET",
                data: {},
                success: function (result) {

                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var name = obj.Name;
                        var popCnt = obj.PopCount;
                        var pieColor = getRandomColor();
                        dataSetB.push({ label: name, data: popCnt, color: pieColor });
                    }
                    //console.table(dataSetB);
                    $(document).ready(function () {
                        if (dataSetB.length > 1) {
                            $.plot($("#flotB"), dataSetB, options);
                        }
                        else {
                            var flot = document.querySelector('#flotB');
                            flot.style = "margin: auto;";
                            var htmlText = `<p>由 ${name} 獨占鰲頭。</p>`;
                            $("#flotB").append(htmlText);
                        }

                    });
                },
                error: function (jqXHR, exception) {
                    var flot = document.querySelector('#flotB');
                    flot.style = "margin: auto;";
                    var htmlText = `<p>尚無資料</p>`;
                    $("#flotB").append(htmlText);
                }
            });
            $.ajax({
                url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=vote&Type=Team",
                type: "GET",
                data: {},
                success: function (result) {
                    //dataSet.push({ label: "KKK", data: 8, color: "#03A36A" });
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var name = obj.Name;
                        var popCnt = obj.PopCount;
                        var pieColor = getRandomColor();
                        dataSetT.push({ label: name, data: popCnt, color: pieColor });
                    }
                    $(document).ready(function () {
                        if (dataSetT.length > 1) {

                            $.plot($("#flotT"), dataSetT, options);
                        } else {
                            var flot = document.querySelector('#flotT');
                            flot.style = "margin: auto;";
                            var htmlText = `<p>由 ${name} 獨占鰲頭。</p>`;
                            $("#flotT").append(htmlText);
                        }
                    });
                },
                error: function (jqXHR, exception) {
                    var flot = document.querySelector('#flotT');
                    flot.style = "margin: auto;";
                    var htmlText = `<p>尚無資料</p>`;
                    $("#flotT").append(htmlText);
                }
            });
            $.ajax({
                url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=vote&Type=Race",
                type: "GET",
                data: {},
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var name = obj.Name;
                        var popCnt = obj.PopCount;
                        var pieColor = getRandomColor();
                        dataSetR.push({ label: name, data: popCnt, color: pieColor });
                    }
                    $(document).ready(function () {
                        if (dataSetR.length > 1) {
                            $.plot($("#flotR"), dataSetR, options);
                        } else {
                            var flot = document.querySelector('#flotR');
                            flot.style = "margin: auto;";
                            var htmlText = `<p>由 ${name} 獨占鰲頭。</p>`;
                            $("#flotR").append(htmlText);
                        }
                    });
                },
                error: function (jqXHR, exception) {
                    var flot = document.querySelector('#flotR');
                    flot.style = "margin: auto;";
                    var htmlText = `<p>尚無資料</p>`;
                    $("#flotR").append(htmlText);
                }
            });
            $.ajax({
                url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=vote&Type=BBadTemp",
                type: "GET",
                data: {},
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var name = obj.Name;
                        var popCnt = obj.PopCount;
                        var pieColor = getRandomColor();
                        dataSetBBT.push({ label: name, data: popCnt, color: pieColor });
                    }
                    $(document).ready(function () {
                        if (dataSetBBT.length > 1) {
                            $.plot($("#flotBBT"), dataSetBBT, options);
                        } else {
                            var flot = document.querySelector('#flotBBT');
                            flot.style = "margin: auto;";
                            var htmlText = `<p>由 ${name} 獨享臭名。</p>`;
                            $("#flotBBT").append(htmlText);
                        }
                    });
                },
                error: function (jqXHR, exception) {
                    var flot = document.querySelector('#flotBBT');
                    flot.style = "margin: auto;";
                    var htmlText = `<p>尚無資料</p>`;
                    $("#flotBBT").append(htmlText);
                }
            });
            $.ajax({
                url: "http://localhost:55092/Handlers/AdminNCHandle.ashx?Act=vote&Type=BFoul",
                type: "GET",
                data: {},
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var name = obj.Name;
                        var popCnt = obj.PopCount;
                        var pieColor = getRandomColor();
                        dataSetBFK.push({ label: name, data: popCnt, color: pieColor });
                    }
                    $(document).ready(function () {
                        if (dataSetBFK.length > 1) {
                            $.plot($("#flotBFK"), dataSetBFK, options);
                        } else {
                            var flot = document.querySelector('#flotBFK');
                            flot.style = "margin: auto;";
                            var htmlText = `<p>由 ${name} 獨享臭名。</p>`;
                            $("#flotBFK").append(htmlText);
                        }
                    });
                },
                error: function (jqXHR, exception) {
                    var flot = document.querySelector('#flotBFK');
                    flot.style = "margin: auto;";
                    var htmlText = `<p>尚無資料</p>`;
                    $("#flotBFK").append(htmlText);
                }
            });


        });
    </script>
    <style>
        body {
            background-color: #3AB0FA;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-1 col-sm-12"></div>
                <div class="col-md-10 col-sm-12 text-center">
                    <br />
                    <br />
                    <div class="card">
                        <div class="card-header">
                            目前投票資訊
                        </div>
                        <div class="card-body">
                            <asp:Literal ID="ltVoteTable" runat="server" />
                            <a href="MainInfoPage.aspx" class="btn btn-primary mt-4">返回資訊總頁</a>
                        </div>
                    </div>
                    <br />
                    <div class="card">
                        <div class="card-header">
                            目前投票狀況
                        </div>
                        <div class="card-body">
                            <div class="row row-cols-1 row-cols-md-2">
                                <div class="col">
                                    <div class="card border-success mb-2">
                                        <div class="card-header">最受歡迎球員</div>
                                        <div class="card-body text-success">
                                            <div id="flotB" style="width: 400px; height: 300px; margin: auto;"></div>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col">
                                    <div class="card border-success mb-2">
                                        <div class="card-header">最受歡迎隊伍</div>
                                        <div class="card-body text-success">
                                            <div id="flotT" style="width: 400px; height: 300px; margin: auto;"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="card border-success mb-2">
                                        <div class="card-header">最受歡迎賽事</div>
                                        <div class="card-body text-success">
                                            <div id="flotR" style="width:400px; height: 300px; margin: auto;"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="card border-success mb-2">
                                        <div class="card-header">公認壞脾氣</div>
                                        <div class="card-body text-success">
                                            <div id="flotBBT" style="width:400px; height: 300px; margin: auto;"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="card border-success mb-2">
                                        <div class="card-header">公認犯規王</div>
                                        <div class="card-body text-success">
                                            <div id="flotBFK" style="width:400px; height: 300px; margin: auto;"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <a href="MainInfoPage.aspx" class="btn btn-primary mt-4">返回資訊總頁</a>
                        </div>
                    </div>
                </div>
                <div class="col-md-1 col-sm-12"></div>
            </div>
        </div>
    </form>
</body>
</html>
