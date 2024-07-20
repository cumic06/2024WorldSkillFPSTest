using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeable
{
    public void TakeItem();
}

public class Item : MonoBehaviour, ITakeable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            TakeItem();
        }
    }

    public void TakeItem()
    {
        
    }
}