<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MainInfoPage.aspx.cs" Inherits="CNCTopic7309.UserPages.MainInfoPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal Text="TEST" ID="ltlTest" runat="server" />
    <asp:Button Text="個資編輯" runat="server" ID="btnUIE" OnClick="btnUIE_Click"/> &nbsp
    <asp:Button Text="球隊編輯(Admin)" runat="server" ID="btnTIE" Visible="false"/> &nbsp
    <asp:Button Text="球員編輯(Admin)" runat="server" ID="btnBIE" Visible="false"/> &nbsp
    <asp:Button Text="賽場編輯(Admin)" runat="server" ID="btnRIE" Visible="false"/> &nbsp
    <asp:Button Text="LogOut" ID="btnLogOut" runat="server" OnClick="btnLogOut_Click"/>
</asp:Content>
