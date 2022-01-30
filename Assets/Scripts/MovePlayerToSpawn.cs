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
        GameObject.Find("Player").transform.position = transform.position + Vector3.up * .2f;
    }
}
