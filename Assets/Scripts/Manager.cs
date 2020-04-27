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

    /// <summary>
    /// The board markers to use to show selections
    /// </summary>
    public GameObject selectedMarker;
    public GameObject unselectableMarker;
    public GameObject removeMarker;
    public GameObject dropMarker;

    /// <summary>
    /// The UI root which contains the UI mono behavior
    /// </summary>
    public GameObject ui;


    /// <summary>
    /// The board empty piece
    /// </summary>
    public GameObject emptyPiece;

    /// <summary>
    /// The game player cameras
    /// </summary>
    public GameObject[] playerCameras;

    public Manager()
    {
        interactions = new Interactions(this);
    }

    /// <summary>
    /// Starts the specific board game
    /// </summary>
    private void Start()
    {
        // Create the game board
        board = CreateBoard(GetComponentsInChildren<Position>());
    }

    /// <summary>
    /// Handles interactions
    /// </summary>
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
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(ui != null)
            {
                ui.GetComponent<UI>().ShowGameMenu();
            }
        }
    }

    /// <summary>
    /// Used to create the current board
    /// </summary>
    /// <param name="positions">The board positions</param>
    /// <returns></returns>
    protected virtual Board CreateBoard(Position[] positions)
    {
        return new Board(this, positions, Player.SinglePlayerArray);
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
    /// Handle the position click event
    /// </summary>
    /// <param name="position">The position that was clicked</param>
    /// <param name="piece">The piece that was clicked</param>
    internal void OnPoistionClicked(Position position, Piece piece)
    {
        // If current position is a valid move from current selection
        if (interactions.SelectMoveIfRelevant(position))
        {
            return;
        }

        OnSelectPosition(position, piece);
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
    /// Create a specific piece
    /// </summary>
    /// <param name="pieceType"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    internal Piece CreatePiece(PieceType pieceType, Position parent)
    {
        Piece originalPiece = GetPiece(pieceType);

        if (originalPiece == null)
        {
            return null;
        }

        return Utils.CloneComponent<Piece>(originalPiece, parent);
    }

    /// <summary>
    /// Get piece that can be cloned
    /// </summary>
    /// <param name="pieceType">The piece type.</param>
    /// <returns></returns>
    protected virtual Piece GetPiece(PieceType pieceType)
    {
        Piece originalPiece = null;

        switch (pieceType)
        {
            case PieceType.Empty:
                originalPiece = emptyPiece.GetComponent<Piece>();
                break;
            default:
                break;
        }

        return originalPiece;
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

        return Utils.CloneObject(originalMarker, parent);
    }

    /// <summary>
    /// Switch the active player camera
    /// </summary>
    /// <param name="player"></param>
    internal void SwitchPlayerCamera(int player)
    {
        // Make sure that there are cameras
        if(playerCameras == null || playerCameras.Length == 0)
        {
            return;
        }

       

        // If is a valid camera index
        if (player > -1 && player < playerCameras.Length)
        {
            Debug.Log(string.Format("Switching to camera {0}", player));
            for (int index = 0; index < playerCameras.Length; index++)
            {
                playerCameras[index].SetActive(index == player);
            }
        }
    }
}

