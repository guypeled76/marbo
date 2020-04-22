using System;
using System.Collections.Generic;

public class SolitaireBoard : Board
{
    public SolitaireBoard(Position[] positions) : base(positions)
    {
    }

    public override bool CanMove(Position position, Piece piece)
    {
        List<Move> moves = new List<Move>();
        FillSecondarySelections(moves, position);
        return moves.Count > 0;
    }

    public override Move[] GetPossibleMoves(Position position, Piece piece)
    {
        List<Move> moves = new List<Move>();
        FillSecondarySelections(moves, position);
        return moves.ToArray();
    }

    private void FillSecondarySelections(List<Move> moves, Position position)
    {
        if(position == null)
        {
            return;
        }

        FillSecondarySelection(moves, position, (p) => p.right);
        FillSecondarySelection(moves, position, (p) => p.left);
        FillSecondarySelection(moves, position, (p) => p.back);
        FillSecondarySelection(moves, position, (p) => p.forward);
    }

    private void FillSecondarySelection(List<Move> moves, Position position, Func<Position, Position> navigator)
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

        moves.Add(new Move(dropPosition,  new Selection(nextPosition, MarkerType.Remove), new Selection(dropPosition, MarkerType.Drop)));
    }
}