using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputAction moveAction;
    private float speed = 4f;
    

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>(); //Reads input
        Vector3 move = new Vector3(moveValue.x, 0, moveValue.y); //Turns into Vector3
        transform.Translate(move * Time.deltaTime * speed); //Applies movement over time
    }

    
}
