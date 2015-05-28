<%@ Page Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="LoginAppmain.Calendar" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
                            <div class="Pagename">

                CALENDAR
            </div>

        <div class="container">

                <div class="calendarDiv">

                    <h1 class="font"><u>Book a Room by selecting a date! </u></h1>
                                 <asp:Panel ID="Panel1" runat="server" style="text-align: center">
                 <asp:Panel ID="Panel2" runat="server">
                     <table class="auto-style3">
                         <tr>
                             <td><asp:GridView ID="GridView1" runat="server" Width="200px">
                                     <HeaderStyle BackColor="#375D81" Font-Names="Arial" Width="200px" />
                                 </asp:GridView></td>
                             <td>
                                 
                                 <asp:Calendar ID="Calendar1" runat="server" BorderStyle="Inset" BorderWidth="2px" DayNameFormat="Full" Font-Bold="True" Font-Names="Arial" Height="500px" NextMonthText="Next &amp;gt;" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" PrevMonthText="&amp;lt; Previous" ShowGridLines="True" Width="700px">
                                     <DayHeaderStyle BackColor="#375D81" Font-Bold="True" Font-Names="Arial" ForeColor="White" />
                                     <NextPrevStyle BackColor="#375D81" Font-Names="Arial" Font-Size="Medium" ForeColor="White" />
                                     <TitleStyle BackColor="White" Font-Names="Arial Black" ForeColor="#375D81" />
                                     <WeekendDayStyle BackColor="#CCCCCC" />
                                 </asp:Calendar>
                             </td>
                         </tr>
                     </table>
                     <asp:Panel ID="Panel3" runat="server" Height="98px" Width="693px">
                     </asp:Panel>
                 </asp:Panel>
            </asp:Panel>

                </div>

            </div>
</asp:Content>