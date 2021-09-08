<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoEditor.aspx.cs" Inherits="CNCTopic7309.UserPages.UserInfoEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>個人資料編輯</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <script src="../js/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-md-2 col-sm-1">
            </div>
            <div class="col-md-8 col-sm-10">
                <asp:PlaceHolder runat="server" ID="ph_changeACC">
                    <div class="card">
                          <div class="card-header">
                            個人資料
                          </div>
                          <div class="card-body">
                            <div class="input-group mb-1">
                                <span class="input-group-text" id="basic-addon1">帳號</span>
                                <asp:TextBox runat="server" ID="txtAccount" CssClass="form-control" aria-describedby="basic-addon1"></asp:TextBox>
                            </div>
                            <div class="input-group mb-1">
                                <span class="input-group-text" id="basic-addon2">姓名</span>
                                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" aria-describedby="basic-addon2"></asp:TextBox>
                            </div>
                            <div class="input-group mb-1">
                                <span class="input-group-text" id="basic-addon3">信箱</span>
                                <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" CssClass="form-control" aria-describedby="basic-addon3"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <%--GUID：<asp:Literal Text="" ID="Literal1" runat="server" />--%>
                </asp:PlaceHolder>

                <asp:PlaceHolder runat="server" ID="ph_changePWD" Visible ="false">
                    <div class="card">
                        <div class="card-header">
                            變更密碼
                        </div>
                        <div class="card-body">
                            <div class="input-group mb-1">
                                <span class="input-group-text" id="basic-addonS1">舊密碼</span>
                                <asp:TextBox runat="server" ID="txtOldPWD" TextMode="Password" CssClass="form-control" aria-describedby="basic-addonS1"></asp:TextBox>
                            </div>
                            <div class="input-group mb-1">
                                <span class="input-group-text" id="basic-addonS2">新密碼</span>
                                <asp:TextBox runat="server" ID="txtNewPWD" TextMode="Password" CssClass="form-control" aria-describedby="basic-addonS2"></asp:TextBox>
                            </div>
                            <div class="input-group mb-1">
                                <span class="input-group-text" id="basic-addonS3">確認密碼</span>
                                <asp:TextBox runat="server" ID="txtAgnPWD" TextMode="Password" CssClass="form-control" aria-describedby="basic-addonS3"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </asp:PlaceHolder>
                <br />
                <asp:Button Text="更新" runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" CssClass="btn btn-outline-dark"/> &nbsp
                <asp:Button Text="啟用/關閉變更密碼" runat="server" ID="btnPWDChange" OnClick="btnPWDChange_Click" CssClass="btn btn-outline-dark"/> &nbsp
                <a href="MainInfoPage.aspx" class="btn btn-outline-dark">返回資訊總頁</a>
                <br />
                <asp:Literal Text="" ID="ltlMsg" runat="server" />
            </div>
            <div class="col-md-2 col-sm-1">
            </div>
        </div>
    </form>
</body>
</html>
