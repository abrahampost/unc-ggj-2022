using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeeterMovement : EnemyMovement
{
    public GameObject bomb;
    public float bombLifetime;
    public float timeBetweenBombs;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(DropBomb());
        base.getTargets();
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
            // Destroy(newBomb, bombLifetime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<PolygonCollider2D>());
        } else {
            collision.gameObject.GetComponent<DamageController>().takeDamage(GetComponent<DamageController>().damage);
        }
    }
}
