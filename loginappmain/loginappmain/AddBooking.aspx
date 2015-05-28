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
                    <td class="auto-style35">Select Date: </td>
                    <td>

                        <asp:TextBox AutoPostBack="True" ID="txtDatePicker6M" runat="server" OnTextChanged="txtDatePicker_TextChanged" CssClass="searchText"></asp:TextBox>
                        <asp:TextBox AutoPostBack="True" ID="txtDatePicker5D" runat="server" OnTextChanged="txtDatePicker_TextChanged" CssClass="searchText"></asp:TextBox>
                    </td>
                    </td>              

                </tr>
                <tr>
                    <td class="auto-style20">Select Room: </td>
                    <td class="auto-style21">
                        <asp:DropDownList ID="ddlChooseRoom" runat="server" OnSelectedIndexChanged="ddlChooseRoom_SelectedIndexChanged" AutoPostBack="True" CssClass="searchText" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style22">Number of Available PC&#39;s: </td>
                    <td class="auto-style23">
                        <asp:Label ID="lblNumberPCs2" runat="server" Text="" CssClass="searchText" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style24">Black &amp; White Printer: </td>
                    <td class="auto-style25">
                        <asp:Label ID="lblBlackPrinter2" runat="server" CssClass="searchText" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style27">Colour Printer: </td>
                    <td class="auto-style26">
                        <asp:Label ID="lblColourPrinter2" runat="server" Text="" CssClass="searchText" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style20">Start Time: </td>
                    <td class="auto-style21">
                        <asp:DropDownList AutoPostBack="True" ID="ddlStartTime" runat="server" OnSelectedIndexChanged="ddlStartTime_SelectedIndexChanged" CssClass="searchText" Width="174px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style28">End Time: </td>
                    <td class="auto-style29">
                        <asp:DropDownList ID="ddlEndTime" runat="server" AutoPostBack="True" CssClass="searchText" Width="173px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style30">
                        <asp:Label ID="lblPcBooking" runat="server" Text="PCs Booking: "></asp:Label>
                    </td>
                    <td class="auto-style31">
                        <asp:DropDownList ID="ddlPcBookings" runat="server" AutoPostBack="True" CssClass="searchText" Width="173px" Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style20">Description: </td>
                    <td class="auto-style21">
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="searchText" Height="90px" Width="269px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style32">
                        <asp:Label ID="lblRecurring" runat="server" Text="Recurring Booking: "></asp:Label>
                    </td>
                    <td class="auto-style33">
                        <asp:CheckBox ID="chkRecurring" runat="server" Height="25px" Text=" " AutoPostBack="True" OnCheckedChanged="chkRecurring_CheckedChanged" />

                        <asp:TextBox AutoPostBack="True" ID="txtRecurring" runat="server" CssClass="searchText" Width="189px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">

                        <asp:Button CssClass="auto-style34" ID="btnConfirmBooking" runat="server" OnClick="Button1_Click" Text="Confirm Booking" OnClientClick="return confirm('Are you sure you wish make this room booking?');" Style="margin-top: 5px; text-align: center; margin-left: 50%" />
                    </td>
                </tr>
            </table>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style20 {
            text-align: center;
            font-size: large;
            font-weight: bold;
            height: 19px;
        }
        .auto-style21 {
            height: 19px;
        }
        .auto-style22 {
            text-align: center;
            font-size: large;
            font-weight: bold;
            height: 20px;
        }
        .auto-style23 {
            height: 20px;
        }
        .auto-style24 {
            text-align: center;
            font-size: large;
            font-weight: bold;
            height: 30px;
        }
        .auto-style25 {
            height: 30px;
        }
        .auto-style26 {
            height: 34px;
        }
        .auto-style27 {
            text-align: center;
            font-size: large;
            font-weight: bold;
            height: 34px;
        }
        .auto-style28 {
            text-align: center;
            font-size: large;
            font-weight: bold;
            height: 8px;
        }
        .auto-style29 {
            height: 8px;
        }
        .auto-style30 {
            text-align: center;
            font-size: large;
            font-weight: bold;
            height: 13px;
        }
        .auto-style31 {
            height: 13px;
        }
        .auto-style32 {
            text-align: center;
            font-size: large;
            font-weight: bold;
            height: 25px;
        }
        .auto-style33 {
            height: 25px;
        }
        .auto-style34 {
            width: 223px;
            height: 30px;
            border-color: #31d3ba;
            border-radius: 5px;
            cursor: pointer;
            text-align: center;
            font-size: large;
        }
        .auto-style35 {
            text-align: center;
            font-size: large;
            font-weight: bold;
            height: 75px;
        }
    </style>
</asp:Content>

