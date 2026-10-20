using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor.SpeedTree.Importer;

public class BallSpawner : MonoBehaviour
{
    private Vector3 spawnLocation = new Vector3(0f, 5f, 0f);
    Quaternion spawnRotation = Quaternion.identity;
    [SerializeField] private GameObject ballprefab;
    [SerializeField] private Material blue_mat;
    [SerializeField] private Material orange_mat;
    [SerializeField] private Material white_mat;
    [SerializeField] private Material black_mat;
    [SerializeField] private Material red_mat;
    [SerializeField] private Material purple_mat;
    [SerializeField] private Material green_mat;

    List<Material> mats;

    private void OnEnable()
    {
        EventBus.Instance.GoalScored += GoalScored;
        
    }
    private void Start()
    {
        mats = new() {blue_mat, orange_mat, 
            white_mat, white_mat, 
            black_mat, black_mat, 
            red_mat, red_mat, red_mat, 
            purple_mat, green_mat};
        SpawnBall();
    }
    public void GoalScored()
    {
        SpawnBall();
    }

    private void SpawnBall()
    {
        Instantiate(ballprefab, spawnLocation, spawnRotation).GetComponent<MeshRenderer>().material = mats[UnityEngine.Random.Range(0, mats.Count)];
    }
    
}
