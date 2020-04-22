using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Provides support for managing a board game
/// </summary>
public class Manager : MonoBehaviour
{
    /// <summary>
    /// The board implementation instance.
    /// </summary>
    protected Board board;

    /// <summary>
    /// The interaction manager
    /// </summary>
    private readonly Interactions interactions;

    public GameObject selectedMarker;
    public GameObject unselectableMarker;
    public GameObject removeMarker;
    public GameObject dropMarker;

    public Manager()
    {
        interactions = new Interactions(this);
    }

    private void Start()
    {
        // Create the game board
        board = CreateBoard(GetComponentsInChildren<Position>());

        OnManagerInitialzed();
    }

    /// <summary>
    /// Used to create the current board
    /// </summary>
    /// <param name="positions">The board positions</param>
    /// <returns></returns>
    protected virtual Board CreateBoard(Position[] positions)
    {
        return new Board(positions);
    }

    /// <summary>
    /// Allows game implementations to initialize game specific manager stuff
    /// </summary>
    protected virtual void OnManagerInitialzed()
    {
        
    }


    /// <summary>
    /// Select a position a primary selection.
    /// </summary>
    /// <param name="position">The position to select.</param>
    /// <param name="piece">The piece that was requested.</param>
    internal void OnSelectPosition(Position position, Piece piece)
    {
        Debug.Log(string.Format("Select '{1}' piece in '{0}' position.", position.name, piece.name));

        if(board.CanMove(position, piece))
        {
            interactions.Select(new Selection(position, MarkerType.Selected), board.GetPossibleMoves(position, piece));
        }
        else
        {
            interactions.Select(new Selection(position, MarkerType.Unselectable));
        }
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

    /// <summary>
    /// Creates a marker instance based on marker type and the target game object 
    /// </summary>
    /// <param name="markerType">The marker type to create.</param>
    /// <param name="parent">The parent of the new marker.</param>
    /// <returns></returns>
    private GameObject CreateMarker(MarkerType markerType, GameObject parent)
    {
        GameObject originalMarker = null;

        switch (markerType)
        {
            case MarkerType.Unselectable:
                originalMarker = unselectableMarker;
                break;
            case MarkerType.Remove:
                originalMarker = removeMarker;
                break;
            case MarkerType.Selected:
                originalMarker = selectedMarker;
                break;
            case MarkerType.Drop:
                originalMarker = dropMarker;
                break;
            case MarkerType.None:
            default:
                break;
        }

        if(originalMarker == null)
        {
            return null;
        }

        return GameObject.Instantiate(originalMarker, parent.transform.position, parent.transform.rotation, parent.transform);
    }
}

