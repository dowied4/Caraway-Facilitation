<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="_395project.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <title>Caraway Login</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Style.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

</head>

<body>
    <form id="form2" runat="server">
        <!-- Login Container -->
        <div class="login-container">
            <div class="row">
                <label class ="loginheader"><b>LOG IN</b></label>
            </div>
            <!-- Email Header -->
            <div class="row">
                <div class="col-lg-4">
                    <label class="inputheader"><b>Email</b></label>
                </div>
            </div>
            <!-- Email Input Field -->
            <div class="row">
                <div class="col-lg-4">
                    <input runat="server" id="Email" type="text" placeholder="Enter Email" class="inputfields" required/>
                </div>
            </div>
            <!-- Password Header -->
            <div class="row">
                <div class="col-lg-4">
                    <label class="inputheader"><b>Password</b></label>
                </div>
            </div>
            <!-- Password Input Field -->
            <div class="row">
                <div class="col-lg-4">
                    <input type="text" placeholder="Enter Password" id="Password" class="inputfields" required/>
                </div>
            </div>
            <!-- Login Button-->
            <div class="row">
                <div class="col-lg-5">
                    <button type="submit" class="loginbtn"><span>Login</span></button>
                </div>
                <!-- Remember me box -->
                <div class="col-lg-7">
                    <label class="remember-me">
                        <input type="checkbox" checked="checked"/> Remember me
                    </label>
                </div>
            </div>
            <!-- Forgot Password Link -->
            <div class ="row">
                <div class="col-lg-8">
                    <a class="forgot-pwd" href="#">Forgot password?</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
