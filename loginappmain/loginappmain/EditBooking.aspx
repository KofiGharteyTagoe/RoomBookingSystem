<%@ Page Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeFile="EditBooking.aspx.cs" Inherits="LoginAppmain.EditBooking"%> 

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<!DOCTYPE html>

    <script>
        $(function () {
            $("#txtDatePicker6M").datepicker({ dateFormat: 'dd/mm/yy', minDate: -0, beforeShowDay: $.datepicker.noWeekends, maxDate: "+6M" });
            $("#txtDatePicker5D").datepicker({ dateFormat: 'dd/mm/yy', minDate: -0, beforeShowDay: $.datepicker.noWeekends, maxDate: "+7D" });
            $("#txtRecurring").datepicker({ dateFormat: 'dd/mm/yy', minDate: -0, beforeShowDay: $.datepicker.noWeekends, maxDate: "+6M" });
        });
  </script> 
    <style type="text/css">
        #datepicker {
            z-index: 1;
            left: 267px;
            top: 22px;
           
        }
        .Label 
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 16px;
            line-height:30px;
        }
        .Text 
        {
            font-family: Arial;
            font-size: 16px;
            line-height:30px;
            margin-left: 8px;
        }
        #lblHeader 
        {
            font-family: Arial;
            font-size: 32px;
            font-weight: bold;
        }
        .auto-style4 {
            height: 22px;
            text-align: right;
            font-size: large;
            font-weight: bold;
        }
        .auto-style5 {
            text-align: right;
            font-size: large;
            font-weight: bold;
        }
        .auto-style6 {
            text-align: right;
            font-size: large;
            font-weight: bold;
            height: 26px;
        }
        .auto-style7 {
            height: 26px;
            width: 521px;
        }
        .auto-style8 {
            text-align: right;
            font-size: large;
            font-weight: bold;
            height: 98px;
        }
        .auto-style9 {
            height: 98px;
            width: 521px;
        }
        .auto-style10 {
            width: 521px;
        }
    </style>

            <div class="Pagename">
                Edit Booking
            </div>

            <div class="container">

                <div class="content">

                    <div class="AccDivRoomsR">

                        <div>
                        <table class="editRoomTable">
                            <tr>
                                <td class="auto-style5">Select A Date : </td>
                                <td class="auto-style10">
         
        <asp:TextBox AutoPostBack="True"  ID="txtDatePicker5D" runat="server" OnTextChanged="txtDatePicker_TextChanged" CssClass="textboxEditRoom"  ></asp:TextBox>
                                    <br />
        <asp:TextBox AutoPostBack="True"  ID="txtDatePicker6M" runat="server" OnTextChanged="txtDatePicker_TextChanged" CssClass="textboxEditRoom"  ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Select A Room : </td>
                                <td class="auto-style10"> 
        <asp:DropDownList ID="ddlChooseRoom" runat="server"  OnSelectedIndexChanged="ddlChooseRoom_SelectedIndexChanged" AutoPostBack="True" CssClass="textboxEditRoom" /></td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Number Of Available PC&#39;s : </td>
                                <td class="auto-style10"><asp:Label ID="lblNumberPCs2" runat="server" Text="" CssClass="Text" /></td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Black &amp; White Printer : </td>
                                <td class="auto-style10"><asp:Label ID="lblBlackPrinter2" runat="server" CssClass="Text"/> </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Colour Printer : </td>
                                <td class="auto-style10"> <asp:Label ID="lblColourPrinter2" runat="server" Text="" CssClass="Text" /></td>
                            </tr>
                            <tr>
                                <td class="auto-style6">Start Time : </td>
                                <td class="auto-style7"> 
        <asp:DropDownList AutoPostBack="True" ID="ddlStartTime" runat="server"  OnSelectedIndexChanged="ddlStartTime_SelectedIndexChanged" CssClass="textboxEditRoom"  Width="174px"/></td>
                            </tr>
                            <tr>
                                <td class="auto-style5">End Time : </td>
                                <td class="auto-style10"> 
        <asp:DropDownList ID="ddlEndTime" runat="server"  AutoPostBack="True" CssClass="textboxEditRoom"  Enabled="False" /></td>
                            </tr>
                            <tr id="PcBookings">
                                <td class="auto-style5"> <asp:Label runat="server" id="lblPcBooking" > PC&#39;s Booked :</asp:Label></td>
                                <td class="auto-style10">         
        <asp:DropDownList ID="ddlPcBookings" runat="server" AutoPostBack="True" CssClass="textboxEditRoom" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">Description : </td>
                                <td class="auto-style9">
        <asp:TextBox ID="txtDescription" runat="server"  TextMode="MultiLine" CssClass="textboxEditRoom" Height="90px" Width="269px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="Button1" runat="server" Height="29px" OnClick="Button1_Click" Text="Edit Booking" Width="91px" />&nbsp;</td>
                            </tr>
                        </table>
                        </div>

                        <hr />

                        <div class="showBlackWhiteDiv">
                            <asp:GridView ID="viewRoomsGridView" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" BorderWidth="2px" Width="667px" DataSourceID="SqlDataSource1">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="roomName" HeaderText="Room Name" ItemStyle-Width="70">

                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="B&W Printer" ItemStyle-Width="70">
                                        <ItemTemplate>
                                            <%#Eval("blackWhitePrinter").ToString() == "True" ? "Yes" : "No" %>
                                        </ItemTemplate>

                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Colour Printer?" ItemStyle-Width="70">
                                        <ItemTemplate>
                                            <%#Eval("colourPrinter").ToString() == "True" ? "Yes" : "No" %>
                                        </ItemTemplate>

                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="numberOfComputers" HeaderText="# of PCs" ItemStyle-Width="70">
                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />

                                <HeaderStyle BackColor="#990000" ForeColor="White" Font-Bold="True"></HeaderStyle>
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                <SortedDescendingHeaderStyle BackColor="#820000" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                        </div>


                    </div>
                </div>
            </div>
    </asp:Content>