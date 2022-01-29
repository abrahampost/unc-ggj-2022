using UnityEngine;

public class GrapplingHook : Ability
{

    public GameObject bullet;
    private GameObject currentBullet;
    public float bulletSpeed;
    public float timeAlive;
    public float startingDistance;
    private bool inUse;
    void Start() {
        inUse = false;
    }

    public override void use()
    {
        Debug.Log(inUse);
        if (!inUse)
        {
            inUse = true;
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject playerObject = GameObject.Find("Player");
            var normalizedDirection = new Vector2(mousePosition.x - playerObject.transform.position.x, mousePosition.y - playerObject.transform.position.y).normalized;
            currentBullet = Instantiate(bullet, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y) + (normalizedDirection * startingDistance), transform.rotation);
            currentBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
            currentBullet.GetComponent<GrappleCollision>().handleCollision = handleCollision;

            Destroy(currentBullet, timeAlive);
        }
        else 
        {
            Destroy(currentBullet);
            inUse = false;
        }
    }

    public override bool canUse()
    {
        return inUse;
    }

    private void handleCollision(Collision2D collision, GameObject bullet)
    {
        inUse = false;
        ContactPoint2D contact = collision.GetContact(0);
        print(contact.point.x);
        Destroy(bullet);
    }
}