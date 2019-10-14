using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/Movement Input")]


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float movementSpeed;
    private CharacterController _charController;
   // private float sprintMultiplier;
   // private bool sprinting;
   // public int stamina = 100;

    void Start()
    {
       // sprinting = false;
        _charController = GetComponent<CharacterController>();
        movementSpeed = 10.0f;
        //sprintMultiplier = 2.0f;
    }

    void Update()
    {
       /* if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(Sprint());
        }
        else
        {
            movementSpeed = 10f;
            sprinting = false;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) || stamina == 0)
            StartCoroutine(RecoverStamina()); */

        float deltaX = Input.GetAxis("Horizontal") * movementSpeed;
        float deltaZ = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, movementSpeed);
		movement.y = 0f;
        
        //Move the character
		movement *= Time.deltaTime;
		movement = transform.TransformDirection(movement);
		_charController.Move(movement);
    }

    
/*IEnumerator Sprint()
    {
        if (stamina > 0)
        {
            stamina--;
            if (!sprinting) { 
                sprinting = true;
                movementSpeed *= sprintMultiplier;
            }
            CameraFollow.speed = 4.0f;
        }
        else
        {
            movementSpeed = 10f;
            sprinting = false;
        }

        yield return null;
    }

    IEnumerator RecoverStamina()
    {
        if (stamina == 0)
            yield return new WaitForSeconds(1.0f);
        while (!Input.GetKey(KeyCode.LeftShift) || stamina == 0)
        {
            if(stamina < 100)
                stamina++;
           yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    } */

}
