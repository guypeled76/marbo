using System;
using System.Collections.Generic;

public class SolitaireBoard : Board
{
    public SolitaireBoard(Position[] positions) : base(positions, Player.SinglePlayerArray)
    {
    }

    /// <summary>
    /// Get possible moves for current piece
    /// </summary>
    /// <param name="position"></param>
    /// <param name="piece"></param>
    /// <returns></returns>
    public override Move[] GetPossibleMoves(Position position, Piece piece)
    {
        List<Move> moves = new List<Move>();
        FillPossibleMoves(moves, position, piece);
        return moves.ToArray();
    }

    /// <summary>
    /// Add possible moves from current position with the givven piece
    /// </summary>
    /// <param name="moves"></param>
    /// <param name="position"></param>
    /// <param name="piece"></param>
    private void FillPossibleMoves(List<Move> moves, Position position, Piece piece)
    {
        // validate arguments
        if(position == null || Piece.IsEmpty(piece))
        {
            return;
        }

        FillPossibleMove(moves, position, (p) => p.right);
        FillPossibleMove(moves, position, (p) => p.left);
        FillPossibleMove(moves, position, (p) => p.back);
        FillPossibleMove(moves, position, (p) => p.forward);
    }

    /// <summary>
    /// Gets the current move based on the navigator
    /// </summary>
    /// <param name="moves">The existing collected moves.</param>
    /// <param name="position">The current position.</param>
    /// <param name="navigator">The current navigator that defines the move direction.</param>
    private void FillPossibleMove(List<Move> moves, Position position, Func<Position, Position> navigator)
    {
        Position nextPosition = navigator(position);
        if(nextPosition == null || Piece.IsEmpty(nextPosition.piece))
        {
            return;
        }

        Position dropPosition = navigator(nextPosition);
        if(dropPosition == null || !Piece.IsEmpty(dropPosition.piece))
        {
            return;
        }

        // The actions to perfome if move is executed
        Action[] actions = new Action[]
        {
            new Action(nextPosition, ActionType.Remove),
            new Action(position, ActionType.Move, dropPosition)
        };

        moves.Add(new Move(dropPosition, actions , new Selection(dropPosition, MarkerType.Drop)));
    }
}