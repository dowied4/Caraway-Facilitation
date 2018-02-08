<%@ Page Title="Child Absence" Language="C#" MasterPageFile="~/Master/Facilitator.Master" AutoEventWireup="true" CodeBehind="ChildAbsence.aspx.cs" Inherits="_395project.Pages.ChildAbsence" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <link href="../Content/dashboard.css" rel="stylesheet" />
   <h2 class="page-title"><%: Title %></h2>
    
    <p class="error-text">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <hr />
    <div class="form-horizontal">
        <asp:ValidationSummary runat="server" CssClass="error-text" />

        <!-- REQUEST: First Name -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="ChildFirst" CssClass="input-header">Child First Name</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="ChildFirst" CssClass="inputfields" />
                </div>
			</div>
		</div>

        <!-- REQUEST: Last Name -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="ChildLast" CssClass="input-header">Child Last Name</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="ChildLast" CssClass="inputfields" />
                </div>
			</div>
		</div>

        <!-- REQUEST: Day -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="DayList" CssClass="input-header">Day</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:DropDownList id="DayList" AutoPostBack="False" runat="server" CssClass="inputfields">

                        <asp:ListItem Selected="True" Value="Monday"> Monday </asp:ListItem>
                        <asp:ListItem Value="Tuesday"> Tuesday </asp:ListItem>
                        <asp:ListItem Value="Wednesday"> Wednesday </asp:ListItem>
                        <asp:ListItem Value="Thursday"> Thursday </asp:ListItem>
                        <asp:ListItem Value="Friday"> Friday </asp:ListItem>

                    </asp:DropDownList>
                </div>
			</div>
		</div>
        
        <!-- REQUEST: Time -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="Time" CssClass="input-header">Time</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:DropDownList id="Time" AutoPostBack="False" runat="server" CssClass="inputfields">

                        <asp:ListItem Selected="True" Value="Morning"> Morning </asp:ListItem>
                        <asp:ListItem Value="Afternoon"> Afternoon </asp:ListItem>
                        <asp:ListItem Value="Afternoon"> Evening </asp:ListItem>
                        <asp:ListItem Value="Full-Day"> Full Day </asp:ListItem>

                    </asp:DropDownList>
                </div>
			</div>
		</div>

        <!-- ADD FACILITATOR: Submit Button -->
		<div class="thing" style="padding-top: 30px;">
            <div class="row">
                <div class="col-md-6">
                    <!-- OnClick="AddFacilitator_Click" -->
                    <asp:Button runat="server" Text="Submit" CssClass="mybutton" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>