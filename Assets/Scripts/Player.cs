using System;

public class Player
{
    /// <summary>
    /// Setting for a single player board
    /// </summary>
    internal static Player[] SinglePlayerArray
    {
        get
        {
            return new Player[] {
                new Player(PieceColor.Default)
            };
        }
    }
        
    /// <summary>
    /// Setting for two players board with a black and white setting.
    /// </summary>
    internal static Player[] BlackWhiteArray
    {
        get
        {
            return new Player[] {
                new Player(PieceColor.White),
                new Player(PieceColor.Black)
            };
        }
    }

    /// <summary>
    /// Holds the current player last position
    /// </summary>
    public Position last;

    /// <summary>
    /// The players piece colors
    /// </summary>
    public readonly PieceColor color;

    /// <summary>
    /// Create a player with a char representing it
    /// </summary>
    /// <param name="key"></param>
    public Player(PieceColor color)
    {
        this.color = color;
    }

    /// <summary>
    /// Gets a flag indicating if the position contains a players piece 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    internal static bool IsPlayerPosition(Player player, Position position)
    {
        if(position == null)
        {
            return false;
        }

        return IsPlayerPiece(player, position.piece);
    }

    /// <summary>
    /// Gets a flag indicating if it is a players piece
    /// </summary>
    /// <param name="player"></param>
    /// <param name="piece"></param>
    /// <returns></returns>
    private static bool IsPlayerPiece(Player player, Piece piece)
    {
        if (piece == null)
        {
            return false;
        }

        return ToPieceColor(player) == piece.color;
    }

    /// <summary>
    /// Gets a flag indicating if piece color is related player
    /// </summary>
    /// <param name="player"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    internal static bool IsPieceColor(Player player, PieceColor color)
    {
        return ToPieceColor(player) == color;
    }

    /// <summary>
    /// Get piece color from player
    /// </summary>
    /// <param name="player">The player to get it's piece color</param>
    /// <returns></returns>
    internal static PieceColor ToPieceColor(Player player)
    {
        if(player == null)
        {
            return PieceColor.Default; 
        }

        return player.color;
    }
}
