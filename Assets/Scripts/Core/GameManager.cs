using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

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
    
    public enum GameState
    { //useful if/when i want to add more formal resets on goals
        MENU,
        PREGAME,
        INGAME
    }
    public GameState state;
    void Start()
    {
        EventBus.Instance.HorrorButton += HorrorModeToggle;
        EventBus.Instance.EscapeHit += Pause;
        EventBus.Instance.Play += GameStart;
        state = GameState.PREGAME;
    }

    void Update()
    {
    }
    private void HorrorModeToggle()
    {
        horror_mode = !horror_mode; 
        Debug.Log(horror_mode);
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
        //TODO
        state = GameState.INGAME;
        EventBus.Instance.OnStateChange();
    }
}
