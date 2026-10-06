using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallState : MonoBehaviour
{
    
    [SerializeField] private GameObject ballGroup;
    [SerializeField] private GameObject camera;
    List<GameObject> balls;
    private List<int> selectedBalls;

    void Start()
    {
        balls = new List<GameObject>();
        selectedBalls = new List<int>();
        foreach (Transform child in ballGroup.transform)
        {
            balls.Add(child.gameObject);
        }
        Debug.Log(balls);
        
    }

    void Update()
    {
        
        if (Keyboard.current != null){
            //Ball Selecting
            if (Keyboard.current.numpad1Key.wasPressedThisFrame) //1
                if (selectedBalls.Contains(0))
                    selectedBalls.Remove(0);
                else
                    selectedBalls.Add(0);
                

            if (Keyboard.current.numpad2Key.wasPressedThisFrame) //2
                if (selectedBalls.Contains(1))
                    selectedBalls.Remove(1);
                else
                    selectedBalls.Add(1);

            if (Keyboard.current.numpad3Key.wasPressedThisFrame) //3
                if (selectedBalls.Contains(2))
                    selectedBalls.Remove(2);
                else
                    selectedBalls.Add(2);

            if (Keyboard.current.numpad4Key.wasPressedThisFrame) //4
                if (selectedBalls.Contains(3))
                    selectedBalls.Remove(3);
                else
                    selectedBalls.Add(3);

            //Visibility
            if (Keyboard.current.vKey.wasPressedThisFrame)
            {
                foreach (int ball in selectedBalls)
                {
                    Visibility(balls[ball]);
                }
            }

            //Grow
            if (Keyboard.current.kKey.isPressed)
            {
                foreach (int ball in selectedBalls)
                {
                    Grow(balls[ball]);
                }
            }

            //Shrink
            if (Keyboard.current.lKey.isPressed)
            {
                foreach (int ball in selectedBalls)
                {
                    Shrink(balls[ball]);
                }
            }

            //Camera Controls
            if (Keyboard.current.upArrowKey.isPressed)
            {
                Zoom(camera, 1);
            }
            if (Keyboard.current.downArrowKey.isPressed)
            {
                Zoom(camera, -1);
            }
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                Tilt(camera, -1);
            }
            if (Keyboard.current.rightArrowKey.isPressed)
            {
                Tilt(camera, 1);
            }
                
            
        }
    }

    void Visibility(GameObject ball)
    {
        if (ball.GetComponent<MeshRenderer>().enabled)
                    ball.GetComponent<MeshRenderer>().enabled = false;
                else
                    ball.GetComponent<MeshRenderer>().enabled = true;
    }
    void Grow(GameObject ball)
    {
        float sizing = .005f;
        ball.transform.localScale += new Vector3(sizing, sizing, sizing);
    }
    void Shrink(GameObject ball)
    {
        float sizing = .005f;
        ball.transform.localScale -= new Vector3(sizing, sizing, sizing);
    }
    void Zoom(GameObject cam, int sign)
    {
        float speed = .05f;
        cam.transform.Translate(0, 0, speed*sign);
    }
    void Tilt(GameObject cam, int sign)
    {
        float speed = .5f;
        cam.transform.Rotate(speed*sign, 0, 0);
    }
}
