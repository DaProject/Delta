using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class CharacterBehaviour : MonoBehaviour
{
    public CharacterController characterController;
    
    public float speed;
    public float jumpSpeed;

    public float gravityMagnitud;

    private bool jump;
    private Vector2 inputAxis;
    public Vector3 moveDirection;

    // Use this for initialization
    void Start ()
    {
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ReadInput();
	}

    void FixedUpdate()
    {
        Vector3 desiredMove = transform.forward * inputAxis.y + transform.right * inputAxis.x;

        if (characterController.isGrounded)
        {
            if (jump)
            {
                jump = false;
                moveDirection.y = jumpSpeed;
            }
        }

        else moveDirection += gravityMagnitud * Physics.gravity * Time.fixedDeltaTime;

        moveDirection.x = desiredMove.x * speed;
        moveDirection.z = desiredMove.z * speed;
        
        //Debug.Log("x: " + moveDirection.x + " z: " + moveDirection.z);

        characterController.Move(moveDirection * Time.fixedDeltaTime);
    }

    void ReadInput()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
    }
}
