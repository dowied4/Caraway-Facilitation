<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login2.aspx.cs" Inherits="_395project.Account.Login2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title></title>
            <!-- Bootstrap Core CSS -->
    <link href="../Content/master.min.css" rel="stylesheet"/>


    <!-- Custom CSS -->
    <link href="../Content/sb-admin-2.css" rel="stylesheet"/>


    <!-- Custom Fonts -->
    <link href="../fonts/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>



<body>
    
            <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Please Sign In</h3>
                    </div>
                    <div class="panel-body">



                        <form role="form">
                            <fieldset>
                                
                                <div class="form-group">
                                    <input class="form-control" placeholder="E-mail" id="Email" name="email" type="email" autofocus>
                                </div>
                                <div class="form-group">
                                    <input class="form-control" placeholder="Password" id="password" name="password" type="password" value="">
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <input name="remember" id="RememberMe" type="checkbox" value="Remember Me">Remember Me
                                    </label>
                                </div>
                                <!-- Change this to a button or input when using this as a form -->
                                <a onclick="LogIn" class="btn btn-lg btn-success btn-block">Login</a>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
   

        <!-- jQuery -->
    <script src="../Scripts/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../Scripts/master.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="../Scripts/metisMenu.min.js"></script>


    <!-- Custom Theme JavaScript -->
    <script src="../Scripts/sb-admin-2.js"></script>

</body>
</html>
