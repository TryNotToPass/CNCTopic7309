<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CNCTopic7309.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>使用者登入</title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <th>帳號：</th>
                <td><asp:TextBox runat="server" ID="txtAccount"></asp:TextBox></td>
            </tr>
            <tr>
                <th>密碼：</th>
                <td><asp:TextBox runat="server" ID="txtPassword"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan ="2" align="center">
                    <asp:Button runat="server" ID="btnLogin" Text="登入" OnClick ="btnLogin_Click" Font-Size="16" /> &nbsp
                    <asp:Button runat="server" ID="btnForget" Text="忘記密碼" OnClick ="btnForget_Click" Font-Size ="16" /> &nbsp
                    <asp:Button runat="server" ID="btnTraveler" Text="訪客進入" OnClick ="btnTraveler_Click" Font-Size ="16" />
                </td>
            </tr>
            <asp:Literal Text="" runat="server" ID="ltlMsg" />
        </table>
    </form>
</body>
</html>
