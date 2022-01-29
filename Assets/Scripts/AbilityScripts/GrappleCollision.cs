using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrappleCollision : MonoBehaviour
{
    public Action<Collision2D, GameObject> handleCollision;

    private void OnCollisionEnter2D(Collision2D other)
    {
        handleCollision(other, gameObject);
    }
}
