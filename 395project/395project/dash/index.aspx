<%@ Page Title="Dashboard" Language="C#" MasterPageFile="/Master/Facilitator.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="_395project.Pages.index" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">


    <link href="../Content/dashboard.css" rel="stylesheet" />
    <h2 class="page-title"><%: Title %></h2>

    <hr />

    <div class="row" style="padding-bottom: 30px;">
        <div class="dashboardMargins" id="dashboardHoursLabel">
            <asp:Label runat="server" CssClass="section-header">HOURS</asp:Label>
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
                        <asp:Label runat="server" CssClass="stats-info" ID="WeeklyHoursLabel">0</asp:Label>
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
                        <asp:Label runat="server" CssClass="stats-info" ID="MonthlyHoursLabel">0</asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

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
                        <asp:GridView ID="GridView1" runat="server" Width="300px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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
                        <asp:GridView ID="GridView2" runat="server" Width="300px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Upcoming Hours -->
    <div class="dashboardMargins" id="dashboardUpcomingLabel">
        <asp:Label runat="server" ID="Label2" CssClass="dash-header">Upcoming Hours</asp:Label>
    </div>

    <div id="container" runat="server">
        <asp:PlaceHolder ID="spacer" runat="server" />
    </div>

    <div class="dashboardMargins" id="dashboardNoneLabel1">
        <asp:Label runat="server" ID="Label1" Visible="false" CssClass="dash-header">None</asp:Label>
    </div>
    <div id="dashboardGridUpcoming" class="dashboardMargins">
        <div class="rounded_corners" style="width: 600px">
            <asp:GridView ID="GridView3" runat="server" Width="600px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                <HeaderStyle CssClass="HeaderStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                <Columns>
                    <asp:BoundField DataField="FacilitatorName" HeaderText="Facilitator Name" />
                    <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                    <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                    <asp:BoundField DataField="Room" HeaderText="Room" />
					<asp:TemplateField HeaderText="Cancel">
                        <ItemTemplate>
                            <asp:LinkButton ID="CancelButton" runat="server" Text="Cancel" OnClick="onCancelGrid" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- Completed Hours -->
    <div class="dashboardMargins" id="dashboardCompletedLabel">
        <asp:Label runat="server" ID="Label3" CssClass="dash-header">Completed Hours</asp:Label>
    </div>

    <div id="Div1" runat="server">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
    </div>

    <div class="dashboardMargins" id="dashboardNoneLabel2">
        <asp:Label runat="server" ID="Label4" Visible="false" CssClass="dash-header">None</asp:Label>
    </div>

    <div id="dashboardGridCompleted" class="dashboardMargins">
        <div class="rounded_corners" style="width: 600px">
            <asp:GridView ID="GridView4" runat="server" Width="600px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                <HeaderStyle CssClass="HeaderStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                <Columns>
                    <asp:BoundField DataField="FacilitatorName" HeaderText="Facilitator Name" />
                    <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                    <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                    <asp:BoundField DataField="Room" HeaderText="Room" />
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:LinkButton ID="comfirmButtons" runat="server" Text="Confirm" OnClick="ConfirmButton" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Decline">
                        <ItemTemplate>
                            <asp:LinkButton ID="declineButtons" runat="server" Text="Decline" OnClick="onDeclineGrid" />
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Donate">
                       <ItemTemplate>
                            <asp:LinkButton ID="DonateButton" runat="server" Text="Donate" OnClick="DonateButton" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

	<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" 
		BackgroundCssClass="modalBackground" TargetControlID="btn" X="-1" Y="-1" 
		RepositionMode="RepositionOnWindowResizeAndScroll"></ajaxToolkit:ModalPopupExtender>

	<asp:Panel ID="Panel1"  CssClass="dashboardMargins" runat="server">
        <div class="dashboardMargins" id="dashboardDonate">
            <asp:Label runat="server" CssClass="section-header">Donate Hours</asp:Label>
        </div>
        <div class="row" style="padding-bottom:10px">
            <div class="col-md-4">
	            <div class="dashboardMargins" id="dashboardDonateEmail">
		            <asp:Label ID="Label5" runat="server" Text="Email" CssClass="input-header"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="dashboardMargins" id="dashboardDonateFac">
                    <asp:TextBox ID="EmailTextbox" AutoPostBack="true" runat="server" CssClass="signupDropDown" OnTextChanged="EmailTextbox_TextChanged" Width="200px"></asp:TextBox>
                </div>
	        </div>
        </div>
        <div class="row" style="padding-bottom:10px;">
            <div class="col-md-4">
                <div class="dashboardMargins" id="dashboardDonateEmailTxt">
                    <asp:Label ID="Label6" runat="server" Text="Facilitator" CssClass="input-header"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="dashboardMargins" id="dashboardDonateFacDrop">
                    <asp:DropDownList ID="FacilitatorDropDown" runat="server" DataSourceID="Facilitators" DataTextField="FullName" DataValueField="FullName" CssClass="signupDropDown" Width="200px"></asp:DropDownList>
                    <asp:SqlDataSource ID="Facilitators" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT FullName FROM Facilitators WHERE (Id = @SelectedUser)">
                        <SelectParameters>
	                        <asp:Parameter DefaultValue="Anonymous" Name="SelectedUser" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="dashboardMargins" id="dashboardDonateButtons">
			        <asp:Button ID="okButton" runat="server" Text="Confirm" OnClick="onConfirm" CssClass="mybutton"/>
			        <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick="onCancel" CssClass="mybutton"/>
                </div>
            </div>
        </div>
     </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2" 
		BackgroundCssClass="modalBackground" TargetControlID="btn" X="-1" Y="-1" 
		RepositionMode="RepositionOnWindowResizeAndScroll"></ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="Panel2"  CssClass="dashboardMargins" runat="server">
        <div class="dashboardMargins" id="dashboardDecline">
            <asp:Label runat="server" CssClass="section-header">Decline Hours</asp:Label>
        </div>
        <div class="row" style="padding-bottom:10px">
            <div class="col-md-12">
	            <div class="dashboardMargins" id="dashboardDeclineTxt" style="padding-left: 55px;">
		            <asp:Label ID="Label7" runat="server" Text="Email" CssClass="input-header">Decline your shift?</asp:Label>
                </div>
            </div>
            <!--
            <div class="col-md-4">
                <div class="dashboardMargins" id="dashboardDeclineInfo">
                    <asp:Label ID="shiftInfo" runat="server" CssClass="input-header"></asp:Label>
                </div>
	        </div> -->
        </div>
        <!--
        <div class="row" style="padding-bottom:10px">
            <div class="col-md-4">
	            <div class="dashboardMargins" id="dashboardDeclineTotal">
		            <asp:Label ID="shiftHours" runat="server" Text="Email" CssClass="input-header">Shift Hours:</asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="dashboardMargins" id="dashboardDeclineNum">
                    <asp:Label ID="shiftTotal" runat="server" CssClass="input-header"></asp:Label>
                </div>
	        </div>
        </div>-->
        <div class="row">
            <div class="col-md-12">
                <div class="dashboardMargins" id="dashboardDeclineButtons" style="padding-left: 70px;">
			        <asp:Button ID="Button1" runat="server" Text="Confirm" OnClick="DeclineButton" CssClass="mybutton"/>
			        <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="onCancelDecline" CssClass="mybutton"/>
                </div>
            </div>
        </div>
     </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="Panel3" 
		BackgroundCssClass="modalBackground" TargetControlID="btn" X="-1" Y="-1" 
		RepositionMode="RepositionOnWindowResizeAndScroll"></ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="Panel3"  CssClass="dashboardMargins" runat="server">
        <div class="dashboardMargins" id="dashboardCancel">
            <asp:Label runat="server" CssClass="section-header">Shift Cancellation</asp:Label>
        </div>
        <div class="row" style="padding-bottom:10px">
            <div class="col-md-12">
	            <div class="dashboardMargins" id="dashboardCancelTxt" style="padding-left: 90px;">
		            <asp:Label ID="Label8" runat="server" Text="Email" CssClass="input-header">Cancel your shift?</asp:Label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="dashboardMargins" id="dashboardCancelButtons" style="padding-left: 100px;">
			        <asp:Button ID="Button3" runat="server" Text="Confirm" OnClick="DeclineButton" CssClass="mybutton"/>
			        <asp:Button ID="Button4" runat="server" Text="Cancel" OnClick="onCancelUpcoming" CssClass="mybutton"/>
                </div>
            </div>
        </div>
     </asp:Panel>
	<!-- Hidden button to link to modalpopup -->
	<div style="visibility:hidden">
		<asp:Button id="btn" runat="server"/>
	</div>


</asp:Content>
