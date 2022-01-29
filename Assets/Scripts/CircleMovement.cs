using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float speed;
    public float forceMag;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 towardPlayer = (mousePosition - gameObject.GetComponent<Rigidbody2D>().position).normalized;
        Vector2 force = forceMag * towardPlayer;

        gameObject.GetComponent<Rigidbody2D>().velocity += force * Time.deltaTime;
    }
}
