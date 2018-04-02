<%@ Page Title="Notifications" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="_395project.dash.Admin.Notifications" %>
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

    <div id="container" runat="server">
        <asp:PlaceHolder id="alerts" runat="server" />
    </div>

    <asp:Panel runat="server" ID="noNotifications">
        <div class="dashboardMargins" id="notiNothing" style="padding-top:100px;">
            <asp:Label runat="server" CssClass="noti-header">Nothing to annoy you with :)</asp:Label>
        </div>
    </asp:Panel>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <ajaxToolkit:ModalPopupExtender ID="ConfirmPopupExtender" runat="server" PopupControlID="facilitatorConfirm" 
        BackgroundCssClass="modalBackground" TargetControlID="btn" X="-1" Y="-1" 
		RepositionMode="RepositionOnWindowResizeAndScroll"></ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="facilitatorConfirm"  CssClass="dashboardMargins" runat="server">
        <div class="dashboardMargins" id="notiAdd">
            <asp:Label runat="server" CssClass="section-header">Facilitator Addition</asp:Label>
        </div>
        <div class="row" style="padding-bottom:10px">
            <div class="col-md-12">
	            <div class="dashboardMargins" id="notiConfirmTxt">
		            <asp:Label ID="infoLabel" runat="server" CssClass="input-header"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="dashboardMargins" id="notiFacilButtons" style="padding-left: 100px;">
			        <asp:Button ID="facilConfirmButton" runat="server" Text="Confirm" OnClick="Confirm_Click" CssClass="mybutton"/>
			        <asp:Button ID="facilCancelButton" runat="server" Text="Cancel" OnClick="cancelFacilitator" CssClass="mybutton"/>
                </div>
            </div>
        </div>
     </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="AbsencePopupExtender" runat="server" PopupControlID="absenceConfirmPanel" 
        BackgroundCssClass="modalBackground" TargetControlID="btn" X="-1" Y="-1" 
		RepositionMode="RepositionOnWindowResizeAndScroll"></ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="absenceConfirmPanel"  CssClass="dashboardMargins" runat="server">
        <div class="dashboardMargins" id="absenceAdd">
            <asp:Label runat="server" CssClass="section-header">Grant Absence</asp:Label>
        </div>
        <div class="row" style="padding-bottom:10px">
            <div class="col-md-12">
	            <div class="dashboardMargins" id="absenceConfirmTxt">
		            <asp:Label ID="infoLabel2" runat="server" CssClass="input-header"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="dashboardMargins" id="notiAbsButtons" style="padding-left: 100px;">
			        <asp:Button ID="confirmAbsence" runat="server" Text="Confirm" OnClick="Confirm_Click" CssClass="mybutton"/>
			        <asp:Button ID="cancelAbsence" runat="server" Text="Cancel" OnClick="cancelAbsenceReq" CssClass="mybutton"/>
                </div>
            </div>
        </div>
     </asp:Panel>
	<!-- Hidden button to link to modalpopup -->
	<div style="visibility:hidden">
		<asp:Button id="btn" runat="server"/>
	</div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
