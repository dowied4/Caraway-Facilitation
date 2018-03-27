<%@ Page Title="Manage Accounts" Language="C#" MasterPageFile="/Master/Main.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="_395project.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <link href="../../Content/dashboard.css" rel="stylesheet" />
   <h2 class="page-title"><%: Title %></h2>
    <p class="error-text">
        <asp:Label runat="server" ID="ErrorMessages" />
    </p>

    <!-- MANAGE ACCOUNT: Clear Button -->
	<div class="thing">
		<div class="row">
            <div class="col-md-6" style="padding-top: 30px;">
                <asp:Button runat="server" Text="Clear" CssClass="mybutton"  OnClientClick="return cancel();" CausesValidation="false"/>
            </div>
        </div>
    </div>

	<!--Clears all TextBoxes, overrides the email validator -->
	<script>
        function cancel() {
			document.getElementById('<%=Email.ClientID%>').value = "";
			document.getElementById('<%=FacilitatorEmail.ClientID%>').value = "";
			document.getElementById('<%=FacilitatorFirst.ClientID%>').value = "";
			document.getElementById('<%=FacilitatorLast.ClientID%>').value = "";
			document.getElementById('<%=ChildEmail.ClientID%>').value = "";
			document.getElementById('<%=ChildFirst.ClientID%>').value = "";
			document.getElementById('<%=ChildLast.ClientID%>').value = "";
			document.getElementById('<%=Room.ClientID%>').value = "";
			document.getElementById('<%=Rank.ClientID%>').value = "";
            return false;
        }
	</script>

    <!-- CREATE ACCOUNT: Label -->
    <div class="form-horizontal">
        <hr />
		<h4>Create a new account</h4>
        <asp:ValidationSummary runat="server" CssClass="error-text" />

        <!-- CREATE ACCOUNT: Email -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="Email" CssClass="input-header">Email</asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" Width="300px" />
                </div>
            </div>
        </div>

        <!-- CREATE ACCOUNT: ROLE -->
		<div class="thing" style="padding-top: 10px;">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="UserRoleDropDown" CssClass="input-header">Role</asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
				    <asp:DropDownList ID="UserRoleDropDown" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" CssClass="form-control" Width="300px"></asp:DropDownList>
				    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Name] FROM [AspNetRoles] ORDER BY [Id]"></asp:SqlDataSource>
			    </div>
           </div>
		</div>

        <!-- CREATE ACCOUNT: Register Button -->
		<div class="thing" style="padding-top: 30px;">
            <div class="row">
                <div class="col-md-6">
                    <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="mybutton" />
                </div>
            </div>
        </div>

        <!-- ADD FACILITATOR: Label -->
		<hr />
		<h4>Add Facilitator</h4>

        <!-- ADD FACILITATOR: Email -->
		<div class="thing">
            <div class="row">
                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="FacilitatorEmail" CssClass="input-header">Email</asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="FacilitatorEmail" CssClass="form-control" Width="300px" TextMode="Email" />
                </div>
            </div>
        </div>

        <!-- ADD FACILITATOR: First Name -->
        <div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="FacilitatorFirst" CssClass="input-header">Facilitator First Name</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="FacilitatorFirst" CssClass="form-control" Width="300px" />
                </div>
			</div>
		</div>

        <!-- ADD FACILITATOR: Last Name -->
		<div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="FacilitatorLast" CssClass="input-header">Facilitator Last Name</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="FacilitatorLast" CssClass="form-control" Width="300px" />
                </div>
			</div>
		</div>

        <!-- ADD FACILITATOR: Facilitator Button -->
		<div class="thing" style="padding-top: 30px;"">
            <div class="row">
                <div class="col-md-6">
                    <asp:Button runat="server" OnClick="AddFacilitator_Click" Text="Add Facilitator" CssClass="mybutton" />
                </div>
            </div>
        </div>

        <!-- ADD CHILD: Label -->
		<hr />
	    <h4>Add Child</h4>

        <!-- ADD CHILD: Email -->
		<div class="thing">
            <div class="row">
                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="ChildEmail" CssClass="input-header">Email</asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="ChildEmail" CssClass="form-control" TextMode="Email" Width="300px" />
                </div>
            </div>
        </div>

        <!-- ADD CHILD: First Name -->
		<div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="ChildFirst" CssClass="input-header">Child First Name</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="ChildFirst" CssClass="form-control" Width="300px" />
                </div>
            </div>
		</div>

        <!-- ADD CHILD: Last Name -->
		<div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="ChildLast" CssClass="input-header">Child Last Name</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="ChildLast" CssClass="form-control" Width="300px" />
                </div>
			</div>
		</div>

        <!-- ADD CHILD: Grade -->
		<div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="Rank" CssClass="input-header">Grade</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:DropDownList runat="server" Width="300px" OnSelectedIndexChanged="ChangeClass" AutoPostBack="true" ID="Rank" CssClass="form-control" >
                        <asp:ListItem Text="K" Value="K"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                    </asp:DropDownList>
                </div>
			</div>
		</div>

        <!-- ADD CHILD: Class -->
		<div class="thing">
            <div class="row">
                <div class="col-md-6">
			        <asp:Label runat="server" AssociatedControlID="Room" CssClass="input-header">Class</asp:Label>
                </div>
            </div>
			<div class="row">
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="Room" CssClass="form-control" Width="300px" >
                        <asp:ListItem Text="Blue" Value="Blue"></asp:ListItem>
                        <asp:ListItem Text="Purple" Value="Purple"></asp:ListItem>
                        
                    </asp:DropDownList>
                </div>
			</div>
		</div>

        <!-- ADD CHILD: Child Button -->
		<div class="thing" style="padding-top: 30px;"">
            <div class="row">
                <div class="col-md-6">
                    <asp:Button runat="server" OnClick="AddChild_Click" Text="Add Child" CssClass="mybutton" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

