using System;
using System.Collections.Generic;
using UnityEngine;

public class Selection
{
    private Manager manager;

    private Position primarySelection;

    private  Position[] secondarySelections;

    public Selection(Manager manager)
    {
        this.manager = manager;
    }

    internal void Clear()
    {
        manager.RemoveSelectionIndications(primarySelection, secondarySelections);

        primarySelection = null;
        secondarySelections = null;
    }

    internal void Apply()
    {
        manager.ApplySelectionIndications(primarySelection, secondarySelections);
    }

    internal void Select(Position primaryPosition, Position[] secondaryPositions)
    {
        this.Clear();
        primarySelection = primaryPosition;
        secondarySelections = secondaryPositions;
        this.Apply();
    }
}
