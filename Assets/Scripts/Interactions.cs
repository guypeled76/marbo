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
    /// Executes a move command
    /// </summary>
    public void DoMove()
    {
        if (this.HasValidMove && this.HasValidSelection)
        {
            manager.ApplyMove(this.selection.position, this.CurrenMove);
        }
    }

    /// <summary>
    /// Moves the current selection using the navigator
    /// </summary>
    /// <param name="navigator"></param>
    internal void DoMoveSelection(Func<Position,Position> navigator)
    {
        if(selection == null)
        {
            return;
        }

        Position nextPosition = navigator(selection.position);
        if(nextPosition == null)
        {
            return;
        }

        manager.OnSelectPosition(nextPosition);
    }

    /// <summary>
    /// Executes a next command
    /// </summary>
    public void DoNext()
    {
        // Move to next move option
        move++;

        // Return to first if excedded amount of moves
        if (!this.HasValidMove)
        {
            move = 0;
        }

        // Update the current selections
        this.Update();
    }

    /// <summary>
    /// Unselect current selected positions
    /// </summary>
    internal void Remove()
    {
        manager.RemoveSelection(this.Selections);
    }

    /// <summary>
    /// Reselects the positions
    /// </summary>
    internal void Update()
    {
        this.Remove();
        this.Apply();
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
            if(this.HasValidMove)
            {
                selections.AddRange(this.CurrenMove.selections);
            }
            return selections.ToArray();
        }
    }

    /// <summary>
    /// A flag indicating if there is a valid move
    /// </summary>
    /// <returns></returns>
    private bool HasValidMove
    {
        get
        {
            return this.CurrenMove != null;
        }
    }

    /// <summary>
    /// Get current move
    /// </summary>
    private Move CurrenMove
    {
        get
        {
            if(moves != null && move > -1 && move < moves.Length)
            {
                return moves[move];
            }
            return null;
        }
    }

    /// <summary>
    /// Indicates if there is a valid selection
    /// </summary>
    private bool HasValidSelection
    {
        get
        {
            return selection != null;
        }
    }


}

