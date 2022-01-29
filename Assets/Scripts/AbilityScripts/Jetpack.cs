using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : Ability
{

    public float speed;
    public float fuelTime;
    public float fuelRechargeRate;
    private float currentFuel;

    private void Start()
    {
        currentFuel = fuelTime;
    }

    public override void use()
    {
        if (currentFuel > 0)
        {
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity += Vector2.up * (speed*100f) * Time.deltaTime;
            currentFuel -= Time.deltaTime;
        }
    }

    public override void notUsing()
    {
        currentFuel += Mathf.Min(Time.deltaTime * fuelRechargeRate, fuelTime);
    }

    public override bool canUse()
    {
        return true;
    }
}
