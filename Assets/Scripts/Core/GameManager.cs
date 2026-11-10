using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager theInstance;
    public static GameManager Instance {  get
    {
        if (theInstance == null)
        {
            theInstance = UnityEngine.Object.FindAnyObjectByType<GameManager>();

            if (theInstance == null)
            {
                GameObject go = new GameObject("GameManager");
                theInstance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go); // Optional: Keeps it alive across scenes
            }
        }
        return theInstance;
    }
    }
    private bool horror_mode = false;
    private float audio_volume = .5f;
    private float fov = 60f;
    private int blue_goals = 0;
    private int oran_goals = 0;
    
    public enum GameState
    { 
        MENU,
        PREGAME,
        INGAME,
        GAMEOVER
    }
    public GameState state;
    void Start()
    {
        EventBus.Instance.HorrorButton += HorrorModeToggle;
        EventBus.Instance.EscapeHit += Pause;
        EventBus.Instance.Play += GameStart;
        EventBus.Instance.GameEnd += GameOver;
        state = GameState.PREGAME;
    }

    void Update()
    {
    }
    private void HorrorModeToggle()
    {
        horror_mode = !horror_mode; 
        //Debug.Log(horror_mode);
    }

    public bool GetHorrorMode()
    {
        return horror_mode;
    }
    private void Pause()
    {
        switch (state)
        {
            case GameState.MENU:
                state = GameState.INGAME;
                break;

            case GameState.PREGAME:
                return;

            case GameState.INGAME:
                state = GameState.MENU;
                break;
        }
        EventBus.Instance.OnStateChange();
    }
    private void GameStart()
    {
        state = GameState.INGAME;
        EventBus.Instance.OnStateChange();
    }

    public float getAudio()
    {
        return audio_volume;
    }
    public void setAudio(float vol)
    {
        audio_volume = vol;
    }
    public float getFOV()
    {
        return fov;
    }
    public void setFOV(float fov_in)
    {
        fov = fov_in;
    }
    public void GameOver()
    {
        state = GameState.GAMEOVER;
        EventBus.Instance.OnStateChange();
    }
    public int GetGoals(string b_o)
    {
        switch (b_o)
        {
            case "b":
            return blue_goals;
            case "o":
            return oran_goals;
        }
        return -1;
    }
    public void AddGoal(string side)
    {
        if (side == "Blue"){oran_goals++;}
        else if (side == "Orange"){blue_goals++;}
    }

}
