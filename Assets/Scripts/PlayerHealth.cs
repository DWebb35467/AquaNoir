/*****************************************************************************
// File Name : PlayerHealth.cs
// Author : Dylan M. Webb
// Creation Date : March 29, 2025
//
// Brief Description : This script controls the players health, death, and taking damage.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //health variables
    [SerializeField] TMP_Text healthText;
    int health = 100;

    //audio variable
    [SerializeField] private AudioClip hit;

    /// <summary>
    /// When the player is hit by a fish cop bubble they will lose 5 health and updates the health text. 
    /// Once they reach 0 healh the death function runs.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "FishBubble(Clone)")
        {
            health -= 3;
            healthText.text = health.ToString();
            AudioSource.PlayClipAtPoint(hit, transform.position);

            if (health <= 0)
            {
                Death();
            }
        }
    }

    /// <summary>
    /// When the player is touched by an unshrunken pufferfish they will lose 15 health and updates the health text. 
    /// Once they reach 0 healh the death function runs.
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Pufferfish" && collision.transform.localScale == new Vector3 (1, 1, 1))
        {
            health -= 15;
            healthText.text = health.ToString();
            AudioSource.PlayClipAtPoint(hit, transform.position);

            if (health <= 0)
            {
                Death();
            }
        }
    }

    /// <summary>
    /// When this function is ran the current scene is reset.
    /// </summary>
    private void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
