<%@ Page Title="Give Absence" Language="C#" MasterPageFile="/Master/Main.Master" AutoEventWireup="true" CodeBehind="GiveAbsence.aspx.cs" Inherits="_395project.dash.Admin.GiveAbsence" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <link href="../../Content/dashboard.css" rel="stylesheet" />
   <h2 class="page-title"><%: Title %></h2>

    <p class="error-text">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <hr />
    <div class="form-horizontal">
        <asp:ValidationSummary runat="server" CssClass="error-text" />

        <!-- Email -->
        <div class="thing" style ="padding-bottom: 30px;">
            <div class="row">
                <div class="col-md-3">
			        <asp:Label runat="server" CssClass="input-header">Email</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="Email" CssClass="inputfields" />
                </div>
			</div>
		</div>

        <!-- From-->
        <div class="thing" style ="padding-bottom: 30px;">
            <div class="row">
                <div class="col-md-3">
			        <asp:Label runat="server" CssClass="input-header">From</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="fromDate" CssClass="inputfields" />
                </div>
			</div>
		</div>

        <!-- To -->
        <div class="thing">
            <div class="row">
                <div class="col-md-3">
			        <asp:Label runat="server" CssClass="input-header">To</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="toDate" CssClass="inputfields" />
                </div>
			</div>
		</div>
        
        <!-- Reason-->
        <div class="thing" style ="padding-bottom: 30px;">
            <div class="row">
                <div class="col-md-3">
			        <asp:Label runat="server" CssClass="input-header">Reason</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="Reason" CssClass="inputfields" />
                </div>
			</div>
		</div>

        <!-- Submit Button -->
		<div class="thing" style="padding-top: 30px;">
            <div class="row">
                <div style="float: left; padding-left: 15px">
                    <asp:Button runat="server" Text="Submit" CssClass="mybutton"/>
                </div>
                <div style="float: left; padding-left: 10px">
                    <asp:Button runat="server" Text="Reset" CssClass="mybutton"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
