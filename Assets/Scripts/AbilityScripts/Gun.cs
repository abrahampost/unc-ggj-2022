using UnityEngine;

public class Gun : Ability
{

    public GameObject bullet;
    public float bulletSpeed;
    public float timeAlive;
    public float startingDistance;

    public override void use()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        var mousePosition = Camera.main.ScreenToWorldPoint(mousePos);

        GameObject playerObject = GameObject.Find("Player");
        var normalizedDirection = new Vector2(mousePosition.x - playerObject.transform.position.x, mousePosition.y - playerObject.transform.position.y).normalized;
        Debug.Log(normalizedDirection);
        GameObject newBullet = Instantiate(bullet, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y) + (normalizedDirection * startingDistance), Quaternion.AngleAxis(Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg, Vector3.forward));
        newBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
        Destroy(newBullet, timeAlive);
    }

    public override bool canUse()
    {
        return true;
    }

    public override void notUsing()
    {

    }
}
