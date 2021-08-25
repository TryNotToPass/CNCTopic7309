<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CssTemp.aspx.cs" Inherits="CNCTopic7309.CssTemp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>TryBootstrap</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="js/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-4 col-sm-6">
                    <button type="button" class="btn btn-primary">PP</button>
                    <br />

                    <div class="mb-3">
                      <label for="exampleFormControlInput1" class="form-label">Email HTML</label>
                      <input type="email" class="form-control" id="exampleFormControlInput1" name="txtEH" placeholder="name@example.com"/>
                    </div>
                    <div class="mb-3">
                      <label for="exampleFormControlTextarea1" class="form-label">Textarea HTML</label>
                      <textarea class="form-control" name="txtDH" id="exampleFormControlTextarea1" rows="3"></textarea>
                    </div>
                    <input type="submit" value="Save" class="btn btn-dark" />
                </div>
                <div class="col-md-4 col-sm-6">
                    

                    <!-- Button trigger modal -->
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
                      Launch demo modal
                    </button>

                    <!-- Modal -->
                    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                          </div>
                          <div class="modal-body">
                            這裡可以放很多東西，譬如textBox之類的，底下的按鈕asp也可用
                          </div>
                          <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-primary">Save changes</button>
                            <asp:Button Text="btn1" ID="btn1" CssClass="btn btn-primary" runat="server" OnClick="btn1_Click"/>
                          </div>
                        </div>
                      </div>
                    </div>

                </div>
                <div class="col-md-4 col-sm-6">
                    <asp:Literal runat="server" ID="ltlMsg"></asp:Literal>
                    <div class="mb-3">
                        <label for="<%= this.txtEmail.ClientID %>" class="form-label">Email address</label>
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="<%= this.txtDesc.ClientID %>" class="form-label">Example textarea</label>
                        <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Button Text="btnSave" runat="server" ID="btnSave" OnClick="btnSave_Click"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
