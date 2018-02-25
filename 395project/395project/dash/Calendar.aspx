<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="_395project.dash.Calendar1" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<asp:Calendar ID="Calendar" runat="server" OnSelectionChanged="Calendar_SelectionChanged"></asp:Calendar>
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
  Scale="Day" BackColor="#FFFFD5" BorderColor="#000000" CssClassPrefix="scheduler_default" DataValueField="VolunteerId" DurationBarColor="Blue" EventBackColor="#FFFFFF" EventBorderColor="#000000" EventHeight="25" HeaderFontColor="0, 0, 0" HeaderHeight="17" HourBorderColor="#EAD098" HourNameBackColor="#ECE9D8" HourNameBorderColor="#ACA899" HoverColor="#FFED95" NonBusinessBackColor="#FFF4BC" StartDate="" style="top: 0px; left: 1px" BusinessBeginsHour="8" BusinessEndsHour="17" CellWidth="50"
  >
</DayPilot:DayPilotScheduler>

		<asp:Panel ID="Panel1" runat="server">
		<div>
			<asp:Label ID="SignUpLabel" runat="server" Text="Sign Up" Font-Bold="True" Font-Size="Larger"></asp:Label>
		</div>
		<asp:Label ID="FacilitatorLabel" runat="server" Text="Facilitator"></asp:Label>
		<asp:DropDownList ID="FacilitatorDropDown" runat="server" DataSourceID="Facilitators" DataTextField="FullName" DataValueField="FullName"></asp:DropDownList>
		<asp:SqlDataSource ID="Facilitators" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT FullName FROM Facilitators WHERE (Id = @CurrentUser)">
			<SelectParameters>
				<asp:Parameter DefaultValue="Anonymous" Name="CurrentUser" />
			</SelectParameters>
		</asp:SqlDataSource>
		<asp:Label ID="RoomLabel" runat="server" Text="Room"></asp:Label>
		<asp:DropDownList ID="RoomDropDown" runat="server" DataSourceID="Rooms" DataTextField="Room" DataValueField="Room"></asp:DropDownList>
		<asp:SqlDataSource ID="Rooms" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Room] FROM [Rooms]"></asp:SqlDataSource>
		<asp:Label ID="TimeSlotLabel" runat="server" Text="Time Slot"></asp:Label>
		<asp:DropDownList ID="TimeSlotDropDown" runat="server">
			<asp:ListItem Value="Morning">Morning (8:45-12)</asp:ListItem>
			<asp:ListItem Value="Lunch">Lunch (12-1) X2</asp:ListItem>
			<asp:ListItem Value="Afternoon">Afternoon (1-3:45)</asp:ListItem>
			</asp:DropDownList>
			<div>
				<asp:Button ID="SignUpButton" runat="server" Text="Sign Up" OnClick="SignUpButton_Click" />
			</div>
	</asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
