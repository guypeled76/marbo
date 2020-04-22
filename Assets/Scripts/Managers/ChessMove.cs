using System;

internal class ChessMove : Move
{
    /// <summary>
    /// The validated ChessDotNet move which if this marbo move
    /// will be applied should be applied on the ChessDotNet game object.
    /// </summary>
    public readonly ChessDotNet.Move move;

    internal ChessMove(ChessDotNet.Move move, Position target, Action[] actions, params Selection[] selections) : base(target, actions, selections)
    {
        this.move = move;
    }
}
