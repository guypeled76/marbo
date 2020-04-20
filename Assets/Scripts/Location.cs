using System;
using System.Text.RegularExpressions;
using UnityEngine;

public struct Location
{
    /// <summary>
    /// Regular expression to parse "a1" style positioning with "a" being the column and "1" the row.
    /// </summary>
    private static Regex expression = new Regex(@"^(?<column>[a-z])(?<row>[0-9]+)$");

    /// <summary>
    /// An empty location
    /// </summary>
    public static readonly Location Empty = new Location();

    public string back;
    public string backLeft;
    public string backRight;
    public string forward;
    public string forwardLeft;
    public string forwardRight;
    public string left;
    public string right;

    public int row;
    public int column;

    /// <summary>
    /// Create a location value based on the location name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Location Create(string name)
    {
        Match match = expression.Match(name);

        if(!match.Success)
        {
            return Location.Empty;
        }
        else
        {
            return Create(match.Groups["column"].Value[0], Int32.Parse(match.Groups["row"].Value));
        }
    }

    /// <summary>
    /// Generate location from offsets
    /// </summary>
    /// <param name="row">The current row.</param>
    /// <param name="column">The current column.</param>
    /// <returns>A location with the position names of the current location neighbours.</returns>
    private static Location Create(char row, int column)
    {
        Location location = new Location();
        location.row = (row - 'a') + 1;
        location.column = column;
        location.back = Create(row, column, 0, -1);
        location.backLeft = Create(row, column, -1, -1);
        location.backRight = Create(row, column, 1, -1);
        location.forward = Create(row, column, 0, 1);
        location.forwardLeft = Create(row, column, -1, 1);
        location.forwardRight = Create(row, column, 1, 1);
        location.left = Create(row, column, -1, 0);
        location.right = Create(row, column, 1, 0);
        return location;
    }

    /// <summary>
    /// Create a name based on column/row and their offsets.
    /// </summary>
    /// <param name="row">The current row.</param>
    /// <param name="column">The current column.</param>
    /// <param name="rowOffset">The row offset.</param>
    /// <param name="columOffset">The column offset.</param>
    /// <returns>The name of the position relative to the current with applying the offsets.</returns>
    private static string Create(char row, int column, int rowOffset, int columOffset)
    {
        return string.Concat((char)(row + rowOffset), column + columOffset);
    }
}
