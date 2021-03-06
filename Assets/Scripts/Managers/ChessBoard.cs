﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides support for storing the board state of a chess game
/// </summary>
public class ChessBoard : Board
{
    /// <summary>
    /// The ChessDotNet game object which implements the chess "brain"
    /// </summary>
    private ChessDotNet.ChessGame game;

    /// <summary>
    /// Create a chess board board managed by ChessDotNet
    /// </summary>
    /// <param name="positions"></param>
    public ChessBoard(Manager manager, Position[] positions) : base(manager, positions, Player.BlackWhiteArray)
    {
        game = new ChessDotNet.ChessGame(this.ToString());
    }

    public override string ToString()
    {
        return base.ToString() + " KQkq - 0 1";
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
        ChessMove chessMove = move as ChessMove;
        if(chessMove == null)
        {
            // Moving requires a valid chess mode that integrates to chessdotnet
            return position;
        }

        // Apply ChessDotnet move
        game.MakeMove(chessMove.move, true);

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

        List<Move> moves = new List<Move>();


        // Loop all posible moves
        foreach (ChessDotNet.Move move in game.GetValidMoves(ConvertPosition(position)))
        {
            FillValidMove(moves, position, move);
        }

        return moves.ToArray();
    }

    /// <summary>
    /// Fill marbo moves list with ChessDotNet moves
    /// </summary>
    /// <param name="moves"></param>
    /// <param name="position"></param>
    /// <param name="move"></param>
    private void FillValidMove(List<Move> moves, Position position, ChessDotNet.Move move)
    {
        // Get marbo position from ChessDotNet move
        Position newPosition = this.GetPositionByName(ConvertToPositionName(move.NewPosition));
        if (newPosition == null)
        {
            return;
        }

        // The actions to perfome if move is executed
        Action[] actions = new Action[]
        {
            new Action(position, ActionType.Move, newPosition)
        };

        moves.Add(new ChessMove(move, newPosition, actions, new Selection(newPosition, MarkerType.Drop)));
    }

    /// <summary>
    /// Convert ChessDotNet position to marbo position name
    /// </summary>
    /// <param name="position">The ChessDotNet position</param>
    /// <returns></returns>
    private string ConvertToPositionName(ChessDotNet.Position position)
    {
        return position.ToString().ToLower();
    }

    /// <summary>
    /// Convert marbo position to chess dotnet position
    /// </summary>
    /// <param name="position">The marbo position.</param>
    /// <returns></returns>
    internal static ChessDotNet.Position ConvertPosition(Position position)
    {
        return new ChessDotNet.Position(position.name);
    }
}
