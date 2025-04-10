/*****************************************************************************
// File Name : Bubble.cs
// Author : Dylan M. Webb
// Creation Date : March 27, 2025
//
// Brief Description : This script controls the bubble projectiles. It will be move in a straight path and
                       destroy itself whenever it hits an object.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //Rigidbody and projecticle speed variables
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float bubbleSpeed;

    /// <summary>
    /// Start is called before the first frame update. This function gives the bubble a slighlty random rotation and
    /// makes it move forward.
    /// </summary>
    void Start()
    {
        transform.Rotate(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.velocity = rb.velocity + (transform.forward * bubbleSpeed);
    }

    /// <summary>
    /// As long as the hit object is not another bubble or its shooter, the bubble will destroy itself.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Detects if the bubble is a player bubble.
        if (transform.name == "Bubble(Clone)")
        {
            if (other.transform.tag != "Player")
            {
                Destroy(gameObject);
            }
        }

        //Detects if the bubble is a fish cop bubble.
        if (transform.name == "FishBubble(Clone)")
        {
            if (other.transform.tag != "FishCop")
            {
                Destroy(gameObject);
            }
        }
    }
}
