using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : Ability
{

    public float maxSpeed;
    public float acceleration;
    public float fuelTime;
    public float fuelRechargeRate;
    private float currentFuel;
    private bool isUsing = false;
    private SoundManager soundManager;

    private void Start()
    {
        currentFuel = fuelTime;
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>();
    }

    public override void use()
    {
        if (currentFuel > 0)
        {
            if (!isUsing)
            {
                isUsing = true;
                soundManager.Jetpack();
            }
            var player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
            player.velocity = new Vector2(player.velocity.x, Mathf.Min(player.velocity.y + Time.deltaTime * (acceleration*100f), maxSpeed));
            currentFuel -= Time.deltaTime;
        }
        else
        {
            soundManager.JetpackStop();
        }
    }

    public override void notUsing()
    {
        if (isUsing)
        {
            isUsing = false;
            soundManager.JetpackStop();
        }
        currentFuel = Mathf.Min(currentFuel + Time.deltaTime * fuelRechargeRate, fuelTime);
    }

    public override bool canUse()
    {
        return true;
    }
}
