using Unity.VisualScripting;
using UnityEngine;

public class GoalScored : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Ball")) {return;}
        Debug.Log("GOAL!");
        other.transform.position = new Vector3(0f, 5f, 0f);
        other.attachedRigidbody.linearVelocity = Vector3.zero;
    }
}
