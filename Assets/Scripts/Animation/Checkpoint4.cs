using UnityEngine;

public class Checkpoint4 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        EventBus.Instance.OnPoint4Hit();
    }
}
