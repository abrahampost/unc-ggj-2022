using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Text;
using System.Collections.Generic;

[System.Serializable]
public struct LevelObject {
    public int x;
    public int y;
    public string name;
}

public class LevelData
{
    public LevelObject[] objects;

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        foreach(LevelObject lo in this.objects) {
            sb.AppendLine(string.Format("({}) - {}, {}", lo.name, lo.x, lo.y));
        }
        return sb.ToString();
    }
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
                LoadLevel(level);
            } else {
                Debug.LogError("Unsuccessfully read level");
            }
        }
    }

    static void LoadLevel(LevelData level) {
        var scene = EditorSceneManager.GetActiveScene();
        Dictionary<string, GameObject> cachedPrefabs = new Dictionary<string, GameObject>();
        foreach(LevelObject lo in level.objects) {
            var prefabName = lo.name;
            cachedPrefabs.TryGetValue(prefabName, out GameObject prefab);
            if (prefab == null) {
                prefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
                if (prefab == null) {
                    throw new System.Exception("No prefab found with name " + prefabName);
                }
                cachedPrefabs.Add(prefabName, prefab);
            }
            Instantiate(prefab, new Vector3(lo.x, lo.y, 0), Quaternion.identity);
        }

        EditorSceneManager.SaveScene(scene);
    }
}