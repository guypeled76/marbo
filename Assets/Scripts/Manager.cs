using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private Board board;

    private readonly SelectionContainer selectionContainer;

    public GameObject selectedMarker;
    public GameObject unselectableMarker;
    public GameObject removeMarker;
    public GameObject dropMarker;

    public string boardState;

    public Manager()
    {
        selectionContainer = new SelectionContainer(this);
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
    internal void OnSelectPosition(Position position, Piece piece)
    {
        Debug.Log(string.Format("Select '{1}' piece in '{0}' position.", position.name, piece.name));

        if(CanSelect(position, piece))
        {
            selectionContainer.Select(new Selection(position, MarkerType.Selected), GetSecondarySelections(position, piece));
        }
        else
        {
            selectionContainer.Select(new Selection(position, MarkerType.Unselectable));
        }
    }

    /// <summary>
    /// Get secondary selections which allows to implement board movement posiblities.
    /// </summary>
    /// <param name="position">The primary position.</param>
    /// <param name="piece">The primary piece.</param>
    /// <returns></returns>
    protected virtual Selection[] GetSecondarySelections(Position position, Piece piece)
    {
        return new Selection[0];
    }

    /// <summary>
    /// Allow manager to define which position are selectables
    /// </summary>
    /// <param name="position">The position that should be selected.</param>
    /// <param name="piece">The piece that is contained in the position.</param>
    /// <returns></returns>
    protected virtual bool CanSelect(Position position, Piece piece)
    {
        return piece != null;
    }

    /// <summary>
    /// Removes selection indicators from the primary and secondary positions
    /// </summary>
    internal void RemoveSelection(params Selection[] selections)
    {
        Debug.Log(string.Format("Remove selection '{0}' position.", selections.Length));

        foreach(Selection selection in selections)
        {
            selection.position.RemoveMarker();
        }
    }

    /// <summary>
    /// Apply selection indicators from the primary and secondary positions
    /// </summary>
    /// <param name="primarySelection"></param>
    /// <param name="secondarySelections"></param>
    internal void ApplySelection(params Selection[] selections)
    {
        Debug.Log(string.Format("Apply selection '{0}' position.", selections.Length));

        foreach (Selection selection in selections)
        {
            selection.position.SetMarker(selection.marker, CreateMarker(selection.marker, selection.position.gameObject));
        }
    }

    private GameObject CreateMarker(MarkerType marker, GameObject parent)
    {
        GameObject originalMarker = null;

        switch (marker)
        {
            case MarkerType.Unselectable:
                originalMarker = unselectableMarker;
                break;
            case MarkerType.Remove:
                originalMarker = unselectableMarker;
                break;
            case MarkerType.Selected:
                originalMarker = unselectableMarker;
                break;
            case MarkerType.Drop:
                originalMarker = unselectableMarker;
                break;
            case MarkerType.None:
            default:
                break;
        }

        if(originalMarker == null)
        {
            return null;
        }

        GameObject instantiatedMarker = GameObject.Instantiate(originalMarker, parent.transform.position, parent.transform.rotation, parent.transform);

        return instantiatedMarker;
    }
}

