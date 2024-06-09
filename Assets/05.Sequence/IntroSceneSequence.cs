using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneSequence : MonoBehaviour
{
    public SequenceExecutor sequenceExecutor;

    private void Start()
    {
        GameStateEventBus.Publish(GameState.Pause);
        sequenceExecutor.PlaySequences(() => GameStateEventBus.Publish(GameState.Play));
    }
}
