using System;
using UnityEngine;

public class EventManager : MonoSingletonGeneric<EventManager>
{
    public delegate void Executable(int s);
    public static event Executable Collected;
    public static event Executable StreakUpdate;

    public delegate void Exec();
    public static event Exec GameOver;

    public void CollectEvent(int sc)
    {
        Collected?.Invoke(sc);
    }

    public void GameOverEvent()
    {
        GameOver?.Invoke();
    }

    public void StreakEvent(int st)
    {
        StreakUpdate?.Invoke(st);
    }
}
