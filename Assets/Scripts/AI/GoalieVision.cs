using UnityEngine;

public class GoalieVision : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update(){}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            
        }
    }
}
