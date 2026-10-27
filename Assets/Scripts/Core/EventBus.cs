using UnityEngine;
using System;

public class EventBus 
{
    private static EventBus theInstance;
    public static EventBus Instance
    {
        get
        {
            if (theInstance == null)
                theInstance = new EventBus();
            return theInstance;
        }
    }
    public event Action INGAME;
    public void GamePlaying()
    {
        INGAME?.Invoke();
    }
    public event Action MENU;
    public void OnMenuOpen()
    {
        MENU?.Invoke();
    }
    public event Action GoalScored;
    public void OnGoalScored()
    {
        GoalScored?.Invoke();
    }

    public event Action HorrorButton;
    public void OnHorrorButtonHit()
    {
        HorrorButton?.Invoke();
        
    }
    public event Action EscapeHit;
    public void OnEscapeHit()
    {
        EscapeHit?.Invoke();
        
    }
    public event Action StateChange;
    public void OnStateChange()
    {
        StateChange?.Invoke();
    }
    public event Action Play;
    public void OnPlayGame()
    {
        Play?.Invoke();
    }
    public event Action AudioChange;
    public void OnAudioChange(float volume)
    {
        GameManager.Instance.setAudio(volume);
        AudioChange?.Invoke();
    }
    public event Action FOVChange;
    public void OnFOVChange(float fov)
    {
        GameManager.Instance.setFOV(fov);
        FOVChange?.Invoke();
    }
    

}
