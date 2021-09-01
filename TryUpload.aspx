<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryUpload.aspx.cs" Inherits="CNCTopic7309.TryUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:FileUpload ID="FileUpload2" runat="server" />
            <asp:FileUpload ID="FileUpload3" runat="server" />
            <asp:Button runat ="server" ID="btnOK" Text="OK" OnClick ="btnOK_Click"/>
            <asp:Label Text="msg" runat="server" ID="lblMsg"/>
        </div>
    </form>
</body>
</html>
