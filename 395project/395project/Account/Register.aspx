<%@ Page Title="Manage Accounts" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="_395project.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
	<div class="form-group">
		<div class="col-md-offset-2 col-md-10">
        <asp:Button runat="server" OnClick="Clear_Click" Text="Clear" CssClass="btn btn-default" />
        </div>
    </div>
    <div class="form-horizontal">
        <hr />
		<h4>Create a new account</h4>
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
            </div>
        </div>
		<div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
		<hr />
		        <h4>Add Facilitator</h4>
		<div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FacilitatorEmail" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FacilitatorEmail" CssClass="form-control" TextMode="Email" />
            </div>
        </div>
        <div class="form-group">
			<asp:Label runat="server" AssociatedControlID="FacilitatorFirst" CssClass="col-md-2 control-label">Facilitator First Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="FacilitatorFirst" CssClass="form-control" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="FacilitatorLast" CssClass="col-md-2 control-label">Facilitator Last Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="FacilitatorLast" CssClass="form-control" />
			</div>
		</div>
		<div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="AddFacilitator_Click" Text="Add Facilitator" CssClass="btn btn-default" />
            </div>
        </div>

		<hr />
	        <h4>Add Child</h4>
		<div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ChildEmail" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ChildEmail" CssClass="form-control" TextMode="Email" />
            </div>
        </div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="ChildFirst" CssClass="col-md-2 control-label">Child First Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="ChildFirst" CssClass="form-control" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="ChildLast" CssClass="col-md-2 control-label">Child Last Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="ChildLast" CssClass="form-control" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="Class" CssClass="col-md-2 control-label">Class</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="Class" CssClass="form-control" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="Grade" CssClass="col-md-2 control-label">Grade</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="Grade" CssClass="form-control" />
			</div>
		</div>
		<div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="AddChild_Click" Text="Add Child" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>

