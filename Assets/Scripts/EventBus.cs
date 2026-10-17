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
    
    public event Action GoalScored;

    public void OnGoalScored()
    {
        GoalScored?.Invoke();
    }

}
