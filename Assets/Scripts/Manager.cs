using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Dictionary<string, Position> positions;

    private void Start()
    {
        this.SynchronizeState();
    }

    internal void SelectPiece(Position position, Piece piece)
    {
        Debug.Log(string.Format("Select '{1}' piece in '{0}' position.", position.name, piece.name));
    }

    public void SynchronizeState()
    {
        positions = GetComponentsInChildren<Position>().ToDictionary(p => p.name);
        positions.Values.ToList().ForEach(p => p.SynchronizeState(positions));
        Debug.Log(string.Format("Starting manager with {0} position and {1} pieces", positions.Count, positions.SelectMany((position) => position.Value.pieces).Count()));
    }
}
