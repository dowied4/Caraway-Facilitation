<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="_395project.dash.Admin.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="/Scripts/fusioncharts/fusioncharts.js"></script>
    <link href="/Content/dashboard.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/fusioncharts/fusioncharts.js"></script>
    <h2 class="page-title"><%: Title %></h2>
    <h2 id="head" runat="server">Dashboard</h2>
    <hr />

	<div class="col-md-offset-2 col-md-3">
		<div class="dashboardMargins" id="dashboardFacilitatorsLabel">
			<asp:Label runat="server" CssClass="dash-header">Current Absences</asp:Label>
		</div>
		<div class="row" id="userFacil" runat="server">
			<div id="dashboardGridFacilitator" class="dashboardMargins">
				<div class="rounded_corners" style="width: 500px">
					<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" CssClass="myGridView" style="width: 500px" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
						<HeaderStyle CssClass="HeaderStyle" />
						<FooterStyle CssClass="FooterStyle" />
						<RowStyle CssClass="RowStyle" />
						<AlternatingRowStyle CssClass="AlternatingRowStyle" />
					</asp:GridView>
				</div>
			</div>
		</div>
	</div>

    

 </asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>

