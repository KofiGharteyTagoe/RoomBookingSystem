<%@ Page Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="AddBooking.aspx.cs" Inherits="LoginAppmain.AddBooking" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

            <div class="container">
            <div id="PopupArea">

                <asp:Label ID="confirmation" runat="server" Text="Are you sure you would like to delete this booking?" Width="300px" />
                <br />
                <asp:Button ID="okay" runat="server" OnClick="okayButton" Text="Okay" />

            </div>


    <div class="Pagename">
        <asp:Label ID="lblHeader" runat="server" Text="Book A Room"></asp:Label>

    </div>



    <div class="SearchAccDiv2">

        <asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center">
            <asp:Button CssClass="ButtonToggle" ID="btnRoom" runat="server" Text="Book A Room" OnClick="btnRoom_Click" />
            <asp:Button CssClass="ButtonToggle" ID="btnPc" runat="server" Text="Book a PC" OnClick="btnPc_Click" />
            <br />
            <hr />
        </asp:Panel>
        <div class="PanelDiv">
            <br />
            <table class="AddBookingTable">
                <tr>
                    <td class="auto-style4">Select Date: </td>
                    <td>

                        <asp:TextBox AutoPostBack="True" ID="txtDatePicker6M" runat="server" OnTextChanged="txtDatePicker_TextChanged" CssClass="searchText"></asp:TextBox>
                        <asp:TextBox AutoPostBack="True" ID="txtDatePicker5D" runat="server" OnTextChanged="txtDatePicker_TextChanged" CssClass="searchText"></asp:TextBox>
                    </td>
                    </td>              

                </tr>
                <tr>
                    <td class="auto-style4">Select Room: </td>
                    <td>
                        <asp:DropDownList ID="ddlChooseRoom" runat="server" OnSelectedIndexChanged="ddlChooseRoom_SelectedIndexChanged" AutoPostBack="True" CssClass="searchText" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Number of Available PC&#39;s: </td>
                    <td>
                        <asp:Label ID="lblNumberPCs2" runat="server" Text="" CssClass="searchText" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Black &amp; White Printer: </td>
                    <td>
                        <asp:Label ID="lblBlackPrinter2" runat="server" CssClass="searchText" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Colour Printer: </td>
                    <td>
                        <asp:Label ID="lblColourPrinter2" runat="server" Text="" CssClass="searchText" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Start Time: </td>
                    <td>
                        <asp:DropDownList AutoPostBack="True" ID="ddlStartTime" runat="server" OnSelectedIndexChanged="ddlStartTime_SelectedIndexChanged" CssClass="searchText" Width="174px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">End Time: </td>
                    <td>
                        <asp:DropDownList ID="ddlEndTime" runat="server" AutoPostBack="True" CssClass="searchText" Width="173px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="lblPcBooking" runat="server" Text="PCs Booking: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPcBookings" runat="server" AutoPostBack="True" CssClass="searchText" Width="173px" Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Description: </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="searchText" Height="90px" Width="269px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="lblRecurring" runat="server" Text="Recurring Booking: "></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkRecurring" runat="server" Height="25px" Text=" " AutoPostBack="True" OnCheckedChanged="chkRecurring_CheckedChanged" />

                        <asp:TextBox AutoPostBack="True" ID="txtRecurring" runat="server" CssClass="searchText" Width="189px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">

                        <asp:Button CssClass="Button" ID="btnConfirmBooking" runat="server" OnClick="Button1_Click" Text="Confirm Booking" OnClientClick="return confirm('Are you sure you wish make this room booking?');" Style="margin-top: 5px; text-align: center; margin-left: 50%" />
                    </td>
                </tr>
            </table>
        </div>

    </div>

</asp:Content>
