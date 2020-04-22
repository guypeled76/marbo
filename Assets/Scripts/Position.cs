using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Position : MonoBehaviour
{

    public Position back;
    public Position backLeft;
    public Position backRight;
    public Position left;
    public Position right;
    public Position forward;
    public Position forwardLeft;
    public Position forwardRight;

    public int row;
    public int column;

    public Piece piece;

    public GameObject marker;
    public MarkerType markerType;

    public void Initialize(Board board)
    {
        piece = this.GetComponentInChildren<Piece>();

        Location location = Location.Create(this.name);

        // Initialize the relative positions
        back = board.GetPositionByName(location.back, back);
        backLeft = board.GetPositionByName(location.backLeft, backLeft);
        backRight = board.GetPositionByName(location.backRight, backRight);
        forward = board.GetPositionByName(location.forward, forward);
        forwardRight = board.GetPositionByName(location.forwardRight, forwardRight);
        forwardLeft = board.GetPositionByName(location.forwardLeft, forwardLeft);
        left = board.GetPositionByName(location.left, left);
        right = board.GetPositionByName(location.right, right);

        row = location.row;
        column = location.column;
        
    }

    internal void OnSelectPiece(Piece piece)
    {
        GetComponentInParent<Manager>().OnSelectPosition(this, piece);
    }

    internal void RemoveMarker()
    {
        markerType = MarkerType.None;
        if(marker != null)
        {
            GameObject.Destroy(marker);
        }
    }

    internal void SetMarker(MarkerType markerType, GameObject marker)
    {
        this.marker = marker;
        this.markerType = markerType;
    }

    /// <summary>
    /// Removes the position piece
    /// </summary>
    internal void RemovePiece(bool distroy = true)
    {
        if(piece == null)
        {
            return;
        }

        if (distroy)
        {
            GameObject.Destroy(piece);
        }

        piece = null;
    }

    internal static void MovePiece(Position source, Position target)
    {
        if(source == null || target == null)
        {
            Debug.Log("Inputs are invalid for a move piece action.");
            return;
        }

        Piece piece = source.piece;
        if(piece == null)
        {
            Debug.Log("Source position shoule have a piece a move piece action.");
            return;
        }
        target.RemovePiece(true);
        piece.transform.parent = target.transform;
        target.piece = piece;
        source.RemovePiece(false);


    }
}
