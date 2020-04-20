using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board
{
    /// <summary>
    /// Holds the board positions index by name
    /// </summary>
    private readonly Dictionary<string, Position> positions = new Dictionary<string, Position>();

    public readonly int columns = 0;
    public readonly int rows = 0;

    /// <summary>
    /// Creates an empty board
    /// </summary>
    public Board() : this(new Dictionary<string, Position>())
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="positions"></param>
    private Board(Dictionary<string, Position> positions)
    {
        this.positions = positions;

        // Loop all positions within manager
        foreach (Position position in this.Positions)
        {
            if (position == null)
            {
                continue;
            }

            // Initialize the current position
            position.Initialize(this);

            // Calculate board width
            rows = Math.Max(rows, position.row);
            columns = Math.Max(columns, position.column);
        }
    }



    /// <summary>
    /// Load a board given all it's positions
    /// </summary>
    /// <param name="positions">The board's positions.</param>
    /// <returns></returns>
    internal static Board Load(Position[] positions)
    {
        return new Board(positions.ToDictionary(p => p.name));
    }

    /// <summary>
    /// Get a position by it's name and allow for default if not found
    /// </summary>
    /// <param name="name">The position name.</param>
    /// <param name="defaultPosition">The default value to return if position cannot be found.</param>
    /// <returns></returns>
    public Position GetPositionByName(string name, Position defaultPosition = null)
    {
        if (positions.TryGetValue(name, out Position position))
        {
            return position;
        }
        return defaultPosition;
    }

    /// <summary>
    /// Gets all the positions on the board
    /// </summary>
    public IEnumerable<Position> Positions
    {
        get
        {
            return positions.Values;
        }
    }

    /// <summary>
    /// Gets all the pieces on the board
    /// </summary>
    public IEnumerable<Piece> Pieces
    {
        get
        {
            return this.Positions.SelectMany(p => p.pieces);
        }
    }

}
