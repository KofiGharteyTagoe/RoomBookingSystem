<%@ Page Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="My Account.aspx.cs" Inherits="LoginAppmain.My_Account" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <!DOCTYPE html>
    <style >
            .auto-style16 {
            text-decoration: underline;
        }

        .auto-style19 {
            width: 165px;
        }
    </style>

        <div class="container">
            <div id="PopupArea">

                <asp:Label ID="confirmation" runat="server" Text="Are you sure you would like to delete this booking?" Width="300px" />
                <br />
                <asp:Button ID="okay" runat="server" OnClick="okayButton" Text="Okay" />

            </div>

                <div class="Pagename">
                    MY ACCOUNT
                </div>

                <div class="container">

                    <hr />
                    <div class="SearchAccDiv2">

                        <h1 class="auto-style16">Change Your Details</h1>


                        <div style="height: 143px">
                            <table class="detailsTable">
                                <tr>
                                    <td class="auto-style19">Email Address:</td>
                                    <td>
                                        <asp:TextBox CssClass="TextBox" ID="txt_emailbox" runat="server" TextMode="Email" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style19">Contact Number:</td>
                                    <td>
                                        <asp:TextBox CssClass="TextBox" ID="txt_contactbox" runat="server" TextMode="Phone"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style19">Verify Password:</td>
                                    <td>
                                        <asp:TextBox CssClass="TextBox" ID="txt_verifybox" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style19"></td>
                                    <td>
                                        <asp:Button CssClass="Button" ID="btn_ModifyDetails" runat="server" Text="Modify Details" OnClick="Button1_Click" CausesValidation="False" OnClientClick="if (!confirm('Are you sure you want to change your details?')) return false;" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <h1 class="auto-style16">Change Your Password</h1>


                        <div style="height: 176px">

                            <table class="detailsTable">
                                <tr>
                                    <td>Enter Old Password:</td>
                                    <td>
                                        <asp:TextBox CssClass="TextBox" ID="txt_oldpass" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Enter New Password:</td>
                                    <td>
                                        <asp:TextBox CssClass="TextBox" ID="txt_newpass" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_newpass"
                                            ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" ErrorMessage="Password must contain: Min 8 characters atleast 1 Alphabet and 1 Number" ForeColor="Red" ValidationGroup="changepass" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>Confirm New Password: </td>
                                    <td>
                                        <asp:TextBox CssClass="TextBox" ID="TextBox6" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="TBvalidator" runat="server" ControlToValidate="TextBox6"
                                            ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" ErrorMessage="Password must contain: Min 8 characters atleast 1 Alphabet and 1 Number" ForeColor="Red" ValidationGroup="changepass" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button CssClass="Button" ID="btn_changepassword" runat="server" Text="Change Password" OnClick="btn_changepassword_Click" CausesValidation="False" OnClientClick="if (!confirm('Are you sure you want to change your Password?')) return false;" />

                                    </td>
                                </tr>
                            </table>


                            <p></p>

                        </div>


                        <p></p>
                    </div>
                </div>
        </div>

    </asp:Content>