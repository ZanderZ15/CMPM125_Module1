using UnityEngine;

public class GoalScored : MonoBehaviour
{
    void Start(){}
    void Update(){}
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Ball")) {return;}
        //Debug.Log("GOAL!");
        EventBus.Instance.OnGoalScored(gameObject.tag);
    }
}
