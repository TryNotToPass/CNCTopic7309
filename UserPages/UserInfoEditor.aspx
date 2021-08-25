<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoEditor.aspx.cs" Inherits="CNCTopic7309.UserPages.UserInfoEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:PlaceHolder runat="server" ID="ph_changeACC">
            GUID：<asp:Literal Text="" ID="Literal1" runat="server" />
            <br />
            帳號：<asp:TextBox runat="server" ID="txtAccount"></asp:TextBox>
            <br />
            姓名：<asp:TextBox runat="server" ID="txtName"></asp:TextBox>
            <br />
            信箱：<asp:TextBox runat="server" ID="txtEmail" TextMode="Email"></asp:TextBox>
        </asp:PlaceHolder>
        <br />
        <asp:PlaceHolder runat="server" ID="ph_changePWD" Visible ="false">
            舊密碼：<asp:TextBox runat="server" ID="txtOldPWD" TextMode="Password"></asp:TextBox>
            <br />
            新密碼：<asp:TextBox runat="server" ID="txtNewPWD" TextMode="Password"></asp:TextBox>
            <br />
            再輸入一次新密碼：<asp:TextBox runat="server" ID="txtAgnPWD" TextMode="Password"></asp:TextBox>
        </asp:PlaceHolder>
        <br />
        <asp:Button Text="更新" runat="server" ID="btnUpdate" OnClick="btnUpdate_Click"/> &nbsp
        <asp:Button Text="啟用/關閉變更密碼" runat="server" ID="btnPWDChange" OnClick="btnPWDChange_Click"/> &nbsp
        <br />
        <a href="MainInfoPage.aspx">返回資訊總頁</a>
        <br />
        <asp:Literal Text="" ID="ltlMsg" runat="server" />
    </form>
</body>
</html>
