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
                LoadDimension(level.red, "Red", 0);
                LoadDimension(level.green, "Green", 1);
                LoadDimension(level.blue, "Blue", 2);
            } else {
                Debug.LogError("Unsuccessfully read level");
            }
        }
    }

    static void LoadDimension(LevelObject[] dimension, string dimensionName, int dimensionNumber) {
        var scene = EditorSceneManager.GetActiveScene();
        Dictionary<string, GameObject> cachedPrefabs = new Dictionary<string, GameObject>();
        foreach(LevelObject lo in dimension) {
            var prefabName = lo.sprite;
            if (prefabName.Equals("spawn")) {
                var spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
                spawnPoint.transform.position = new Vector2(lo.x, lo.y);
                continue;
            } else if (prefabName.Equals("goal")) {
                var goal = GameObject.FindGameObjectWithTag("Finish");
                goal.transform.position = new Vector2(lo.x, lo.y);
                goal.tag = ("Dimension" + dimensionNumber + "Terrain");
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

        EditorSceneManager.SaveScene(scene);
    }
}