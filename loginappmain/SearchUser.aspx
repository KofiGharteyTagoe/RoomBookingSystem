<%@ Page Language="C#" MasterPageFile="~/RoomBooking.Master" AutoEventWireup="true" CodeBehind="SearchUser.aspx.cs" Inherits="LoginAppmain.SearchUser" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

        <div class="Pagename">
            SEARCH USER!
        </div>

        <div class="container">


            <div class="searchDiv">
                <asp:DropDownList runat="server" CssClass="searchDrop" ID="ddl_search">
                    <asp:ListItem>Name</asp:ListItem>
                    <asp:ListItem>User Name</asp:ListItem>
                    <asp:ListItem>Email address</asp:ListItem>
                    <asp:ListItem>Position</asp:ListItem>

                </asp:DropDownList>
                <asp:TextBox runat="server" CssClass="searchText" ID="txt_searchItem"></asp:TextBox>
                <asp:Button runat="server" Text="Search" CssClass="searchButton" ID="btn_Search" OnClick="Search" />
            </div>

            <div class="SearchAccDiv2">


                <asp:GridView CssClass="GridviewTable" ID="grd_getUserGridView" runat="server" AutoGenerateColumns="false" OnRowCommand="grd_getUserGridView_RowCommand">
                    <Columns>
                        <asp:ButtonField ButtonType="Button" Text="View" ControlStyle-Font-Bold="true" CommandName="buttonView" />
                        <asp:BoundField DataField="UserId" HeaderText="User ID" ItemStyle-Width="70" />
                        <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-Width="70" />
                        <asp:BoundField DataField="FullName" HeaderText="Full Name" ItemStyle-Width="70" />
                        <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="70" />
                        <asp:BoundField DataField="IsLockedOut" HeaderText="IsLockedOut?" ItemStyle-Width="70" />
                        <asp:BoundField DataField="AccessLvl" HeaderText="User Type" ItemStyle-Width="70" />
                        <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" ItemStyle-Width="70" />

                    </Columns>

                </asp:GridView>


            </div>
        </div>

</asp:Content>
