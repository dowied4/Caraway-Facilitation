<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="_395project.Pages.AccountList" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href="/Content/dashboard.css" rel="stylesheet" />
    <h1 class="page-header">Account List</h1>
    <div class="dashboardMargins" id="alSearch" style="padding-bottom: 20px;">
	    <asp:TextBox ID="SearchBox" runat="server" CssClass="signupDropDown" placeholder="Enter Email"/>
	    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="Search_Click"  CssClass="mybutton"/>
	    <asp:Button ID="Button1" runat="server" Text="Clear" OnClick="Page_Load" CssClass="mybutton"/>
    </div>
    
    <div class="row" id="df" runat="server">
        <div id="accountListGrid" class="dashboardMargins">
            <div class="rounded_corners" style="width: 1200px">
                <asp:GridView ID="GridView1" runat="server" Width="1200px" AutoGenerateColumns="false" CssClass="myGridView" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                    <HeaderStyle CssClass="HeaderStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="NumFacilitators" HeaderText="Number of Facilitators" NullDisplayText="0" />
                        <asp:BoundField DataField="NumChildren" HeaderText="Number of Children" NullDisplayText="0" />
                        <asp:BoundField DataField="MonthlyHours" HeaderText="Hours This Month" NullDisplayText="0" />
				        <asp:BoundField DataField="YearlyHours" HeaderText="Hours This Year" NullDisplayText="0" />
                        <asp:TemplateField HeaderText ="Stats">
                            <ItemTemplate>
                                    <asp:LinkButton ID="StatLink" runat="server" Text="Stats"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText ="Edit Account">
                            <ItemTemplate>
                                <asp:LinkButton ID="EditLink" runat="server" Text="Edit" OnClick="EditButton"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
         </div>
    </div>

</asp:Content>

