using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginAppmain
{
    public partial class ModifyPC : System.Web.UI.Page
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
               
                createRoomInformation();
                createRoomGridView();
                detectAccessLevel(userId);

                if (!Page.IsPostBack)
                {
                    createPCDropDown();
                    loadAllPcDetail();
                }
            }
        }


        //---------------------------------------------------------------------------------
        // FUNCTION : Detect access level of user
        //---------------------------------------------------------------------------------

        protected void detectAccessLevel(int userId)
        {
            int accessLevelInt = DatabaseConnections.GetAcessLevel(userId);
            if (accessLevelInt != 1)
            {
                Panel_adminOptions.Visible = false;
            }
        }

        //---------------------------------------------------------------------------------
        // FUNCTION : Get room information from database
        //---------------------------------------------------------------------------------
        protected void createRoomInformation()
        {
            DataTable roomInformationDataTable = DatabaseConnections.GetRooms();
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
            }
            if (!IsPostBack)
            {
                DataTable dt = DatabaseConnections.GetRooms();
                foreach (DataRow dr in dt.Rows)
                {
                    ddlSelectRoomName.Items.Add(dr[0].ToString());
                    ddlMovePCToNewRoom.Items.Add(dr[0].ToString());
                    ddlAddRoomName.Items.Add(dr[0].ToString());
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
            ddlSelectRoomName.Items.Clear();
            DataTable dt = DatabaseConnections.GetRooms();
            foreach (DataRow dr in dt.Rows)
            {

                ddlSelectRoomName.Items.Add(dr[0].ToString());
            }

            txtaddOperatingSystem.Text = "";
            txtaddScreenSize.Text = "";
            txtaddPCModel.Text = "";
            txtaddRamSize.Text = "";

            txtEditOperatingSystem.Text = "";
            txtEditScreenSize.Text = "";
            txtEditPCModel.Text = "";
            txtEditRamSize.Text = "";
        }


        protected void ClearEditArea()
        {
            txtEditOperatingSystem.Text = "";
            txtEditScreenSize.Text = "";
            txtEditPCModel.Text = "";
            txtEditRamSize.Text = "";
        }


        //---------------------------------------------------------------------------------
        // FUNCTION : View room information gridview
        //---------------------------------------------------------------------------------

        protected void createRoomGridView()
        {
            DataTable viewRoomDataTable = DatabaseConnections.GetRooms();
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
        // FUNCTION : When delete room is clicked
        //---------------------------------------------------------------------------------

        protected void Button_deleteRoom_Click(object sender, EventArgs e)
        {
            if (ddlSelectPC.Text.Length <= 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('Please select a PC to delete');", true);
            }
            else
            {
                string roomName = ddlSelectPC.SelectedValue;
                int roomID = DatabaseConnections.GetRoomID(roomName);
                DatabaseConnections.DeleteRoom(roomID);

                reloadAndClearPage();
            }
        }

        //---------------------------------------------------------------------------------
        // FUNCTION : Logout on Users Command
        //-

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            int userId = (int)Session["UserId"];
            DatabaseConnections.Logout(userId);

            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            Session["UserId"] = null;
            Response.Redirect("LoginPage.aspx");
        }

        protected void txtaddRamSize_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddRoom_Click(object sender, EventArgs e)
        {
            var RoomName = ddlAddRoomName.Text;
            var OS = txtaddOperatingSystem.Text;
            var screenSize = Convert.ToDecimal(txtaddScreenSize.Text);
            var model = txtaddPCModel.Text;
            var ram = Convert.ToInt32(txtaddRamSize.Text);
            var systemID = Convert.ToInt32(txtsytemID.Text);

            DatabaseConnections.AddPC(systemID, RoomName, OS, screenSize, model, ram);
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            var roomName = ddlSelectRoomName.Text;
            var PC = Convert.ToInt32(ddlSelectPC.Text);
            var OS = txtEditOperatingSystem.Text;
            var screenSize = Convert.ToDecimal(txtEditScreenSize.Text);
            var model = txtEditPCModel.Text;
            var ram = Convert.ToInt32(txtEditRamSize.Text);
            var systemID = 0;

            DatabaseConnections.EditPC(PC, systemID, roomName, OS, screenSize, model, ram);
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            string IdPC = ddlSelectPC.Text;
            //string[] split = IdPC.Split(new Char[] { ' ' });
            //string PCId = split[0];
            //string newPCId = PCId.Substring(0, PCId.Length - 1); // Takes the full stop from the PC ID

            DatabaseConnections.DeletePC(Convert.ToInt32(IdPC));
            reloadAndClearPage();
        }

        ////////////////////////////////////////////////////


        protected void ddlSelectRoomName_SelectedIndexChanged(object sender, EventArgs e)
        {
            createPCDropDown();
        }

        public void createPCDropDown()
        {
            var roomName = ddlSelectRoomName.Text;

            string[] PCList = DatabaseConnections.getPCs(roomName);

            ddlSelectPC.Items.Clear();

            foreach (string PC in PCList)
            {
                ddlSelectPC.Items.Add(PC);
            }
        }

        protected void ddlSelectPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAllPcDetail();
        }

        public void loadAllPcDetail()
        {
   //         ClearEditArea();
            int PCId = Convert.ToInt32(ddlSelectPC.Text);
            DataTable getPCInfo = DatabaseConnections.getPCInfo(PCId);

            foreach (DataRow row in getPCInfo.Rows)
            {
                try
                {
                    txtEditOperatingSystem.Text = row["OperatingSystem"].ToString();
                    txtEditScreenSize.Text = row["ScreenSize"].ToString();
                    txtEditPCModel.Text = row["PCModel"].ToString();
                    txtEditRamSize.Text = row["RAM"].ToString();
                }
                catch { }
            }

        }
    }
}