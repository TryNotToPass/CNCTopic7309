<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MainInfoPage.aspx.cs" Inherits="CNCTopic7309.UserPages.MainInfoPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal Text="TEST" ID="ltlTest" runat="server" />
    <asp:Button Text="個資編輯" runat="server" ID="btnUIE" OnClick="btnUIE_Click"/> &nbsp
    <asp:Button Text="冠軍賽資訊編輯(Admin)" runat="server" ID="btnTIE" Visible="false" OnClick="btnTIE_Click"/> &nbsp
    <asp:Button Text="權限變更(SuperAdmin)" runat="server" ID="btnLvChange" OnClick="btnLvChange_Click" Visible="false"/> &nbsp
    <asp:Button Text="LogOut" ID="btnLogOut" runat="server" OnClick="btnLogOut_Click"/>
</asp:Content>
