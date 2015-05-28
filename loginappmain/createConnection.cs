using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;


namespace LoginAppmain
{
    public class createConnection
    {

        public static string CreateConStr(string server, string dbName, string user, string pass)
        {
            // String ConStr = "server=" + server + "; port=" + port + "; database=" + dbName + "; user id=" + user + "; password=" + pass + "; pooling=" + pooling + ";";
            String ConStr = "Data Source=" + server + "; Initial Catalog=" + dbName + "; User id=" + user + "; Password=" + pass + ";";

            return ConStr;
        }

        public static SqlConnection conn = null;

        public void Connection_Create()
        {

                conn = new SqlConnection(CreateConStr("ITPSNOV2K12SQL", "dbRoomBooking", "sa", "2014RRN0vus"));

                conn.Open();
            }

    }
}
