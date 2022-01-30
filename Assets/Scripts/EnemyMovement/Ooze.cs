using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ooze : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<DamageController>().takeDamage(GetComponent<DamageController>().damage);
        }
    }
}
