using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Stores the colour of a row and cell. A list of 'ColourCell' is created and colourcells are added.
/// The colours are applied to the cell ina GridView later in the program
/// </summary>
public class ColourCell
{
    public int row { get; private set; }
    public int day { get; private set; }
    public Color colour { get; private set; }
    public int room { get; private set; }
    public int id { get; private set; }
    public int column { get; private set; }


    public ColourCell(int id, int row, int day, Color colour, int room, int column)
    {
        this.id = id;
        this.row = row;
        this.day = day;
        this.colour = colour;
        this.room = room;
        this.column = column;
    }
}

