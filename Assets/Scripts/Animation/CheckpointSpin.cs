using UnityEngine;

public class CheckpointSpin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        EventBus.Instance.OnSpinPoint();
    }
}
