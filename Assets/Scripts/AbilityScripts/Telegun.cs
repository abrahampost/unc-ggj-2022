using UnityEngine;
using System.Collections;

public class Telegun : Ability
{

    public GameObject bullet;
    public float bulletSpeed;
    public float startingDistance;
    private bool inUse;
    private GameObject currentBullet;
    public float cooldown;
    private bool onCooldown = false;
    private SoundManager soundManager;

    private void Start()
    {
        inUse = false;
        currentBullet = null;
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>();
    }

    public override void use()
    {
        if (!inUse && !onCooldown)
        {
            GameObject playerObject = GameObject.Find("Player");
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            var mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            var normalizedDirection = new Vector2(mousePosition.x - playerObject.transform.position.x, mousePosition.y - playerObject.transform.position.y).normalized;
            currentBullet = Instantiate(bullet, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y) + (normalizedDirection * startingDistance), transform.rotation);
            currentBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
            inUse = true;
            soundManager.ShootingTelegun();
        }
    }

    public override bool canUse()
    {
        return inUse;
    }

    public override void notUsing()
    {
        if (inUse)
        {
            onCooldown = true;
            StartCoroutine(Cooldown(cooldown));

            GameObject playerObject = GameObject.Find("Player");
            playerObject.transform.position = currentBullet.transform.position;
            Destroy(currentBullet);
            inUse = false;
            soundManager.ShootingTelegunStop();
            soundManager.TeleportingTelegun();
        }
    }

    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        onCooldown = false;
    }
}
