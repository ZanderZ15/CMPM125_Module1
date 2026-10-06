using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private float speed = 4f;
    private float 

    Rigidbody rigidbody;
    

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>(); //Reads input
        Vector3 move = new Vector3(moveValue.x, 0, moveValue.y); //Turns into Vector3
        gameObject.
        //transform.Translate(move * Time.deltaTime * speed); //Applies movement over time
    
        if (jumpAction.IsPressed())
        {
            rigidbody.AddForce(Vector3 force, ForceMode mode = ForceMode.Force);
        }

    }

    
}
