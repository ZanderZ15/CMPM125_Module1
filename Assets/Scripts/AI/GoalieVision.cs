using UnityEngine;

public class GoalieVision : MonoBehaviour
{
    [SerializeField] private GameObject goalie;
    void Update(){}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            goalie.GetComponent<GoalieAI>().SawBall(other.gameObject);
        }
    }
}
