<%@ Page Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="ViewTheUser.aspx.cs" Inherits="LoginAppmain.ViewTheUser" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<!DOCTYPE html>

    <style type="text/css">

        .auto-style3 {
            text-align: center;
            text-decoration: underline;
        }
        .auto-style5 {
            font-weight: bold;
            color: #333399;
        }
        .auto-style6 {
            width: 132px;
            text-align: right;
            font-weight: bold;
            color: #870FFF;
        }

        .auto-style7 {
            width: 226px;
        }

    </style>

                            <div class="Pagename">

                USER INFORMATION
            </div>

        <div class="container">

            

            <div class="content">

                <div class="AccDiv2">
                    
                
                    <table class="userInfo1">
                        <tr>
                            <td class="auto-style6">User ID:</td>
                            <td class="auto-style7">
                                <asp:Label ID="lbl_userID" runat="server" Text="User ID Goes Here" CssClass="auto-style5"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style6">User Name:</td>
                            <td class="auto-style7"><asp:Label ID="lbl_UserName" runat="server" Text="User Name Goes Here" CssClass="auto-style5"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">Full Name:</td>
                            <td class="auto-style7"><asp:Label ID="lbl_Fullnamelbl" runat="server" Text="Fullname Goes Here" CssClass="auto-style5"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">Email:</td>
                            <td class="auto-style7"><asp:Label ID="lbl_email" runat="server" Text="Email Goes Here" CssClass="auto-style5"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">Date Created:</td>
                            <td class="auto-style7"><asp:Label ID="lbl_createDate" runat="server" Text="DateCreated Goes Here" CssClass="auto-style5"></asp:Label></td>
                        </tr>

                    </table>
                    

                     <table class="userInfo2">
                        <tr>
                            <td class="auto-style6">Is Locked Out?:</td>
                            <td class="auto-style7"><asp:Label ID="lbl_lockedout" runat="server" Text="Is Lockedout Goes Here" CssClass="auto-style5"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">Access Level:</td>
                            <td class="auto-style7"><asp:Label ID="lbl_AccessLevel" runat="server" Text="Access Level Goes Here" CssClass="auto-style5"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">Contact Number:</td>
                            <td class="auto-style7"><asp:Label ID="lbl_contactnumber" runat="server" Text="Contact Number Goes Here" CssClass="auto-style5"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">Is Online?:</td>
                            <td class="auto-style7"><asp:Label ID="lbl_isApproved" runat="server" Text="Is Approved Goes Here" CssClass="auto-style5"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">Last Login Date:</td>
                            <td class="auto-style7"><asp:Label ID="lbl_lastlogin" runat="server" Text="Last Login Goes Here" CssClass="auto-style5"></asp:Label></td>
                        </tr>
                                     <tr>
                            <td class="auto-style6">Expiry Date:</td>
                            <td class="auto-style7"><asp:Label ID="lbl_expiryDate" runat="server" Text="Expiry Date Goes Here" CssClass="auto-style5"></asp:Label></td>
                        </tr>
                    </table>

                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <hr style="width: 930px" />

                    <h1 class="auto-style3"> User Bookings </h1>
       
                    <div id="PopupArea">

        <asp:Label ID="confirmation" runat="server" Text="Are you sure you would like to delete this booking?"/>
	    <asp:Button ID="yes" runat="server" OnClick="yesButton" Text="Yes"/>
        <asp:Button ID="no" runat="server" OnClick="noButton" Text="Cancel"/>

        </div>


                    <asp:GridView CssClass="GridviewTable" ID="grd_getEventGridView" runat="server" AutoGenerateColumns="false" OnRowCommand="grd_getEventGridView_RowCommand" >
                    <Columns>
                        <asp:ButtonField ButtonType="Button" Text="Delete" ControlStyle-Font-Bold="true" CommandName="buttonDelete"/>
                        <asp:BoundField DataField="EventID" HeaderText="Event ID" ItemStyle-Width="70" />
                        <asp:BoundField DataField="Room" HeaderText="Room" ItemStyle-Width="70" />
                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="70" />
                         <asp:BoundField DataField="StartTime" HeaderText="Start Time" ItemStyle-Width="70" />
                        <asp:BoundField DataField="EndTime" HeaderText="End Time" ItemStyle-Width="70" />
                        <asp:BoundField DataField="PcBooking" HeaderText="PC Bookings" ItemStyle-Width="70" />
                         <asp:BoundField DataField="PcsBooked" HeaderText="PC Booked" ItemStyle-Width="70" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
   
                    </Columns>
                    
                    </asp:GridView>

                
                </div>

            </div>
        </div>

</html>
    </asp:Content>