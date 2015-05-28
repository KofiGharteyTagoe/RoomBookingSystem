<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="LoginAppmain.LoginPage" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Room Booking</title>

    <style type="text/css">
        body {
            font: 13px/20px 'Lucida Grande', Tahoma, Verdana, sans-serif;
            color: #404040;
            background: #ADADAD url('purple-shade-abstract-hd-wallpaper-1920x1200-18254.png');
            height: 796px;
        }

        .login-table {
            width: 45%;
            border-collapse: collapse;
            float: left;
            height: 232px;
            border-left-style: solid;
            border-left-width: 2px;
            border-right: 2px solid #C0C0C0;
            border-top-style: solid;
            border-top-width: 2px;
            border-bottom: 2px solid #C0C0C0;
            background-color: #EBEBEB;
            vertical-align: middle;
            border-radius: 5px;
        }

        #Text3 {
            height: 33px;
            width: 273px;
        }

        #PasswordTb {
            margin: 5px;
            padding: 0 10px;
            height: 33px;
            width: 273px;
            color: #404040;
            background: white;
            border: 1px solid;
            border-color: #6E6E6E #6E6E6E #6E6E6E #6E6E6E;
            border-radius: 2px;
            outline: 5px solid #eff4f7;
            -moz-outline-radius: 3px;
        }

        #Text2 {
            height: 33px;
            width: 273px;
        }

        #UsernameTb {
            margin: 5px;
            padding: 0 10px;
            width: 273px;
            height: 33px;
            color: #404040;
            background: white;
            border: 1px solid;
            border-color: #6E6E6E #6E6E6E #6E6E6E #6E6E6E;
            border-radius: 2px;
            outline: 5px solid #eff4f7;
            -moz-outline-radius: 3px;
        }

        #Submit1 {
            width: 303px;
            height: 35px;
            cursor: pointer;
            border-radius: 4px;
            text-align: center;
        }

                .Submit {
            width: 303px;
            cursor: pointer;
            border-radius: 4px;
            text-align: center;
        }

        .auto-style4 {
            border-style: none;
            border-color: inherit;
            border-width: thick;
            position: relative;
            margin: 0 auto;
            padding: 20px 20px 20px;
            width: 352px;
            background: #FAFAFA;
            border-radius: 3px;
            box-shadow: inset;
            height: 338px;
            margin-top: 12%;
            margin-bottom: 12%;
        }

        .headerdiv {
            padding: 0 10px;
        }

        .auto-style8 {
            height: 39px;
            text-align: center;
            width: 305px;
        }

        .auto-style5 {
            height: 76px;
            width: 305px;
            text-align: left;
        }

        .auto-style10 {
            height: 58px;
            width: 305px;
            text-align: left;
        }

        .auto-style11 {
            width: 305px;
        }

        .auto-style3 {
            width: 305px;
            height: 31px;
            text-align: left;
        }

        .auto-style12 {
            font-size: medium;
        }

        .maindiv {
            position: fixed;
            width: 100%;
            height: 100%;
        }
        .auto-style13 {
            width: 305px;
            height: 31px;
            text-align: left;
        }
    </style>
</head>
<body >

    <form id="form1" runat="server">

    <div>

                        <a href="HomePage.aspx">
                <asp:Image ID="Image2" runat="server" Height="53px" ImageUrl="~/logo.png" />
         </a>
    </div>
    <hr />

    <div class="maindiv">

        <table class="auto-style4">
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="Label1" runat="server" Text="Log in to Room Booking" style="text-align: center; font-size: medium; font-weight: 700"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="auto-style5"><span class="auto-style12">Username:</span><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UsernameTb" CssClass="auto-style13" ErrorMessage="* Please enter your username" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="UsernameTb" runat="server" Height="25px" Width="264px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style10"><span class="auto-style12">Password:
                    </span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PasswordTb" CssClass="auto-style13" ErrorMessage="* Please enter your password" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="PasswordTb" runat="server" Height="27px" TextMode="Password" Width="266px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style11">
                    <asp:Button CssClass="Submit" ID="LoginButton" runat="server" style="font-weight: 700; font-size: medium; text-align: center" Text="Log in" Width="292px" Height="33px" OnClick="LoginButton_Click" />
                    
                </td>
            </tr>
            <tr>
                <td class="auto-style13">Forgot your password? <a href="getNewPassword.aspx" target="_blank" style="text-align: left">Request for Password</a></td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <hr />
                    <asp:Label ID="lblSql" runat="server" style="color: #CC0000; font-size: large; text-decoration: underline"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
