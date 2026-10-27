using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float minutes = .1f;
    // The true default would be 5 minutes but for the sake of testing and demoing, 6 seconds will work

    private Coroutine countdownCoroutine;
    
    void Start()
    {
        EventBus.Instance.Play += OnPlay;
    }

    void Update()
    {
        
    }
    void OnDisable()
    {
        EventBus.Instance.Play -= OnPlay;
        StopCoroutine(countdownCoroutine);
    }
    IEnumerator MatchCountdown()
    {
        float seconds_passed = 0f;
        float minutes_in_seconds = minutes*60f;
        while (seconds_passed < minutes_in_seconds)
        {
            yield return new WaitForSeconds(1f);
            seconds_passed++;
        }
        Debug.Log("GAME OVER");
        
    }

    private void OnPlay()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        countdownCoroutine = StartCoroutine(MatchCountdown());
    }
}
