using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    Stack objects = new();
}

public class ObjectPoolManager : Sigleton<ObjectPoolManager>
{
    private Dictionary<Pool, GameObject> objects = new();

    public void PopObject(GameObject gameObject, Vector3 spawnPos = default, Quaternion rotation = default)
    {

    }

    public void PushObject()
    {

    }
}
