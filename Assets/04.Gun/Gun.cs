using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int tanhwan;
    [SerializeField] private int maxTanhwan;
    [SerializeField] private int tanchang;
    [SerializeField] private int maxTanchang;
    [SerializeField] private float reloadDelay;

    public void Shoot()
    {
        if (IsTanhwanZero())
        {
            ReLoad();
        }
        else
        {

            tanhwan--;
        }
    }

    public void ReLoad()
    {

        tanhwan = maxTanhwan;
        tanchang--;
    }

    private bool IsTanhwanZero()
    {
        return tanhwan <= 0;
    }
}
