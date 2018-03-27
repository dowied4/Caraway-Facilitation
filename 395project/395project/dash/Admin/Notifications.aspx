<%@ Page Title="Notifications" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="_395project.dash.Admin.Notifications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

  <link href="/Content/dashboard.css" rel="stylesheet" />
    <h2 class="page-title"><%: Title %></h2>
    <p class="error-text">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <hr />

    <div id="container" runat="server">
        <asp:PlaceHolder id="alerts" runat="server" />
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
