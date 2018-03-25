<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="ViewCalendar.aspx.cs" Inherits="_395project.dash.ViewCalendar" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
        <link href="/fonts/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link href="../Content/dashboard.css" rel="stylesheet" />
    <h2 class="page-title"><%: Title %></h2>

    <hr />
    
    <div class="dashboardMargins" id="signupLabel" style="padding-bottom:60px; text-decoration:underline;">
			<asp:Label ID="SignUpLabel" runat="server" CssClass="section-header">Sign Up</asp:Label>
	</div>

    <div class="row">
        <div class="col-lg-offset-3 col-lg-6">
            <div class="dashboardMargins" id="signupCalender">
                <div class="calenderWrapper">
                    <asp:Calendar ID="Calendar" runat="server" OnSelectionChanged="Calendar_SelectionChanged" Width="400px" Height="300px" CssClass="calender">
                        <OtherMonthDayStyle ForeColor="#b0b0b0" />
                        <SelectorStyle CssClass="calSelector" />
                        <SelectedDayStyle CssClass="calSelectedDay"/>
                        <TitleStyle CssClass="calTitle" />
                        <TodayDayStyle CssClass="calTodayDay" />
                        <NextPrevStyle CssClass="calNextPrev" />
                        <DayHeaderStyle CssClass="calDayHeader" />
                        <DayStyle CssClass="calDay" ForeColor="#2d3338" />
                    </asp:Calendar>
                </div>
            </div>
        </div>
    </div>

    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" Font-Size="Large"></asp:Label>	
    <DayPilot:DayPilotScheduler 
      ID="DayPilotScheduler1" 
      runat="server" 
      DataStartField="StartTime" 
      DataEndField="EndTime" 
      DataTextField="FullName" 
      DataIdField="Id" 
      DataResourceField="RoomId" 
      CellGroupBy="Day"
      Scale="Day" BackColor="#FFFFD5" BorderColor="#000000" CssClassPrefix="scheduler_default" DataValueField="VolunteerId" DurationBarColor="Blue" EventBackColor="#FFFFFF" EventBorderColor="#000000" EventHeight="25" HeaderFontColor="0, 0, 0" HeaderHeight="17" HourBorderColor="#EAD098" HourNameBackColor="#ECE9D8" HourNameBorderColor="#ACA899" HoverColor="#FFED95" NonBusinessBackColor="#FFF4BC" StartDate="" style="top: 0px; left: 1px" BusinessBeginsHour="8" BusinessEndsHour="17" CellWidth="50">
    </DayPilot:DayPilotScheduler>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
