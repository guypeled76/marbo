using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public PieceType type;

    private void OnMouseDown()
    {
        GetComponentInParent<Position>().SelectPiece(this);
    }
}