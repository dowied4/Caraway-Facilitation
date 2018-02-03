<%@ Page Title="Request Facilitator Form" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RequestFacilitator.aspx.cs" Inherits="_395project.Account.RequestFacilitator" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <p class="error-text">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <div class="form-horizontal">
        <div class="form-group">
			<asp:Label runat="server" AssociatedControlID="FacilitatorFirst" CssClass="col-md-4 input-header">Facilitator First Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="FacilitatorFirst" CssClass="inputfields" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="FacilitatorLast" CssClass="col-md-4 input-header">Facilitator Last Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="FacilitatorLast" CssClass="inputfields" />
			</div>
		</div>
		<div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Request Facilitator" CssClass="mybutton" />
            </div>
        </div>
    </div>
</asp:Content>
