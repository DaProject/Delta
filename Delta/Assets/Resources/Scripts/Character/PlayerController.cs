using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerAttack playerAttack;
    public PlayerAnimation playerAnimation;

    public Rigidbody rigidBody;
    public Transform trans;

    // Controller
    [Header("Controller")]
    public float playerSpeed;                         // The speed of the player.
    public float playerDisplacementSpeed;
    public float playerRotation;
    public float playerRotationAux;
    public float moveHorizontal;                    // Variable that gets the horizontal axis value.
    public float moveVertical;                      // Variable that gets the vertical axis value.
    private Vector3 movement;                       // Vector 3 with the values of the movement.


    // Movement
    [Header("Movement")]
    public float turnSmoothing;                     // A smoothing value for turning the player.
    public float speedDampTime;                     // The damping for the speed parameter.

    public float distanceAttack10;
    public float distanceAttack01;
    public float distanceSword10;
    public float distanceChain01;
    public float distanceDash;

    // Use this for initialization
    void Start ()
    {
        playerManager = GetComponent<PlayerManager>();
        playerAttack = GetComponent<PlayerAttack>();
        playerAnimation = GetComponent<PlayerAnimation>();
        rigidBody = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    /*
    public void MoveForward(float speed)
    {
        transform.localPosition += transform.forward * speed * Time.deltaTime;
    }

    public void MoveBackward(float speed)
    {
        transform.localPosition -= transform.forward * speed * Time.deltaTime;
    }

    public void RotateRight()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, playerRotation += playerRotationAux, transform.localRotation.z);
    }

    public void RotateLeft()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, playerRotation -= playerRotationAux, transform.localRotation.z);
    }
    
    public float AnimationDisplacement(float finalPosition, float time)
    {
        Debug.Log("calculating playerDisplacementSpeed");
        return finalPosition / time;
    }
    */
    public void ControllerAction(float speed)
    {
        // TODO: Change the controller behaviour. Use transform.positions insted of rigidbody.velocity.
        moveHorizontal = Input.GetAxis("Horizontal");                       // Takes the horizontal axis.
        moveVertical = Input.GetAxis("Vertical");                           // Takes the vertical axis.
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement.Normalize();
        rigidBody.velocity = movement * speed;
    }

    public void AnimationControllerAction()
    {
        if (moveHorizontal >= 0) transform.Translate(Vector3.right * 5 * Time.deltaTime);
    }

    public void MovementManagement(float horizontal, float vertical)
    {
        //If there is some axis input...
        if (horizontal != 0 || vertical != 0)
        {
            // ...set the players rotation and set the speed paramter to 5.5f
            Rotating(horizontal, vertical);
        }
    }
    public void Rotating(float horizontal, float vertical)
    {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0.0f, vertical);
        // Create a rotation based on this new vector assuming that up is the global axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        // Create a reotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(rigidBody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        //Quaternion rotPhase = Quaternion.AngleAxis (45.0f, Vector3.up);
        //newRotation *= rotPhase;
        // Change the players rotation to this new rotation.
        rigidBody.MoveRotation(newRotation);
    }

    public void Animating(float horizontal, float vertical)
    {
        bool walking = horizontal != 0f || vertical != 0f;
        playerAnimation.anim.SetBool("IsWalking", walking);
    }
}
