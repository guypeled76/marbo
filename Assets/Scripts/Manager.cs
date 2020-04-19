using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<Position> positions;

    private void Start()
    {
        this.SynchronizeState();
    }


    public void SynchronizeState()
    {
        positions = new List<Position>(GetComponentsInChildren<Position>());
        positions.ForEach((position) => position.SynchronizeState());
        Debug.Log(string.Format("Starting manager with {0} position and {1} pieces", positions.Count, positions.SelectMany((position) => position.pieces).Count()));
    }

}
