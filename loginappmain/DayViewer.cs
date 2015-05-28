using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// A day consists of two objects. A header (saying the date) and a table showing the rooms. We need to treat these as one object so 
/// use this class to keep references to them both/
/// </summary>
public class DayViewer
{
 
    public GridView table { get; private set; }
    public Label header { get; private set; }
    public Label day { get; private set; }

    /*
     * Constrcutor
     */
	public DayViewer(GridView table, Label header, Label day)
	{
		this.table = table;
        this.header = header;
        this.day = day;
	}

    /*
     * Applies the width of the table and the header
     */
    public void SetWidth(int width)
    {
        table.Width = width;
        table.Height = 150;
        day.Width = width-6;
        header.Width = width-6; // The headers have a border around them and spacing between them - the -6 takes this into account to ensure the header and the table lines up correctly. 
    }

    /*
     * Add the data to the table
     */
    public void DataSource(DataTable data)
    {

       
        table.DataSource = data;
        DataBind();


      

    }

    /*
     * Bind the data to the table
     */
    public void DataBind()
    {
        table.DataBind();
    }

    public void Clear()
    {
        Style style = new Style();

        for (int i = 0; i < table.Rows.Count; i++)
        {
            for (int j = 0; j < table.Rows[i].Cells.Count; j++)
            {
                table.Rows[i].Cells[j].MergeStyle(style);
                table.Rows[i].Cells[j].Text = "";
            }
        }
    }
}