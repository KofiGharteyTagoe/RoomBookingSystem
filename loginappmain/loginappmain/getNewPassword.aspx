<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getNewPassword.aspx.cs" Inherits="LoginAppmain.getNewPassword" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <style type="text/css">
        body {
            font: 13px/20px 'Lucida Grande', Tahoma, Verdana, sans-serif;
            color: #404040;
            background: #ADADAD url('purple-shade-abstract-hd-wallpaper-1920x1200-18254.png');
            height: 796px;
        }

        .maindiv {
            position: fixed;
            width: 100%;
            height: 100%;
        }
        .auto-style1 {
            width: 64%;
            height: 189px;
            margin-bottom: 0px;
            border-style: none;
            border-color: inherit;
            border-width: thick;
            position: relative;
            margin: 12% auto;
            padding: 20px 20px 20px;
            width: 588px;
            background: #FAFAFA;
            border-radius: 3px;
            box-shadow: inset;
            height: 180px;
            top: 30px;
            left: -110px;
            float: left;
            margin-left:35%;
            margin-top:10%;
        }
        .auto-style3 {
            text-align: center;
            font-size: large;
            height: 55px;
        }
        .auto-style8 {
            width: 300px;
            height: 55px;
        }
        .auto-style11 {
            text-align: left;
            width: 300px;
            height: 55px;
        }
        .auto-style14 {
            width: 332px;
            text-align: right;
            height: 55px;
        }
        .auto-style15 {
            width: 332px;
            text-align: right;
            height: 55px;
            font-size: large;
        }

        .Button1 {
            width: 303px;
            height: 35px;
            cursor: pointer;
            border-color: aqua;
            border-radius: 3px;
            text-align: center;
        
        }
        .auto-style17 {
            text-align: right;
        }
    </style>
</head>
<body >

    <form id="form1" runat="server">


    <div>
                        <a href="HomePage.aspx">
&nbsp;<asp:Image ID="Image1" runat="server" Height="53px" ImageUrl="~/logo.png" Width="314px" style="text-align: left" />
                            </a>
    </div>
    <hr />

    <div class="maindiv">

        <br />
        <table class="auto-style1">
            <tr>
                <td colspan="2">
                    <h3 class="auto-style17">Remember your password? <a href="LoginPage.aspx" target="_blank">Login</a></h3>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h3 style="text-decoration: underline; text-align: center">Enter username or email address to request a new password</h3>
                </td>
            </tr>
            <tr>
                <td class="auto-style15">Enter Username:</td>
                <td style="text-align: left" class="auto-style8">
                    <asp:TextBox ID="txt_username" runat="server" Height="25px" Width="218px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" colspan="2"><strong>OR</strong></td>
            </tr>
            <tr>
                <td class="auto-style15">Enter Email Address:</td>
                <td class="auto-style11">
                    <asp:TextBox ID="txt_emailAdd" runat="server" Height="25px" Width="218px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style14">
                    <asp:Label ID="lbl_random" runat="server"></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Button ID="btn_resetPassword" CssClass="Button1" runat="server" Height="29px" Text="Request Password" Width="226px" OnClick="btn_resetPassword_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
