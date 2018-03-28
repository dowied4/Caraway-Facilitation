<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="_395project.dash.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/dashboard.css" rel="stylesheet" />
    <h2 class="page-title"><%: Title %></h2>
    <h2 id="head" runat="server">Dashboard</h2>
    <hr />

    <h2 id="role" runat="server">Current Absences:</h2>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"></asp:GridView>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>

