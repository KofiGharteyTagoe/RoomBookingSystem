<%@ Page Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="Modify Booking.aspx.cs" Inherits="LoginAppmain.Modify_Booking" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <!DOCTYPE html>

    <style type="text/css">
        .auto-style3 {
            width: 100%;
        }
    </style>

    <div class="Pagename">
        MODIFY BOOKING
    </div>

    <div class="container">



        <div class="content">
            <div class="AccDiv2">



                <div>
                    <table id="PopupArea">
                        <tr>
                            <td>
                                <asp:Label ID="confirmation" runat="server"> Are you sure you would like to delete this booking?</asp:Label>
                                <asp:Button ID="yes" runat="server" OnClick="yesButton" Text="Yes" />
                                <asp:Button ID="no" runat="server" OnClick="noButton" Text="Cancel" />
                            </td>
                        </tr>
                    </table>


                </div>

                <asp:GridView CssClass="GridviewTable" ID="BookingsTable" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"
                    runat="server" AutoGenerateColumns="False" OnRowCommand="BookingsTable_RowCommand">
                    <Columns>

                        <asp:BoundField DataField="RoomID" HeaderText="Room ID" ItemStyle-Width="70" />
                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="90" />
                        <asp:BoundField DataField="StartTime" HeaderText="Start Time" ItemStyle-Width="70" />
                        <asp:BoundField DataField="EndTime" HeaderText="End Time" ItemStyle-Width="70" />
                        <asp:BoundField DataField="Information" HeaderText="Information" ItemStyle-Width="140" />
                        <asp:BoundField DataField="EventID" HeaderText="EventID" ItemStyle-Width="140" />
                        <asp:ButtonField ButtonType="Button" Text="Edit  " ControlStyle-Font-Bold="true" CommandName="buttonEdit" />
                        <asp:ButtonField ButtonType="Button" Text="Delete" ControlStyle-Font-Bold="true" CommandName="buttonDelete" />
                    </Columns>
                </asp:GridView>
            </div>


        </div>
    </div>
</asp:Content>
