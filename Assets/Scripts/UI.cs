using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides a UI behavior to control the UI
/// </summary>
public class UI : MonoBehaviour
{
    /// <summary>
    /// Event used to open the main menu
    /// </summary>
    public UnityEvent showMainMenu;

    /// <summary>
    /// Event used to open the game menu
    /// </summary>
    public UnityEvent showGameMenu;

    /// <summary>
    /// Holds the list of previously opened UIs so we can implement back
    /// </summary>
    private readonly Stack<GameObject> activeUIHistory = new Stack<GameObject>();

    /// <summary>
    /// The current active UI
    /// </summary>
    public GameObject activeUI;

    public void SetActiveUI(GameObject newActiveUI)
    {
        // Make sure the previuos UI is closed
        if (activeUI != null)
        {
            // Hide current
            activeUI.SetActive(false);
        }

        activeUI = newActiveUI;
    }

    /// <summary>
    /// Shows the main menu
    /// </summary>
    public void ShowMainMenu()
    {
        // Make sure the previuos UI is closed
        if(activeUI != null)
        {
            // Hide current
            activeUI.SetActive(false);
        }

        // Clear the history
        activeUIHistory.Clear();

        // Raise the show main menu event
        showMainMenu.Invoke();
    }

    /// <summary>
    /// Shows the game menu
    /// </summary>
    public void ShowGameMenu()
    {
        // Make sure the previuos UI is closed
        if (activeUI != null)
        {
            // Hide current
            activeUI.SetActive(false);
        }

        // Clear the history
        activeUIHistory.Clear();

        // Raise the show game menu event
        showGameMenu.Invoke();
    }

    /// <summary>
    /// Show the next UI
    /// </summary>
    /// <param name="currentUI"></param>
    /// <param name="nextUI"></param>
    public void ShowNextUI(GameObject nextUI)
    {
        if (nextUI == null)
        {
            return;
        }

        Debug.Log(string.Format("Show the next UI '{0}'.", nextUI.name));

        // If there is a valid current UI
        if (this.activeUI != null)
        { 
            // Remove the current UI
            this.activeUI.SetActive(false);

            // Push current UI to historty
            this.activeUIHistory.Push(this.activeUI);
        }

        // Activate the next UI
        nextUI.SetActive(true);

        // Set active UI
        this.activeUI = nextUI;
    }

    /// <summary>
    /// Closes the current UI and shows the previous UI
    /// </summary>
    /// <param name="currentUI"></param>
    public void ShowPreviousUI(GameObject currentUI)
    {
        if (currentUI != null)
        {
            Debug.Log(string.Format("Close UI '{0}' and show previous if available.", currentUI.name));

            currentUI.SetActive(false);
        }

        if(this.activeUIHistory.Count > 0)
        {
            this.activeUI = this.activeUIHistory.Pop();

            Debug.Log(string.Format("Show previous '{0}'.", this.activeUI.name));

            this.activeUI.SetActive(true);
        }
    }

    /// <summary>
    /// Show the main scene
    /// </summary>
    public void ShowMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Show the board scene
    /// </summary>
    /// <param name="type"></param>
    public void ShowBoard(string type)
    {
        switch (type)
        {
            case "chess":
                SceneManager.LoadScene("ChessScene");
                break;
            case "checkers":
                SceneManager.LoadScene("CheckersScene");
                break;
            case "solitaire":
                SceneManager.LoadScene("SolitaireScene");
                break;
        }
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void Quit()
    {
        Debug.Log("Quiting the game.");

        Application.Quit();
    }
}