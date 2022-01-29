using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        int DistanceAway = 30;
        Vector3 PlayerPOS = GameObject.Find("Player").transform.transform.position;
        GameObject.Find("Main Camera").transform.position = new Vector3(PlayerPOS.x, PlayerPOS.y, PlayerPOS.z - DistanceAway);
        // Temporary vector
        // int x = 0;
        // int y = 0;
        // int z = -30;
        // Vector3 temp = player.transform.position;
        // temp.x = temp.x - x;
        // temp.y = y;
        // temp.z = temp.z - z;
        // Assign value to Camera position
        // transform.position = temp;
        // transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
    }
}
