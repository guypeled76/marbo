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

    public List<Piece> pieces;

    public void Initialize(Board board)
    {
        pieces = new List<Piece>(this.GetComponentsInChildren<Piece>());

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

    internal void SelectPiece(Piece piece)
    {
        GetComponentInParent<Manager>().SelectPiece(this, piece);
    }

}
