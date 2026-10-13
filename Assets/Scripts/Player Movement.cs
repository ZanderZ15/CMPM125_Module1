using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private bool jumpRequested = false;
    private float acc = 160f;

    private float max_speed = 12.5f;
    private float jumpPower = 5f;
    Vector2 move2d;
    Vector3 moveVal;

    private Rigidbody rigidbody;

    private float rayLength = .55f;
    [SerializeField] private LayerMask groundMask;
    

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
        
        if (jumpAction.WasPerformedThisFrame() && Grounded())
        {
            //Debug.Log("Made it out the check");
            jumpRequested = true;
        }

    }
    void FixedUpdate()
    {
        rigidbody.AddRelativeForce(moveVal * acc, ForceMode.Acceleration);
        CapSpeed(rigidbody);
    
        if (jumpRequested)
        {
            //Debug.Log("Trying so hard to jump");
            // Use Impulse mode for instant bursts like jumps
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            
            jumpRequested = false;
        }
        if (!Grounded() && rigidbody.linearVelocity.y < 0)
        {
            //Debug.Log("AHH IM FALLING");
            // Pulls the player down slightly harder during a fall
            rigidbody.AddForce(Vector3.down * 15f, ForceMode.Acceleration); 
        }
    }
    void CapSpeed(Rigidbody rb)
    {
        Vector3 v_current = rb.linearVelocity;
        Vector3 v_horizontal = new Vector3(v_current.x, 0f, v_current.z);
        if (v_horizontal.magnitude > max_speed)
        {
            v_horizontal = Vector3.ClampMagnitude(v_horizontal, max_speed);
            rb.linearVelocity = new Vector3(v_horizontal.x, v_current.y, v_horizontal.z);
        }
    }    
    bool Grounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red, 0.1f);
        bool tf = Physics.Raycast(transform.position, Vector3.down, rayLength, groundMask);
        //Debug.Log(tf);
        return tf;
    }
}
