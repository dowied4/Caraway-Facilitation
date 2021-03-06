﻿<%@ Page Title="Facilitation Sign Up" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="_395project.dash.Calendar1" %>
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
    <p class="error-text">
        <asp:Literal runat="server" Visible="false" ID="ErrorMessage" />
    </p>
    <asp:ValidationSummary runat="server" CssClass="error-text" />
    <div class="dashboardMargins" id="signupLabel" style="padding-bottom:60px; text-decoration:underline;">
			<asp:Label ID="SignUpLabel" runat="server" CssClass="section-header">Sign Up</asp:Label>
	</div>

    <div class="row">
        <div class="col-lg-offset-1 col-lg-6">
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

        <div class="col-lg-2">
            <div class="dashboardMargins" id="signupFacilitator">
                <!-- Facilitator Choice -->
                <asp:Label id="FacilitatorLabel" runat="server" CssClass="input-header">Facilitator</asp:Label>
                <div class="row" style="padding-bottom:10px;padding-right:14px;">
                    <asp:DropDownList ID="FacilitatorDropDown" runat="server" DataSourceID="Facilitators" DataTextField="FullName" DataValueField="FullName" CssClass="signupDropDown" Width="200px"></asp:DropDownList>
                    <asp:SqlDataSource ID="Facilitators" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT FullName FROM Facilitators WHERE (Id = @CurrentUser)">
                        <SelectParameters>
	                        <asp:Parameter DefaultValue="Anonymous" Name="CurrentUser" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>

                <!-- Room Choice -->
                <div class="row" style="padding-right:14px;">
	                <asp:Label ID="RoomLabel" runat="server" CssClass="input-header">Room</asp:Label>
                </div>
                <div class="row" style="padding-bottom:10px;padding-right:14px;">
	                <asp:DropDownList ID="RoomDropDown" runat="server" DataSourceID="Rooms" DataTextField="Room" DataValueField="Room" CssClass="signupDropDown" Width="200px"></asp:DropDownList>
	                <asp:SqlDataSource ID="Rooms" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Room] FROM [Rooms]"></asp:SqlDataSource>
                </div>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                <!-- Time Slot Choice -->
                <div class="row" style="padding-right:14px;">
                    <asp:Label ID="TimeSlotLabel" runat="server" CssClass="input-header">Time Slot</asp:Label>
                </div>										
				<div class="row" style="padding-bottom:10px;" >
					<asp:DropDownList ID="TimeSlotDropDown" runat="server" AutoPostBack="false" CssClass="signupDropDown" OnSelectedIndexChanged="TimeSlotDropDown_SelectedIndexChanged" Width="168px">
					</asp:DropDownList>
                    <button runat="server" id="editRun" class="btn btn-default" AutoPostBack="false" title="Edit" onserverclick="editClick" o>
                        <i class="fa fa-edit"></i>
                    </button>
				</div>
                </ContentTemplate>
                </asp:UpdatePanel>
                
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" 
                    BackgroundCssClass="modalBackground" TargetControlID="btn2" X="-1" Y="-1" 
                    RepositionMode="RepositionOnWindowResizeAndScroll"></ajaxToolkit:ModalPopupExtender>

                <asp:Panel ID="Panel1" runat ="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <div class="dashboardMargins" id="calendarTime">
                        <asp:Label runat="server" CssClass="section-header">Custom Time Slot</asp:Label>
                    </div>             
                    <div class="row" style="padding-bottom:10px;" >
                    <div class="col-md-4">
                        <div class="dashboardMargins" id="calendarStart">
                            <asp:Label ID="Label2" runat="server" Text="Start" CssClass="input-header"></asp:Label>
                        </div>
                    </div>
               		    <asp:TextBox ID="StartTimeTextBox" type="time" runat="server" CssClass="signupDropDown"></asp:TextBox>
					</div>
					<div class="row" style="padding-bottom:10px;">
                        <div class="col-md-4">
                            <div class="dashboardMargins" id="calendarEnd">
                                <asp:Label ID="Label9" runat="server" Text="End" CssClass="input-header"></asp:Label>
                            </div>
                    </div>
					    <asp:TextBox ID="EndTimeTextBox" type="time" runat="server" CssClass="signupDropDown"></asp:TextBox>
					</div>
                    <asp:Button ID="okButton" runat="server" Text="Confirm" OnClick="onConfirm" CssClass="mybutton"/>
                    <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick="onCancel" CssClass="mybutton"/>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2"
                    BackgroundCssClass="modalBackground" TargetControlID="btn" 
                    X="-1" Y="-1" RepositionMode="RepositionOnWindowResizeAndScroll"></ajaxToolkit:ModalPopupExtender>

                <asp:Panel ID="Panel2" runat ="server">
                    <div class="row" style="padding-bottom:10px;" >
               		    <asp:Label ID="popLabel" runat="server" CssClass="input-header">There are 3 or more users signed up sometime in your time slot, continue?</asp:Label>
					</div>
                    <asp:Button ID="Button1" runat="server" Text="Confirm" OnClick="SignUpButton_Click" CssClass="mybutton"/>
                    <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="onFullCancel" CssClass="mybutton" />
                </asp:Panel>


                <!-- Sign Up Button -->
                <div class="row">
		            <asp:Button ID="SignUpButton" runat="server" Text="Sign Up" OnClick="onSignUp" CssClass="mybutton"/>
                </div>
            </div>
        </div>
    </div>

    <div style="visibility:hidden">
		<asp:Button id="btn2" runat="server"/>
	</div>

    <div style="visibility:hidden">
		<asp:Button id="btn" runat="server"/>
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
      Scale="Day" BackColor="#FFFFD5" BorderColor="#000000" CssClassPrefix="scheduler_default" DataValueField="VolunteerId" DurationBarColor="Blue" EventBackColor="#FFFFFF" EventBorderColor="#000000" EventHeight="25" HeaderFontColor="0, 0, 0" HeaderHeight="17" HourBorderColor="#EAD098" HourNameBackColor="#ECE9D8" HourNameBorderColor="#ACA899" HoverColor="#FFED95" NonBusinessBackColor="#FFF4BC" StartDate="" style="top: 0px; left: 1px" BusinessBeginsHour="8" BusinessEndsHour="17" CellWidth="50"
      >
    </DayPilot:DayPilotScheduler>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
