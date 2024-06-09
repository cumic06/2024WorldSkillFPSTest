using System;
using UnityEngine;

public class SequenceExecutor : MonoBehaviour
{
    private int currentSequenceIndex;
    public Sequence[] sequences;

    public void PlaySequences(Action endAction)
    {
        foreach (var sequence in sequences)
        {
            if (sequence == null)
            {
                return;
            }

            sequence.StartSequenceData(() =>
            {
                if (IsEndSeqence())
                {
                    endAction?.Invoke();
                }
            });
        }
    }

    private bool IsEndSeqence()
    {
        return ++currentSequenceIndex >= sequences.Length;
    }
}
