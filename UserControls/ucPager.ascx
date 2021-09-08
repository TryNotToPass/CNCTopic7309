<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPager.ascx.cs" Inherits="CNCTopic7309.UserControls.ucPager" %>

<div class="row">
    <div class="col-4"></div>
    <div class="col-4">
        <div class="btn-toolbar" role="toolbar" aria-label="Toolbar with button groups">
            <div class="btn-group me-2" role="group" aria-label="First group">
                <a href="#" runat="server" id="aLinkF" class="btn btn-outline-secondary">Front</a>
                <asp:Literal Text="" ID="ltlPager" runat="server"/>
                <a href="#" runat="server" id="aLinkL" class="btn btn-outline-secondary">Last</a>
            </div>
        </div>
    </div>
    <div class="col-4"></div>

    <div class="col-12">
        <asp:Literal runat="server" ID="ltlMsg"></asp:Literal>
    </div>
</div>