<%@ Page Title="Request Facilitation Absence" Language="C#" MasterPageFile="/Master/Facilitator.Master" AutoEventWireup="true" CodeBehind="FacilitatorAbsence.aspx.cs" Inherits="_395project.Pages.FacilitatorAbsence" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href="../Content/dashboard.css" rel="stylesheet" />

    <h2 class="page-title"><%: Title %></h2>

    <p class="error-text">
        <asp:Label runat="server" ID="ErrorMessages" />
    </p>
    <hr />

    <div class="form-horizontal">

        <!-- Time From -->
        <div class="thing">
            <div class="row">
                <div class="col-lg-3">
                    <asp:Label runat="server" ID="From" CssClass="input-header">From</asp:Label>
                    <div>
                        <asp:TextBox runat="server" ID="datepickerFrom" type="date" CssClass="signupDropDown"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-3">
                    <asp:Label runat="server" ID="To" CssClass="input-header">To</asp:Label>
                    <div>
                        <asp:TextBox runat="server" ID="datepickerTo" type="date" CssClass="signupDropDown"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 30px;">
                <div class="col-lg-6">
                    <asp:Label runat="server" CssClass="input-header">Reason</asp:Label>
                    <div>
                        <asp:TextBox runat="server" ID="Reason" CssClass="signupDropDown" Width="200px"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Reason" CssClass="error-text" ErrorMessage="The 'Reason' field is required." />
                </div>
            </div>

        <!-- Submit Button -->
            <div class="row" style="padding-top: 30px;">
                <div class="col-md-6">
                    <!-- Click -->
                    <asp:Button runat="server" Text="Submit" CssClass="mybutton" OnClick="Submit_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
