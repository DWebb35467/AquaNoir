/*****************************************************************************
// File Name : PlayerController.cs
// Author : Dylan M. Webb
// Creation Date : March 25, 2025
//
// Brief Description : This script contains all of the players input functions, such as movement, camera controls, and
                       shooting.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerController : MonoBehaviour
{
    //Player input action map variable
    [SerializeField] private PlayerInput playerInput;

    //player movement variables
    [SerializeField] private float playerSpeed;
    [SerializeField] private float rotationSpeed;

    //player physics/transform variables
    private Rigidbody rb;
    private Vector3 playerMovement;
    private Vector3 playerRotation;

    //camera variables
    [SerializeField] private GameObject playerCam;
    [SerializeField] private Vector3 camOffset;
    private float camXRotation;
    private float camYRotation;

    //Bubble projectile
    [SerializeField] private GameObject bubble;

    //Shooting variabls
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootDelay;
    private bool isShooting = false;

    //audio variable
    [SerializeField] private AudioClip shoot;

    //pause menu variable
    [SerializeField] private GameObject pauseMenu;

    /// <summary>
    /// Start is called before the first frame update. This function asigns the needed variables, 
    /// enables the player inputs, and locks the cursor to the center of the screen.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput.currentActionMap.Enable();

        //Locks the cursor to the center of the screen and makes it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// This function gets the postion of the mouse and assigns the player rotation value. 
    /// </summary>
    /// <param name="iValue"></param>
    void OnMousePos(InputValue iValue)
    {
        Vector2 inputMouseValue = iValue.Get<Vector2>();
        playerRotation.y = inputMouseValue.x * rotationSpeed;
        playerRotation.x = inputMouseValue.y * rotationSpeed;
    }

    /// <summary>
    /// This function detacts when the player has pressed and let go of the shoot button. When pressed the player will
    /// begin shooting.
    /// </summary>
    void OnShoot()
    {
        if (isShooting == false)
        {
            isShooting = true;
            StartShooting();
        }
        else
        {
            isShooting = false;
        }
    }

    /// <summary>
    /// This function runs the ShootBUbbles corountine to have a constant stream of bubbles with a delay.
    /// </summary>
    void StartShooting()
    {
        StartCoroutine(ShootBubbles());
    }

    /// <summary>
    /// This function constantly spawns bubbles while the player is shooting. A delay is added in between the bubbles.
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootBubbles()
    {
        while (isShooting == true)
        {
            AudioSource.PlayClipAtPoint(shoot, transform.position);
            Instantiate(bubble, shootPoint.position, playerCam.transform.rotation);
            yield return new WaitForSeconds(shootDelay);
        }
    }

    /// <summary>
    /// When ran. This function pauses the timescale of the game, disables the players input, unlocks the cursor,
    /// opens the pause menu, and stops any camera rotation.
    /// </summary>
    void OnPause()
    {
        Time.timeScale = 0;
        playerInput.currentActionMap.Disable();
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);

        playerRotation = new Vector2(0, 0);
    }

    /// <summary>
    /// Update is called once per frame. This function applys the movement and rotation of the player to the game
    /// world. It also controls the camera rotation and locks it to the player.
    /// </summary>
    void Update()
    {
        //Updates the rotation and position of the camera to match the player
        playerCam.transform.position = transform.position + camOffset;

        //These get the values for the camera rotation based on the mouse position and sets a limit for the x rotation
        camXRotation -= playerRotation.x;
        camYRotation -= playerRotation.y * -1;
        camXRotation = Mathf.Clamp(camXRotation, -90f, 90f);

        //Rotates the camera rotation based on the values above
        playerCam.transform.eulerAngles = new Vector3(camXRotation, camYRotation, 0);

        //This gets the movment values based on the players WASD keys
        Vector2 inputMovementValue = playerInput.actions["Move"].ReadValue<Vector2>();

        //This gets the direction the player needs to move in my using the input values and taking into acount the
        //rotation of the player.
        Vector3 moveDirection = (transform.forward * inputMovementValue.y + transform.right * inputMovementValue.x)
            * playerSpeed;

        //This turns the movement direction into velocity for the player.
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

        //This rotates the player on the y axis based on the camera Y rotation.
        transform.eulerAngles = new Vector3(0, camYRotation, 0);
    }
}
