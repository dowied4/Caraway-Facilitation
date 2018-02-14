<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="_395project.Pages.AccountList" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href="/Content/dashboard.css" rel="stylesheet" />
    <h1 class="page-header">Account List</h1>
	<asp:TextBox ID="SearchBox" runat="server"></asp:TextBox>
	<asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="Search_Click" />
	<asp:Button ID="Button1" runat="server" Text="Clear" OnClick="Page_Load" />
    <div id="df" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="myGridClass" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="Email" HeaderText="Email"  ItemStyle-Width="400px"/>
                <asp:BoundField DataField="NumFacilitators" HeaderText="Number of Facilitators" ItemStyle-Width="108px" NullDisplayText="0" />
                <asp:BoundField DataField="NumChildren" HeaderText="Number of Children" NullDisplayText="0" />
                <asp:BoundField DataField="MonthlyHours" HeaderText="Hours This Month" ItemStyle-Width="128px" NullDisplayText="0" />
				<asp:BoundField DataField="YearlyHours" HeaderText="Hours This Year" ItemStyle-Width="128px" NullDisplayText="0" />
            </Columns>
        </asp:GridView> 
    </div>

</asp:Content>

