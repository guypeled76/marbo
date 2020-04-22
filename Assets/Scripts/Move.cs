using System;

public class Move
{
    public static readonly Move[] EmptyArray = new Move[0];

    public readonly Position target;
    public readonly Selection[] selections;

    public Move(Position target, params Selection[] selections)
    {
        this.target = target;
        this.selections = selections;
    }
}
