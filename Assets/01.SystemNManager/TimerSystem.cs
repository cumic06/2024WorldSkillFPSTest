using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerSystem : MonoBehaviour
{
    private static HashSet<TimeAgent> timeAgents = new();
    private static HashSet<TimeAgent> destroyTimeAgents = new();

    public void Update()
    {
        if (timeAgents.Count > 0)
        {
            UpdateTimeAgent();
            DestoryTimeAgent();
        }
    }

    public static void AddTimer(TimeAgent timeAgent)
    {
        timeAgents.Add(timeAgent);
    }

    public static void UpdateTimeAgent()
    {
        foreach (var timeAgent in timeAgents)
        {
            timeAgent.AddTime(Time.deltaTime);
            timeAgent.UpdateAction?.Invoke(timeAgent);
            if (timeAgent.IsTimeUp)
            {
                timeAgent.EndAction?.Invoke(timeAgent);
            }
        }
    }

    public static void DestoryTimeAgent()
    {
        foreach (var destoryTimeAgent in destroyTimeAgents)
        {
            timeAgents.Remove(destoryTimeAgent);
            destoryTimeAgent.EndAction?.Invoke(destoryTimeAgent);
        }
        destroyTimeAgents.Clear();
    }
}

public class TimeAgent
{
    private float currentTime;
    public float CurrentTime => currentTime;

    private readonly float timerTime;
    public float TimerTime => timerTime;

    public bool IsTimeUp => currentTime >= timerTime;

    public Action<TimeAgent> UpdateAction;
    public Action<TimeAgent> EndAction;

    public TimeAgent(float timerTime, Action<TimeAgent> updateAction, Action<TimeAgent> endAction)
    {
        this.timerTime = timerTime;
        UpdateAction = updateAction;
        EndAction = endAction;
    }

    public void AddTime(float value)
    {
        currentTime += value;
    }
}
