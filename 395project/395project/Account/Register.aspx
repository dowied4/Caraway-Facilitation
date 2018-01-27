<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="_395project.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="Facilitator1First" CssClass="col-md-2 control-label">Facilitator 1: First Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="Facilitator1First" CssClass="form-control" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="Facilitator1Last" CssClass="col-md-2 control-label">Facilitator 1: Last Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="Facilitator1Last" CssClass="form-control" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="Facilitator2First" CssClass="col-md-2 control-label">Facilitator 2: First Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="Facilitator2First" CssClass="form-control" />
			</div>
		</div>
		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="Facilitator2Last" CssClass="col-md-2 control-label">Facilitator 2: Last Name</asp:Label>
			<div class="col-md-10">
                <asp:TextBox runat="server" ID="Facilitator2Last" CssClass="form-control" />
			</div>
		</div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>

