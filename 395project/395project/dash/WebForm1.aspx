<%@ Page Title="Todays Facilitators" Language="C#" MasterPageFile="~/Master/Facilitator.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="_395project.dash.WebForm1" %>
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

    <div class="dashboardMargins" id="blueLabel">
        <asp:Label runat="server" CssClass="dash-header">Blue</asp:Label>
    </div>
    <div class="dashboardMargins" id="bluePeople" runat="server">
        <div class="col-lg-offset-5 col-lg-5">
            <asp:PlaceHolder id="blueRoom" runat="server" />
        </div>
    </div>

    <br />

    <div class="dashboardMargins" id="purpleLabel" style="padding-top:100px;">
        <asp:Label runat="server" CssClass="dash-header">Purple</asp:Label>
    </div>
    <div class="dashboardMargins" id="purplePeople" runat="server">
        <div class="col-lg-offset-5 col-lg-5">
            <asp:PlaceHolder id="purpleRoom" runat="server" />
        </div>
    </div>

    <br />

    <div class="dashboardMargins" id="greenLabel" style="padding-top:100px;">
        <asp:Label runat="server" CssClass="dash-header">Green</asp:Label>
    </div>
    <div class="dashboardMargins" id="greenPeople" runat="server">
        <div class="col-lg-offset-5 col-lg-5">
            <asp:PlaceHolder id="greenRoom" runat="server" />
        </div>
    </div>

    <br />

    <div class="dashboardMargins" id="redLabel" style="padding-top:100px;">
        <asp:Label runat="server" CssClass="dash-header">Red</asp:Label>
    </div>
    <div class="dashboardMargins" id="redPeople" runat="server">
        <div class="col-lg-offset-5 col-lg-5">
            <asp:PlaceHolder id="redRoom" runat="server" />
        </div>
    </div>

    <br />

    <div class="dashboardMargins" id="greyLabel" style="padding-top:100px;">
        <asp:Label runat="server" CssClass="dash-header">Grey</asp:Label>
    </div>
    <div class="dashboardMargins" id="greyPeople" runat="server">
        <div class="col-lg-offset-5 col-lg-5">
            <asp:PlaceHolder id="greyRoom" runat="server" />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
