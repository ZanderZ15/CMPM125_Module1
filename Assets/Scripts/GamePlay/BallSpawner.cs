using UnityEngine;
using System.Collections.Generic;

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
    [SerializeField] private int ballLayer;

    List<Material> mats;

    private GameObject ball;

    private void OnEnable()
    {
        EventBus.Instance.GoalScored += GoalScored;
        mats = new() {blue_mat, orange_mat, 
            white_mat, white_mat, 
            black_mat, black_mat, 
            red_mat, red_mat, red_mat, 
            purple_mat, green_mat};
        
    }
    private void Start()
    {
        SpawnBall();
    }
    private void OnDisable()
    {
        EventBus.Instance.GoalScored -= GoalScored;
    }
    public void GoalScored()
    {
        ResetBall();
    }

    private void SpawnBall()
    {
        ball = Instantiate(ballprefab, spawnLocation, spawnRotation);
        ball.GetComponent<MeshRenderer>().material = mats[UnityEngine.Random.Range(0, mats.Count)];
        ball.layer = ballLayer;
    }
    private void ResetBall()
    {
        ball.GetComponent<Rigidbody>().freezeRotation = true;
        ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        ball.transform.position = spawnLocation;
        ball.GetComponent<MeshRenderer>().material = mats[UnityEngine.Random.Range(0, mats.Count)];
        ball.GetComponent<Rigidbody>().freezeRotation = false;
    }
    
}
