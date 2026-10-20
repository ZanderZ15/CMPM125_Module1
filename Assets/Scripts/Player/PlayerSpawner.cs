using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UIElements;


public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player_prefab;
    [SerializeField] private Material blue_mat;
    [SerializeField] private Material orange_mat;
    Transform b_point;
    Transform o_point;

    void Start()
    {
        //SpawnPlayers();
    }
    void Update()
    {
        
    }
    void OnEnable()
    {
        SpawnPlayers();
    }
    //TODO:
    //SpawnPlayer should be a different kind of function, right now it's doing too much
    //Split it up later
    void SpawnPlayers(int number = 1) //could sub this out for actual player classes in the future
    {
        List<int> players = new List<int>();
        Dictionary<string, int> spawns = new Dictionary<string, int>();
        for (int i = 0; i < number; i++)
        {
            players.Add(i);
        }

        List<string> positions = new() {"left", "center", "right"};
        for (int j = 3; j > 0; j--)
        {
            
            int num = UnityEngine.Random.Range(0, j);
            string pos = positions[num];
            
            b_point = transform.Find("BlueSide").transform.Find("Spawnpoints").transform.Find(pos);
            o_point = transform.Find("OrangeSide").transform.Find("Spawnpoints").transform.Find(pos);
            //TODO:
            //Call a "REAL" spawn player
            //need to figure out rotation based on spawn point later
            Debug.Log(transform.Find(pos));
            GameObject blue = Instantiate(player_prefab, b_point.position, Quaternion.identity);
            GameObject orange = Instantiate(player_prefab, o_point.position, Quaternion.identity);
            

            blue.transform.Find("Player").GetComponent<MeshRenderer>().material = blue_mat;
            orange.transform.Find("Player").GetComponent<MeshRenderer>().material = orange_mat;

            Destroy(orange.transform.Find("Player").GetComponent<PlayerLook>());
            Destroy(orange.transform.Find("Player").GetComponent<Movement>());
            Destroy(orange.transform.Find("Player").transform.Find("PlayerCam").gameObject);

            if (players.Count >= number){break;}

            positions.Remove(pos);
            
        }
    
    }
}
