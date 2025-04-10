/*****************************************************************************
// File Name : MenuButtons.cs
// Author : Dylan M. Webb
// Creation Date : March 30, 2025
//
// Brief Description : This script is put on the main menu, end game, and cutscene scenes. It controls entering the 
                       next level, quiting the game, and returning to the main menu.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    /// <summary>
    /// Loads the next scene and locks the cursor for the level.
    /// </summary>
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Quits the game both in build and in editor.
    /// </summary>
    public void QuitGame()
    {
        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
