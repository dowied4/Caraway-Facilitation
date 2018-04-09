<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="AllAccounts.aspx.cs" Inherits="_395project.dash.Stats.AllAccounts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
		<link href="/Content/dashboard.css" rel="stylesheet" />

	     <div class="row" style="padding-bottom: 30px; text-align: center;">
        <div class="dashboardMargins" id="statsHoursLabel">
            <asp:Label runat="server" CssClass="section-header">HOURS</asp:Label>
			<div class="row">
				<asp:Label ID="Label1" runat="server" Text="Month" CssClass="input-header"></asp:Label>
        		<asp:DropDownList ID="MonthDropDown" runat="server" CssClass="signupDropDown">
				</asp:DropDownList>
			</div>  
			<div></div>

			<div class="row">
				<asp:Label ID="Label2" runat="server" Text="Year" CssClass="input-header"></asp:Label>
				<asp:DropDownList ID="YearDropDown" runat="server" CssClass="signupDropDown" DataSourceID="GetYears" DataTextField="Year" DataValueField="Year">
				</asp:DropDownList>
				<asp:SqlDataSource ID="GetYears" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT Year FROM dbo.Stats WHERE Year IS NOT NULL"></asp:SqlDataSource>
			</div>			
			<div></div>
			<asp:Button ID="UpdateButton" runat="server" Text="Get Stats" CssClass="mybutton" OnClick="UpdateButton_Click" />
		</div>
    </div>

	    <!-- Facilitator hours this month -->
	<div class="dashboardMargins" id="statsTotalMonthLabel">
		<asp:Label runat="server" CssClass="dash-header">Facilitator Hours This Month</asp:Label>
	</div>
	<div id="FacilitatorHours" class="dashboardMargins">
		<div class="rounded_corners" style="width: 600px">
			<asp:GridView ID="FacilitatorHoursGridView" AutoGenerateColumns="False" runat="server" Width="600px" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
					<HeaderStyle CssClass="HeaderStyle" />
					<FooterStyle CssClass="FooterStyle" />

<PagerStyle CssClass="pgr"></PagerStyle>

					<RowStyle CssClass="RowStyle" />
					<AlternatingRowStyle CssClass="AlternatingRowStyle" />
				<Columns>
					<asp:BoundField DataField="ID" HeaderText="ID" />
					<asp:BoundField DataField="Name" HeaderText="Name" NullDisplayText="0" />
					<asp:BoundField DataField="Week1" HeaderText="Week 1" NullDisplayText="0" />
					<asp:BoundField DataField="Week2" HeaderText="Week 2" NullDisplayText="0" />
					<asp:BoundField DataField="Week3" HeaderText="Week 3" NullDisplayText="0" />
					<asp:BoundField DataField="Week4" HeaderText="Week 4" NullDisplayText="0" />
					<asp:BoundField DataField="MonthTotal" HeaderText="Month Total" NullDisplayText="0" />
					<asp:BoundField DataField="YearTotal" HeaderText="Year Total" NullDisplayText="0" />
				</Columns>
			</asp:GridView>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
