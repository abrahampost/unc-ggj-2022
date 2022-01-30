using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeeterMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float decel;
    public GameObject target;
    public float yOffset;
    public GameObject bomb;
    public float bombLifetime;
    public float timeBetweenBombs;
    public Animator animator;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(DropBomb());
        print("Started");
    }
    void FixedUpdate()
    {

        // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get Target position
        Vector2 targetPosition = target.GetComponent<Rigidbody2D>().position;
        targetPosition.y += yOffset;

        Vector2 targetVector = (targetPosition - gameObject.GetComponent<Rigidbody2D>().position);
        Vector2 deltaVel = speed * targetVector;

        // Go toward target
        // gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(deltaVel * Time.deltaTime, maxSpeed);
        Vector2 newVel = Vector2.ClampMagnitude(gameObject.GetComponent<Rigidbody2D>().velocity + deltaVel * Time.deltaTime, maxSpeed);

        if (newVel.magnitude > .01) {
            gameObject.GetComponent<Rigidbody2D>().velocity = newVel / decel;
        } else {
            gameObject.GetComponent<Rigidbody2D>().velocity = newVel;
        }
        
        
        // Flip sprite
        if (targetVector.x < 0) {
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    IEnumerator DropBomb() {
        while (true) {
            yield return new WaitForSeconds(timeBetweenBombs);

            animator.SetBool("IsBombing", true);

            yield return new WaitForSeconds(timeBetweenBombs/3);

            animator.SetBool("IsBombing", false);
            GameObject newBomb = Instantiate(bomb, transform.position, transform.rotation);
            Destroy(newBomb, bombLifetime);
        }
    }
}
