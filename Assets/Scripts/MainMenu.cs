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

    public void Quit() {
        Debug.Log("Quit");
    }

    public void Level1() {
        SceneManager.LoadScene("Level01");
    }

    public void Level2() {
        SceneManager.LoadScene("Level02");
    }

    public void Level3() {
        SceneManager.LoadScene("Level03");
    }

    public void Level4() {
        SceneManager.LoadScene("Level04");
    }
}
