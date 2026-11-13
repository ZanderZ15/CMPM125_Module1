using System.Collections.Generic;
using UnityEngine;

public class GoalieAI : MonoBehaviour
{
    //Something is off with vision/ sawball
    [SerializeField] List<GameObject> patrol_points;
    [SerializeField] private float rayLength = .55f;
    [SerializeField] private LayerMask ballMask;
    [SerializeField] private LayerMask groundMask;
    private GameObject ball;
    private Rigidbody rb;
    private bool paused = true;
    private float max_speed = 18f;
    private float jumpPower = 7.5f;
    private RaycastHit hit_data;
    private bool returning = false;
    Quaternion tiltRotation;
    private enum GoalieState
    {
        IDLE1,
        IDLE2,
        IDLE3,
        IDLE4,
        SEEBALL
    }
    private GoalieState state;
    private GoalieState last_state;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        state = GoalieState.IDLE1;
        EventBus.Instance.StateChange += Pause;
        //ball = GameObject.FindWithTag("Ball");
        float upwardAngle = 25f; 
        tiltRotation = Quaternion.AngleAxis(-upwardAngle, transform.right);
    }

    void FixedUpdate()
    {
        //might be possible to remove this whole switch case and replace with just Drive(patrol_points[state]) somehow
        
        //if (returning && state == GoalieState.SEEBALL) {state = last_state;}
        if (paused) {return;}
        switch (state)
        {
            case GoalieState.IDLE1:
                Drive(patrol_points[0].transform.position, 2);
                break;
            case GoalieState.IDLE2:
                Drive(patrol_points[1].transform.position, 2);
                break;
            case GoalieState.IDLE3:
                Drive(patrol_points[2].transform.position, 2);
                break;
            case GoalieState.IDLE4:
                Drive(patrol_points[3].transform.position, 2);
                break;
            case GoalieState.SEEBALL:
                Drive(ball.transform.position, 1);
                Vector3 angledForwardVector = (tiltRotation * transform.forward) * 5f;
                
                Debug.DrawRay(transform.position, angledForwardVector, Color.red, 2f);
                if(Physics.Raycast(transform.position, angledForwardVector, out hit_data, 5f, ballMask))
                {
                    TryJump();
                }
                break;
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
    private void TryJump()
    { 
        if (Physics.Raycast(transform.position, Vector3.down, rayLength, groundMask))
        {
            if (hit_data.collider.CompareTag("Ball"))
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
        
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
        if (state == GoalieState.SEEBALL){return;}
        returning = false;
        switch (point)
        {
            case 1:
                if (state == GoalieState.IDLE1)
                {
                    state = GoalieState.IDLE2;
                }
                break;
            case 2:
                if (state == GoalieState.IDLE2)
                {
                    state = GoalieState.IDLE3;
                }
                break;
            case 3:
                if (state == GoalieState.IDLE3)
                {
                    state = GoalieState.IDLE4;
                }
                break;
            case 4:
                if (state == GoalieState.IDLE4)
                {
                    state = GoalieState.IDLE1;
                }
                break;
        }
    }
    public void SawBall(GameObject found_ball)
    {
        if (state == GoalieState.SEEBALL || returning) {return;} 
    
        ball = found_ball;
        last_state = state;
        state = GoalieState.SEEBALL;
        //Debug.Log("BRO BALLLLLLLL");
    }
    public void GiveUpBall()
    {
        //Debug.Log("Ahhh, its not worth it");
        state = last_state;
        returning = true;
        
    }
    
}
