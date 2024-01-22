using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Level level = (Level)target;

        if (GUILayout.Button("Построить стены"))
        {
            level.GenerateWalls();
        }
    }
}

[ExecuteInEditMode]
public class Level : MonoBehaviour
{
    public GameObject wallPrefab;
    public int levelSize = 15;

    private GameObject wallsObject;

    public void GenerateWalls()
    {
        Transform existingWalls = transform.Find("Walls");
        if (existingWalls != null)
        {
            DestroyImmediate(existingWalls.gameObject);
        }

        wallsObject = new GameObject("Walls");
        wallsObject.transform.parent = transform;

        GenerateExternalWalls();
        GenerateInternalWalls();
    }

    private void GenerateExternalWalls()
    {
        for (int x = 0; x < levelSize; x++)
        {
            InstantiateWall(new Vector3(x, 0, 0) * 4);
            InstantiateWall(new Vector3(x, 0, -(levelSize - 1)) * 4);
        }

        for (int z = 1; z < levelSize - 1; z++)
        {
            InstantiateWall(new Vector3(0, 0, -z) * 4);
            InstantiateWall(new Vector3(levelSize - 1, 0, -z) * 4);
        }
    }

    private void GenerateInternalWalls()
    {
        for (int x = 1; x < levelSize -1; x++)
        {
            for (int z = 1; z < levelSize - 1; z++)
            {
                if (x%2 == 0 && z%2 == 0)
                {
                    InstantiateWall(new Vector3(x, 0, -z) * 4);
                }
            }
        }
    }

    private void InstantiateWall(Vector3 position)
    {
        GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
        wall.transform.parent = wallsObject.transform;
    }
}