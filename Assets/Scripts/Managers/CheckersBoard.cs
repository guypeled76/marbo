using System;
public class CheckersBoard : Board
{
    public CheckersBoard(Manager manager, Position[] positions) : base(manager, positions, Player.BlackWhiteArray)
    {
    }
}
