using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    private Vector3 spawnLocation = new Vector3(0f, 5f, 0f);
    Quaternion spawnRotation = Quaternion.identity;
    [SerializeField] private GameObject ballprefab;

    void Start(){Instantiate(ballprefab, spawnLocation, spawnRotation);}
    void Update(){}

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(ballprefab, spawnLocation, spawnRotation);
    }
}
