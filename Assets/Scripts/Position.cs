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

    public void Initialize()
    {
        pieces = new List<Piece>(this.GetComponentsInChildren<Piece>());
    }

    internal void SelectPiece(Piece piece)
    {
        GetComponentInParent<Manager>().SelectPiece(this, piece);
    }

}
