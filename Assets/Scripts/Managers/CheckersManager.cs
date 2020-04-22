using System;

public class CheckersManager : Manager
{
    protected override Board CreateBoard(Position[] positions)
    {
        return new CheckersBoard(positions);
    }
}
