<%@ Page Title="Caraway Facilitation" Language="C#" MasterPageFile="/Caraway.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_395project.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2 id="carTitle"><%: Title %></h2>

    <!-- Login layout container -->
    <div class="login-container">
        <!-- Error messages text -->
        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
            <p class="error-text">
                <asp:Literal runat="server" ID="FailureText" />
            </p>
        </asp:PlaceHolder>
        <!-- Email label -->
        <div class="row">
            <div class="col-lg-4">
                <asp:Label runat="server" AssociatedControlID="Email" CssClass="input-header">Email</asp:Label>
            </div>
        </div>
        <!-- Email input field -->
        <div class="row">
            <div class="col-lg-10">
                <asp:TextBox runat="server" ID="Email" placeholder="Enter Email" CssClass="inputfields" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="error-text" ErrorMessage="The email field is required." />
            </div>
        </div>
        <!-- Password label -->
        <div class="row">
            <div class="col-lg-4">
                <asp:Label runat="server" AssociatedControlID="Password" CssClass="input-header">Password</asp:Label>
            </div>
        </div>
        <!-- Password input field -->
        <div class="row">
            <div class="col-lg-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" placeholder="Enter Password" CssClass="inputfields" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="error-text" ErrorMessage="The password field is required." />
            </div>
        </div>
        <!-- Remember me layout -->
        <div class="row">
            <div class="col-lg-8">
                <div class="checkbox">
                    <!-- Remember me checkbox -->
                    <asp:CheckBox runat="server" ID="RememberMe" />
                    <!-- Remember me label -->
                    <asp:Label runat="server" AssociatedControlID="RememberMe" CssClass="remember-me">Remember me?</asp:Label>
                </div>
            </div>
        </div>
        <!-- Login Button -->
        <div class="row">
            <div class="col-lg-5">
                <asp:Button runat="server" OnClick="LogIn" Text="Login" class="mybutton"></asp:Button>
            </div>
        </div>
        <!-- Forgot password hyperlink -->
        <div class="row">
            <div class="col-lg-8">
                <p>
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink"  NavigateUrl="~/Account/Forgot.aspx" ViewStateMode="Disabled" CssClass="forgot-pwd">Forgot your password?</asp:HyperLink>
                </p>
            </div>
        </div>
    </div>


</asp:Content>
