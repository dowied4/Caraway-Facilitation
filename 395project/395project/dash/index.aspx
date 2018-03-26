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
                            <asp:LinkButton ID="CancelButton" runat="server" Text="Cancel" OnClick="DeclineButton" />
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure you want to cancel your shift?" TargetControlID="CancelButton" ConfirmOnFormSubmit="True" />
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
                            <asp:LinkButton ID="declineButtons" runat="server" Text="Decline" OnClick="DeclineButton" />
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure you want to decline your shift?" TargetControlID="declineButtons" ConfirmOnFormSubmit="True" />
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
	<asp:Panel ID="Panel1" runat ="server">
		<div class="row" style="padding-bottom:10px;" >
			<asp:Label ID="Label5" runat="server" Text="Email" CssClass="input-header"></asp:Label>
            <asp:TextBox ID="EmailTextbox" AutoPostBack="true" runat="server" CssClass="signupDropDown" OnTextChanged="EmailTextbox_TextChanged"></asp:TextBox>
		</div>
		<div class="row" style="padding-bottom:10px;">
			<asp:Label ID="Label6" runat="server" Text="Facilitator" CssClass="input-header"></asp:Label>
			 <asp:DropDownList ID="FacilitatorDropDown" runat="server" DataSourceID="Facilitators" DataTextField="FullName" DataValueField="FullName" CssClass="signupDropDown"></asp:DropDownList>
            <asp:SqlDataSource ID="Facilitators" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT FullName FROM Facilitators WHERE (Id = @SelectedUser)">
                <SelectParameters>
	                <asp:Parameter DefaultValue="Anonymous" Name="SelectedUser" />
                </SelectParameters>
            </asp:SqlDataSource>
		</div>
			<asp:Button ID="okButton" runat="server" Text="Confirm" OnClick="onConfirm" CssClass="mybutton"/>
			<asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick="onCancel" CssClass="mybutton"/>
     </asp:Panel>
	<!-- Hidden button to link to modalpopup -->
	<div style="visibility:hidden">
		<asp:Button id="btn" runat="server"/>
	</div>


</asp:Content>
