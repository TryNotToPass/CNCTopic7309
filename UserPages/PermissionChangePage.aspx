<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermissionChangePage.aspx.cs" Inherits="CNCTopic7309.UserPages.PermissionChangePage" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <script src="../js/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal Text="" ID="ltAlert" runat="server" />
        <div class="row">
            <div class="col-md-1 col-sm-12">
            </div>

            <div class="col-md-9 col-sm-12 text-center">
                <div class="d-grid gap-2 col-6 mx-auto">
                    <a href="MainInfoPage.aspx" class="btn btn-primary">返回資訊主頁</a>
                </div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-bordered">
                    <Columns>
                        <asp:TemplateField HeaderText="層級">
                            <ItemTemplate>
                                <%# ((int)Eval("UserLevel") == 1) ? "版主" : "使用者" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="暱稱"/>
                        <asp:BoundField DataField="Account" HeaderText="帳號" />
                        <asp:BoundField DataField="Email" HeaderText="信箱" />
                        <asp:BoundField DataField="CreateDate" HeaderText="加入日期" />
                        <asp:TemplateField HeaderText="行動">
                            <ItemTemplate>
                                <asp:Button Text="升級" CssClass="btn btn-primary" runat="server" ID="btnUG" OnCommand="btnUG_Command" CommandArgument='<%# Eval("ID") %>'/>
                                <asp:Button Text="降級" CssClass="btn btn-primary" runat="server" ID="btnDG" OnCommand="btnDG_Command" CommandArgument='<%# Eval("ID") %>'/>
                                <asp:Button Text="刪除" CssClass="btn btn-primary" ID="btnDel" runat="server"
                                OnCommand="btnDel_Command" CommandArgument='<%# Eval("ID") %>' OnClientClick='<%# $"return funcAlert(\"{Eval("Account")}\")" %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <uc1:ucPager runat="server" id="ucPager" PageSize="5" CurrentPage="1" TotalSize="2" Url="/UserPages/PermissionChangePage.aspx"/>
                <br />
                <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                    <p>
                        目前尚無任何使用者。
                    </p>
                </asp:PlaceHolder>
                <asp:Label Text="" runat="server" ID="lblMsg"/>
            </div>

            <div class="col-md-1 col-sm-12">
            </div>
        </div>
    </form>
    <script>
        function funcAlert(txtAccount) {
            return confirm('確認刪除目標帳號嗎？: ' + txtAccount);
        }
    </script>
</body>
</html>
