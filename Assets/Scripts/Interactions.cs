using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides support for managing board interactions
/// </summary>
public class Interactions
{
    private readonly Manager manager;

    /// <summary>
    /// The current selected position
    /// </summary>
    private Selection selection;

    /// <summary>
    /// The current available moves
    /// </summary>
    private Move[] moves = Move.EmptyArray;

    /// <summary>
    /// The current selected move
    /// </summary>
    private int move;


    public Interactions(Manager manager)
    {
        this.manager = manager;
    }

    /// <summary>
    /// Clear current selection state
    /// </summary>
    private void Clear()
    {
        // Remove selection before clearing
        this.Remove();

        // Clear state
        selection = null;
        moves = Move.EmptyArray;
    }

    /// <summary>
    /// Unselect current selected positions
    /// </summary>
    internal void Remove()
    {
        manager.RemoveSelection(this.Selections);
    }

    /// <summary>
    /// Applay current selected positions
    /// </summary>
    internal void Apply()
    {
        manager.ApplySelection(this.Selections);
    }

    /// <summary>
    /// Change the current selection
    /// </summary>
    /// <param name="selection">The selected position./param>
    /// <param name="secondarySelections">The secondary selected positions.</param>
    internal void Select(Selection selection, params Move[] moves)
    {
        this.Clear();
        this.selection = selection;
        this.moves = moves;
        this.move = 0;
        this.Apply();
    }


    /// <summary>
    /// The current selections
    /// </summary>
    public Selection[] Selections
    {
        get
        {
            List<Selection> selections = new List<Selection>();

            // If there is a selection
            if(selection != null)
            {
                selections.Add(selection);
            }

            // If there is a valid move
            if(moves != null && move > -1 && move < moves.Length)
            {
                selections.AddRange(moves[move].selections);
            }
            return selections.ToArray();
        }
    }
}
