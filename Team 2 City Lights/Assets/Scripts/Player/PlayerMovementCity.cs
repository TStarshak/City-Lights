using System.Collections;
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
    private Transform playerTransform;
    // Variation of PlayerMovement that clamps the vertical movement of the player
    void Start()
    {
        sprinting = false;
        _charController = GetComponent<CharacterController>();
        movementSpeed = PlayerState.localPlayerData.movementSpeed;
        sprintMultiplier = 2.0f;
        playerTransform = gameObject.transform;
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

        float deltaX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float deltaZ = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        //Bound the vertical movement of the player
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
        movement = Vector3.ClampMagnitude(movement, movementSpeed);
		movement.y = 0f;

        //Move the character
		movement = transform.TransformDirection(movement);
		_charController.Move(movement);

    }
    

}
