using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int health = 3;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damageTaken) {
        this.health -= damageTaken;
        // print(this.health);
    }

    void dealDamage(DamageController target, int damage) {
        target.takeDamage(damage);
    }
}
