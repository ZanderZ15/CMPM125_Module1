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

        EventBus.Instance.OnGoalScored();
    }
}
