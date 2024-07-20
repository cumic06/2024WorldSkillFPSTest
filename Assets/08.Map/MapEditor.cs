using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditor : EditorWindow
{
    private const int gridSize = 15;
    private bool[,] map = new bool[gridSize, gridSize];

    public GameObject curvePrefab;
    public GameObject straightPrefab;
    public GameObject floorPrefab;
    public GameObject parentTransform;

    [MenuItem("Window/Map Editor")]
    public static void ShowWindow()
    {
        GetWindow<MapEditor>("Map Editor");
    }

    private void OnGUI()
    {
        for (int y = 0; y < gridSize; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < gridSize; x++)
            {
                map[x, y] = GUILayout.Toggle(map[x, y], "", GUILayout.Width(20), GUILayout.Height(20));
            }
            GUILayout.EndHorizontal();
        }

        curvePrefab = EditorGUILayout.ObjectField("Curve", curvePrefab, typeof(GameObject), false) as GameObject;
        floorPrefab = EditorGUILayout.ObjectField("Floor", floorPrefab, typeof(GameObject), false) as GameObject;
        straightPrefab = EditorGUILayout.ObjectField("Straight", straightPrefab, typeof(GameObject), false) as GameObject;
        parentTransform = EditorGUILayout.ObjectField("Parent", parentTransform, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("MapGenerate"))
        {
            Spawn();
        }

        if (GUILayout.Button("Clear"))
        {
            for (int y = 0; y < gridSize; y++)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < gridSize; x++)
                {
                    map[x, y] = false;
                }
                GUILayout.EndHorizontal();
            }
        }
    }

    private void Spawn()
    {
        Transform parentPos = Instantiate(parentTransform.transform);

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (map[y, x])
                {
                    if (IsCenter(y, x))
                    {
                        Vector3 position = new(y * -5, -1.6f, x * 5);
                        GameObject spawnObject = Instantiate(curvePrefab, parentPos);
                        spawnObject.transform.position = position;

                    }
                    else
                    {
                        Vector3 position = new(y * -5, 0, x * 5);
                        GameObject spawnObject = Instantiate(floorPrefab, parentPos);
                        spawnObject.transform.position = position;

                        //Vector3 position = new(y * -5, 0, x * 5);
                        //GameObject spawnObject = Instantiate(straightPrefab, parentPos);
                        //spawnObject.transform.position = position;
                    }
                }
            }
        }
    }

    private bool IsCenter(int x, int y)
    {
        if (x != gridSize - 1 && y != gridSize - 1)
        {
            if (map[x + 1, y] && map[x, y + 1])
            {
                return true;
            }
        }

        if (x != gridSize - 1 && y != 0)
        {
            if (map[x + 1, y] && map[x, y - 1])
            {
                return true;
            }
        }

        if (x != 0 && y != gridSize - 1)
        {
            if (map[x - 1, y] && map[x, y + 1])
            {
                return true;
            }
        }

        if (x != 0 && y != 0)
        {
            if (map[x - 1, y] && map[x, y - 1])
            {
                return true;
            }
        }

        return false;
    }
}