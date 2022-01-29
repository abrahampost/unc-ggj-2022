using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScreen : MonoBehaviour
{
    public void loadlevel(string level) {
        SceneManager.LoadScene(level);
    }
    // Start is called before the first frame update
    // void Start() {
        
    // }
}
