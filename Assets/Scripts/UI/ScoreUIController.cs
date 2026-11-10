using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private Canvas ui;
    [SerializeField] private GameObject blue_side;
    [SerializeField] private GameObject oran_side;


    void Start()
    {
        EventBus.Instance.GoalScored += UpdateScore;
        EventBus.Instance.Play += StartGame;
    }
    void OnDisable()
    {
        EventBus.Instance.GoalScored -= UpdateScore;
        EventBus.Instance.Play -= StartGame;
    }
    private void StartGame()
    {
        ui.transform.localPosition = Vector3.zero;
    }

    void UpdateScore()
    {
        blue_side.GetComponent<TMPro.TextMeshProUGUI>().text = GameManager.Instance.GetGoals("b").ToString();
        oran_side.GetComponent<TMPro.TextMeshProUGUI>().text = GameManager.Instance.GetGoals("o").ToString();
    }
}
