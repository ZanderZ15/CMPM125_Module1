using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private bool jumpRequested = false;
    private float speed = 40f;
    private float jumpPower = 5f;
    Vector2 move2d;
    Vector3 moveVal;

    private Rigidbody rigidbody;
    

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Move
        move2d = moveAction.ReadValue<Vector2>(); //Reads input
        moveVal = new Vector3(move2d.x, 0, move2d.y); //Turns into Vector3

        if (jumpAction.WasPerformedThisFrame())
        {
            jumpRequested = true;//rigidbody.AddForce(Vector3 force, ForceMode mode = ForceMode.Force);
        }

    }
    void FixedUpdate()
    {
        rigidbody.AddForce(moveVal * speed, ForceMode.Acceleration);
    
        if (jumpRequested)
        {
            // Use Impulse mode for instant bursts like jumps
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            
            jumpRequested = false; // Reset the flag immediately
        }
    }
    

    
}
