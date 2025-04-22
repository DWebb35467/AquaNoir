/*****************************************************************************
// File Name : DetectorAndGate.cs
// Author : Dylan M. Webb
// Creation Date : March 29, 2025
//
// Brief Description : This script controls a connected detector and gate. Whenever an object is witin the detectors
                       collider the gate will be activated. The gate can be set to open or close when the detector is
                       activated.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectorAndGate : MonoBehaviour
{
    //gate variables
    [SerializeField] private bool open;
    [SerializeField] private GameObject gate;

    //detector variables
    private bool activated;

    //object list that contains all objects in the detector
    private List<GameObject> objects = new List<GameObject>();

    //audio variables
    [SerializeField] private AudioClip detectorPress;
    [SerializeField] private AudioClip gateSound;

    /// <summary>
    /// Assigns variables on game start and opens/closes the gate depending on its starting variable.
    /// </summary>
    void Start()
    {
        activated = false;
        gate.SetActive(!open);
    }

    /// <summary>
    /// Adds objects to thee list that enter the detector and runs the CheckObjects function.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        objects.Add(other.gameObject);
        DetectObjects();
    }

    /// <summary>
    /// Removes objects from the list that exit the detector and runs the CheckObjects function.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        objects.Remove(other.gameObject);
        DetectObjects();
    }

    /// <summary>
    /// Every frame this function will detect each object in the list and removes any objects that have been destroyed
    /// </summary>
    void Update()
    {
        //For Future: The first obj is to get each object (It can be named whatever),
        //the => makes the check, obj == null is whats checked
        objects.RemoveAll(obj => obj == null);
        DetectObjects();
    }

    /// <summary>
    /// Detects the amount of objects in the detector and will activate or detactivate the gate depending on if the
    /// detector is activated or not.
    /// </summary>
    private void DetectObjects()
    {
        if (objects.Count > 0)
        {
            if (activated == false)
            {
                AudioSource.PlayClipAtPoint(detectorPress, transform.position);
                open = !open;
                gate.SetActive(!open);
                AudioSource.PlayClipAtPoint(gateSound, gate.transform.position);
                activated = true;
            }
        }

        if (objects.Count == 0)
        {
            if (activated == true)
            {
                open = !open;
                gate.SetActive(!open);
                AudioSource.PlayClipAtPoint(gateSound, gate.transform.position);
                activated = false;
            }
        }
    }
}
