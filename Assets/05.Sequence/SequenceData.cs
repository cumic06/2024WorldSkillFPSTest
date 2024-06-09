using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceData : MonoBehaviour
{
    private SequenceEndAction sequenceEndAction;
    public List<UnityEvent> unityEvents;

    private void Awake()
    {
        sequenceEndAction = GetComponent<SequenceEndAction>();
    }

    public void PlaySequenceEvent(Action onEnd)
    {
        foreach (var unityEvent in unityEvents)
        {
            unityEvent?.Invoke();
        }
        sequenceEndAction.PlayEndAction(onEnd);
    }
}
