using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides support for storing the board state of a chess game
/// </summary>
public class ChessBoard : Board
{
    private ChessDotNet.ChessGame game;

    public ChessBoard(Position[] positions) : base(positions)
    {
        Debug.Log("Initializing chess " + this.ToString());
        game = new ChessDotNet.ChessGame(this.ToString() + " w KQkq - 0 1");
    }


    public override bool CanMove(Position position, Piece piece)
    {
        return GetPossibleMoves(position).Length > 0;
    }

    public override Move[] GetPossibleMoves(Position position, Piece piece)
    {
        return GetPossibleMoves(position);
    }

    private Move[] GetPossibleMoves(Position position)
    {
        if(game == null)
        {
            return Move.EmptyArray;
        }
        List<Move> moves = new List<Move>();

        // Loop all posible moves
        foreach(ChessDotNet.Move move in game.GetValidMoves(ConvertPosition(position)))
        {
            Position newPosition = this.GetPositionByName(move.NewPosition.ToString());
            if(newPosition == null)
            {
                Debug.Log(string.Format("Cannot find a valid position for {0}", move.NewPosition));
                continue;
            }

            moves.Add(new Move(newPosition, Action.EmptyArray, new Selection(newPosition, MarkerType.Drop)));
        }

        return moves.ToArray();
    }


    internal static ChessDotNet.Position ConvertPosition(Position position)
    {
        return new ChessDotNet.Position(position.name);
    }
}
