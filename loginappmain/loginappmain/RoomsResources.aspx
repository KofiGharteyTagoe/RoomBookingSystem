<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/RoomBooking.Master" CodeBehind="RoomsResources.aspx.cs" Inherits="LoginAppmain.RoomsResources" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <!DOCTYPE html>
    <style type="text/css">
                .auto-style4 {
            text-align: center;
            font-size: x-large;
            font-weight: bold;
            height: 75px;
        }

        .auto-style7 {
            text-align: left;
            font-weight: bold;
        }

        .auto-style8 {
            width: 256px;
        }
        .auto-style10 {
            color: #3333CC;
            font-size: small;
        }
    </style>


            <div class="Pagename">
                ROOMS & RESOURCES
            </div>

            <div class="container">

                <div class="content">

                    <div class="AccDivRoomsR">

                        <asp:Panel ID="Panel_adminOptions" runat="server">
                            <table class="editRoomTable">
                                <tr>
                                    <td class="auto-style4" colspan="2">Add A New Room
                                
                                    </td>
                                    <td class="auto-style4" colspan="3">Delete/ Edit Rooms</td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">New Room Name :</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtNewRoomName" runat="server" CssClass="textboxEditRoom" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td class="auto-style7">Room Name : </td>
                                    <td>

                                        <asp:DropDownList ID="ddlRoomName" runat="server" CssClass="textboxEditRoom" AutoPostBack="True" Height="18px" OnSelectedIndexChanged="ddlRoomName_SelectedIndexChanged" Width="127px"></asp:DropDownList>

                                    </td>
                                    <td>&nbsp;<asp:Button ID="Button_deleteRoom" runat="server" OnClick="deleteButton_Click" CssClass="deletebutton" Text="Delete Room" OnClientClick="return confirm('Are you sure you wish to edit room?');" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">Number Of Computers :</td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtNumberOfComputers" runat="server" TextMode="Number" CssClass="textboxEditRoom" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td class="auto-style7">Change Room Name : </td>
                                    <td>

                                        <asp:TextBox ID="changeRoomNameTextBox" runat="server" CssClass="textboxEditRoom" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">B &amp; W Printer? :</td>
                                    <td class="auto-style8">
                                        <asp:CheckBox ID="cbxBlackAndWhite" runat="server" AutoPostBack="True" />
                                    </td>
                                    <td class="auto-style7">Number Of Computers : </td>
                                    <td>
                                        <asp:TextBox ID="numberOfComputersTextBox" runat="server" CssClass="textboxEditRoom" Enabled="False" Width="44px"></asp:TextBox>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ModifyPC.aspx">Add/Remove PCs</asp:HyperLink>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">Colour Printer? :</td>
                                    <td class="auto-style8">
                                        <asp:CheckBox ID="cbxColour" runat="server" AutoPostBack="True" />
                                    </td>
                                    <td class="auto-style7">B&amp;W Printer? : </td>
                                    <td>

                                        <asp:CheckBox ID="cbxEditBlackAndWhite" runat="server" AutoPostBack="True" />
                                        &nbsp;<span class="auto-style10">(select to add printer)</span></td>
                                    <td class="auto-style10">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="auto-style8">&nbsp;</td>
                                    <td class="auto-style7">Colour Printer ? :</td>
                                    <td>
                                        <asp:CheckBox ID="cbxEditColour" runat="server" AutoPostBack="True" />
                                        &nbsp;<span class="auto-style10">(select to add printer)</span></td>
                                    <td class="auto-style10">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">

                                        <asp:Button ID="Button_addRoom" runat="server" OnClick="btnAddRoom_Click" CssClass="addbutton" Text="Add Room" OnClientClick="return confirm('Are you sure you wish to add room?');" />

                                    </td>
                                    <td colspan="3" style="text-align: center">

                                        <asp:Button ID="Button_editRoom" runat="server" OnClick="editButton_Click" CssClass="editbutton" Text="Edit Room" OnClientClick="return confirm('Are you sure you wish to edit room?');" Width="179px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <hr />

                        <div class="showBlackWhiteDiv">
                            <asp:GridView ID="viewRoomsGridView" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" BorderWidth="2" GridLines="Both" Width="667px">
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
                        </div>


                    </div>
                </div>
            </div>
</asp:Content>