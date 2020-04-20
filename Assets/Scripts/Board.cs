using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Board
{

    private readonly Dictionary<string, Position> positionsByName = new Dictionary<string, Position>();


    private readonly int columns;
    private readonly int rows;
    private readonly Position[,] positions;

    /// <summary>
    /// Creates an empty board
    /// </summary>
    public Board() : this(new Position[0])
    {

    }

    /// <summary>
    /// Creates a board based on the positions.
    /// </summary>
    /// <param name="positions">The board positions.</param>
    private Board(Position[] positions)
    {
        // Index the positions by name to allow for position navigation
        this.positionsByName = positions.ToDictionary(p => p.name);

        // Inizialize all positions and calculate board size
        foreach (Position position in positions)
        {
            position.Initialize(this);

            rows = Math.Max(rows, position.row);
            columns = Math.Max(columns, position.column);
        }

        // Create the board position metrix
        this.positions = new Position[rows, columns];

        // Index positions by location
        foreach (Position position in positions)
        {
            this.positions[position.column - 1, position.row - 1] = position;
        }
    }



    /// <summary>
    /// Load a board given all it's positions
    /// </summary>
    /// <param name="positions">The board's positions.</param>
    /// <returns></returns>
    internal static Board Load(Position[] positions)
    {
        return new Board(positions);
    }

    /// <summary>
    /// Get a position by it's name and allow for default if not found
    /// </summary>
    /// <param name="name">The position name.</param>
    /// <param name="defaultPosition">The default value to return if position cannot be found.</param>
    /// <returns></returns>
    public Position GetPositionByName(string name, Position defaultPosition = null)
    {
        if (positionsByName.TryGetValue(name, out Position position))
        {
            return position;
        }
        return defaultPosition;
    }

    /// <summary>
    /// Generates a string representation of the board state
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            FillRowString(builder, rowIndex);
        }

        return builder.ToString();
    }
    
    /// <summary>
    /// Fills the string builder with the current row content.
    /// </summary>
    /// <param name="builder">The current builder.</param>
    /// <param name="rowIndex">The current row index with in the board.</param>
    private void FillRowString(StringBuilder builder, int rowIndex)
    {
        if (rowIndex > 0)
        {
            builder.Append('/');
        }

        int emptyCount = 0;

        for (int columnIndex = 0; columnIndex < columns; columnIndex++)
        {
            FillColumnString(builder, rowIndex, columnIndex, ref emptyCount);
        }

        // If we have empty columns at the end of the row
        FillEmptyColumnString(builder, ref emptyCount);
    }

    /// <summary>
    /// Fills the string builder with the current column content.
    /// </summary>
    /// <param name="builder">The current builder.</param>
    /// <param name="rowIndex">The current row index with in the board.</param>
    /// <param name="columnIndex"></param>
    /// <param name="emptyCount">The amount of empty positions encountered untill now.</param>
    private void FillColumnString(StringBuilder builder, int rowIndex, int columnIndex, ref int emptyCount)
    {
        // Even if we are missing a position we are still working as if we have a rectangle
        Position position = positions[rowIndex, columnIndex];
        if (position == null)
        {
            emptyCount++;
            return;
        }

        // Get piece at the specific position
        Piece piece = position.piece;
        if (piece == null)
        {
            emptyCount++;
            return;
        }

        FillEmptyColumnString(builder, ref emptyCount);

        builder.Append((char)piece.type);
    }

    /// <summary>
    /// Adds a number representing the amount of empty columns we encountered untill now and resets the counter.
    /// </summary>
    /// <param name="builder">The current builder.</param>
    /// <param name="emptyCount">The amount of empty positions encountered untill now.</param>
    private void FillEmptyColumnString(StringBuilder builder, ref int emptyCount)
    {
        if (emptyCount > 0)
        {
            builder.Append(emptyCount);
            emptyCount = 0;
        }
    }
}
