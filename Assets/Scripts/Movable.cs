using Unity.VisualScripting;
using UnityEngine;

public class Movable : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        EventBus.Instance.MENU += OnMenuOpened;
        EventBus.Instance.INGAME += OnGamePlaying;
        EventBus.Instance.Play += OnGamePlaying;
        if (GameManager.Instance.state != GameManager.GameState.INGAME)
        {
            OnMenuOpened();
        }
    }

    void Update()
    {}
    private void OnDisable()
    {
        EventBus.Instance.MENU -= OnMenuOpened;
        EventBus.Instance.INGAME -= OnGamePlaying;
        EventBus.Instance.Play -= OnGamePlaying;
    }

    private void OnMenuOpened()
    {
        velocity = rb.linearVelocity;
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;

    } 

    private void OnGamePlaying()
    {
        Debug.Log(velocity);
        rb.linearVelocity = velocity;
        rb.useGravity = true;
    }


}
