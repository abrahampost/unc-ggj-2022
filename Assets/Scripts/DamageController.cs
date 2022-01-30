using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int health = 3;
    public int damage = 1;
    protected bool onCooldown;
    public float cooldown = .5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damageTaken) {
        if (onCooldown) {
            return;
        }

        this.health -= damageTaken;
        onCooldown = true;
        StartCoroutine(Cooldown(cooldown));

        // print(this.health);
    }

    void dealDamage(DamageController target, int damage) {
        target.takeDamage(damage);
    }

    IEnumerator Cooldown(float time) {
        yield return new WaitForSeconds(time);
        onCooldown = false;
    }
}
