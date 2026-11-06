using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UIElements;


public class CarSpawnController : MonoBehaviour
{
    [SerializeField] List<GameObject> blue_cars;
    [SerializeField] List<GameObject> orange_cars;
    Transform b_point;
    Transform o_point;
    void Update()
    {
        
    }
    void OnEnable()
    {
        SpawnPlayers();
    }

    void SpawnPlayers(int max = 1) //could sub this out for actual player classes in the future
    {
        Dictionary<string, int> spawns = new Dictionary<string, int>();

        List<string> positions = new() {"left", "center", "right"};
        for (int i = 0; i < max; i++)
        {
            
            int num = UnityEngine.Random.Range(0, i);
            string pos = positions[num];
            
            b_point = transform.Find("BlueSide").transform.Find("Spawnpoints").transform.Find(pos);
            o_point = transform.Find("OrangeSide").transform.Find("Spawnpoints").transform.Find(pos);
            //TODO:
            //need to figure out rotation based on spawn point later
            blue_cars[i].transform.position = b_point.position;
            blue_cars[i].transform.rotation =  Quaternion.identity;
            orange_cars[i].transform.position = o_point.position;
            orange_cars[i].transform.rotation =  Quaternion.identity;

            positions.Remove(pos);   
        }
    }
}
