using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board
{
    /// <summary>
    /// Holds the board positions index by name
    /// </summary>
    private readonly Dictionary<string, Position> positions;


    /// <summary>
    /// Create an empty board
    /// </summary>
    public Board() : this(new Dictionary<string, Position>())
    {
        
    }

    /// <summary>
    /// Creates a new board based on board positions indexed by name 
    /// </summary>
    /// <param name="positions"></param>
    private Board(Dictionary<string, Position> positions)
    {
        this.positions = positions;
    }

    /// <summary>
    /// Load a board based on a list of board positions
    /// </summary>
    /// <param name="positions"></param>
    /// <returns></returns>
    internal static Board Load(Position[] positions)
    {
        Board board = new Board(positions.ToDictionary(p => p.name));
        board.Initialize();

        Debug.Log(string.Format("Loaded board with {0} position and {1} pieces", board.Positions.Count(), board.Pieces.Count()));

        return board;
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

    /// <summary>
    /// Initializes the board and it's positions
    /// </summary>
    private void Initialize()
    {
        foreach(Position position in positions.Values)
        {
            if(position == null)
            {
                continue;
            }

            position.Initialize();
        }
    }
}
