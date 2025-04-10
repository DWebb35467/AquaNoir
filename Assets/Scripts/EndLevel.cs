/*****************************************************************************
// File Name : EndLevel.cs
// Author : Dylan M. Webb
// Creation Date : March 30, 2025
//
// Brief Description : This script is put on objects that will load the next scene on collision will an object.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    /// <summary>
    /// When this is triggered by the player the next scene will be loaded.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
