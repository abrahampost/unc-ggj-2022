using UnityEngine;

public class Telegun : Ability
{

    public GameObject bullet;
    public float bulletSpeed;
    public float startingDistance;
    private bool inUse;
    private GameObject currentBullet;

    private void Start()
    {
        inUse = false;
        currentBullet = null;
        // playerObject = GameObject.Find("Player");
    }

    public override void use()
    {
        if (!inUse)
        {
            GameObject playerObject = GameObject.Find("Player");
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var normalizedDirection = new Vector2(mousePosition.x - playerObject.transform.position.x, mousePosition.y - playerObject.transform.position.y).normalized;
            currentBullet = Instantiate(bullet, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y) + (normalizedDirection * startingDistance), transform.rotation);
            currentBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
            inUse = true;
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
            GameObject playerObject = GameObject.Find("Player");
            playerObject.transform.position = currentBullet.transform.position;
            Destroy(currentBullet);
            inUse = false;
        }
    }
}
