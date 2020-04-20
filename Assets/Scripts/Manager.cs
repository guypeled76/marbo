using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    /// <summary>
    /// Holds the board positions index by name
    /// </summary>
    private Dictionary<string, Position> positions;

    private void Start()
    {
        // Load and index positions
        this.positions = GetComponentsInChildren<Position>().ToDictionary(p => p.name);

        // Loop all positions within manager
        foreach (Position position in this.Positions)
        {
            if (position == null)
            {
                continue;
            }

            position.Initialize(this);
        }
    }


    /// <summary>
    /// Select position with piece in it.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="piece"></param>
    internal void SelectPiece(Position position, Piece piece)
    {
        Debug.Log(string.Format("Select '{1}' piece in '{0}' position.", position.name, piece.name));
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
}
