using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamerMovement : MonoBehaviour
{
    private Vector3 movement;
    private float speed = 5f;
    void Start(){}

    void Update()
    {
        movement = Vector3.zero;
        if (Keyboard.current != null){
            if (Keyboard.current.wKey.isPressed) //^^^
            {
                movement += new Vector3(0, 0, 1);
            }
            if (Keyboard.current.sKey.isPressed) //vvv
            {
                movement += new Vector3(0, 0, -1);
            }
            if (Keyboard.current.aKey.isPressed) //<<<
            {
                movement += new Vector3(-1, 0, 0);
            }
            if (Keyboard.current.dKey.isPressed) //>>>
            {
                movement += new Vector3(1, 0, 0);
            }

            if (movement.magnitude > 0)
            {
                movement = movement.normalized;
            }

            transform.position += movement * speed * Time.deltaTime;
        }
    }
}
