using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    None,
    Playin,
    Win,
    Lose
}


public class GameStrateManager
{
    private static State gameState = State.None;

    public static void SetGameState(State state)
    {
        gameState = state;
    }

    public static State GetState()
    {
        return gameState;
    }
}
