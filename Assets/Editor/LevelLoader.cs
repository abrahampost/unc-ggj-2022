using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public struct LevelObject {
    public int x;
    public int y;

    public string sprite;
}

[System.Serializable]
public class LevelData
{
    public LevelObject[] red;
    public LevelObject[] green;
    public LevelObject[] blue;
}

public class LevelLoader : EditorWindow
{
    [MenuItem("LevelEditor/Load Level")]
    static void LoadFile()
    {
        string path = EditorUtility.OpenFilePanel("Level Loader", "", "json");
        if (path.Length != 0)
        {
            byte[] fileContent = File.ReadAllBytes(path);
            string textContent = System.Text.Encoding.UTF8.GetString(fileContent);
            LevelData level = JsonUtility.FromJson<LevelData>(textContent);
            if (level != null) {
                Debug.Log("Successfully read level");
                int lowest = int.MaxValue;
                lowest = Mathf.Min(lowest, LoadDimension(level.red, "Red", 0));
                lowest = Mathf.Min(lowest, LoadDimension(level.green, "Green", 1));
                lowest = Mathf.Min(lowest, LoadDimension(level.blue, "Blue", 2));

                var deathZone = GameObject.FindGameObjectWithTag("DeathZone");
                deathZone.transform.position = new Vector3(deathZone.transform.position.x, lowest - 30, 0);
            } else {
                Debug.LogError("Unsuccessfully read level");
            }
        }
    }

    static int LoadDimension(LevelObject[] dimension, string dimensionName, int dimensionNumber) {
        Dictionary<string, GameObject> cachedPrefabs = new Dictionary<string, GameObject>();
        int lowest = int.MaxValue;
        foreach(LevelObject lo in dimension) {
            var prefabName = lo.sprite;
            lowest = Mathf.Min(lowest, lo.y);
            if (prefabName.Equals("spawn")) {
                var spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
                spawnPoint.transform.position = new Vector2(lo.x, lo.y);
                continue;
            } else if (prefabName.Equals("goal")) {
                var goal = GameObject.FindGameObjectWithTag("Finish");
                goal.transform.position = new Vector2(lo.x, lo.y);
                continue;
            }
            cachedPrefabs.TryGetValue(prefabName, out GameObject prefab);
            if (prefab == null) {
                prefab = Resources.Load<GameObject>(prefabName);
                if (prefab == null) {
                    throw new System.Exception("No prefab found with name " + prefabName);
                }
                cachedPrefabs.Add(prefabName, prefab);
            }
            var item = Instantiate(prefab, new Vector3(lo.x, lo.y, 0), Quaternion.identity);
        }

        return lowest;
    }
}