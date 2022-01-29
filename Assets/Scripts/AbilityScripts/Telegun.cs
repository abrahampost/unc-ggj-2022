using UnityEngine;

public class Telegun : Ability
{

    public GameObject bullet;
    public float bulletSpeed;
    public float startingDistance;
    private bool inUse = false;
    private GameObject currentBullet;

    public override void use()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (!inUse)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var normalizedDirection = new Vector2(mousePosition.x - playerObject.transform.position.x, mousePosition.y - playerObject.transform.position.y).normalized;
            currentBullet = Instantiate(bullet, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y) + (normalizedDirection * startingDistance), transform.rotation);
            currentBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
            inUse = true;
        }
        else
        {
            playerObject.transform.position = currentBullet.transform.position;
            Destroy(currentBullet);
            inUse = false;
        }

    }

    public override bool canUse()
    {
        return inUse;
    }

    public override void notUsing()
    {

    }
}
