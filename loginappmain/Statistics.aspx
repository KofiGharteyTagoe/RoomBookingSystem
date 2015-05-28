<%@ Page Title="Statistics for novus website" Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="LoginAppmain.Statistics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="Pagename">
        STATISTICS
    </div>
                <div class="container">
    <asp:Panel ID="Panel1" runat="server" style="text-align: center">
    
    <div class="AccDivRoomsR" style="margin-top:0%; text-align:center">
        <div id="StatsGraphs">
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

                <asp:View ID="AmountOfUsersView" runat="server">
                    <asp:Chart ID="AmountOfUsersChart" runat="server" OnLoad="AmountOfUsersChart_Load" Width="800px">
                    </asp:Chart>
                </asp:View>
                <asp:View ID="AverageUsageView" runat="server">
                    <asp:Chart ID="AverageUsageChart" runat="server" OnLoad="AverageUsageChart_Load" Width="800px" Palette="Bright">
                    </asp:Chart>
                </asp:View>
                <asp:View ID="BookingsView" runat="server">
                    <asp:Chart ID="BookingsChart" runat="server" OnLoad="BookingsChart_Load" Width="800px">
                    </asp:Chart>
                </asp:View>
                <asp:View ID="RoomBookingsView" runat="server">
                    <asp:Chart ID="RoomBookingsChart" runat="server" OnLoad="RoomBookingsChart_Load" Width="800px">
                    </asp:Chart>
                </asp:View>
                <asp:View ID="PCBookingsView" runat="server">
                    <asp:Chart ID="PCBookingsChart" runat="server" OnLoad="PCBookingsChart_Load" Width="800px">
                    </asp:Chart>
                </asp:View>
                <asp:View ID="UserActivityView" runat="server">
                    <asp:Chart ID="UserActivityChart" runat="server" OnLoad="UserActivityChart_Load" Width="800px">
                    </asp:Chart>
                </asp:View>
            </asp:MultiView>
        </div>

        <br />
        <div id="controls">
            <div>
                <asp:Label ID="lblChoose" runat="server" CssClass="Label" Text="Choose Your Chart" />
                     <asp:DropDownList ID="ChooseChartDropDown" CssClass="searchText" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChooseChart_SelectedIndexChanged">
                    <asp:ListItem Text=" " disabled="disabled"></asp:ListItem>
                    <asp:ListItem Text="Amount of Users"></asp:ListItem>
                    <asp:ListItem Text="Average Usage"></asp:ListItem>
                    <asp:ListItem Text="Bookings"></asp:ListItem>
                    <asp:ListItem Text="Room Bookings"></asp:ListItem>
                    <asp:ListItem Text="PC Bookings"></asp:ListItem>
                    <asp:ListItem Text="User Activity"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <br />
            <hr />
            <br />
           <asp:Label ID="lblHeader" runat="server" CssClass="Label" Text="Select a date/range to view data from" Font-Bold="True" Font-Size="X-Large" />
            <br />
            <asp:Panel ID="pnldate" runat="server" HorizontalAlign="Left">
           <asp:Label ID="lblYear" runat="server" CssClass="Label" Text="Choose A Year:" />

            <asp:DropDownList ID="YearDropDown" CssClass="searchText" runat="server" AutoPostBack="true" OnSelectedIndexChanged="YearDropDown_SelectedIndexChanged">
                <asp:ListItem Text=" " disabled="disabled"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <div>
              
               <asp:Label ID="lblCalender" runat="server" CssClass="Label" Text="Or select a day, week or month: " />
                <asp:Calendar runat="server"
                    SelectionMode="DayWeekMonth" SelectWeekText="<>" SelectMonthText="<>"
                    OnSelectionChanged="Calendar_SelectionChanged" ID="calendar1" BorderStyle="Ridge" Font-Names="Arial" Height="300px" Width="400px" BorderColor="Black" BorderWidth="5px" WeekendDayStyle-HorizontalAlign="Center">
                    <DayHeaderStyle BackColor="#375D81" Font-Names="Arial" ForeColor="White" />
                    <NextPrevStyle BackColor="#375D81" Font-Names="Arial" Font-Size="Medium" />
                    <SelectedDayStyle BackColor="White" Font-Bold="True" Font-Names="Arial" ForeColor="Black" />
                    <SelectorStyle BackColor="#375D81" Font-Names="Arial" ForeColor="White" />
                    <TitleStyle BackColor="#375D81" Font-Names="Arial Black" Font-Strikeout="False" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCCC" />
                </asp:Calendar>

                    
            </div>

            </asp:Panel>
        </div>
        </div>
    </div>
        </asp:Panel>
</asp:Content>
