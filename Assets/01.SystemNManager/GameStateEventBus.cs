using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Play,
    Pause
}

public class GameStateEventBus : Sigleton<GameStateEventBus>
{
    private static Dictionary<GameState, Action> gameStates = new();

    public static void Subscribe(GameState gameState, Action onEvent)
    {
        if (!gameStates.ContainsKey(gameState))
        {
            gameStates.Add(gameState, onEvent);
        }
        else
        {
            gameStates[gameState] += onEvent;
        }
    }

    public static void UnSubscribe(GameState gameState, Action onEvent)
    {
        if (gameStates.ContainsValue(onEvent))
        {
            gameStates[gameState] -= onEvent;
        }
    }

    public static void Publish(GameState gameState)
    {
        if (gameStates.ContainsKey(gameState))
        {
            gameStates[gameState]?.Invoke();
        }
    }
}
