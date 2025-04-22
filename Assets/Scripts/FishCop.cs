/*****************************************************************************
// File Name : FishCop.cs
// Author : Dylan M. Webb
// Creation Date : March 27, 2025
//
// Brief Description : This script controls the fish cop's movement, and shooting. The enemy follows along a path and
                       shoots at the player if they are in line of sight.
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using static UnityEngine.GraphicsBuffer;

public class FishCop : MonoBehaviour
{
    //Movement variables
    [SerializeField] private GameObject[] movePoints;
    [SerializeField] private float speed;
    private int currentIndex;

    //Shooting variables
    [SerializeField] private GameObject fishBody;
    [SerializeField] private LayerMask layerName;

    //Player variables
    [SerializeField] private GameObject bubble;
    [SerializeField] private float shootDelay;
    private GameObject player;
    private bool canShoot;

    //audio variable
    [SerializeField] private AudioClip shoot;

    /// <summary>
    /// Sets all the starting variables on game start.
    /// </summary>
    void Start()
    {
        canShoot = true;
        currentIndex = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Shoots 3 bubbles in quick sucession and waits the shootdelay before the fish can shoot again.
    /// </summary>
    /// <returns></returns>
    private IEnumerator BubbleShoot()
    {
        if (canShoot == true)
        {
            canShoot = false;
            AudioSource.PlayClipAtPoint(shoot, transform.position);
            Instantiate(bubble, fishBody.transform.position, fishBody.transform.rotation);
            yield return new WaitForSeconds(0.1f);
            AudioSource.PlayClipAtPoint(shoot, transform.position);
            Instantiate(bubble, fishBody.transform.position, fishBody.transform.rotation);
            yield return new WaitForSeconds(0.1f);
            AudioSource.PlayClipAtPoint(shoot, transform.position);
            Instantiate(bubble, fishBody.transform.position, fishBody.transform.rotation);
            yield return new WaitForSeconds(shootDelay);
            canShoot = true;
        }
    }

    /// <summary>
    /// Update is called once per frame. This function contains the movement between points and selecting the next
    /// point to move to. Along with this it rotates the fish to look at the player can fires a raycast to see if the
    /// fish should be shooting.
    /// </summary>
    void Update()
    {
        //Variables for the direction to the player and the rotation needed to look at the player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        //Variable for hit raycast object
        RaycastHit hit;

        //checks distance between the statue and the point to move to.
        //if less than 0.1, updates index to the next point
        if (Vector3.Distance(transform.position, movePoints[currentIndex].transform.position) < 0.1f)
        {
            currentIndex++;

            //if current index gets to the end of the array the current index will not change.
            if (currentIndex == movePoints.Length)
            {
                currentIndex--;
            }
        }

        //moves the fish to currently selected point.
        transform.position = Vector3.MoveTowards(transform.position, movePoints[currentIndex].transform.position,
            speed * Time.deltaTime);

        //smoothy rotates the fish to look at the player
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5 * Time.deltaTime);

        //Fires a raycast that will activate the fish cops shooting if the player is in sight
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 
            Mathf.Infinity, layerName))
        {
            Debug.DrawRay(fishBody.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, 
                Color.blue);

            //when the player is hit by the raycast the shoot coroutnine will be activated.
            if (hit.transform.tag == "Player")
            {
                StartCoroutine(BubbleShoot());
            }
        }
    }
}
