using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneSequence : MonoBehaviour
{
    public SequenceExecutor sequenceExecutor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sequenceExecutor.PlaySequences(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
}
