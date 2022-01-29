using UnityEngine;

public class GrapplingHook : Ability
{
    public GameObject bullet;
    private GameObject currentBullet = null;
    public float bulletSpeed;
    public float timeAlive;
    public float startingDistance;
    public SpringJoint2D joint;
    private bool isGrappleReady = true;
    private bool grappleActive = false;
    private GameObject playerObject;
    public GameObject line;
    private GameObject currentLine = null;
    private LineRenderer currentLineRenderer = null;

    void Start()
    {
        playerObject = GameObject.Find("Player");
    }

    public override void use()
    {
        if (currentLine != null)
        {
            Destroy(currentLine);
            currentLine = null;
        }
        else
        {
            if (currentBullet == null)
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                playerObject = GameObject.Find("Player");
                var normalizedDirection = new Vector2(mousePosition.x - playerObject.transform.position.x, mousePosition.y - playerObject.transform.position.y).normalized;
                currentBullet = Instantiate(bullet, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y) + (normalizedDirection * startingDistance), transform.rotation);
                currentBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
                currentBullet.GetComponent<GrappleCollision>().handleCollision = handleCollision;

                Destroy(currentBullet, timeAlive);
            }
            else
            {
                Destroy(currentBullet);
            }
        }
    }

    public override bool canUse()
    {
        return true;
    }

    private void handleCollision(Collision2D collision, GameObject bullet)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Destroy(currentLine);
        Destroy(bullet);
        currentBullet = null;
        Destroy(currentBullet);

        float len = (contact.point - (Vector2)transform.position).magnitude;
        joint.connectedBody = contact.collider.GetComponent<Rigidbody2D>();
        joint.connectedAnchor = contact.point;
        joint.distance = .95f * len;

        currentLine = Instantiate(line, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y), transform.rotation);
        currentLineRenderer = currentLine.GetComponent<LineRenderer>();
        currentLineRenderer.SetPosition(1, contact.point);
        currentLineRenderer.SetPosition(0, playerObject.transform.position);
        currentLineRenderer.SetWidth(.1f, .1f);
        joint.enabled = true;
    }

    void Update()
    {
        if (currentLineRenderer != null) currentLineRenderer.SetPosition(0, playerObject.transform.position);
    }

    public override void notUsing()
    {

    }
}