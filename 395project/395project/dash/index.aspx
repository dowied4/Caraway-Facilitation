<%@ Page Title="Dashboard" Language="C#" MasterPageFile="/Master/Facilitator.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="_395project.Pages.index" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <link href="../Content/dashboard.css" rel="stylesheet" />
   <h2 class="page-title"><%: Title %></h2>

    <hr />
    <div class="Hours">
        <div class="row" style="padding-bottom: 30px; padding-left: 46px;">
            <div class="col-md-offset-5 col-md-4">
                <asp:Label runat="server" CssClass="hours-header">Hours</asp:Label>
            </div>
        </div>

        <!-- Hours Headers Row -->
        <div class="thing">
            <div class="row">
                <div class="col-md-offset-1 col-md-3">
			        <asp:Label runat="server" CssClass="dash-header">Upcoming</asp:Label>
                </div>
                <div class="col-md-offset-1 col-md-3">
			        <asp:Label runat="server" CssClass="dash-header">Weekly Total</asp:Label>
                </div>
                <div class="col-md-offset-1 col-md-3">
			        <asp:Label runat="server" CssClass="dash-header">Monthly Total</asp:Label>
                </div>
            </div>
            <!-- Stats Labels Row -->
            <div class="row">
                <div class="col-md-offset-1 col-md-3" style="padding-top: 20px; padding-left: 48px;">
			        <asp:Label runat="server" CssClass="stats-info" >Nothing!</asp:Label>
                </div>
                <div class="col-md-offset-1 col-md-3" style="padding-top: 20px; padding-left: 76px;">
			        <asp:Label runat="server" CssClass="stats-info" ID="WeeklyHoursLabel" >0</asp:Label>
                </div>
                <div class="col-md-offset-1 col-md-3" style="padding-top: 20px; padding-left: 76px;">
			        <asp:Label runat="server" CssClass="stats-info" ID="MonthlyHoursLabel" >0</asp:Label>
                </div>
            </div>
	    </div>
    </div>
    
    <hr />

    <!-- Facilitators Label and GridView -->
    <div class="row" style="padding-top: 25px">
        <div class="col-md-6">
             <asp:Label runat="server" CssClass="dash-header">Facilitators</asp:Label>
             <div class="row" id="userFacil" runat="server">
                 <div class="col-md-6">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="myGridClass" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:BoundField DataField="FacilitatorName" HeaderText="Name"  ItemStyle-Width="400px"/>
                        </Columns>
                    </asp:GridView> 
                </div>
            </div>
        </div>

        <!-- Children Label and GridView -->
        <div class="col-md-6">
             <asp:Label runat="server" CssClass="dash-header">Children</asp:Label>
             <div class="row" id="userChildren" runat="server">
                 <div class="col-md-6">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="myGridClass" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Children"  ItemStyle-Width="400px"/>
                            <asp:BoundField DataField="Grade" HeaderText="Grade"  ItemStyle-Width="400px"/>
                            <asp:BoundField DataField="Classroom" HeaderText="Classroom"  ItemStyle-Width="400px"/>
                        </Columns>
                    </asp:GridView> 
                </div>
            </div>
        </div>
    </div>

</asp:Content>
