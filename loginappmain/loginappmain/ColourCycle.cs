using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Contains an array of colours that is cycled through using the methods
/// </summary>
public class ColourCycle
{
    // The list of colours, colours can be added and removed as required but there must be at least one colour in the array.
    private static Color[] colours = { Color.LightBlue, Color.LightSkyBlue, Color.SkyBlue, Color.CornflowerBlue };   
    // The current index we are on.
    private int current = 0;
    // A default colour that will be returned should an exception arise (this way, a colour will always be returned)
    private static Color defaultColour = Color.LightGray;

    /*
     * Increments the current index and returns the next colour. If the index exceeds the length of the array, reset it to 0 so it looks around
     */
    public Color NextColour()
    {        
        current++;

        if (colours != null && // If 'colours' hasn't been intialized then don't check the other conditions because an exception will be thrown
            (current >= colours.Length ||  // Check if it exceeds the length of the array
            current < 0))  // If, for any reason, the value of current drops below 0 then reset it 
            {
                current = 0; // Reset the varible
            }
        return CurrentColour();
    }

    /*
     * Returns the current colour
     */
    public Color CurrentColour()
    {
        try
        {
            return colours[current];
        }
        catch
        {
            // If we reach here, there is a problem with the colours so return the default colour.
            return defaultColour;
        }
    }
}