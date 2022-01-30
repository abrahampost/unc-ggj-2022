using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugKnightMovement : EnemyMovement
{
    public float readyTime;
    public float dashTime;
    public float pauseTime;
    public float distanceToDash;
    private bool collided = false;

    private enum State {
        WALKING = 0,
        READYING = 1,
        DASHING = 2,
        PAUSING = 3
    }

    void FixedUpdate()
    {
        if (IsStunned()) {
            animator.SetInteger("State", ((int)State.WALKING));
            return;
        }

        // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get Target position
        Vector2 targetPosition = target.GetComponent<Rigidbody2D>().position;
        targetPosition.y += yOffset;

        Vector2 targetVector = targetPosition - gameObject.GetComponent<Rigidbody2D>().position;
        Vector2 deltaVel = speed * targetVector;
        deltaVel.y = 0;

        // Go toward target
        // gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(deltaVel * Time.deltaTime, maxSpeed);
        Vector2 newVel = Vector2.ClampMagnitude(gameObject.GetComponent<Rigidbody2D>().velocity + deltaVel * Time.deltaTime, maxSpeed);

        // Normal walking
        if (animator.GetInteger("State") == ((int)State.WALKING)) {
            if (newVel.magnitude > .01) {
                gameObject.GetComponent<Rigidbody2D>().velocity = newVel / decel;
            } else {
                gameObject.GetComponent<Rigidbody2D>().velocity = newVel;
            }
        }

        // print(targetVector.magnitude);
        // Ready dash attack
        if (targetVector.magnitude < distanceToDash && animator.GetInteger("State") == ((int)State.WALKING)) {
            StartCoroutine(dashAttack());
            animator.SetInteger("State", ((int)State.READYING));
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        
        // Dash Attack
        if (animator.GetInteger("State") == ((int)State.DASHING)) {
            newVel *= 3;
            if (newVel.magnitude > .01) {
                gameObject.GetComponent<Rigidbody2D>().velocity = newVel / decel;
            } else {
                gameObject.GetComponent<Rigidbody2D>().velocity = newVel;
            }
        }
        
        // Pause between attacks
        if (animator.GetInteger("State") == ((int)State.PAUSING)) {
            var y = gameObject.GetComponent<Rigidbody2D>().velocity.y;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, y);
        }

        // Flip sprite
        if (targetVector.x < 0) {
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetFloat("Speed", gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
    }

    IEnumerator dashAttack() {
        yield return new WaitForSeconds(readyTime);
        animator.SetInteger("State", ((int)State.DASHING));

        yield return new WaitForSeconds(dashTime);
        if (!collided) {
            animator.SetInteger("State", ((int)State.PAUSING));
            yield return new WaitForSeconds(pauseTime);
            animator.SetInteger("State", ((int)State.WALKING));
            collided = false;
        }

    }

    IEnumerator OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<DamageController>().takeDamage(GetComponent<DamageController>().damage);

            if (animator.GetInteger("State") == ((int)State.DASHING)) {
                yield return new WaitForSeconds(0.1f);
                animator.SetInteger("State", ((int)State.PAUSING));
                collided = true;

                yield return new WaitForSeconds(pauseTime);
                animator.SetInteger("State", ((int)State.WALKING));
            }
        }
    }


}
