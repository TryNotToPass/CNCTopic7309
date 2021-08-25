<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermissionChangePage.aspx.cs" Inherits="CNCTopic7309.UserPages.PermissionChangePage" %>

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
        <div>
            最高管理員
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <%--<asp:BoundField DataField="UserLevel" HeaderText="LV" />--%>
                <asp:TemplateField HeaderText="層級">
                    <ItemTemplate>
                        <%# ((int)Eval("UserLevel") == 1) ? "管理員" : "使用者" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="暱稱" />
                <asp:BoundField DataField="Account" HeaderText="帳號" />
                <asp:BoundField DataField="Email" HeaderText="信箱" />
                <asp:TemplateField HeaderText="行動">
                    <ItemTemplate>
                            <asp:Button Text="刪除" CssClass="btn btn-primary" ID="btnDel" runat="server"
                            OnCommand="btnDel_Command" CommandArgument='<%# Eval("ID") %>' OnClientClick='<%# $"return funcAlert(\"{Eval("Account")}\")" %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label Text="text" runat="server" ID="label1"/>
    </form>
    <script>
        function funcAlert(txtAccount) {
            return confirm('確認刪除目標帳號嗎？: ' + txtAccount);
        }
    </script>
</body>
</html>
