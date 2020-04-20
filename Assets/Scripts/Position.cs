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

    public List<Piece> pieces;

    public void Initialize(Manager manager)
    {
        pieces = new List<Piece>(this.GetComponentsInChildren<Piece>());

        Location location = Location.Create(this.name);

        // Initialize the relative positions
        back = manager.GetPositionByName(location.back, back);
        backLeft = manager.GetPositionByName(location.backLeft, backLeft);
        backRight = manager.GetPositionByName(location.backRight, backRight);
        forward = manager.GetPositionByName(location.forward, forward);
        forwardRight = manager.GetPositionByName(location.forwardRight, forwardRight);
        forwardLeft = manager.GetPositionByName(location.forwardLeft, forwardLeft);
        left = manager.GetPositionByName(location.left, left);
        right = manager.GetPositionByName(location.right, right);
    }

    internal void SelectPiece(Piece piece)
    {
        GetComponentInParent<Manager>().SelectPiece(this, piece);
    }

}
