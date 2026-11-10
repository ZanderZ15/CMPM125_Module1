using UnityEngine;

public class Checkpoint2 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        EventBus.Instance.OnPoint2Hit();
    }
}
