<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="RequestFacilitator.aspx.cs" Inherits="_395project.Pages.RequestFacilitator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Request Additional Facilitators</h1>
    <br />
                    <div class="row">
                                <div class="col-lg-4">
                                    <form role="form">
                                        <div class="form-group">
                                            

                                            <p class="help-block">First Name</p>
                                            <input class="form-control" id="firstName">
                                            <br />
                                            <p class="help-block">Last Name</p>
                                            <input class="form-control" id="lastName">
                                            <br />
                                            <p class="help-block">Email</p>
                                            <input class="form-control" id="email">
                                           

                                        </div>
                                        <br />
                                        <button type="submit" class="btn btn-default">Submit</button>
                                        <button type="reset" class="btn btn-default">Reset</button>
                                        </form>
                                        </div>
                                        
                        </div>





</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
