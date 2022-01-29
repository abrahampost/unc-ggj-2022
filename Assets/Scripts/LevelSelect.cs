using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public string levelToLoad = "FirstLevel";
    public void Play() {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Quit() {
        Debug.Log("Exiting...");
    }
    
    // Start is called before the first frame update
    // public void Select(string levelName) {

    // }
}
