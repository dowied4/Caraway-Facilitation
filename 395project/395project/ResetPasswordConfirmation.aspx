﻿<%@ Page Title="Password Changed" Language="C#" MasterPageFile="/Caraway.Master" AutoEventWireup="true" CodeBehind="ResetPasswordConfirmation.aspx.cs" Inherits="_395project.Account.ResetPasswordConfirmation" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <div>
        <p>Your password has been changed. Click <asp:HyperLink ID="login" runat="server" NavigateUrl="/Login">here</asp:HyperLink> to login </p>
    </div>
</asp:Content>