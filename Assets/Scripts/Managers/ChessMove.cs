using System;

public class ChessMove : Move
{
    public readonly ChessDotNet.Move move;

    public ChessMove(ChessDotNet.Move move, Position target, Action[] actions, params Selection[] selections) : base(target, actions, selections)
    {
        this.move = move;
    }
}
