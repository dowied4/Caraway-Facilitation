<%@ Page Title="Request Facilitation Absence" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="FacilitatorAbsence.aspx.cs" Inherits="_395project.Pages.FacilitatorAbsence" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href="../Content/dashboard.css" rel="stylesheet" />

    <h2 class="page-title"><%: Title %></h2>

    <p class="error-text">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <hr />
    <div class="form-horizontal">
        <asp:ValidationSummary runat="server" CssClass="error-text" />

        <!-- Time From -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="timeFrom" CssClass="input-header">From (MM/DD/YY)</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="timeFrom" CssClass="inputfields" />
                </div>
			</div>
		</div>

        <!-- Time To -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="timeTo" CssClass="input-header">To (MM/DD/YY)</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="timeTo" CssClass="inputfields" />
                </div>
			</div>
		</div>

        <!-- Submit Button -->
		<div class="thing" style="padding-top: 30px;">
            <div class="row">
                <div class="col-md-6">
                    <!-- Click -->
                    <asp:Button runat="server" Text="Submit" CssClass="mybutton" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
