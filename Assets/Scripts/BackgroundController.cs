using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private LevelState levelState;
    public GameObject red;
    public GameObject green;
    public GameObject blue;

    private void Start() {
        levelState = GameObject.Find("Game").GetComponent<LevelState>();
    }

    private void Update() {
        if (levelState.dimension == Dimension.RED)
        {
            red.SetActive(true);
            green.SetActive(false);
            blue.SetActive(false);
        }
        else if (levelState.dimension == Dimension.GREEN)
        {
            red.SetActive(false);
            green.SetActive(true);
            blue.SetActive(false);
        }
        else if (levelState.dimension == Dimension.BLUE)
        {
            red.SetActive(false);
            green.SetActive(false);
            blue.SetActive(true);
        }
    }
}
