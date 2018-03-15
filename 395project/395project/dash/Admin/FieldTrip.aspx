<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="FieldTrip.aspx.cs" Inherits="_395project.dash.Admin.FieldTrip" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
	<link href="../../Content/dashboard.css" rel="stylesheet" />
		<br />
		<br />
		<br />
		<br />
	    <div class="row">
        <div class="col-lg-offset-1 col-lg-6">
            <div class="dashboardMargins" id="signupCalender">
                <div class="calenderWrapper">
                    <asp:Calendar ID="Calendar" runat="server" Width="400px" Height="300px" CssClass="calender">
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

        <div class="col-lg-2">
            <div class="dashboardMargins" id="signupFacilitator">
                <!-- Facilitator Choice -->
				<asp:Label ID="Label1" runat="server" Text="Location" CssClass="input-header"></asp:Label>
                <div class="row" style="padding-bottom:10px;">
                <asp:TextBox ID="LocationTextBox" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>   
                </div>

                <!-- Room Choice -->
                <div class="row">
	                <asp:Label ID="Label4" runat="server" Text="Start Time"  CssClass="input-header"></asp:Label>
                </div>
                <div class="row" style="padding-bottom:10px;">
	            	<asp:TextBox ID="StartTimeTextBox" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                </div>

                <!-- Time Slot Choice -->
                <div class="row">
                    <asp:Label ID="Label3" runat="server" Text="End Time"  CssClass="input-header"></asp:Label>
                </div>										
				<div class="row" style="padding-bottom:10px;">
						<asp:TextBox ID="EndTimeTextBox" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
				</div>

                <!-- Sign Up Button -->
                <div class="row">
					<asp:Button ID="SubmitButton" runat="server" Text="Submit" CssClass="mybutton" OnClick="SubmitButton_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
