<%@ Page Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="ModifyUser.aspx.cs" Inherits="LoginAppmain.ModifyUser" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <!DOCTYPE html>
    <link href="AllFormsSheet.css" rel="stylesheet" />
    <script>
        $(function () {
            $("#txtDatePicker").datepicker({ dateFormat: 'dd/mm/yy', minDate: -0, beforeShowDay: $.datepicker.noWeekends });

        });

    </script>
    <style type="text/css">
        #userDataGrid {
    margin: 0px auto;
    width: 90%;
}

#userDataGrid input
{
    width: 95%;
}

        .text1 {
            width:100px;
        }
        .num1 {
        width:50px;
        }

        .auto-style3 {
            text-align: center;
            font-size: x-large;
            text-decoration: underline;
        }

        .auto-style4 {
            text-align: right;
        }

        .createUser {
            margin: 0px auto;
        }

        .auto-style24 {
            text-align: center;
            height: 31px;
        }
        .auto-style25 {
            height: 31px;
        }
        .auto-style27 {
            text-align: center;
            height: 32px;
        }
        .auto-style28 {
            height: 32px;
        }
        .auto-style29 {
            text-align: right;
            font-size: large;
            font-weight: bold;
            height: 32px;
        }
        .auto-style30 {
            text-align: right;
            font-size: large;
            font-weight: bold;
            height: 31px;
        }
    </style>


    <div class="Pagename">
        USERS
    </div>
    <div class="container">
                                              <div id="PopupArea">

        <asp:Label ID="confirmation" runat="server" Text="Are you sure you would like to delete this booking?" Width="300px"/>
                                                  <br />
	    <asp:Button ID="okay" runat="server"  Text="Okay"/>

        </div>
                
        <div class="container">

            <div class="searchDiv">
                <asp:DropDownList runat="server" CssClass="searchDrop" ID="ddl_search">

                    <asp:ListItem>User Name</asp:ListItem>

                </asp:DropDownList>
                <asp:TextBox runat="server" CssClass="searchText" ID="txt_searchUserName"></asp:TextBox>
                <asp:Button runat="server" Text="Search" CssClass="searchButton" ID="btn_Search" OnClick="Search" />
            </div>


            <div class="SearchAccDiv2">

                <br />
                <asp:GridView ID="userDataGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="UserID" DataSourceID="SqlDataSource1" Style="text-align: center" HorizontalAlign="Center" Width="1000px">
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="UserID" InsertVisible="False" ReadOnly="True" SortExpression="UserID" />
                        <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username"/>
                        <asp:BoundField DataField="Forename" HeaderText="Forename" SortExpression="Forename"/>
                        <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname"  />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="IsLockedOut" HeaderText="IsLockedOut" SortExpression="IsLockedOut" />
                        <asp:BoundField DataField="AccessLvl" HeaderText="AccessLvl" SortExpression="AccessLvl" HeaderStyle-Width="10" />
                        <asp:BoundField DataField="ExpiryDate" HeaderText="ExpiryDate" SortExpression="ExpiryDate" HeaderStyle-Width="50" />
                        <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" SortExpression="ContactNumber"  />
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbRoomBookingConnectionString %>" SelectCommand="SELECT [UserID], [Username], [Forename], [Surname], [Email], [IsLockedOut], [AccessLvl], [ExpiryDate], [ContactNumber], [IsApproved] FROM [tableUser]" DeleteCommand="DELETE FROM [tableUser] WHERE [UserID] = @UserID" InsertCommand="INSERT INTO [tableUser] ([Username], [Forename], [Surname], [Email], [IsLockedOut], [AccessLvl], [ExpiryDate], [ContactNumber], [IsApproved]) VALUES (@Username, @Forename, @Surname, @Email, @IsLockedOut, @AccessLvl, @ExpiryDate, @ContactNumber, @IsApproved)" UpdateCommand="UPDATE [tableUser] SET [Username] = @Username, [Forename] = @Forename, [Surname] = @Surname, [Email] = @Email, [IsLockedOut] = @IsLockedOut, [AccessLvl] = @AccessLvl, [ExpiryDate] = @ExpiryDate, [ContactNumber] = @ContactNumber, [IsApproved] = @IsApproved WHERE [UserID] = @UserID">
                    <DeleteParameters>
                        <asp:Parameter Name="UserID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Username" Type="String" />
                        <asp:Parameter Name="Forename" Type="String" />
                        <asp:Parameter Name="Surname" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="IsLockedOut" Type="Int16" />
                        <asp:Parameter Name="AccessLvl" Type="Int32" />
                        <asp:Parameter Name="ExpiryDate" Type="DateTime" />
                        <asp:Parameter Name="ContactNumber" Type="String" />
                        <asp:Parameter Name="IsApproved" Type="Boolean" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Username" Type="String" />
                        <asp:Parameter Name="Forename" Type="String" />
                        <asp:Parameter Name="Surname" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="IsLockedOut" Type="Int16" />
                        <asp:Parameter Name="AccessLvl" Type="Int32" />
                        <asp:Parameter Name="ExpiryDate" Type="DateTime" />
                        <asp:Parameter Name="ContactNumber" Type="String" />
                        <asp:Parameter Name="IsApproved" Type="Boolean" />
                        <asp:Parameter Name="UserID" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>

                <asp:GridView ID="searchDataGrid" runat="server" AllowPaging="True" CellPadding="4" DataSourceID="SearchSource" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataKeyNames="UserID" Width="942px" Visible="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Style="text-align: center">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="UserID" InsertVisible="False" ReadOnly="True" SortExpression="UserID" />
                        <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                        <asp:BoundField DataField="Forename" HeaderText="Forename" SortExpression="Forename" />
                        <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="ExpiryDate" HeaderText="ExpiryDate" SortExpression="ExpiryDate" />
                        <asp:BoundField DataField="IsLockedOut" HeaderText="IsLockedOut" SortExpression="IsLockedOut" />
                        <asp:BoundField DataField="AccessLvl" HeaderText="AccessLvl" SortExpression="AccessLvl" />
                        <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" SortExpression="ContactNumber" />
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                <asp:SqlDataSource ID="SearchSource" runat="server" ConnectionString="<%$ ConnectionStrings:dbRoomBookingConnectionString %>"
                    SelectCommand="SELECT [UserID], [Username], [Forename], [Surname], [Email], [ExpiryDate], [IsLockedOut], [AccessLvl], [ContactNumber], [IsApproved] FROM [tableUser] WHERE ([Username] LIKE '%' + @Username + '%') ORDER BY [Username]" DeleteCommand="DELETE FROM [tableUser] WHERE [UserID] = @UserID" InsertCommand="INSERT INTO [tableUser] ([Username], [Forename], [Surname], [Email], [ExpiryDate], [IsLockedOut], [AccessLvl], [ContactNumber], [IsApproved]) VALUES (@Username, @Forename, @Surname, @Email, @ExpiryDate, @IsLockedOut, @AccessLvl, @ContactNumber, @IsApproved)" UpdateCommand="UPDATE [tableUser] SET [Username] = @Username, [Forename] = @Forename, [Surname] = @Surname, [Email] = @Email, [ExpiryDate] = @ExpiryDate, [IsLockedOut] = @IsLockedOut, [AccessLvl] = @AccessLvl, [ContactNumber] = @ContactNumber, [IsApproved] = @IsApproved WHERE [UserID] = @UserID">
                    <DeleteParameters>
                        <asp:Parameter Name="UserID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Username" Type="String" />
                        <asp:Parameter Name="Forename" Type="String" />
                        <asp:Parameter Name="Surname" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="ExpiryDate" Type="DateTime" />
                        <asp:Parameter Name="IsLockedOut" Type="Int16" />
                        <asp:Parameter Name="AccessLvl" Type="Int32" />
                        <asp:Parameter Name="ContactNumber" Type="String" />
                        <asp:Parameter Name="IsApproved" Type="Boolean" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txt_searchUserName" DefaultValue="null" Name="Username" PropertyName="Text" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Username" Type="String" />
                        <asp:Parameter Name="Forename" Type="String" />
                        <asp:Parameter Name="Surname" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="ExpiryDate" Type="DateTime" />
                        <asp:Parameter Name="IsLockedOut" Type="Int16" />
                        <asp:Parameter Name="AccessLvl" Type="Int32" />
                        <asp:Parameter Name="ContactNumber" Type="String" />
                        <asp:Parameter Name="IsApproved" Type="Boolean" />
                        <asp:Parameter Name="UserID" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <br />

                <table class="createUser" align="center">
                    <tr>
                        <td colspan="4" class="auto-style3"><strong>Create A New User      </strong></td>

                    </tr>
                    <tr>
                        <td class="auto-style29">Title:</td>
                        <td class="auto-style27">
                            <asp:DropDownList CssClass="createUserTextbox" ID="ddl_title" runat="server">
                                <asp:ListItem>Mr</asp:ListItem>
                                <asp:ListItem>Mrs</asp:ListItem>
                                <asp:ListItem>Dr</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style29">User Name:</td>
                        <td class="auto-style28">
                            <asp:TextBox CssClass="createUserTextbox" ID="txt_userName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="auto-style29">Expiry Date: </td>
                        <td class="auto-style27">
                            <asp:TextBox CssClass="createUserTextbox" ID="txtDatePicker" runat="server"></asp:TextBox>
                        </td>
                            &nbsp;</td>
                        <td class="auto-style30">E-mail:</td>
                        <td class="auto-style25">
                            <asp:TextBox CssClass="createUserTextbox" ID="txt_email" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style30">First Name</td>
                        <td class="auto-style24">
                            <asp:TextBox CssClass="createUserTextbox" ID="txt_firstName" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style30">Access Level:</td>
                        <td class="auto-style25">
                            <asp:DropDownList CssClass="createUserTextbox" ID="ddl_accessLvl" runat="server">
                                <asp:ListItem Value="1">Admin</asp:ListItem>
                                <asp:ListItem Value="2">Novus Team</asp:ListItem>
                                <asp:ListItem Value="3">Non-Novus</asp:ListItem>
                                <asp:ListItem Value="4">Novus Trainees</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style30">Last Name</td>
                        <td class="auto-style24">
                            <asp:TextBox CssClass="createUserTextbox" ID="txt_lastName" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style30">Contact Number:</td>
                        <td class="auto-style25">
                            <asp:TextBox CssClass="createUserTextbox" ID="txt_contactNumber" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       
                        <td class="auto-style27" colspan="2">
                            <asp:Button CssClass="Button" ID="btn_insert" runat="server" OnClick="btn_insert_Click" Text="Create User" Width="177px" />
                        </td>
                    </tr>
                </table>
            </div>



        </div>
</asp:Content>
