using System;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : Manager
{

    protected override Board CreateBoard(Position[] positions)
    {
        return new ChessBoard(positions);
    }
    
}
