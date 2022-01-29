using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Ability
{

    public float maxDistance;
    public float fanSpeed;
    public override void use()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject playerObject = GameObject.Find("Player");
        var direction = new Vector2(mousePosition.x - playerObject.transform.position.x, mousePosition.y - transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

        if (hit.collider != null)
        {
            var xDiff = hit.point.x - playerObject.transform.position.x;
            var yDiff = hit.point.y - playerObject.transform.position.y;
            var distance = Mathf.Sqrt(xDiff * xDiff + yDiff * yDiff);

            if (distance < maxDistance)
            {
                playerObject.GetComponent<Rigidbody2D>().velocity = playerObject.GetComponent<Rigidbody2D>().velocity + new Vector2(direction.x * -1, direction.y * -1).normalized * (maxDistance*2 - distance) * (fanSpeed/100);
            }
        }
    }

    // Update is called once per frame
    public override bool canUse()
    {
        return true;
    }

    public override void notUsing()
    {

    }
}
