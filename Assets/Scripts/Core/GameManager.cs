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
                theInstance = new GameManager();
            
            return theInstance;
        }
    }
    private bool horror_mode = false;
    
    public enum GameState
    { //useful if/when i want to add more formal resets on goals
        INGAME
    }
    public GameState state;
    void Start()
    {
        EventBus.Instance.HorrorButton += HorrorModeToggle;
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
}
