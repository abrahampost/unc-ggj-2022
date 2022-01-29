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
                LoadDimension(level.red, "Red");
                LoadDimension(level.green, "Green");
                LoadDimension(level.blue, "Blue");
            } else {
                Debug.LogError("Unsuccessfully read level");
            }
        }
    }

    static void LoadDimension(LevelObject[] dimension, string dimensionName) {
        var scene = EditorSceneManager.GetActiveScene();
        Dictionary<string, GameObject> cachedPrefabs = new Dictionary<string, GameObject>();
        foreach(LevelObject lo in dimension) {
            var prefabName = lo.sprite;
            cachedPrefabs.TryGetValue(prefabName, out GameObject prefab);
            if (prefab == null) {
                prefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
                if (prefab == null) {
                    throw new System.Exception("No prefab found with name " + prefabName);
                }
                cachedPrefabs.Add(prefabName, prefab);
            }
            var item = Instantiate(prefab, new Vector3(lo.x, lo.y, 0), Quaternion.identity);
            item.tag = "Dimension" + dimensionName + "Terrain";
        }

        EditorSceneManager.SaveScene(scene);
    }
}