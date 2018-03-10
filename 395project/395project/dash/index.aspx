<%@ Page Title="Dashboard" Language="C#" MasterPageFile="/Master/Facilitator.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="_395project.Pages.index" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">


   <link href="../Content/dashboard.css" rel="stylesheet" />
   <h2 class="page-title"><%: Title %></h2>

    <hr />

    <div class="row" style="padding-bottom: 30px;">
        <div class="dashboardMargins" id="dashboardHoursLabel">
            <asp:Label runat="server" CssClass="section-header">Hours</asp:Label>
        </div>
    </div>

    <!-- Total Hours Section -->
        <div class="row">
            <!-- Weekley Section -->
            <div class="col-md-offset-2 col-md-3">
                <div class="dashboardMargins" id="dashboardWeeklyLabel">
			        <asp:Label runat="server" CssClass="dash-header">Weekly Total</asp:Label>
                </div>
                <div class="row">
                    <div id="dashboardWeeklyTotal" class="dashboardMargins">
                        <div>
			                <asp:Label runat="server" CssClass="stats-info" ID="WeeklyHoursLabel" >Error</asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Monthly Section -->
            <div class="col-md-offset-2 col-md-3">
                <div class="dashboardMargins" id="dashboardMonthlyLabel">
			        <asp:Label runat="server" CssClass="dash-header">Monthly Total</asp:Label>
                </div>
                <div class="row">
                    <div id="dashboardMonthlyTotal" class="dashboardMargins">
                        <div>
			                <asp:Label runat="server" CssClass="stats-info" ID="MonthlyHoursLabel">Error</asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    
    <hr />

    <div class="row" style="padding-bottom: 30px;">
        <div class="dashboardMargins" id="dashboardInfoLabel">
            <asp:Label runat="server" CssClass="section-header">Information</asp:Label>
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
                        <asp:GridView ID="GridView1" runat="server" Width="300px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowFooter="true">
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
                        <asp:GridView ID="GridView2" runat="server" Width="300px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowFooter="true">
                            <HeaderStyle CssClass="HeaderStyle" />
                            <FooterStyle CssClass="FooterStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="Children"  />
                                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                <asp:BoundField DataField="Classroom" HeaderText="Classroom" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Upcoming Hours -->
    <div class="dashboardMargins" id="dashboardUpcomingLabel">
        <asp:Label runat="server" ID="Label2" CssClass="dash-header">Upcoming Hours</asp:Label>
    </div>

    <div id="container" runat="server">
        <asp:PlaceHolder id="spacer" runat="server" />
    </div>

    <div class="dashboardMargins" id="dashboardNoneLabel1">
        <asp:Label runat="server" ID="Label1" Visible="false" CssClass="dash-header">None</asp:Label>
    </div>

    <div id="dashboardGridUpcoming" class="dashboardMargins">
        <div class="rounded_corners" style="width: 600px">
            <asp:GridView ID="GridView3" runat="server" Width="600px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowFooter="true" >
                <HeaderStyle CssClass="HeaderStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                <Columns>
                    <asp:BoundField DataField="FacilitatorName" HeaderText="Facilitator Name" />
                    <asp:BoundField DataField="StartTime" HeaderText="StartTime" />
                    <asp:BoundField DataField="EndTime" HeaderText="EndTime" />
                    <asp:BoundField DataField="RoomId" HeaderText="RoomId" />
                </Columns>
             </asp:GridView> 
        </div>
    </div>

    <!-- Completed Hours -->
    <div class="dashboardMargins" id="dashboardCompletedLabel">
        <asp:Label runat="server" ID="Label3" CssClass="dash-header">Completed Hours</asp:Label>
    </div>

    <div id="Div1" runat="server">
        <asp:PlaceHolder id="PlaceHolder1" runat="server" />
    </div>

    <div class="dashboardMargins" id="dashboardNoneLabel2">
        <asp:Label runat="server" ID="Label4" Visible="false" CssClass="dash-header">None</asp:Label>
    </div>

    <div id="dashboardGridCompleted" class="dashboardMargins">
        <div class="rounded_corners" style="width: 600px">
            <asp:GridView ID="GridView4" runat="server" Width="600px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowFooter="true" >
                <HeaderStyle CssClass="HeaderStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                <Columns>
                    <asp:BoundField DataField="FacilitatorName" HeaderText="Facilitator Name"/>
                    <asp:BoundField DataField="StartTime" HeaderText="StartTime"/>
                    <asp:BoundField DataField="EndTime" HeaderText="EndTime"/>
                    <asp:BoundField DataField="RoomId" HeaderText="RoomId"/>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:LinkButton ID="comfirmButtons" runat="server" Text="Confirm" OnClick="ConfirmButton" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText ="Decline">
                        <ItemTemplate>
                            <asp:LinkButton ID="declineButtons" runat="server" Text="Decline" OnClick="DeclineButton" />
                        </ItemTemplate>
                    </asp:TemplateField>
               </Columns>
            </asp:GridView>
        </div>
    </div>


</asp:Content>
