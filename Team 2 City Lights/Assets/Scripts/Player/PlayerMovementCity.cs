﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/Movement Input")]

// Variation of PlayerMovement that clamps the vertical movement of the player
public class PlayerMovementCity : MonoBehaviour
{
    private Transform playerTransform;
    private PlayerStatistics playerData;
    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        playerTransform = gameObject.transform;
    }

    void Update()
    {
        if (PauseController.isPaused == false){
            playerData = PlayerState.localPlayerData;

            float deltaX = Input.GetAxis("Horizontal") * playerData.movementSpeed * Time.deltaTime;
            float deltaZ = Input.GetAxis("Vertical") * playerData.movementSpeed * Time.deltaTime;

            // Bound the vertical movement of the player
            float newPosZ = deltaZ + playerTransform.position.z;
            if (newPosZ > 4.0f || newPosZ < -4.0f){
                deltaZ = 0.0f;
            }
            float newPosX = deltaX + playerTransform.position.x;
            // Bound the horizontal movement of the player
            if (newPosX > 25.0f || newPosX < -25.0f){
                deltaX = 0.0f;
            }
            
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, playerData.movementSpeed);
            movement.y = 0f;

            //Move the character
            movement = transform.TransformDirection(movement);
            _charController.Move(movement);
        }
    }
    

}
