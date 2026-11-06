using UnityEngine;

public class GoalieReturn : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Goalie"))
        {
            other.GetComponent<GoalieAI>().GiveUpBall();
        }
    }
}
