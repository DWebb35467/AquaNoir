/*****************************************************************************
// File Name : FishCopSpawner.cs
// Author : Dylan M. Webb
// Creation Date : March 28, 2025
//
// Brief Description : This script spawns a fishcop on game start. Whenever the spawned fish is destoyed a new one
                       will instantly spawn.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishCopSpawner : MonoBehaviour
{
    //Variables for the stored fish cop and the currently cloned fish cop.
    [SerializeField] private GameObject fishCop;
    private GameObject currentCop;

    //respawning variables
    [SerializeField] private float respawnTime;
    private bool isRespawning;

    /// <summary>
    /// At start of game spawns in a fish cop.
    /// </summary>
    void Start()
    {
        currentCop = Instantiate(fishCop, fishCop.transform.position, Quaternion.identity);
        currentCop.SetActive(true);
    }

    /// <summary>
    /// Detects every frame if the fish cop has been destroyed an runs the respawn coroutine.
    /// </summary>
    void Update()
    {
        if (currentCop == null && isRespawning == false)
        {
            isRespawning = true;
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        currentCop = Instantiate(fishCop, fishCop.transform.position, Quaternion.identity);
        currentCop.SetActive(true);
        isRespawning = false;
    }

}
