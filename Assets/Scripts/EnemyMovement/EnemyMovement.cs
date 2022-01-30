using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float decel;
    protected GameObject target;
    public Animator animator;
    public float yOffset;
    public float xOffset;
    protected Vector2 offset;
    public Vector2 targetVector;
    private bool stunned;
    public float stunTime;
    private Vector2 startPos;
    public float range;

    void Start() {
        getTargets();
    }

    protected void getTargets() {
        startPos = transform.position;
        target = GameObject.Find("Player");
        offset = new Vector2(xOffset, yOffset);
    }
    void Update()
    {
        
    }

    public GameObject getTarget() {
        return target;
    }

    protected bool IsStunned() {
        return stunned;
    }

    protected bool InRange() {
        return targetVector.magnitude < range;
    }

    public void SendToStart() {
        GetComponent<Rigidbody2D>().transform.position = startPos;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void FixedUpdate()
    {
        if (stunned) {
            return;
        }
        // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get Target position
        Vector2 targetPosition = target.GetComponent<Rigidbody2D>().position;
        targetPosition.y += yOffset;

        targetVector = (targetPosition - gameObject.GetComponent<Rigidbody2D>().position);
        Vector2 deltaVel = speed * targetVector;
        deltaVel.y = 0;

        // Go toward target
        // gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(deltaVel * Time.deltaTime, maxSpeed);
        Vector2 newVel = Vector2.ClampMagnitude(gameObject.GetComponent<Rigidbody2D>().velocity + deltaVel * Time.deltaTime, maxSpeed);

        if (newVel.magnitude > .01) {
            gameObject.GetComponent<Rigidbody2D>().velocity = newVel / decel;
        } else {
            gameObject.GetComponent<Rigidbody2D>().velocity = newVel;
        }
        
        if (targetVector.magnitude < offset.magnitude || !InRange()) {
        // if (targetVector.magnitude < offset.magnitude) {
            var y = gameObject.GetComponent<Rigidbody2D>().velocity.y;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, y);
        }
        
        // Flip sprite
        if (targetVector.x < 0) {
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetFloat("Speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<DamageController>().takeDamage(GetComponent<DamageController>().damage);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Bullet")) {
            GetComponent<DamageController>().takeDamage(collider.gameObject.GetComponent<DamageController>().damage);
            StartCoroutine(collider.gameObject.GetComponent<LaserMovement>().SetHit());
            StartCoroutine(Stun());
        }
    }

    protected IEnumerator Stun() {
        stunned = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        animator.SetFloat("Speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));

        yield return new WaitForSeconds(stunTime);
        stunned = false;
    }
}
