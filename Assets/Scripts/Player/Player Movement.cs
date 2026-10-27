using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction pauseAction;
    private bool jumpRequested = false;
    private float acc = 100f;

    private float max_speed = 18f;
    private float jumpPower = 7.5f;

    private bool paused = true;
    Vector2 move2d;
    Vector3 moveVal;

    private Rigidbody rigidbody;

    private float rayLength = .55f;
    [SerializeField] private LayerMask groundMask;
    

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        pauseAction = InputSystem.actions.FindAction("Pause");
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        EventBus.Instance.StateChange += Pause;
    }

    void Update()
    {
        
        //Move
        move2d = moveAction.ReadValue<Vector2>(); //Reads input
        moveVal = new Vector3(move2d.x, 0, move2d.y); //Turns into Vector3
        
        if (jumpAction.WasPerformedThisFrame() && Grounded())
        {
            jumpRequested = true;
        }
        if (pauseAction.WasPerformedThisFrame())
        {
            EventBus.Instance.OnEscapeHit();
        }

    }
    void FixedUpdate()
    {
        if (paused) {return;}
        rigidbody.AddRelativeForce(moveVal * acc, ForceMode.Acceleration);
        CapSpeed(rigidbody);
    
        if (jumpRequested)
        {
            // Use Impulse mode for jumping
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            
            jumpRequested = false;
        }
        if (!Grounded() && rigidbody.linearVelocity.y < 0)
        {
            // Pulls the player down slightly harder during a fall
            rigidbody.AddForce(Vector3.down * 5f, ForceMode.Acceleration); 
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
    void Pause()
    {
        switch (GameManager.Instance.state)
        {
            case GameManager.GameState.INGAME:
            paused = false;
            break;

            case GameManager.GameState.MENU:
            paused = true;
            break;
        }
    }
}
