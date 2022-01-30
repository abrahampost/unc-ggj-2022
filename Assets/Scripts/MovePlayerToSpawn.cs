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
        var player = GameObject.Find("Player");
        player.transform.position = transform.position + Vector3.up * .2f;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        var levelState = GameObject.Find("Game").GetComponent<LevelState>();
        levelState.ResetTerrain();
    }
}
