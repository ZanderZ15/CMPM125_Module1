using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float minutes = .1f;
    // The true default would be 5 minutes but for the sake of testing and demoing, 6 seconds will work
    void Start()
    {
        StartCoroutine(MatchCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MatchCountdown()
    {
        WaitForSeconds gameTime = new WaitForSeconds(60f*minutes);

        yield return gameTime;
        Debug.Log("Oh no! Game over? That was quick huh?");
    }
}
