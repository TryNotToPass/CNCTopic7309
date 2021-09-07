<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CNCTopic7309.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>使用者登入</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="js/bootstrap.js"></script>
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
                    <asp:Button runat="server" ID="btnTraveler" Text="訪客進入" OnClick ="btnTraveler_Click" Font-Size ="16" /> &nbsp
                    
                    <!-- Button trigger modal -->
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
                      新人註冊
                    </button>
                    <!-- Modal -->
                    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">新人註冊</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                          </div>
                          <div class="modal-body">
                              帳戶：<asp:TextBox runat="server" ID="txtAccCreate"/>
                              <br />
                              密碼：<asp:TextBox runat="server" ID="txtPWCreate" TextMode="Password"/>
                              <br />
                              確認密碼：<asp:TextBox runat="server" ID="txtPWCheck" TextMode="Password"/>
                              <br />
                              稱呼：<asp:TextBox runat="server" ID="txtName"/>
                              <br />
                              信箱：<asp:TextBox runat="server" ID="txtMail" TextMode="Email"/>
                          </div>
                          <div class="modal-footer">
                            <asp:Button Text="確認送出" ID="btnSave" CssClass="btn btn-primary" runat="server" OnClick="btnSave_Click"/>
                          </div>
                        </div>
                      </div>
                    </div>

                </td>
            </tr>
            <asp:Literal Text="" runat="server" ID="ltlMsg" />
        </table>
        <a href="#">測試專用</a>
    </form>
</body>
</html>
