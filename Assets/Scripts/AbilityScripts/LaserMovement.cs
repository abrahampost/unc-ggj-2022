using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public Animator animator;
    public float explosionTime;

    public IEnumerator SetHit() {
        animator.SetBool("Hit", true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(explosionTime);
        if (this.gameObject != null) {
            Destroy(this.gameObject);
        }
    }
}
