using System;

public class Player
{
    /// <summary>
    /// Setting for a single player board
    /// </summary>
    internal static readonly Player[] SinglePlayerArray = new Player[] {
        new Player('p')
    };

    /// <summary>
    /// Setting for two players board with a black and white setting.
    /// </summary>
    internal static readonly Player[] BlackWhiteArray = new Player[] {
        new Player('w'),
        new Player('b')
    };

    /// <summary>
    /// Holds the current player last position
    /// </summary>
    public Position last;

    /// <summary>
    /// The player key used for (FEN format)
    /// </summary>
    public readonly char key;

    /// <summary>
    /// Create a player with a char representing it
    /// </summary>
    /// <param name="key"></param>
    public Player(char key)
    {
        this.key = key;
    }
}
