<%@ Page Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="LoginAppmain.HomePage" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

        <div class="container">


            <div class="homePagecalendarDiv">
            
                    <div class="putInMiddle">
                        
                        <div id="light">
                            <div id="textArea">
                               
                            </div>
                            <div id="buttonArea">

                                                                                <table class="auto-style3">
                            <tr>
                                <td class="auto-style4"><asp:Label ID="lblID1" runat="server" Text="ID: " ClientIDMode="Static" Font-Bold="True"></asp:Label></td>
                                <td><asp:Label ID="lblID2" runat="server" ClientIDMode="Static" ></asp:Label><br /></td>
                            </tr>
                            <tr>
                                <td class="auto-style4"><asp:Label ID="lblRoom1" runat="server" Text="Room ID: " ClientIDMode="Static" Font-Bold="True"></asp:Label></td>
                                <td><asp:Label ID="lblRoom2" runat="server" ClientIDMode="Static" ></asp:Label><br /></td>
                            </tr>
                            <tr>
                                <td class="auto-style4"><asp:Label ID="lblDate1" runat="server" Text="Booking Date: " ClientIDMode="Static" Font-Bold="True"></asp:Label></td>
                                <td><asp:Label ID="lblDate2" runat="server" ClientIDMode="Static" ></asp:Label><br /></td>
                            </tr>
                            <tr>
                                <td class="auto-style4"><asp:Label ID="lblSTime1" runat="server" Text="Start Time: " ClientIDMode="Static" Font-Bold="True"></asp:Label></td>
                                <td><asp:Label ID="lblSTime2" runat="server" ClientIDMode="Static" ></asp:Label><br /></td>
                            </tr>
                            <tr>
                                <td class="auto-style4"><asp:Label ID="lblETime1" runat="server" Text="End Time: " ClientIDMode="Static" Font-Bold="True"></asp:Label></td>
                                <td><asp:Label ID="lblETime2" runat="server" ClientIDMode="Static" ></asp:Label><br /></td>
                            </tr>
                            <tr>
                                <td class="auto-style4"> <asp:Label ID="lblBooked1" runat="server" Text="Booked By: " ClientIDMode="Static" Font-Bold="True"></asp:Label></td>
                                <td><asp:Label ID="lblBooked2" runat="server" ClientIDMode="Static" ></asp:Label><br /></td>
                            </tr>
                            <tr>
                                <td class="auto-style4"><asp:Label ID="lblType1" runat="server" Text="BookingType: " ClientIDMode="Static" Font-Bold="True"></asp:Label></td>
                                <td><asp:Label ID="lblType2" runat="server" ClientIDMode="Static" ></asp:Label><br /></td>
                            </tr>
                            <tr>
                                <td class="auto-style4"><asp:Label ID="lblDescription1" runat="server" Text="Description: " ClientIDMode="Static" Font-Bold="True"></asp:Label></td>
                                <td><asp:Label ID="lblDescription2" runat="server" ClientIDMode="Static" ></asp:Label><br /></td>
                            </tr>
                            <tr>
                                <td class="center" colspan="2">
                               <asp:Button ID="btnEdit" runat="server" OnClick="EditButtonClick" Text="Edit" />
                               <asp:Button ID="btnMakeBooking" runat="server" OnClick="btnMakeBookingClick" Text="Make A Booking"/>

                                </td>
                            </tr>
                        </table>



                            </div>
                        </div>
                        <div id="fade" onclick="lightbox_close();">
                        </div>
                        <div class="putInMiddlebuttons">
                                     <asp:Button CssClass="ButtonToggle" ID="btnPreviousWeek" runat="server" OnClick="btnNextPrevious_Click" Text="&lt; Previous Week" Width="125px"/>
                                     <asp:Button CssClass="ButtonToggle"  ID="btnPreviousDay" runat="server" OnClick="btnNextPrevious_Click" Text="&lt; Previous Day" Width="125px"/>
                                     <asp:Button CssClass="ButtonToggle"  ID="btnNextDay" runat="server" OnClick="btnNextPrevious_Click" Text="Next Day &gt;" Width="125px"/>
                                     <asp:Button CssClass="ButtonToggle"  ID="btnNextWeek" runat="server" OnClick="btnNextPrevious_Click" Text="Next Week &gt;" Width="125px"/>
                            <br />
                                 
                                     
                            <hr />
                            </div>
                        <asp:Button CssClass="ButtonToggle" ID="Button1" runat="server" Text="Print" Width="155px" Height="25px" OnClick="Button1_Click"/>

                        <div class="putInMiddlediv">
                        <asp:Panel ID="pnlDays" runat="server" Style="text-align: left" Width="1403px">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="XX-Large" Style="text-align: center" Text=" " Width="56px"></asp:Label>
                            <asp:Label ID="lblMonday" runat="server" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" Style="text-align: center" Text="Monday" Width="144px"></asp:Label>
                            <asp:Label ID="lblTuesday" runat="server" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" Style="text-align: center" Text="Tuesday" Width="144px"></asp:Label>
                            <asp:Label ID="lblWednesday" runat="server" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" Style="text-align: center" Text="Wednesday" Width="144px"></asp:Label>
                            <asp:Label ID="lblThursday" runat="server" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" Style="text-align: center" Text="Thursday" Width="144px"></asp:Label>
                            <asp:Label ID="lblFriday" runat="server" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" Style="text-align: center" Text="Friday" Width="144px"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="pnlDates" runat="server" Height="23px" Style="text-align: left" Width="1403px">
                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="XX-Large" Style="text-align: center" Text=" " Width="56px"></asp:Label>
                            <asp:Label ID="lblDay1" runat="server" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="Large" Height="20px" Style="text-align: center" Text="17/09/14" Width="174px"></asp:Label>
                            <asp:Label ID="lblDay2" runat="server" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="Large" Height="20px" Style="text-align: center" Text="18/09/14" Width="174px"></asp:Label>
                            <asp:Label ID="lblDay3" runat="server" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="Large" Height="20px" Style="text-align: center" Text="19/09/14" Width="174px"></asp:Label>
                            <asp:Label ID="lblDay4" runat="server" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="Large" Style="text-align: center" Text="20/09/14" Width="174px"></asp:Label>
                            <asp:Label ID="lblDay5" runat="server" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="Large" Style="text-align: center" Text="21/09/14" Width="174px"></asp:Label>
                        </asp:Panel>

                        <asp:GridView CssClass="GridView" ID="grdTimeSlots" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="2px" Font-Names="Arial" ForeColor="Black" GridLines="Vertical" Height="189px" HorizontalAlign="Left" Style="table-layout: fixed" Width="61px">
                            <AlternatingRowStyle BackColor="#CCCCFF" />
                            <EditRowStyle BorderStyle="None" />
                            <EmptyDataRowStyle BorderStyle="None" Width="100px" />
                            <HeaderStyle BackColor="#375D81" Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="White" Width="100px" />
                            <RowStyle BorderStyle="None" Width="100px" />
                            <SelectedRowStyle BorderStyle="None" />
                        </asp:GridView>
                        <asp:GridView CssClass="GridView" ID="grdDay1" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="2px" Font-Names="Arial" ForeColor="Black" GridLines="Vertical" Height="189px" HorizontalAlign="Left" OnRowCommand="RowCommand" Style="table-layout: fixed" Width="180px">
                            <AlternatingRowStyle BackColor="#CCCCFF" />
                            <EditRowStyle BorderStyle="None" />
                            <EmptyDataRowStyle BorderStyle="None" Width="100px" />
                            <HeaderStyle BackColor="#375D81" Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="White" Width="100px" />
                            <RowStyle BorderStyle="None" Width="100px" />
                            <SelectedRowStyle BorderStyle="None" />
                        </asp:GridView>
                        <asp:GridView CssClass="GridView" ID="grdDay2" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="2px" Font-Names="Arial" ForeColor="Black" GridLines="Vertical" Height="189px" HorizontalAlign="Left" OnRowCommand="RowCommand" Style="table-layout: fixed" Width="180px">
                            <AlternatingRowStyle BackColor="#CCCCFF" BorderColor="#FF66CC" ForeColor="Black" />
                            <EditRowStyle BorderStyle="None" />
                            <EmptyDataRowStyle BorderStyle="None" Width="100px" />
                            <HeaderStyle BackColor="#375D81" Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="White" Width="100px" />
                            <RowStyle Width="100px" />
                            <SelectedRowStyle BorderStyle="None" />
                        </asp:GridView>
                        <asp:GridView CssClass="GridView" ID="grdDay3" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="2px" Font-Names="Arial" ForeColor="Black" GridLines="Vertical" Height="189px" HorizontalAlign="Left" OnRowCommand="RowCommand" Style="table-layout: fixed" Width="180px">
                            <AlternatingRowStyle BackColor="#CCCCFF" />
                            <EditRowStyle BorderStyle="None" />
                            <EmptyDataRowStyle BorderStyle="None" Width="100px" />
                            <HeaderStyle BackColor="#375D81" Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="White" Width="100px" />
                            <RowStyle BorderStyle="None" Width="100px" />
                            <SelectedRowStyle BorderStyle="None" />
                        </asp:GridView>
                        <asp:GridView CssClass="GridView" ID="grdDay4" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="2px" Font-Names="Arial" ForeColor="Black" GridLines="Vertical" Height="189px" HorizontalAlign="Left" OnRowCommand="RowCommand" Style="table-layout: fixed" Width="180px">
                            <AlternatingRowStyle BackColor="#CCCCFF" />
                            <EditRowStyle BorderStyle="None" />
                            <EmptyDataRowStyle BorderStyle="None" Width="100px" />
                            <HeaderStyle BackColor="#375D81" Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="White" Width="100px" />
                            <RowStyle BorderStyle="None" Width="100px" />
                            <SelectedRowStyle BorderStyle="None" />
                        </asp:GridView>
                        <asp:GridView CssClass="GridView" ID="grdDay5" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="2px" Font-Names="Arial" ForeColor="Black" GridLines="Vertical" Height="189px" HorizontalAlign="Left" OnRowCommand="RowCommand" Style="table-layout: fixed" Width="180px">
                            <AlternatingRowStyle BackColor="#CCCCFF" />
                            <EditRowStyle BorderStyle="None" />
                            <EmptyDataRowStyle BorderStyle="None" Width="100px" />
                            <HeaderStyle BackColor="#375D81" Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="White" Width="100px" />
                            <RowStyle BorderStyle="None" Width="100px" />
                            <SelectedRowStyle BorderStyle="None" />
                        </asp:GridView>
</div>
                    </div>
            </div>
        </div>





 </asp:Content>