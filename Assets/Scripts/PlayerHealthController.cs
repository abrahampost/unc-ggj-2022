using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : DamageController
{
    private GameObject healthbar;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = GameObject.Find("HealthBar");
    }
}
