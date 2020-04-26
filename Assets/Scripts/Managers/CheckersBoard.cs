using System;
using System.Collections.Generic;

public class CheckersBoard : Board
{
    public CheckersBoard(Manager manager, Position[] positions) : base(manager, positions, Player.BlackWhiteArray)
    {
    }

    public override bool CanMove(Position position, Piece piece)
    {
        return GetValidMoves(position).Length > 0;
    }

    public override Move[] GetPossibleMoves(Position position, Piece piece)
    {
        return GetValidMoves(position);
    }

    /// <summary>
    /// Apply move both to the board and to the chessdotnet game
    /// </summary>
    /// <param name="position">The current position.</param>
    /// <param name="move">The move.</param>
    /// <returns></returns>
    public override Position ApplyMove(Position position, Move move)
    {
        // Apply move and switch the cuerrent player
        return SwitchPlayer(base.ApplyMove(position, move));
    }

    /// <summary>
    /// Get the valid moves from the chess engine
    /// </summary>
    /// <param name="position">The position to check it's move.</param>
    /// <returns></returns>
    private Move[] GetValidMoves(Position position)
    {
        // If is not a position that contains the players piece
        if(!Player.IsPlayerPosition(this.CurrentPlayer, position))
        {
            return Move.EmptyArray;
        }

        List<Move> moves = new List<Move>();

        // If is the "white" player
        if(Player.IsPieceColor(this.CurrentPlayer, PieceColor.White))
        {
            FillValidMoves(moves, position, (p) => p.forwardLeft, (p) => Piece.IsBlack(p));
            FillValidMoves(moves, position, (p) => p.forwardRight, (p) => Piece.IsBlack(p));
        }
        else
        {
            FillValidMoves(moves, position, (p) => p.backLeft, (p) => Piece.IsWhite(p));
            FillValidMoves(moves, position, (p) => p.backRight, (p) => Piece.IsWhite(p));
        }

        return moves.ToArray();
    }

    /// <summary>
    /// Fill the valid moves based on the navigator
    /// </summary>
    /// <param name="moves"></param>
    /// <param name="position"></param>
    /// <param name="navigator"></param>
    /// <param name="isOther"></param>
    private void FillValidMoves(List<Move> moves, Position position, Func<Position, Position> navigator, Func<Piece, bool> isOther)
    {
        Position nextPosition = navigator(position);
        if(nextPosition == null)
        {
            return;
        }

        // If we can move to next position
        if(Piece.IsEmpty(nextPosition.piece))
        {
            Action[] actions = Action.ToArray(new Action(position, ActionType.Move, nextPosition));
            moves.Add(new Move(nextPosition, actions, new Selection(nextPosition, MarkerType.Drop)));
            return;
        }

        // If we can not eat the next piece
        if(!isOther(nextPosition.piece))
        {
            return;
        }

        Position nextNextPosition = navigator(nextPosition);
        if (nextNextPosition == null)
        {
            return;
        }

        // If we can "eat" the next poistion
        if (Piece.IsEmpty(nextNextPosition.piece))
        {
            Action[] actions = Action.ToArray(
                new Action(nextPosition, ActionType.Remove),
                new Action(position, ActionType.Move, nextNextPosition));
            moves.Add(new Move(nextPosition, actions, new Selection(nextNextPosition, MarkerType.Drop)));
            return;
        }
    }
}
