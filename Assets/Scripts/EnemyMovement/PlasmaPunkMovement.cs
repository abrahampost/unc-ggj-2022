using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaPunkMovement : EnemyMovement
{
    public GameObject bullet;
    public float bulletSpeed;
    public float timeAlive;
    public float startingDistance;
    // public Vector2 offset;
    public float cooldown;
    private bool onCooldown = false;
    private SoundManager soundManager;
    // private Vector2 gunSpawn;

    void Start() {
        getTargets();
        // gunSpawn = GameObject.Find("GunEnd").transform.position;
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>();
    }

    void Update()
    { 
        if (!onCooldown && !IsStunned())
        {
            // Vector3 mousePos = Input.mousePosition;
            // mousePos.z = Camera.main.nearClipPlane;
            // var mousePosition = Camera.main.ScreenToWorldPoint(mousePos);

            // GameObject playerObject = GameObject.Find("Player");
            // Vector2 targetPosition = GetComponent<EnemyMovement>().target.transform.position;
            Vector2 gunSpawn = GameObject.Find("GunEnd").transform.position;
            Vector2 targetPosition = GetComponent<EnemyMovement>().getTarget().transform.position;
            Vector2 normalizedDirection = (targetPosition - gunSpawn).normalized;
            // var normalizedDirection = (GetComponent<EnemyMovement>().targetVector - gunSpawn).normalized;
            GameObject newBullet = Instantiate(bullet, gunSpawn + (normalizedDirection * startingDistance), Quaternion.AngleAxis(Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg, Vector3.forward));
            newBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
            StartCoroutine(newBullet.GetComponent<LaserMovement>().setTimeAlive(timeAlive));
            // Destroy(newBullet, timeAlive);

            soundManager.ShootGun();

            onCooldown = true;
            StartCoroutine(Cooldown(cooldown));
        }
    }

    // public override bool canUse()
    // {
    //     return true;
    // }

    // public override void notUsing()
    // {

    // }

    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        onCooldown = false;
    }
}
