<%@ Page Title="" Language="C#" MasterPageFile="/Master/Facilitator.Master" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="_395project.Pages.AccountList" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href="../Content/dashboard.css" rel="stylesheet" />
    <h1 class="page-header">Account List</h1>

    <div id="df" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="myGridClass" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Username" ItemStyle-Width="400px"/>
                <asp:BoundField DataField="Email" HeaderText="Email"  ItemStyle-Width="400px"/>
                <asp:BoundField DataField="EmailConfirmed" HeaderText="Email Confirmed" ItemStyle-Width="108px"/>
                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
                <asp:BoundField DataField="AccessFailedCount" HeaderText="Access Failed Count" ItemStyle-Width="128px"/>
            </Columns>
        </asp:GridView> 
    </div>

</asp:Content>

