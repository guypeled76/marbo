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
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            interactions.DoNext();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            interactions.DoMove();
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            interactions.DoMoveSelection(p=>p.forward);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            interactions.DoMoveSelection(p => p.back);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            interactions.DoMoveSelection(p => p.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            interactions.DoMoveSelection(p => p.right);
        }
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
    /// Select a position a primary selection.
    /// </summary>
    /// <param name="position">The position to select.</param>
    internal void OnSelectPosition(Position position)
    {
        OnSelectPosition(position, position?.piece);
    }

    /// <summary>
    /// Select a position a primary selection.
    /// </summary>
    /// <param name="position">The position to select.</param>
    /// <param name="piece">The piece that was requested.</param>
    internal void OnSelectPosition(Position position, Piece piece)
    {
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
        foreach (Selection selection in selections)
        {
            selection.position.SetMarker(selection.marker, CreateMarker(selection.marker, selection.position.gameObject));
        }
    }

    /// <summary>
    /// Apply the move
    /// </summary>
    /// <param name="source">The source position.</param>
    /// <param name="target">The move to apply.</param>
    internal void ApplyMove(Position source, Move move)
    {
        this.OnSelectPosition(board.ApplyMove(source, move));
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

