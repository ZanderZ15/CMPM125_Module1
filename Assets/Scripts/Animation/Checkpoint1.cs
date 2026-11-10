using UnityEngine;

public class Checkpoint1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        EventBus.Instance.OnPoint1Hit();
    }
}
