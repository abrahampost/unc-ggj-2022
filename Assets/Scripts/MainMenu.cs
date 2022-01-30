using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // public string levelToLoad = "Level Select";
    public void Play() {
        Debug.Log("Play");
        SceneManager.LoadScene("Level01");
    }

    public void Menu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void LevelScreen() {
        SceneManager.LoadScene("Level Select");
    }

    public void Settings() {
        SceneManager.LoadScene("Settings");
    }

    public void Quit() {
        Debug.Log("Quit");
    }
}
