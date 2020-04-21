using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides support for managing board selection markers
/// </summary>
public class SelectionContainer
{
    private readonly Manager manager;

    /// <summary>
    /// The current selected positions
    /// </summary>
    private Selection[] selections = Selection.EmptyArray;

    public SelectionContainer(Manager manager)
    {
        this.manager = manager;
    }


    /// <summary>
    /// Unselect current selected positions
    /// </summary>
    internal void Remove()
    {
        manager.RemoveSelection(selections);

        // Clear the selections
        selections = Selection.EmptyArray;
    
    }

    /// <summary>
    /// Applay current selected positions
    /// </summary>
    internal void Apply()
    {
        manager.ApplySelection(selections);
    }

    /// <summary>
    /// Change the current selection
    /// </summary>
    /// <param name="primarySelection">The primary selected position./param>
    /// <param name="secondarySelections">The secondary selected positions.</param>
    internal void Select(Selection primarySelection, params Selection[] secondarySelections)
    {
        this.Remove();

        List<Selection> selectionList = new List<Selection>();
        if(primarySelection != null)
        {
            selectionList.Add(primarySelection);
        }
        if (secondarySelections != null)
        {
            selectionList.AddRange(secondarySelections);
        }
        selections = selectionList.ToArray();

        this.Apply();
    }
}
