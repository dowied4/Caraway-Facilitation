<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="_395project.Pages.AccountList" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h1 class="page-header">Account List</h1>

    <div id="df" runat="server">
        <asp:GridView id="GridView1" runat="server" /> 
    </div>

</asp:Content>

