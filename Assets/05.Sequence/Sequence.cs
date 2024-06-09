using System;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{
    private Action onEnd;
    private int currentSequenceDataIndex;
    public List<SequenceData> sequenceDatas;

    public void StartSequenceData(Action onEnd)
    {
        this.onEnd = onEnd;
        if (sequenceDatas.Count <= 0)
        {
            EndSequenceData();
            return;
        }

        PlaySequenceData(sequenceDatas[currentSequenceDataIndex]);
    }


    private void PlaySequenceData(SequenceData sequenceData)
    {
        sequenceData.PlaySequenceEvent(() =>
        {
            if (IsEndSequenceData())
            {
                EndSequenceData();
            }
            else
            {
                PlaySequenceData(sequenceDatas[currentSequenceDataIndex]);
            }
        });
    }

    private bool IsEndSequenceData()
    {
        return ++currentSequenceDataIndex >= sequenceDatas.Count;
    }

    private void EndSequenceData()
    {
        onEnd?.Invoke();
        currentSequenceDataIndex = 0;
    }
}
