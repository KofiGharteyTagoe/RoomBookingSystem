<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminUser.aspx.cs" Inherits="LoginAppmain.AdminUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 288px;
        }
        .auto-style5 {
            font-size: xx-large;
            text-decoration: underline;
        }
        .auto-style7 {
            width: 288px;
            text-align: right;
        }
        .auto-style8 {
            text-align: left;
        }
        .auto-style9 {
            width: 290px;
            text-align: right;
        }
        .auto-style10 {
            width: 290px;
            height: 30px;
        }
        .auto-style11 {
            text-align: center;
        }
        .auto-style12 {
            width: 288px;
            text-align: right;
            height: 26px;
        }
        .auto-style13 {
            text-align: left;
            height: 26px;
        }
        .auto-style14 {
            text-align: left;
            height: 30px;
        }
        .auto-style15 {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

         <div class="container">
                                              <div id="PopupArea">

        <asp:Label ID="confirmation" runat="server" Text="Are you sure you would like to delete this booking?" Width="300px"/>
                                                  <br />
	    <asp:Button ID="okay" runat="server" OnClick="okayButton" Text="Okay"/>

        </div>

    <div>
        <div class="auto-style11">
            <span class="auto-style5">My Account</span><br />
            <br />
            <br />
            Show email:
            <asp:Label ID="Email_label" runat="server" Text="Email Goes Here"></asp:Label>
            <br />
            fullname: <asp:Label ID="fullname_label" runat="server" Text="Fullname goes here"></asp:Label>
            <br />
            contact number: <asp:Label ID="Contact_label" runat="server" Text="Contact Number Goes Here"></asp:Label>
            <br />
            Last Online:
            <asp:Label ID="LastOn_label" runat="server" Text="Last Online goes here"></asp:Label>
            <br />
            <div>
                <div class="auto-style15">
                    <asp:LinkButton ID="btn_logout" runat="server" OnClick="btn_logout_Click" style="font-size: large; font-weight: 700; color: #CC3300">Logout</asp:LinkButton>
                </div>
            </div>
            <div class="auto-style8">
                <br />Change Details:<br />
            </div>
        </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style9">Email Address:</td>
                <td class="auto-style8">
                    <asp:TextBox ID="txt_emailbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Contact Number:</td>
                <td class="auto-style8">
                    <asp:TextBox ID="txt_contactbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Verify Password:</td>
                <td class="auto-style8">
                    <asp:TextBox ID="txt_verifybox" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style10"></td>
                <td class="auto-style14">
                    <asp:Button ID="btn_ModifyDetails" runat="server" Text="Modify Details" Width="134px" OnClick="Button1_Click" CausesValidation="False" OnClientClick="if (!confirm('Are you sure you want to change your details?')) return false;" />
                </td>
            </tr>
        </table>



    </div>
        <p>
            Change Password:</p>
        <table class="auto-style1">
            <tr>
                <td class="auto-style7">Enter Old Password:</td>
                <td class="auto-style8">
                    <asp:TextBox ID="txt_oldpass" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style12">Enter New Password:</td>
                <td class="auto-style13">
                    <asp:TextBox ID="txt_newpass" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_newpass"
                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" ErrorMessage="Password must contain: Minimum 8 characters atleast 1 Alphabet and 1 Number" ForeColor="Red" ValidationGroup="changepass" />

                </td>
            </tr>
            <tr>
                <td class="auto-style7">Confirm New Password: </td>
                <td class="auto-style8">
                    <asp:TextBox ID="TextBox6" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="TBvalidator" runat="server" ControlToValidate="TextBox6"
                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" ErrorMessage="Password must contain: Minimum 8 characters atleast 1 Alphabet and 1 Number" ForeColor="Red" ValidationGroup="changepass" />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style8">
                    <asp:Button ID="btn_changepassword" runat="server" Text="Change Password" Width="126px" OnClick="btn_changepassword_Click" CausesValidation="False" OnClientClick="if (!confirm('Are you sure you want to change your Password?')) return false;"/>

                     </td>
            </tr>
        </table>
        <asp:Label ID="lblLogoutEx" runat="server"></asp:Label>
    </form>
</body>
</html>
