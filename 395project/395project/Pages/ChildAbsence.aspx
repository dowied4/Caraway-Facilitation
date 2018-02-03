<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="ChildAbsence.aspx.cs" Inherits="_395project.Pages.ChildAbsence" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <!-- When we get into integrating js and server side, we will have a drop down menu where 
        they can choose how many children and the form will print out that many number of forms
        -->

        <h1>Request Child Absence</h1>
    <br />
                    <div class="row">
                                <div class="col-lg-4">
                                    <form role="form">
                                        <div class="form-group">
                                            <p class="help-block">Child's First Name</p>
                                            <input class="form-control" id="childfirstName">
                                            <br />
                                            <p class="help-block">Child's Last Name</p>
                                            <input class="form-control" id="childlastName">
                                            <br />
                                        </div>
                                        <button type="submit" class="btn btn-default">Submit</button>
                                        </form>
                                        </div>
                                        
                        </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
