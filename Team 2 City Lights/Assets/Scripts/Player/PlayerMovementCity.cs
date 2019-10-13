﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/Movement Input")]
public class PlayerMovementCity : MonoBehaviour
{
    public float movementSpeed;
    private CharacterController _charController;
    private float sprintMultiplier;
    private bool sprinting;

    // Variation of PlayerMovement that clamps the vertical movement of the player
    void Start()
    {
        sprinting = false;
        _charController = GetComponent<CharacterController>();
        movementSpeed = 10.0f;
        sprintMultiplier = 2.0f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(!sprinting) 
                movementSpeed *= sprintMultiplier;
            sprinting = true;
            CameraFollow.speed = 4.0f;
        }
        else
        {
            movementSpeed = 10f;
            sprinting = false;
        }

        float deltaX = Input.GetAxis("Horizontal") * movementSpeed;
        float deltaZ = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, movementSpeed);
		movement.y = 0f;
        
        //Move the character
		movement *= Time.deltaTime;
        movement.z = Mathf.Clamp(movement.z, -4, 4);
		movement = transform.TransformDirection(movement);
		_charController.Move(movement);
    }
    

}
