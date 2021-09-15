<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetPWDBack.aspx.cs" Inherits="CNCTopic7309.GetPWDBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>NBA資訊系統-密碼重設頁</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:PlaceHolder ID="phInput" runat="server">
                請輸入帳號確認：<asp:TextBox ID="txtAcc" runat="server" OnTextChanged="txtAcc_TextChanged"></asp:TextBox>
                <br />
                <asp:Button Text="確定重設密碼" runat="server" ID="btnCheck" OnClick="btnCheck_Click"/>
                <br />
                <asp:Label Text="" runat="server" ID="lblMiss"/>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phAcessSucess" runat="server" Visible = "false">
                <p>下列字串將是你的新密碼，請立刻登入後變更之</p>
                <br />
                <asp:Label Text="text" runat="server" ID="lblMsg"/>
                <br />
                <a href="Login.aspx">前去NBA冠軍賽系統登入</a>
            </asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
