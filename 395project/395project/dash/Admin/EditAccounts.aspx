<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="EditAccounts.aspx.cs" Inherits="_395project.dash.Admin.EditAccounts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <h2 id="head" runat="server">Edit Account</h2>
    <hr />
       <link href="/Content/dashboard.css" rel="stylesheet" />
   <h2 class="page-title"><%: Title %></h2>
    <p class="error-text">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <asp:ValidationSummary runat="server" CssClass="error-text" />
    <!-- CREATE ACCOUNT: Label -->

            <div class="panel-body">
                <div class="row">
                      <div class="col-lg-6">
                          <div class="row">
                              <div class="col-md-6">
                                    <h2>Add</h2>
                                  </div>
                              </div>
                             <div class="row">
                                    <div class="col-md-6">
			                            <asp:Label runat="server" AssociatedControlID="FacilitatorFirst" CssClass="input-header">First Name</asp:Label>
                                    </div>
                             </div>
			                <div class="row">
                                    <div class="col-md-6">
                                         <asp:TextBox runat="server" ID="FacilitatorFirst" CssClass="form-control" Width="250px"/>
                                     </div>
			                </div>
                            <div class="row">
                                <div class="col-md-6">
			                        <asp:Label runat="server" AssociatedControlID="FacilitatorLast" CssClass="input-header">Last Name</asp:Label>
                                </div>
                            </div>
			                <div class="row">
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="FacilitatorLast" CssClass="form-control" Width="250px" />
                                </div>
			                </div>

		                    <div class="thing" style="padding-top: 30px;">
                                 <div class="row">
                                    <div class="col-md-6">
                                        <asp:Button runat="server" OnClick="AddFacilitator_Click" Text="Add Facilitator" CssClass="mybutton" />
                                    </div>
                                </div>
                            </div>

                      </div>
                            <div class="row">
                                <div class="col-md-6" style="padding-bottom: 30px;">
                                    <h2>Remove</h2>
                               </div>
                                <div class="thing" style="padding-top: 30px; padding-bottom: 30px;">
                                    <div class="row">
                                        <div class="col-md-6">
			                                <asp:Label runat="server" CssClass="input-header" Font-Bold="true">Select Facilitator</asp:Label>
                                        </div>
                           
                                   <div class="row">
                                        <div class="col-md-6">
                                             <asp:DropDownList ID="FacilitatorDropDown" runat="server" DataSourceID="Facilitators" DataTextField="FullName" DataValueField="FullName" CssClass="form-control" Width="300px"></asp:DropDownList>
                                             <asp:SqlDataSource ID="Facilitators" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT FullName FROM Facilitators WHERE (Id = @CurrentUser)">
                                                
                                            </asp:SqlDataSource>
                                            </div>
                                       <div class="row">
                                           <div class="col-md-6" style="margin-top: 135px;">
                                               
                                                <asp:Button runat="server" OnClick="RemoveFacilitator" Text="Remove Facilitator" CssClass="mybutton" />
                                          </div>
                                       </div>
                                   </div>
                                </div>
                                    
                           </div>
		                       
                         </div>
                    </div>
                </div>
        
		<hr />



</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
