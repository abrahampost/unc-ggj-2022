using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToSpawn : MonoBehaviour
{

    private int iteration = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.ReturnToStart();
        iteration++;
    }

    public void ReturnToStart() {
        // var levelState = GameObject.Find("Game");
        // levelState.GetComponent<LevelState>().ResetTerrain();
        // GameObject.Find("Player").transform.position = transform.position + Vector3.up * .2f;
        // GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        // Reset Enemies and effects
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Dimension0Enemy"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Dimension1Enemy"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Dimension2Enemy"));
        // var dim1enemies = GameObject.FindGameObjectsWithTag("Dimension1Enemy");
        // var dim2enemies = GameObject.FindGameObjectsWithTag("Dimension2Enemy");

        GameObject[] effects = GameObject.FindGameObjectsWithTag("Effect");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        

        foreach (GameObject enemy in enemies) {
            if (iteration > 0) {
                enemy.GetComponent<EnemyMovement>().SendToStart();
            }
        }

        foreach (GameObject effect in effects) {
            Destroy(effect);
        }

        foreach (GameObject bullet in bullets) {
            Destroy(bullet);
        }

        var player = GameObject.Find("Player");
        player.transform.position = transform.position + Vector3.up * .2f;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        var levelState = GameObject.Find("Game").GetComponent<LevelState>();
        levelState.ResetTerrain();
    }
}
