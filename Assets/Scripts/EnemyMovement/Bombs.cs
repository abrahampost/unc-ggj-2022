using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour
{

    public GameObject ooze;
    public Animator animator;
    void OnCollisionEnter2D(Collision2D collision) {
        // if (!collision.gameObject.CompareTag("Player")) {
        //     Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        // } else {
        //     collision.gameObject.GetComponent<DamageController>().takeDamage(GetComponent<DamageController>().damage);
        // }

        animator.SetBool("Falling", false);

        if (collision.gameObject.CompareTag("Player")) {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<PolygonCollider2D>());
            collision.gameObject.GetComponent<DamageController>().takeDamage(GetComponent<DamageController>().damage);
        }

        StartCoroutine(CreateOoze());

        // Instantiate(ooze, transform.position, transform.rotation);
        // Destroy(this.gameObject);
    }
    
    IEnumerator CreateOoze() {
        yield return new WaitForSeconds(0.2f);
        Instantiate(ooze, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
