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
    private GameObject lineDuringShoot = null;
    private LineRenderer lineRendererDuringShoot = null;
    private bool holdingDown = false;

    void Start()
    {
        playerObject = GameObject.Find("Player");
    }

    public override void use()
    {
        if (!holdingDown)
        {
            if (currentLine != null)
            {
                Destroy(currentLine);
                currentLine = null;
                joint.enabled = false;
            }
            else
            {
                if (currentBullet == null)
                {
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = Camera.main.nearClipPlane;
                    var mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
                    playerObject = GameObject.Find("Player");
                    var normalizedDirection = new Vector2(mousePosition.x - playerObject.transform.position.x, mousePosition.y - playerObject.transform.position.y).normalized;
                    currentBullet = Instantiate(bullet, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y) + (normalizedDirection * startingDistance), transform.rotation);
                    currentBullet.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
                    currentBullet.GetComponent<GrappleCollision>().handleCollision = handleCollision;
                    lineDuringShoot = Instantiate(line, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y), transform.rotation);
                    lineRendererDuringShoot = lineDuringShoot.GetComponent<LineRenderer>();
                    lineRendererDuringShoot.startWidth = .05f;
                    lineRendererDuringShoot.endWidth = .05f;

                    Destroy(currentBullet, timeAlive);
                }
            }
            holdingDown = true;
        }
        else if (holdingDown && currentLine == null)
        {
            var currentVelocity = currentBullet.GetComponent<Rigidbody2D>().velocity.normalized;
            currentBullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(currentVelocity.y, currentVelocity.x) * Mathf.Rad2Deg, Vector3.forward);
            lineRendererDuringShoot.SetPosition(1, currentBullet.transform.position);
            lineRendererDuringShoot.SetPosition(0, playerObject.transform.position);
        }
    }

    public override bool canUse()
    {
        return true;
    }

    private void handleCollision(Collision2D collision, GameObject bullet)
    {
        Destroy(lineRendererDuringShoot);
        Destroy(lineDuringShoot);

        ContactPoint2D contact = collision.GetContact(0);
        Destroy(currentLine);

        Vector2 bulletFinalSpot = bullet.transform.position;
        Vector2 diff = bulletFinalSpot - new Vector2(transform.parent.position.x, transform.parent.position.y);
        joint.connectedBody = contact.collider.GetComponent<Rigidbody2D>();
        joint.distance = diff.magnitude * .8f;

        currentBullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg, Vector3.forward);
        currentBullet.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        currentLine = Instantiate(line, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y), transform.rotation);
        currentLineRenderer = currentLine.GetComponent<LineRenderer>();
        currentLineRenderer.SetPosition(1, contact.point);
        currentLineRenderer.SetPosition(0, playerObject.transform.position);
        currentLineRenderer.startWidth = .05f;
        currentLineRenderer.endWidth = .05f;
        joint.enabled = true;
    }

    void Update()
    {
        if (currentLineRenderer != null) currentLineRenderer.SetPosition(0, playerObject.transform.position);
    }

    public override void notUsing()
    {
        if (holdingDown)
        {
            Destroy(currentLineRenderer);
            Destroy(lineDuringShoot);
            Destroy(currentBullet);
            Destroy(currentLine);
            currentBullet = null;
            currentLine = null;
            holdingDown = false;
            joint.enabled = false;

            Destroy(currentBullet);
            currentBullet = null;
        }
    }
}