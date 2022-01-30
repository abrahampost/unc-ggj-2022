using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.ReturnToStart();
    }

    public void ReturnToStart() {
        var levelState = GameObject.Find("Game");
        levelState.GetComponent<LevelState>().ResetTerrain();
        GameObject.Find("Player").transform.position = transform.position + Vector3.up * .2f;
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
