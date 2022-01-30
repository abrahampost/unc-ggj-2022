using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : DamageController
{

    public Image[] hearts;
    public Sprite heart;
    private GameObject healthbar;
    // Start is called before the first frame update
    // void Start()
    // {
    //     healthbar = GameObject.Find("HealthBar");
    // }

    void Update() {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }
}
