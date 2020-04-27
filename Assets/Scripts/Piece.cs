using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public PieceType type;
    public PieceColor color;

    private void OnMouseDown()
    {
        GetComponentInParent<Position>().OnPieceClicked(this);
    }

    public static bool IsEmpty(Piece piece)
    {
        return piece == null || piece.type == PieceType.Empty;
    }

    internal static bool IsWhite(Piece p)
    {
        return p != null && p.color == PieceColor.White;
    }

    internal static bool IsBlack(Piece p)
    {
        return p != null && p.color == PieceColor.Black;
    }
}