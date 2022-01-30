using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaPunkMovement : EnemyMovement
{
    public GameObject bullet;
    public float bulletSpeed;
    public float timeAlive;
    public float startingDistance;
    public float cooldown;
    private bool onCooldown = false;
    private SoundManager soundManager;

    void Start() {
        getTargets();
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>();
    }

    void Update()
    { 
        if (!onCooldown)
        {
            // Vector3 mousePos = Input.mousePosition;
            // mousePos.z = Camera.main.nearClipPlane;
            // var mousePosition = Camera.main.ScreenToWorldPoint(mousePos);

            // GameObject playerObject = GameObject.Find("Player");
            // Vector2 targetPosition = GetComponent<EnemyMovement>().target.transform.position;
            var normalizedDirection = GetComponent<EnemyMovement>().targetVector.normalized;
            GameObject newBullet = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y) + (normalizedDirection * startingDistance), Quaternion.AngleAxis(Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg, Vector3.forward));
            newBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
            Destroy(newBullet, timeAlive);

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
