using UnityEngine;

public class HorrorButtonToggle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventBus.Instance.OnHorrorButtonHit();
    }
}