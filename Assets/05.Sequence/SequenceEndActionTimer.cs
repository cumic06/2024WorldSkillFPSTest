using System;
using System.Collections;
using UnityEngine;

public class SequenceEndActionTimer : SequenceEndAction
{
    public float actionDelay;

    public override void PlayEndAction(Action onEnd)
    {
        StartCoroutine(EndActionTimer(onEnd));
    }

    private IEnumerator EndActionTimer(Action onEnd)
    {
        yield return new WaitForSeconds(actionDelay);
        onEnd?.Invoke();
    }
}
