using System;
using System.Collections.Generic;

public class SolitaireManager : Manager
{

    protected override bool CanSelect(Position position, Piece piece)
    {
        List<Selection> secondarySelections = new List<Selection>();
        FillSecondarySelections(secondarySelections, position);
        return secondarySelections.Count > 0;
    }

    protected override Selection[] GetSecondarySelections(Position position, Piece piece)
    {
        List<Selection> secondarySelections = new List<Selection>();
        FillSecondarySelections(secondarySelections, position);
        return secondarySelections.ToArray();
    }

    private void FillSecondarySelections(List<Selection> selections, Position position)
    {
        if(position == null)
        {
            return;
        }

        FillSecondarySelection(selections, position, (p) => p.right);
        FillSecondarySelection(selections, position, (p) => p.left);
        FillSecondarySelection(selections, position, (p) => p.back);
        FillSecondarySelection(selections, position, (p) => p.forward);
    }

    private void FillSecondarySelection(List<Selection> selections, Position position, Func<Position, Position> navigator)
    {
        Position nextPosition = navigator(position);
        if(nextPosition == null || nextPosition.piece == null)
        {
            return;
        }

        Position dropPosition = navigator(nextPosition);
        if(dropPosition == null || dropPosition.piece != null)
        {
            return;
        }

        selections.Add(new Selection(nextPosition, MarkerType.Remove));
        selections.Add(new Selection(dropPosition, MarkerType.Drop));
    }
}
