using System.Collections.Generic;
using UnityEngine;

public class BlockerToggle : MonoBehaviour
{
    [SerializeField] private GameObject blockers_group;
    private Dictionary<string, float> range;
        

    void Start()
    {
        EventBus.Instance.GoalScored += Replace;
        EventBus.Instance.GoalScored += MoveButton;

        range = new Dictionary<string, float>();
        range["x1"] = -26f;
        range["x2"] = 26f;
        range["z1"] = -26f;
        range["z2"] = 26f;
    }
    void OnDisable()
    {
        EventBus.Instance.GoalScored -= Replace;
        EventBus.Instance.GoalScored -= MoveButton;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoNotTrigger")){return;}
        if (blockers_group.activeInHierarchy)
            blockers_group.SetActive(false);
        else
            blockers_group.SetActive(true);
    }

    void Replace()
    {
        blockers_group.SetActive(true);
    }
    void MoveButton()
    {
        gameObject.transform.position = new Vector3(UnityEngine.Random.Range(
            range["x1"], range["x2"]),
            0.6500006f,
            UnityEngine.Random.Range(range["z1"], range["z2"]));
    }
}
