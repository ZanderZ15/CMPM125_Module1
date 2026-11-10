using UnityEngine;

public class PatrolPointHelper : MonoBehaviour
{
    private int point_num = 0;
    void Start()
    {
        if (!int.TryParse(gameObject.name, out point_num))
        {
            Debug.Log("Make sure the spawnpoint names are ints");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        //Debug.Log(point_num);
        if (other.CompareTag("Goalie"))
        {
            other.GetComponent<GoalieAI>().PointHit(point_num);
        }
    }
    void Update()
    {
        
    }
}
