using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Board
{

    private readonly Dictionary<string, Position> positionsByName = new Dictionary<string, Position>();


    private readonly int columns;
    private readonly int rows;
    private readonly Position[,] positions;

    /// <summary>
    /// Creates an empty board
    /// </summary>
    public Board() : this(new Position[0])
    {

    }

    /// <summary>
    /// Creates a board based on the positions.
    /// </summary>
    /// <param name="positions">The board positions.</param>
    public Board(Position[] positions)
    {
        // Index the positions by name to allow for position navigation
        this.positionsByName = positions.ToDictionary(p => p.name);

        // Inizialize all positions and calculate board size
        foreach (Position position in positions)
        {

            position.Initialize(this);

            rows = Math.Max(rows, position.row);
            columns = Math.Max(columns, position.column);
        }

        
        Debug.Log(string.Format("Creating {0}X{1} board.", rows, columns));

        // Create the board position metrix
        this.positions = new Position[rows, columns];

        

        // Index positions by location
        foreach (Position position in positions)
        {
            this.positions[position.row - 1, position.column - 1] = position;
        }
    }

    /// <summary>
    /// Get a position by it's name and allow for default if not found
    /// </summary>
    /// <param name="name">The position name.</param>
    /// <param name="defaultPosition">The default value to return if position cannot be found.</param>
    /// <returns></returns>
    public Position GetPositionByName(string name, Position defaultPosition = null)
    {
        if (positionsByName.TryGetValue(name, out Position position))
        {
            return position;
        }
        return defaultPosition;
    }

    /// <summary>
    /// Get the posible moves
    /// </summary>
    /// <param name="position">The primary position.</param>
    /// <param name="piece">The primary piece.</param>
    /// <returns></returns>
    public virtual Move[] GetPossibleMoves(Position position, Piece piece)
    {
        return Move.EmptyArray;
    }

    /// <summary>
    /// Check if the current piece can move.
    /// </summary>
    /// <param name="position">The position that the piece is in.</param>
    /// <param name="piece">The piece that is move is checked for.</param>
    /// <returns></returns>
    public virtual bool CanMove(Position position, Piece piece)
    {
        return (GetPossibleMoves(position, piece)?.Length ?? 0) > 0;
    }

    /// <summary>
    /// Applies the move
    /// </summary>
    /// <param name="position">The position to move.</param>
    /// <param name="move">The move data.</param>
    /// <returns></returns>
    public virtual Position ApplyMove(Position position, Move move)
    {
        ApplyActions(move.actions);
        return move.target;
    }

    /// <summary>
    /// Apply board actions
    /// </summary>
    /// <param name="actions">The actions to apply</param>
    private void ApplyActions(Action[] actions)
    {
        if(actions == null)
        {
            return;
        }

        foreach(Action action in actions)
        {
            ApplyAction(action);
        }
    }

    /// <summary>
    /// Apply board action
    /// </summary>
    /// <param name="action">The action to apply.</param>
    private void ApplyAction(Action action)
    {
        if(action == null)
        {
            return;
        }

        Debug.Log(string.Format("Applying action {0} on source='{1}' target='{2}'", action.type, action.source?.name, action.target?.name));

        switch (action.type)
        {
            case ActionType.Remove:
                action.source.RemovePiece();
                break;
            case ActionType.Move:
                Position.MovePiece(action.source, action.target);
                break;
        }
    }

    /// <summary>
    /// Generates fen string noration (Forsyth–Edwards Notation)
    /// </summary>
    /// <returns></returns>
    /// <seealso cref="https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation"/>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            FillRowString(builder, rowIndex);
        }

        return builder.ToString();
    }

    

    /// <summary>
    /// Fills the string builder with the current row content.
    /// </summary>
    /// <param name="builder">The current builder.</param>
    /// <param name="rowIndex">The current row index with in the board.</param>
    private void FillRowString(StringBuilder builder, int rowIndex)
    {
        if (rowIndex > 0)
        {
            builder.Append('/');
        }

        int emptyCount = 0;

        for (int columnIndex = 0; columnIndex < columns; columnIndex++)
        {
            FillColumnString(builder, rowIndex, columnIndex, ref emptyCount);
        }

        // If we have empty columns at the end of the row
        FillEmptyColumnString(builder, ref emptyCount);
    }

    /// <summary>
    /// Fills the string builder with the current column content.
    /// </summary>
    /// <param name="builder">The current builder.</param>
    /// <param name="rowIndex">The current row index with in the board.</param>
    /// <param name="columnIndex"></param>
    /// <param name="emptyCount">The amount of empty positions encountered untill now.</param>
    private void FillColumnString(StringBuilder builder, int rowIndex, int columnIndex, ref int emptyCount)
    {

        Debug.Log(string.Format("Printing r{0}Xc{1} of board.", rowIndex, columnIndex));

        // Even if we are missing a position we are still working as if we have a rectangle
        Position position = positions[rowIndex, columnIndex];
        if (position == null)
        {
            emptyCount++;
            return;
        }

        // Get piece at the specific position
        Piece piece = position.piece;
        if (Piece.IsEmpty(piece))
        {
            emptyCount++;
            return;
        }

        FillEmptyColumnString(builder, ref emptyCount);

        // Make sure type is defined
        if (Enum.IsDefined(typeof(PieceType), piece.type))
        {
            builder.Append((char)piece.type);
        }
        else
        {
            Debug.LogError(string.Format("Position '{0}' has an undifined piece type in '{1}' piece.", position.name, piece.name));
            builder.Append('?');
        }
    }

    /// <summary>
    /// Adds a number representing the amount of empty columns we encountered untill now and resets the counter.
    /// </summary>
    /// <param name="builder">The current builder.</param>
    /// <param name="emptyCount">The amount of empty positions encountered untill now.</param>
    private void FillEmptyColumnString(StringBuilder builder, ref int emptyCount)
    {
        if (emptyCount > 0)
        {
            builder.Append(emptyCount);
            emptyCount = 0;
        }
    }
}
