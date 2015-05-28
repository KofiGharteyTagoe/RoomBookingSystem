<%@ Page Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="ModifyPC.aspx.cs" Inherits="LoginAppmain.ModifyPC" %>

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
            text-align: right;
            font-weight: bold;
        }

        .auto-style8 {
            width: 256px;
        }
        .auto-style10 {
            color: #3333CC;
            font-size: small;
        }
        .auto-style11 {
            color: #0066FF;
        }
        .auto-style12 {
            text-align: right;
            font-weight: bold;
            height: 38px;
        }
        .auto-style13 {
            width: 256px;
            height: 38px;
        }
        .auto-style14 {
            height: 38px;
        }
    </style>

            <div class="Pagename">
                Add & Edit PC's
            </div>

            <div class="container">



                <div class="content">

                    <div class="AccDivRoomsR">

                        <asp:Panel ID="Panel_adminOptions" runat="server">
                            <table class="editRoomTable">
                                <tr>
                                    <td class="auto-style4" colspan="2">Add A PC
                                
                                    </td>
                                    <td class="auto-style4" colspan="3">Delete/ Edit PC</td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">Room Name :</td>
                                    <td class="auto-style8">
                                        <asp:DropDownList ID="ddlAddRoomName" runat="server" CssClass="textboxEditRoom">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style7">Room Name : </td>
                                    <td>

                                        <asp:DropDownList ID="ddlSelectRoomName" runat="server" CssClass="textboxEditRoom" OnSelectedIndexChanged="ddlSelectRoomName_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">Operating System : </td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtaddOperatingSystem" runat="server" CssClass="textboxEditRoom" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td class="auto-style7">Select PC: </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSelectPC" runat="server" CssClass="textboxEditRoom" OnSelectedIndexChanged="ddlSelectPC_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_deleteRoom" runat="server" CssClass="deletebutton" OnClick="deleteButton_Click" OnClientClick="return confirm('Are you sure you wish to edit room?');" Text="Delete PC" /> 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style12">Screen Size :</td>
                                    <td class="auto-style13">
                                        <asp:TextBox ID="txtaddScreenSize" runat="server" CssClass="textboxEditRoom" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td class="auto-style12">Operating System : </td>
                                    <td class="auto-style14">

                                        <asp:TextBox ID="txtEditOperatingSystem" runat="server" CssClass="textboxEditRoom" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td class="auto-style14"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">PC Model : </td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtaddPCModel" runat="server" CssClass="textboxEditRoom" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td class="auto-style7">Screen Size: </td>
                                    <td>
                                        <asp:TextBox ID="txtEditScreenSize" runat="server" CssClass="textboxEditRoom"></asp:TextBox>
                                    </td>
                                    <td class="auto-style11">(inches)</td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">RAM Size : </td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtaddRamSize" runat="server" CssClass="textboxEditRoom" MaxLength="10" TextMode="Number" OnTextChanged="txtaddRamSize_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="auto-style7">PC Model : </td>
                                    <td>

                                        <asp:TextBox ID="txtEditPCModel" runat="server" CssClass="textboxEditRoom"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">System ID : </td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtsytemID" runat="server" CssClass="textboxEditRoom" MaxLength="10" OnTextChanged="txtaddRamSize_TextChanged" TextMode="Number"></asp:TextBox>
                                    </td>
                                    <td class="auto-style7">New Room ? : </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMovePCToNewRoom" runat="server" AutoPostBack="True" CssClass="textboxEditRoom">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style10">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">&nbsp;</td>
                                    <td class="auto-style8">
                                        &nbsp;</td>
                                    <td class="auto-style7">RAM Size:</td>
                                    <td>
                                        <asp:TextBox ID="txtEditRamSize" runat="server" CssClass="textboxEditRoom" TextMode="Number"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10">(MB)</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">

                                        <asp:Button ID="Button_addPC" runat="server" OnClick="btnAddRoom_Click" CssClass="addbutton" Text="Add PC" OnClientClick="return confirm('Are you sure you wish to add a PC?');" />

                                    </td>
                                    <td colspan="3" style="text-align: center">

                                        <asp:Button ID="Button_editPC" runat="server" OnClick="editButton_Click" CssClass="editbutton" Text="Edit PC" OnClientClick="return confirm('Are you sure you wish to edit a PC?');" Width="179px" />
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