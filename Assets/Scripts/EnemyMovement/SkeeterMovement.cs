using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeeterMovement : MonoBehaviour
{
    public float speed;
    public GameObject target;

    // Start is called before the first frame update
    void FixedUpdate()
    {

        // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPosition = target.GetComponent<Rigidbody2D>().position;
        Vector2 targetVector = (targetPosition - gameObject.GetComponent<Rigidbody2D>().position).normalized;
        Vector2 deltaVel = speed * targetVector;

        gameObject.GetComponent<Rigidbody2D>().velocity += deltaVel * Time.deltaTime;
        
        if (targetVector.x < 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        } else {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
