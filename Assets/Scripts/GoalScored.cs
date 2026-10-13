using Unity.VisualScripting;
using UnityEngine;

public class GoalScored : MonoBehaviour
{
    void Start(){}
    void Update(){}
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Ball")) {return;}
        Destroy(other.gameObject);
        Debug.Log("GOAL!");

        //other.transform.position = new Vector3(0f, 5f, 0f); // Reset Ball position
        //other.attachedRigidbody.linearVelocity = Vector3.zero; //reset speed
    }
}
