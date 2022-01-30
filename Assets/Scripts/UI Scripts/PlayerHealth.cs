using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : DamageController
{

    public Image[] hearts;
    public Sprite heart;
    private GameObject healthbar;

    void Update() {
        if (health <= 0) {
            GameObject.Find("SpawnPoint").GetComponent<MovePlayerToSpawn>().ReturnToStart();
            health = 3;
            onCooldown = false;
        }

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
