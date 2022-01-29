using UnityEngine;

public class Gun : Ability  {

    public GameObject bullet;
    public float bulletSpeed;
    public float timeAlive;
    public float startingDistance;
    
    public override void use()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject playerObject = GameObject.Find("Player");
        var normalizedDirection = new Vector2(mousePosition.x - playerObject.transform.position.x, mousePosition.y - playerObject.transform.position.y).normalized;
        GameObject newBullet = Instantiate(bullet, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y) + (normalizedDirection*startingDistance), transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
        Destroy (newBullet, timeAlive);
    }

    public override bool canUse()
    {
        return true;
    }

    public override void notUsing()
    {
        
    }
}
