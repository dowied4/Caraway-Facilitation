<%@ Page Title="Request Facilitation Absence" Language="C#" MasterPageFile="/Master/Facilitator.Master" AutoEventWireup="true" CodeBehind="FacilitatorAbsence.aspx.cs" Inherits="_395project.Pages.FacilitatorAbsence" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href="../Content/dashboard.css" rel="stylesheet" />

    <h2 class="page-title"><%: Title %></h2>

    <p class="error-text">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <hr />
    <div class="form-horizontal">
        <asp:ValidationSummary runat="server" CssClass="error-text" />

        <!-- Time From -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" ID="fromLabel" CssClass="input-header">From</asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:DropDownList runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="FromYear_Change" ID="fromYear" CssClass="form-control" >
                        <asp:ListItem Text="Pick a year" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                        <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                        <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="FromMonth_Change" ID="fromMonth" CssClass="form-control" >
                        <asp:ListItem Text="Pick a month" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" Width="300px" AutoPostBack="true" ID="fromDay" CssClass="form-control" >
                        <asp:ListItem Text="Pick a day" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
			</div>
		</div>

        <!-- Time To -->
        <div class="thing" style="padding-top: 30px;">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" CssClass="input-header" ID="toLabel">To</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-3">
                    <asp:DropDownList runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ToYear_Change" ID="toYear" CssClass="form-control" >
                        <asp:ListItem Text="Pick a year" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                        <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                        <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ToMonth_Change" ID="toMonth" CssClass="form-control" >
                        <asp:ListItem Text="Pick a month" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" Width="300px" AutoPostBack="true" ID="toDay" CssClass="form-control" >
                        <asp:ListItem Text="Pick a day" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
			</div>
		</div>

        <!-- REQUEST: Last Name -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="Reason" CssClass="input-header">Reason</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="Reason" CssClass="form-control" />
                </div>
			</div>
		</div>

        <!-- Submit Button -->
		<div class="thing" style="padding-top: 30px;">
            <div class="row">
                <div class="col-md-6">
                    <!-- Click -->
                    <asp:Button runat="server" Text="Submit" CssClass="mybutton" OnClick="Submit_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
