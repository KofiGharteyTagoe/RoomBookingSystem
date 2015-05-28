using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class RoomsResources : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LoginPage.aspx");

            }
            else
            {
                int userId = (int)Session["UserId"];
                DataTable userInfo = new DataTable();
                try
                {
                    userInfo = DatabaseConnections.GetUser(userId);
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }


                createRoomInformation();
                createRoomGridView();
                detectAccessLevel(userId);
                reloadRoomInfo(ddlRoomName.SelectedValue);
            }
            
        }
        //---------------------------------------------------------------------------------
        // FUNCTION : Detect access level of user
        //---------------------------------------------------------------------------------

        protected void detectAccessLevel(int userId)
        {

            int accessLevelInt = 0;
            try
            {
                accessLevelInt = DatabaseConnections.GetAcessLevel(userId);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            if (accessLevelInt != 1)
            {
                Panel_adminOptions.Visible = false;                
            }
        }

        //---------------------------------------------------------------------------------
        // FUNCTION : View room information gridview
        //---------------------------------------------------------------------------------

        protected void createRoomGridView()
        {
            DataTable viewRoomDataTable = new DataTable();
            try
            {
                DatabaseConnections.GetRooms();
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            DataTable tempViewRoomGridView = new DataTable();

            tempViewRoomGridView.Columns.AddRange(new DataColumn[4]
        {
            new DataColumn("roomName", typeof(string)),
            new DataColumn("blackWhitePrinter", typeof(string)),
            new DataColumn("colourPrinter", typeof(string)),
            new DataColumn("numberOfComputers", typeof(string))
            
        });

            foreach (DataRow row in viewRoomDataTable.Rows)
            {
                DataRow tempRow = tempViewRoomGridView.NewRow();
                try
                {
                    tempRow[0] = row["RoomName"].ToString();
                    tempRow[1] = row["PrinterBlack"].ToString();
                    tempRow[2] = row["PrinterColour"].ToString();
                    tempRow[3] = row["PcNumbers"].ToString();
                }
                catch { }
                tempViewRoomGridView.Rows.Add(tempRow);

                viewRoomsGridView.DataSource = tempViewRoomGridView;
                viewRoomsGridView.DataBind();
            }
        }

        //---------------------------------------------------------------------------------
        // FUNCTION : Get room information from database
        //---------------------------------------------------------------------------------
        protected void createRoomInformation()
        {
            /*
            DataTable roomInformationDataTable = new DataTable();
            try
            {
                roomInformationDataTable = DatabaseConnections.GetRooms();
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            DataTable tempRoomDataTable = new DataTable();

            tempRoomDataTable.Columns.AddRange(new DataColumn[4]
            {
                new DataColumn("roomName", typeof(string)),
                new DataColumn("printerBlack", typeof(string)),
                new DataColumn("printerColour", typeof(string)),
                new DataColumn("numberOfPC", typeof(string))
            });

            foreach (DataRow row in roomInformationDataTable.Rows)
            {
                DataRow tempRow = tempRoomDataTable.NewRow();
                try
                {
                    tempRow[0] = row[0].ToString();
                    tempRow[1] = row[1].ToString();
                    tempRow[2] = row[2].ToString();
                    tempRow[3] = row[3].ToString();
                }
                catch { }
                tempRoomDataTable.Rows.Add(tempRow);
            }*/
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                try
                {
                    dt = DatabaseConnections.GetRooms();
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }
                foreach (DataRow dr in dt.Rows)
                {
                    ddlRoomName.Items.Add(dr["RoomName"].ToString());
                }
            }
        }

        //---------------------------------------------------------------------------------
        // FUNCTION : Reload drop down list and clear text boxes
        //---------------------------------------------------------------------------------

        protected void reloadAndClearPage()
        {
            createRoomInformation();
            createRoomGridView();
            ddlRoomName.Items.Clear();
            DataTable dt = new DataTable();
            try
            {
                dt = DatabaseConnections.GetRooms();
            }
             catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            foreach (DataRow dr in dt.Rows)
            {
                ddlRoomName.Items.Add(dr[0].ToString());
            }

            changeRoomNameTextBox.Text = "";
            numberOfComputersTextBox.Text = "";
            cbxEditBlackAndWhite.Checked = false;
            cbxEditColour.Checked = false;

            txtNewRoomName.Text = "";
            //txtNumberOfComputers.Text = "";
            cbxBlackAndWhite.Checked = false;
            cbxColour.Checked = false;
        }

        //---------------------------------------------------------------------------------
        // FUNCTION : When add room is clicked
        //---------------------------------------------------------------------------------

        protected void btnAddRoom_Click(object sender, EventArgs e)
        {

            bool boolRoomName = false;
            bool boolNumberOfComputers = false;

            //----------new room name----------

            DataTable roomDataTable = new DataTable();
            try
            {
                roomDataTable = DatabaseConnections.GetRooms();
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            List<string> roomNameList = new List<string>();
            foreach (DataRow dr in roomDataTable.Rows)
            {
                roomNameList.Add(dr[0].ToString().Trim());

            }

            string newRoomName = txtNewRoomName.Text;
            bool b = roomNameList.Any(s => newRoomName.Contains(s));

            string roomName = "";
            if (txtNewRoomName.Text.Length <= 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('Plese enter a name for the new room.');", true);
            }
            else if (txtNewRoomName.Text.Length > 10)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('The name for the new room is too long. 10 characters max.');", true);
            }
            else if (b == true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('There is already a room with that name! Please enter a different one.');", true);
            }
            else
            {
                roomName = txtNewRoomName.Text;
                boolRoomName = true;
            }

            //----------number of computers---------- 

      /*      int numberOfComputers = 0;

            if (txtNewRoomName.Text.Length > 0)
            {
                try
                {
                    numberOfComputers = Convert.ToInt16(txtNewRoomName.Text);
                    boolNumberOfComputers = true;
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('Plese enter a valid number into Number of Computers');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('Plese enter the number of computers the room has');", true);
            } */

            //----------Black and white printer----------

            bool printerBlack = true;

            if (cbxBlackAndWhite.Checked == true)
            {
                printerBlack = true;
            }
            else
            {
                printerBlack = false;
            }

            //----------Colour printer----------

            bool printerColour = cbxColour.Checked;

            if (boolRoomName == true)
            {
                try
                {
                    DatabaseConnections.AddRooms(roomName, printerColour, printerBlack);
                    reloadAndClearPage();
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }
            }
            else
            {
                reloadAndClearPage();
            }
        }

        //---------------------------------------------------------------------------------
        // FUNCTION : When delete room is clicked
        //---------------------------------------------------------------------------------

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            if (ddlRoomName.Text.Length <= 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('Please select a room to delete');", true);
            }
            else
            {
                string roomName = ddlRoomName.SelectedValue;
                int roomID = DatabaseConnections.GetRoomID(roomName);
                try
                {
                    DatabaseConnections.DeleteRoom(roomID);
                }
                catch
                {
                    Session["Disconnected"] = true;
                    Response.Redirect("LoginPage.aspx");
                }

                reloadAndClearPage();
            }
        }

        //---------------------------------------------------------------------------------
        // FUNCTION : When edit room is clicked
        //---------------------------------------------------------------------------------

protected void editButton_Click(object sender, EventArgs e)
    {

        bool boolRoomName = false;
        bool boolNumberOfComputers = false;

        //----------roomName from DropDown List----------
        string roomName = "";
        string originalRoomName = ddlRoomName.SelectedValue;

        //----------change roonName if text box is filled (and not the same as an existing name)----------

        DataTable roomDataTable = new DataTable();
        try
        {
            DatabaseConnections.GetRooms();
        } 
         catch
        {
            Session["Disconnected"] = true;
            Response.Redirect("LoginPage.aspx");
        }

        List<string> roomNameList = new List<string>();
        foreach (DataRow dr in roomDataTable.Rows)
        {
            roomNameList.Add(dr[0].ToString().Trim());

        }
        string changedName = changeRoomNameTextBox.Text;
        bool b = roomNameList.Any(s => changedName.Contains(s));
       

        if (changeRoomNameTextBox.Text.Length == 0)
        {
            roomName = ddlRoomName.SelectedValue;
            boolRoomName = true;
        }
        else if (changeRoomNameTextBox.Text.Length > 10)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('The name for the new room is too long. 10 characters max.');", true);
        }
        else if (b == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('There is already a room with that name! Please enter a different one.');", true);
        }
        else
        {
            roomName = changeRoomNameTextBox.Text;
            boolRoomName = true;
        }

        //----------number of computers----------

        int numberOfComputers = 0;

        if (numberOfComputersTextBox.Text.Length > 0)
        {
            try
            {
                numberOfComputers = Convert.ToInt16(numberOfComputersTextBox.Text);
            }
            catch (Exception h)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('Plese enter a valid number');", true);
            }

            if (numberOfComputers < 0)
            {
                numberOfComputers *= -1;
            }
            boolNumberOfComputers = true;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('Plese Enter the number of available computers');", true);

        }

        //----------Black and white printer----------

        bool printerBlack = true;

        if (cbxEditBlackAndWhite.Checked == true)
        {
            printerBlack = true;
        }
        else
        {
            printerBlack = false;
        }

        //----------Colour printer----------

        bool printerColour = true;

        if(cbxEditColour.Checked == true)
        {
            printerColour = true;
        }
        else
        {
            printerColour = false;
        }

        //----------Send to database----------
        if (boolRoomName == true && boolNumberOfComputers == true)
        {
            int roomID = DatabaseConnections.GetRoomID(originalRoomName);
            DatabaseConnections.EditRoom(roomID, roomName, printerColour, printerBlack);
            reloadAndClearPage();
        }
        else
        {
            reloadAndClearPage();
        }
    }


        //---------------------------------------------------------------------------------
        // FUNCTION : Logout on Users Command
        //-

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            int userId = (int)Session["UserId"];
            try
            {
                DatabaseConnections.Logout(userId);
            }
            catch { /* Normally we'd redirect the user to the LoginPage if we can't connect to the DB but they're logging our anyways..."*/ }

            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            Session["UserId"] = null;
            Response.Redirect("LoginPage.aspx");
        }

        protected void ddlRoomName_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadRoomInfo(ddlRoomName.SelectedValue);
        }

        private void reloadRoomInfo(string roomName)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DatabaseConnections.GetRoom(roomName);
            }
            catch
            {
                Session["Disconnected"] = true;
                Response.Redirect("LoginPage.aspx");
            }
            DataRow dr = dt.Rows[0];
            numberOfComputersTextBox.Text = dr["PcNumbers"].ToString();
            changeRoomNameTextBox.Text = dr["RoomName"].ToString();
            bool bw = Convert.ToBoolean(dr["PrinterBlack"].ToString());
            bool cl = Convert.ToBoolean(dr["PrinterColour"].ToString());

            cbxEditBlackAndWhite.Checked = bw;
            cbxEditColour.Checked = cl;
        }



    }
}