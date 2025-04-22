/*****************************************************************************
// File Name : FishHealth.cs
// Author : Dylan M. Webb
// Creation Date : March 27, 2025
//
// Brief Description : This script controls a fish's health.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHealth : MonoBehaviour
{
    //health variable
    [SerializeField] int health;

    //audio variable
    [SerializeField] private AudioClip hit;

    /// <summary>
    /// When the fish is hit by one of the players bubbles they will lose 1 health and once they run out they are
    /// destroyed.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Bubble(Clone)")
        {
            health--;
            AudioSource.PlayClipAtPoint(hit, transform.position);

            if (health < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
