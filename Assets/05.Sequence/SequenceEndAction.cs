using System;
using UnityEngine;

public class SequenceEndAction : MonoBehaviour
{
    public virtual void PlayEndAction(Action onEnd)
    {
        onEnd?.Invoke();
    }
}
