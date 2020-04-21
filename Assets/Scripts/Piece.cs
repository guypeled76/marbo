using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public PieceType type;

    private void OnMouseDown()
    {
        GetComponentInParent<Position>().OnSelectPiece(this);
    }

    public static bool IsEmpty(Piece piece)
    {
        return piece == null || piece.type == PieceType.Empty;
    }
}