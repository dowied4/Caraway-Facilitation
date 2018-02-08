<%@ Page Title="Request Additional Facilitators" Language="C#" MasterPageFile="/Master/Facilitator.Master" AutoEventWireup="true" CodeBehind="RequestFacilitator.aspx.cs" Inherits="_395project.Pages.RequestFacilitator" %>

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
			        <asp:Label runat="server" AssociatedControlID="FacilitatorFirst" CssClass="input-header">Facilitator First Name</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="FacilitatorFirst" CssClass="inputfields" />
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
                    <asp:TextBox runat="server" ID="FacilitatorLast" CssClass="inputfields" />
                </div>
			</div>
		</div>

        <!-- REQUEST: Email -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="FacilitatorEmail" CssClass="input-header">Email</asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="FacilitatorEmail" CssClass="inputfields" TextMode="Email" />
                </div>
            </div>
        </div>

        <!-- ADD FACILITATOR: Submit Button -->
		<div class="thing" style="padding-top: 30px;">
            <div class="row">
                <div style="float: left; padding-left: 15px">
                    <!-- OnClick="AddFacilitator_Click" -->
                    <asp:Button runat="server" Text="Submit" CssClass="mybutton" />
                </div>
                <div style="float: left; padding-left: 10px">
                    <!-- OnClick="AddFacilitator_Click" -->
                    <asp:Button runat="server" Text="Reset" CssClass="mybutton" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
