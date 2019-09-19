using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/Movement Input")]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float movementSpeed = 10.0f;
    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
      //  if(transform.rotation.x > )
        /*
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                                                            mouse.x,
                                                            mouse.y,
                                                            player.transform.position.y));
        Vector3 forward = mouseWorld - player.transform.position;
        player.transform.rotation = Quaternion.LookRotation(forward, Vector3.up); 

        
        //Find mouse on screen.  Face the mouse
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //transform.LookAt(mousePosition);

        var objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        var directionVector = Input.mousePosition - objectPosition;
        transform.LookAt(new Vector3(0, 0, Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg)); 
        */
        //Calculate how much to move the player


        float deltaX = Input.GetAxis("Horizontal") * movementSpeed;
        float deltaZ = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, movementSpeed);
		movement.y = -1.0f;
        
        //Move the character
		movement *= Time.deltaTime;
		movement = transform.TransformDirection(movement);
		_charController.Move(movement);
	}
}
