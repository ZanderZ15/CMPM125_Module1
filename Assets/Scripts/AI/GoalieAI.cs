using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GoalieAI : MonoBehaviour
{
    [SerializeField] List<GameObject> patrol_points;
    private GameObject ball;
    private Rigidbody rb;
    private bool paused = true;
    private float max_speed = 18f;
    private enum GoalieState
    {
        IDLE1,
        IDLE2,
        IDLE3,
        IDLE4,
        SEEBALL
    }
    private GoalieState state;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        state = GoalieState.IDLE1;
        EventBus.Instance.StateChange += Pause;
        ball = GameObject.FindWithTag("Ball");
    }

    void FixedUpdate()
    {
        if (paused) {return;}
        switch (state)
        {
            case GoalieState.IDLE1:
                Drive(patrol_points[0].transform.position, 2);
                return;
            case GoalieState.IDLE2:
                Drive(patrol_points[1].transform.position, 2);
                return;
            case GoalieState.IDLE3:
                Drive(patrol_points[2].transform.position, 2);
                return;
            case GoalieState.IDLE4:
                Drive(patrol_points[3].transform.position, 2);
                return;
            case GoalieState.SEEBALL:
                Drive(ball.transform.position, 1);
                return;

        }
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

    void Drive(Vector3 target, int modifier)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0; 

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100f * Time.deltaTime);
        }
        rb.AddRelativeForce(Vector3.forward*25f, ForceMode.Acceleration);
        CapSpeed(rb, modifier);
    }

    void CapSpeed(Rigidbody rb, int modifier)
    {
        Vector3 v_current = rb.linearVelocity;
        Vector3 v_horizontal = new Vector3(v_current.x, 0f, v_current.z);
        if (v_horizontal.magnitude > max_speed/modifier)
        {
            v_horizontal = Vector3.ClampMagnitude(v_horizontal, max_speed/modifier);
            rb.linearVelocity = new Vector3(v_horizontal.x, v_current.y, v_horizontal.z);
        }
    }    
    
    public void PointHit(int point)
    {
        switch (point)
        {
            case 1:
                state = GoalieState.IDLE2;
                break;
            case 2:
                state = GoalieState.IDLE3;
                break;
            case 3:
                state = GoalieState.IDLE4;
                break;
            case 4:
                state = GoalieState.IDLE1;
                break;
        }
    }
    public void SawBall()
    {
        state = GoalieState.SEEBALL;
        Debug.Log("BRO BALLLLLLLL");
    }
    //Idle: Drive around near the goal
    //See ball: drive at it and jump at it
    //Should be only forwards and rotate code (god save me)
    
    //Have a trigger in scene that is too far from the goal that onexit causes it to return to idling
}
