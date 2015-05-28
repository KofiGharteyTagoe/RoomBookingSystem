<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintCalendar.aspx.cs" Inherits="PrintView.PrintCalendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" style="text-align: center">       
             <asp:Image ID="Image1" runat="server" ImageUrl="http://www.capitanovus.co.uk/furniture/capita/logo.png" Height="70px" style="text-align: center" Width="416px" />
             <br />
             <br />
             <asp:DropDownList ID="DropDownList1" runat="server" Font-Bold="True" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
             </asp:DropDownList>
             <br />
             <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="X-Small" Text="The table below shows booking data for the given room. You are not allowed to use the room unless you have a valid booking or are attending a booking made in the room. If you wish to use the room during free time please make a booking via the RoomBooking service or contact reception to make a booking.The room is for use only by Capita staff and cannot not be used by anyone else." EnableTheming="True" Font-Italic="True"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" Height="20px" ShowHeader="False" Width="1000px">
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" Height="20px" ShowHeader="False" Width="1000px">
        </asp:GridView>    
  
        <asp:GridView ID="GridView3" runat="server" Height="20px" ShowHeader="False" Width="1000px">
        </asp:GridView>
    
        <asp:GridView ID="GridView4" runat="server" Height="20px" ShowHeader="False" Width="1000px">
        </asp:GridView>

        <asp:GridView ID="GridView5" runat="server" Height="20px" ShowHeader="False" Width="1000px">
        </asp:GridView>
       </asp:Panel>

       
    </div>
    </form>
</body>
</html>
