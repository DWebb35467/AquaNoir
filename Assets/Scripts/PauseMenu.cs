/*****************************************************************************
// File Name : PauseMenu.cs
// Author : Dylan M. Webb
// Creation Date : March 30, 2025
//
// Brief Description : This script contains all of the button functions in the pause menu. Resuming, Reseting, and 
                       returning to the main menu.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    ///Player variable
    private GameObject player;

    /// <summary>
    /// On game start, sets the player variable.
    /// </summary>
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// Unpauses the game.
    /// </summary>
    public void ResumeLevel()
    {
        Time.timeScale = 1f;
        player.GetComponent<PlayerInput>().currentActionMap.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Resets the current scene and unpauses the game.
    /// </summary>
    public void ResetLevel()
    {
        ResumeLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Loads the main menu scene, unlocks the cursor, and unpauses the game.
    /// </summary>
    public void ReturnToMenu()
    {
        ResumeLevel();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
