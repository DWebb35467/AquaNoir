/*****************************************************************************
// File Name : Pufferfish.cs
// Author : Dylan M. Webb
// Creation Date : March 29, 2025
//
// Brief Description : This script controls the pufferfish cop's movement and shrink function. THe pufferfish will
                       move to the player if they are within line of sight. On collision witht he player they will
                       be bounced back. When the player shoots the pufferfish it will shrink and stay still for an
                       amount of time.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pufferfish : MonoBehaviour
{
    //player variable
    private GameObject player;

    //movement variables
    [SerializeField] private float speed;
    [SerializeField] private float bounceBack;
    private bool canMove;
    private Rigidbody rb;

    //shrunk variables
    [SerializeField] private float shrunkTime;
    private float shrunkTimer = 0;
    private bool shrunk;

    //Raycast variables
    [SerializeField] private LayerMask layerName;

    /// <summary>
    /// Start is called before the first frame update. It sets all the starting variables values.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
        shrunk = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Starts the bounce back coroutine on collision with the player.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            StartCoroutine(BounceBack());
        }
    }

    /// <summary>
    /// Launches the pufferfish backwards on activation.
    /// </summary>
    /// <returns></returns>
    private IEnumerator BounceBack()
    {
        canMove = false;
        rb.AddForce(transform.forward * -bounceBack);
        yield return new WaitForSeconds(1f);
        canMove = true;
    }

    /// <summary>
    /// Starts the shrink coroutine when the pufferfish is hit by a bubble. Also does the initial shrink.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Bubble(Clone)" )
        {
            shrunkTimer = shrunkTime;

            if (shrunk == false)
            {
                shrunk = true;
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                StartCoroutine(Shrink());
            }
        }
    }

    /// <summary>
    /// Works as a timer for the pufferfish shrink. When the timer runs out the pufferfish will grow back to full size
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shrink()
    {
        while (shrunkTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            shrunkTimer--;

            if (shrunkTimer <= 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                shrunk = false;
            }
        }
    }

    /// <summary>
    /// Update is called once per frame. Controls the movement of the pufferfish. It will face the player and move
    /// towards them. Also freezes the pufferfish when the player is not within sght and if they are shrunk.
    /// </summary>
    void Update()
    {
        //Variables for the direction to the player and the rotation needed to look at the player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        //Variable for hit raycast object
        RaycastHit hit;

        //smoothy rotates the pufferfish to look at the player
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5 * Time.deltaTime);

        //Fires a raycast that will activate the pufferfish moving if the player is in sight
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
            Mathf.Infinity, layerName))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.yellow);

            //when the player is hit by the raycast the pufferfish will begin moving to the player
            if (hit.transform.tag == "Player" && canMove == true)
            {
                //makes the pufferfish move forward
                rb.velocity = transform.forward * speed;
            }
            else if (canMove == true)
            {
                rb.velocity = Vector3.zero;
            }
        }

        if (shrunk == true)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
