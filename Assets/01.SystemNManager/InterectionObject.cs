using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInterectable
{
    public void Intererct();
}

public class InterectionObject : MonoBehaviour, IInterectable
{
    public void Intererct()
    {

    }
}