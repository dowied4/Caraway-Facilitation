<%@ Page Title="Manage Accounts" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="_395project.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <p class="error-text">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
	<div class="form-group">
		<div class="col-md-offset-2 col-md-10">
        <asp:Button runat="server" OnClick="Clear_Click" Text="Clear" CssClass="mybutton" />
        </div>
    </div>
    <div class="form-horizontal">
        <hr />
		<h4>Create a new account</h4>
        <asp:ValidationSummary runat="server" CssClass="error-text" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 input-header">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="inputfields" TextMode="Email" />
            </div>
        </div>
		<div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="mybutton" />
            </div>
        </div>
		<hr />
		        <h4>Add Facilitator</h4>
		<div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FacilitatorEmail" CssClass="col-md-2 input-header">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FacilitatorEmail" CssClass="inputfields" TextMode="Email" />
            </div>
        </div>
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
                <asp:Button runat="server" OnClick="AddFacilitator_Click" Text="Add Facilitator" CssClass="mybutton" />
            </div>
        </div>

		<hr />
	        <h4>Add Child</h4>
		<div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ChildEmail" CssClass="col-md-2 input-header">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ChildEmail" CssClass="inputfields" TextMode="Email" />
            </div>
        </div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="ChildFirst" CssClass="col-md-4 input-header">Child First Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="ChildFirst" CssClass="inputfields" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="ChildLast" CssClass="col-md-4 input-header">Child Last Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="ChildLast" CssClass="inputfields" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="Class" CssClass="col-md-2 input-header">Class</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="Class" CssClass="inputfields" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="Grade" CssClass="col-md-2 input-header">Grade</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="Grade" CssClass="inputfields" />
			</div>
		</div>
		<div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="AddChild_Click" Text="Add Child" CssClass="mybutton" />
            </div>
        </div>
    </div>
</asp:Content>

