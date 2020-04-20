using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private Board board;

    private readonly Selection selection;

    public string boardState;

    public Manager()
    {
        selection = new Selection(this);
    }

    private void Start()
    {
        board = Board.Load(GetComponentsInChildren<Position>());

        boardState = board.ToString();
    }


    /// <summary>
    /// Select a position a primary selection.
    /// </summary>
    /// <param name="position">The position to select.</param>
    /// <param name="piece">The piece that was requested.</param>
    internal void SelectPosition(Position position, Piece piece)
    {
        Debug.Log(string.Format("Select '{1}' piece in '{0}' position.", position.name, piece.name));

        if(CanSelect(position, piece))
        {
            selection.Select(position, GetSecondarySelections(position, piece));
        }
        else
        {
            Debug.Log(string.Format("Cannot select '{1}' piece in '{0}' position.", position.name, piece.name));
        }
    }

    /// <summary>
    /// Get secondary selections which allows to implement board movement posiblities.
    /// </summary>
    /// <param name="position">The primary position.</param>
    /// <param name="piece">The primary piece.</param>
    /// <returns></returns>
    protected Position[] GetSecondarySelections(Position position, Piece piece)
    {
        return new Position[0];
    }

    /// <summary>
    /// Allow manager to define which position are selectables
    /// </summary>
    /// <param name="position">The position that should be selected.</param>
    /// <param name="piece">The piece that is contained in the position.</param>
    /// <returns></returns>
    protected bool CanSelect(Position position, Piece piece)
    {
        return piece != null;
    }

    /// <summary>
    /// Removes selection indicators from the primary and secondary positions
    /// </summary>
    /// <param name="primarySelection"></param>
    /// <param name="secondarySelections"></param>
    internal void RemoveSelectionIndications(Position primarySelection, Position[] secondarySelections)
    {
        if (primarySelection != null)
        {
            Debug.Log(string.Format("Remove selection '{0}' position.", primarySelection.name));
        }
    }

    /// <summary>
    /// Apply selection indicators from the primary and secondary positions
    /// </summary>
    /// <param name="primarySelection"></param>
    /// <param name="secondarySelections"></param>
    internal void ApplySelectionIndications(Position primarySelection, Position[] secondarySelections)
    {
        if(primarySelection != null)
        {
            Debug.Log(string.Format("Apply selection '{0}' position.", primarySelection.name));
        }
    }
}
