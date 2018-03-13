<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="AccountStat.aspx.cs" Inherits="_395project.dash.Admin.AccountStat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/dashboard.css" rel="stylesheet" />
    <h2 class="page-title"><%: Title %></h2>
    <h2 id="head" runat="server">Account Stats</h2>
    <hr />
       <div class="row" style="padding-bottom: 30px;">
        <div class="dashboardMargins" id="dashboardInfoLabel">
            <asp:Label runat="server" CssClass="section-header">INFORMATION</asp:Label>
        </div>
    </div>

        <!-- Facilitators Label and GridView -->
    <div class="row">
        <div class="col-md-offset-2 col-md-3">
            <div class="dashboardMargins" id="dashboardFacilitatorsLabel">
                <asp:Label runat="server" CssClass="dash-header">Facilitators</asp:Label>
            </div>
            <div class="row" id="userFacil" runat="server">
                <div id="dashboardGridFacilitator" class="dashboardMargins">
                    <div class="rounded_corners" style="width: 300px">
                        <asp:GridView ID="FacView" runat="server" Width="300px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle CssClass="HeaderStyle" />
                            <FooterStyle CssClass="FooterStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="FacilitatorName" HeaderText="Name" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <!-- Children Label and GridView -->
        <div class="col-md-offset-2 col-md-3">
            <div class="dashboardMargins" id="dashboardChildrenLabel">
                <asp:Label runat="server" CssClass="dash-header">Children</asp:Label>
            </div>
            <div class="row" id="userChildren" runat="server">
                <div id="dashboardGridChildren" class="dashboardMargins">
                    <div class="rounded_corners" style="width: 300px">
                        <asp:GridView ID="ChildView" runat="server" Width="300px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle CssClass="HeaderStyle" />
                            <FooterStyle CssClass="FooterStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="Children" />
                                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                <asp:BoundField DataField="Classroom" HeaderText="Classroom" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <hr />




    <link href="/Content/dashboard.css" rel="stylesheet" />
     <div class="row" style="padding-bottom: 30px; text-align: center;">
        <div class="dashboardMargins" id="statsHoursLabel">
            <asp:Label runat="server" CssClass="section-header">HOURS</asp:Label>
        </div>
    </div>

<div class="row" style="padding-bottom:50px">
    <!-- Weekley Section -->
     <div class="col-md-offset-1 col-md-2">
            <div class="dashboardMargins" id="Week1Label">
                <asp:Label runat="server" CssClass="dash-header">Week 1</asp:Label>
            </div>
            <div class="row">
                <div id="week1" class="dashboardMargins">
                    <div>
                        <asp:Label runat="server" CssClass="stats-info" ID="weekLabel1">0</asp:Label>
                    </div>
                </div>
            </div>
        </div>

    <div class="col-md-offset-1 col-md-2">
            <div class="dashboardMargins" id="Week2Label">
                <asp:Label runat="server" CssClass="dash-header">Week 2</asp:Label>
            </div>
            <div class="row">
                <div id="week2" class="dashboardMargins">
                    <div>
                        <asp:Label runat="server" CssClass="stats-info" ID="weekLabel2">0</asp:Label>
                    </div>
                </div>
            </div>
        </div>

    <div class="col-md-offset-1 col-md-2">
            <div class="dashboardMargins" id="Week3Label">
                <asp:Label runat="server" CssClass="dash-header">Week 3</asp:Label>
            </div>
            <div class="row">
                <div id="week3" class="dashboardMargins">
                    <div>
                        <asp:Label runat="server" CssClass="stats-info" ID="weekLabel3">0</asp:Label>
                    </div>
                </div>
            </div>
        </div>

    <div class="col-md-offset-1 col-md-2">
            <div class="dashboardMargins" id="Week4Label">
                <asp:Label runat="server" CssClass="dash-header">Week 4</asp:Label>
            </div>
            <div class="row">
                <div id="week4" class="dashboardMargins">
                    <div>
                        <asp:Label runat="server" CssClass="stats-info" ID="weekLabel4">0</asp:Label>
                    </div>
                </div>
            </div>
        </div>
</div>
<div class="row">
       
        <div class="col-md-offset-2 col-md-3">
            <div class="dashboardMargins" id="statsMonthlyLabel">
                <asp:Label runat="server" CssClass="dash-header">Monthly Total</asp:Label>
            </div>
            <div class="row">
                <div id="statsMonthlyTotal" class="dashboardMargins">
                    <div>
                        <asp:Label runat="server" CssClass="stats-info" ID="monthlyHoursLabel">0</asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <!-- Monthly Section -->
        <div class="col-md-offset-2 col-md-3">
            <div class="dashboardMargins" id="statsYearlyLabel">
                <asp:Label runat="server" CssClass="dash-header">Yearly Total</asp:Label>
            </div>
            <div class="row">
                <div id="statsYearlyTotal" class="dashboardMargins">
                    <div>
                        <asp:Label runat="server" CssClass="stats-info" ID="yearlyHoursLabel">0</asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
     


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>


