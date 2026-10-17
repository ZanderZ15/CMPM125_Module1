using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;

public class BallSpawner : MonoBehaviour
{
    private Vector3 spawnLocation = new Vector3(0f, 5f, 0f);
    Quaternion spawnRotation = Quaternion.identity;
    [SerializeField] private GameObject ballprefab;

    private void OnEnable()
    {
        EventBus.Instance.GoalScored += GoalScored;
        
    }
    private void Start()
    {
        SpawnBall();
    }
    public void GoalScored()
    {
        SpawnBall();
    }

    private void SpawnBall()
    {
        Instantiate(ballprefab, spawnLocation, spawnRotation);
    }
    
}
