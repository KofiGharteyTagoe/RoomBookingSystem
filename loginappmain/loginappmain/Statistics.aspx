<%@ Page Title="Statistics for novus website" Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="LoginAppmain.Statistics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="container">
    <div class="Pagename">
        CALENDAR
    </div>

    <div class="AccDivRoomsR">
        <div id="StatsGraphs">
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

                <asp:View ID="AmountOfUsersView" runat="server">
                    <asp:Chart ID="AmountOfUsersChart" runat="server" OnLoad="AmountOfUsersChart_Load" Width="800px">
                    </asp:Chart>
                </asp:View>
                <asp:View ID="AverageUsageView" runat="server">
                    <asp:Chart ID="AverageUsageChart" runat="server" OnLoad="AverageUsageChart_Load" Width="800px">
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
                <asp:DropDownList ID="ChooseChartDropDown" CssClass="searchText" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChooseChart_SelectedIndexChanged">
                    <asp:ListItem Text="---choose a chart---" disabled="disabled"></asp:ListItem>
                    <asp:ListItem Text="---amount of users---"></asp:ListItem>
                    <asp:ListItem Text="---average usage---"></asp:ListItem>
                    <asp:ListItem Text="---bookings----"></asp:ListItem>
                    <asp:ListItem Text="---room bookings----"></asp:ListItem>
                    <asp:ListItem Text="---pc bookings----"></asp:ListItem>
                    <asp:ListItem Text="---user activity----"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <br />
            <asp:DropDownList ID="YearDropDown" CssClass="searchText" runat="server" AutoPostBack="true" OnSelectedIndexChanged="YearDropDown_SelectedIndexChanged">
                <asp:ListItem Text="---Choose year---" disabled="disabled"></asp:ListItem>
            </asp:DropDownList>
            <div>
                <asp:Calendar runat="server"
                    SelectionMode="DayWeekMonth" SelectWeekText="<>" SelectMonthText="<>"
                    OnSelectionChanged="Calendar_SelectionChanged" ID="calendar1"></asp:Calendar>


            </div>
        </div>
        </div>
    </div>
</asp:Content>
