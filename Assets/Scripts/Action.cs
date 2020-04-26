using System;

public class Action
{
    public static readonly Action[] EmptyArray = new Action[0];

    public readonly Position source;
    public readonly ActionType type;
    public readonly Position target;

    public Action(Position source, ActionType type, Position target = null)
    {
        this.source = source;
        this.type = type;
        this.target = target;
    }

    internal static Action[] ToArray(params Action[] actions)
    {
        return actions;
    }
}
