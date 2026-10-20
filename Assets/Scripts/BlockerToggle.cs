using System;
using UnityEngine;

public class BlockerToggle : MonoBehaviour
{
    [SerializeField] private GameObject blockers_group;

    void OnTriggerEnter(Collider other)
    {
        if (blockers_group.activeInHierarchy)
            blockers_group.SetActive(false);
        else
            blockers_group.SetActive(true);
    }
}
