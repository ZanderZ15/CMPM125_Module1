using Unity.VisualScripting;
using UnityEngine;

public class RunCutscene1 : MonoBehaviour
{
    [SerializeField] private GameObject AnimeRoom;
    [SerializeField] private GameObject AnimePlayer;
    [SerializeField] private GameObject AnimeBot;
    private GameObject player;
    private GameObject camera;
    private Rigidbody rb;
    //[SerializeField] GameObject cameraTarget;
    private float acc = 100f;

    private float max_speed = 18f;
    private float jumpPower = 7.5f;
    [SerializeField] private GameObject ballPrefab;
    private GameObject ball;

    private bool scene = false;

    public enum AnimeState
    {
        ZERO,
        FIRST,
        SECOND,
        THIRD,
        FOURTH,
        SPIN,
        FIFTH,
        SIXTH
    }
    private AnimeState state;
    Vector3 spawnLocation;
    Quaternion spawnRotation;
    Vector3 cameraTarget;
    void Start()
    {
        EventBus.Instance.Point1 += Point1;
        EventBus.Instance.Point2 += Point2;
        EventBus.Instance.Point4 += Point4;
        EventBus.Instance.SpinPoint += Spin;

        spawnLocation = AnimePlayer.transform.position;
        spawnLocation.z -= .5f;
        spawnLocation.y += 5f;
        spawnRotation = Quaternion.identity;

        cameraTarget = new Vector3(-75f, 10f, 0f);
    }
    void OnDisable()
    {
        EventBus.Instance.Point1 -= Point1;
        EventBus.Instance.Point2 -= Point2;
        EventBus.Instance.Point4 -= Point4;
        EventBus.Instance.SpinPoint -= Spin;
        
    }
    bool lighted = false;
    bool negative = false;
    Light myLight;
    void Update()
    {
        if (!scene){return;}

        switch (state)
        {
            case AnimeState.ZERO:
                break;
            case AnimeState.FIRST:
                if (ball == null){ball = Instantiate(ballPrefab, spawnLocation, spawnRotation);}
                lookDirection = ball.transform.position - camera.transform.position;
                rb.AddRelativeForce(Vector3.back*acc/4, ForceMode.Acceleration);
                CapSpeed(rb);
                break;
            case AnimeState.SECOND:
                rb.AddRelativeForce(Vector3.forward*acc/4, ForceMode.Acceleration);
                CapSpeed(rb);
                lookDirection = ball.transform.position - camera.transform.position;
                break;
            case AnimeState.THIRD:
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                Point3();
                lookDirection = ball.transform.position - camera.transform.position;
                break;
            case AnimeState.FOURTH:
                rb.AddRelativeForce(Vector3.forward*acc/2, ForceMode.Acceleration);
                CapSpeed(rb);
                lookDirection = ball.transform.position - camera.transform.position;
                break;
            case AnimeState.SPIN:
                rb.transform.Rotate(0f, 90 * Time.deltaTime, 0f);
                lookDirection = ball.transform.position - camera.transform.position;
                break;
            case AnimeState.FIFTH:
                AnimeBot.transform.Find("Body").GetComponent<Rigidbody>().AddForce(Vector3.up*18, ForceMode.Impulse);
                AnimeBot.transform.Rotate(0f, 90 * Time.deltaTime, 0f);
                rb.transform.Rotate(0f, 90 * Time.deltaTime, 0f);
                lookDirection = ball.transform.position - camera.transform.position;
                state = AnimeState.SIXTH;
                break;
            case AnimeState.SIXTH:
                AnimeBot.transform.Rotate(0f, 90 * Time.deltaTime, 0f);
                rb.transform.Rotate(0f, 90 * Time.deltaTime, 0f);
                lookDirection = ball.transform.position - camera.transform.position;
                if (!lighted)
                {
                    myLight = ball.AddComponent<Light>();
                    myLight.type = LightType.Point;
                    myLight.color = Color.white;
                    myLight.intensity = 2.0f;
                    myLight.range = 20.0f;
                    lighted = true;
                } else
                {
                    if (!negative) {
                        myLight.intensity ++;
                        if (myLight.intensity > 100f)
                        {
                            negative = true;
                        }
                    } 
                    else
                    {
                        myLight.intensity --;
                        if (myLight.intensity < 2f)
                        {
                            negative = false;
                        }
                    }

                    
                }

                break;
        }
        TweenCamera();
    }

    void OnTriggerEnter(Collider other)
    {
        EventBus.Instance.OnMenuOpen(); //Should pause all movement
        camera = other.transform.Find("PlayerCam").gameObject;
        player = other.transform.parent.gameObject;

        GameObject body = AnimePlayer.transform.Find("Body").gameObject;
        rb = body.GetComponent<Rigidbody>();
        //camera.transform.SetParent(body.transform, true);
        //camera.transform.position = new Vector3(0, 6, -4);

        

        state = AnimeState.ZERO;

        scene = true;
    }
    Vector3 direction;
    Vector3 lookDirection;
    float speed = 1f;
    float lookSpeed = 10f;
    void TweenCamera()
    {
        //Ideally parabola over and out of the current room and into the box, cut for time
        
        direction = cameraTarget - camera.transform.position;

        if (state == AnimeState.ZERO) {
            lookDirection = AnimePlayer.transform.position - camera.transform.position;
            lookSpeed = 10f;
        }
        else
        {
            lookSpeed = 20f;
        }

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation, targetRotation, lookSpeed * Time.deltaTime);


        if (state == AnimeState.ZERO &&
            camera.transform.position.x < -74f && 
            camera.transform.position.y > 9.5f &&
            camera.transform.position.z < .5f)
        {
            camera.transform.position = cameraTarget;
            state = AnimeState.FIRST;
        } 
        else
        {
            camera.transform.position += direction * speed * Time.deltaTime;
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
    void Point1()
    {
        state = AnimeState.SECOND;
    }
    void Point2()
    {
        state = AnimeState.THIRD;
    }
    void Point3()
    {
        state = AnimeState.FOURTH;
    }
    void Point4()
    {
        state = AnimeState.FIFTH;
    }
    void Spin()
    {
        state = AnimeState.SPIN;
    }
}
