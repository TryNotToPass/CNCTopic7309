<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoEditor.aspx.cs" Inherits="CNCTopic7309.UserPages.UserInfoEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            GUID：<asp:Literal Text="" ID="Literal1" runat="server" />
            <br />
            帳號：<asp:TextBox runat="server" ID="txtAccount"></asp:TextBox>
            <br />
            姓名：<asp:TextBox runat="server" ID="txtName"></asp:TextBox>
            <br />
            信箱：<asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
            <br />
            <asp:Button Text="更新" runat="server" ID="btnUpdate" OnClick="btnUpdate_Click"/> &nbsp
            <asp:Button Text="更改密碼" runat="server" ID="btnPWDChange" /> &nbsp
        </div>
        <asp:Literal Text="" ID="ltlMsg" runat="server" />
    </form>
</body>
</html>
