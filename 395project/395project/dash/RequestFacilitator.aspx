<%@ Page Title="Request Additional Facilitators" Language="C#" MasterPageFile="/Master/Facilitator.Master" AutoEventWireup="true" CodeBehind="RequestFacilitator.aspx.cs" Inherits="_395project.Pages.RequestFacilitator" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <link href="../Content/dashboard.css" rel="stylesheet" />
   <h2 class="page-title"><%: Title %></h2>
    <hr />
    <p class="error-text">
        <asp:Label runat="server" ID="ErrorMessages" />
    </p>
    <div class="form-horizontal">

        <!-- REQUEST: First Name -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="FacilitatorFirst" CssClass="input-header">Facilitator First Name</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="FacilitatorFirst" CssClass="form-control" Width="300px" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="FacilitatorFirst" CssClass="error-text" ErrorMessage="The 'First Name' field is required." />
                </div>
			</div>
		</div>

        <!-- REQUEST: Last Name -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="FacilitatorLast" CssClass="input-header">Facilitator Last Name</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="FacilitatorLast" CssClass="form-control" Width="300px" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="FacilitatorLast" CssClass="error-text" ErrorMessage="The 'Last Name' field is required." />
                </div>
			</div>
		</div>

        <!-- ADD FACILITATOR: Submit Button -->
		<div class="thing" style="padding-top: 30px;">
            <div class="row">
                <div style="float: left; padding-left: 15px">
                    <!-- OnClick="AddFacilitator_Click" -->
                    <asp:Button runat="server" Text="Submit" CssClass="mybutton" OnClick="Submit_Click"/>
                </div>
                <div style="float: left; padding-left: 10px">
                    <!-- OnClick="AddFacilitator_Click" -->
                    <asp:Button runat="server" Text="Reset" CssClass="mybutton" OnClick="Clear_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
