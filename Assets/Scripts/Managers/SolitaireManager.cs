using System;
using System.Collections.Generic;

public class SolitaireManager : Manager
{

    protected override Board CreateBoard(Position[] positions)
    {
        return new SolitaireBoard(this, positions);
    }
}
