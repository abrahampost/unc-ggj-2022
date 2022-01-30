using UnityEngine;
using System.Collections;

public class Gun : Ability
{

    public GameObject bullet;
    public float bulletSpeed;
    public float timeAlive;
    public float startingDistance;
    public float cooldown;
    private bool onCooldown = false;
    private SoundManager soundManager;

    void Start() {
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>();
    }

    public override void use()
    { 
        if (!onCooldown)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            var mousePosition = Camera.main.ScreenToWorldPoint(mousePos);

            GameObject playerObject = GameObject.Find("Player");
            var normalizedDirection = new Vector2(mousePosition.x - playerObject.transform.position.x, mousePosition.y - playerObject.transform.position.y).normalized;
            GameObject newBullet = Instantiate(bullet, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y) + (normalizedDirection * startingDistance), Quaternion.AngleAxis(Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg, Vector3.forward));
            newBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
            StartCoroutine(newBullet.GetComponent<LaserMovement>().setTimeAlive(timeAlive));
            // Destroy(newBullet, timeAlive);

            soundManager.ShootGun();

            onCooldown = true;
            StartCoroutine(Cooldown(cooldown));
        }
    }

    public override bool canUse()
    {
        return true;
    }

    public override void notUsing()
    {

    }

    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        onCooldown = false;
    }
}
