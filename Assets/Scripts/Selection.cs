using System;
public class Selection
{

    internal static readonly Selection[] EmptyArray = new Selection[0];

    public readonly Position position;
    public readonly MarkerType marker;

    public Selection(Position position, MarkerType marker)
    {
        this.position = position;
        this.marker = marker;
    }
}
