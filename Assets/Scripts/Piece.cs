using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public PieceType type;

    private void OnMouseDown()
    {
        Debug.Log(string.Format("Piece clicked on {0}",  gameObject.name));

        GetComponentInParent<Position>().SelectPiece(this);
    }
}