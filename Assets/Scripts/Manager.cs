using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private Board board;

    public string boardState;

    private void Start()
    {
        board = Board.Load(GetComponentsInChildren<Position>());

        boardState = board.ToString();
    }


    /// <summary>
    /// Select position with piece in it.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="piece"></param>
    internal void SelectPiece(Position position, Piece piece)
    {
        Debug.Log(string.Format("Select '{1}' piece in '{0}' position.", position.name, piece.name));
    }
}
