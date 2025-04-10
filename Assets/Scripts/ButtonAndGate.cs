/*****************************************************************************
// File Name : ButtonAndGate.cs
// Author : Dylan M. Webb
// Creation Date : March 29, 2025
//
// Brief Description : This script controls a connected button and gate. The button activates when shot and will open
                       or close a gate. The button resets after a couple seconds after being shot. The button can also
                       be set to be able to activate from player shots or fish cop shots.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAndGate : MonoBehaviour
{
    //variables for button and gate states
    [SerializeField] private bool forPlayer;
    [SerializeField] private bool open;

    //button timer variables
    private int timer;
    private bool isRunning;

    //gate object variable
    [SerializeField] private GameObject gate;

    /// <summary>
    /// At the start of the game opens/closes the gate depending on its starting variable.
    /// </summary>
    void Start()
    {
        gate.SetActive(!open);
    }

    /// <summary>
    /// Depending on if the button has been set for the player or not. When the button is hit by a bubble its timer
    /// will be set to 5.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        if (forPlayer == true)
        {
            if (other.transform.name == "Bubble(Clone)")
            {
                timer = 5;
                StartTimer();
            }
        }
        else
        {
            if (other.transform.name == "FishBubble(Clone)")
            {
                timer = 5;
                StartTimer();
            }
        }
    }

    /// <summary>
    /// Starts the timer and activates the button as long as its not currently running.
    /// </summary>
    void StartTimer()
    {
        if (isRunning == false)
        {
            isRunning = true;
            open = !open;
            gate.SetActive(!open);
            StartCoroutine(ButtonTimer());
        }
    }

    /// <summary>
    /// Counts down the timer by 1 every second. When the timer runs out the button will deactivated.
    /// </summary>
    /// <returns></returns>
    IEnumerator ButtonTimer()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;

            if (timer <= 0)
            {
                open = !open;
                gate.SetActive(!open);
                isRunning = false;
            }
        }
    }
}
