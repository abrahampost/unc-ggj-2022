using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{

    public GameObject ooze;

    void Start() {
        StartCoroutine(CreateOoze());
    }
    void OnCollisionEnter2D(Collision2D collision) {
        // if (!collision.gameObject.CompareTag("Player")) {
        //     Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        // } else {
        //     collision.gameObject.GetComponent<DamageController>().takeDamage(GetComponent<DamageController>().damage);
        // }

        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<DamageController>().takeDamage(GetComponent<DamageController>().damage);
        }
    }

    IEnumerator CreateOoze() {
        yield return new WaitForSeconds(0.2f);
        print(this.gameObject);
        Instantiate(ooze, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
