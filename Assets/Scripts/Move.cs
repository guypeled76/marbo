using System;

public class Move
{
    public static readonly Move[] EmptyArray = new Move[0];

    public readonly Position target;
    public readonly Selection[] selections;
    public readonly Action[] actions;

    public Move(Position target, Action[] actions ,params Selection[] selections)
    {
        this.target = target;
        this.selections = selections;
        this.actions = actions;
    }
}
