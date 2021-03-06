using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    private LevelState levelState;
    public float maxHorizSpeed;
    public float horizAccel;
    public float horizDecel;
    public float inAirAccel;
    public float jumpAccel;
    public float gravity;

    public int maxJumps;
    private int jumps;
    private bool canJump = true;
    public float jumpCooldown;
    public bool onGround = true;

    // For Animations
    public Animator animator;

    private Rigidbody2D _rigidbody;

    private GameObject Goal;
    [SerializeField]
    private Transform leftDown;
    [SerializeField]
    private Transform rightDown;

    private int levelToGoTo;
    private SoundManager soundManager;
    private WeaponController weaponController;

    // Start is called before the first frame update
    void Start()
    {
        levelState = GameObject.Find("Game").GetComponent<LevelState>();
        _rigidbody = GetComponent<Rigidbody2D>();
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>();
        weaponController = GameObject.Find("Player").GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckVectors();
        if (Input.GetKeyUp(KeyCode.LeftShift)) levelState.ChangeDimension();
        if (Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene("Main Menu");
        float horizAxis = Input.GetAxisRaw("Horizontal");
        bool jump = Input.GetButtonDown("Jump");
        Vector2 vel = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
        if (weaponController.currentAbility == "Grapple")
        {
            return;
        }
        if (horizAxis > 0)
        {
            if (!onGround && !Input.GetMouseButton(1))
            {
                vel.x = Mathf.Clamp(vel.x + (horizAxis * horizAccel * Mathf.Min(inAirAccel, 1) * Time.deltaTime), -maxHorizSpeed, maxHorizSpeed);
            }
            else
            {
                vel.x = Mathf.Clamp(vel.x + (horizAxis * horizAccel * Time.deltaTime), -maxHorizSpeed, maxHorizSpeed);
            }
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizAxis < 0)
        {
            if (!onGround && !Input.GetMouseButton(1))
            {
                vel.x = Mathf.Clamp(vel.x + (horizAxis * horizAccel * Mathf.Min(inAirAccel, 1) * Time.deltaTime), -maxHorizSpeed, maxHorizSpeed);
            }
            else
            {
                vel.x = Mathf.Clamp(vel.x + (horizAxis * horizAccel * Time.deltaTime), -maxHorizSpeed, maxHorizSpeed);
            }
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (onGround)
        {
            if (Mathf.Abs(vel.x) > .05)
            {
                var newVel = vel.x - Mathf.Sign(vel.x) * horizDecel * Time.deltaTime;
                if (Mathf.Sign(newVel) != Mathf.Sign(vel.x))
                {
                    vel.x = 0;
                }
                else
                {
                    vel.x = vel.x - Mathf.Sign(vel.x) * horizDecel * Time.deltaTime;
                }
            }
            else
            {
                vel.x = 0;
            }
        }

        if (jump && jumps > 0 && canJump)
        {
            vel.y = jumpAccel;
            jumps--;
            canJump = false;
            soundManager.PlayJump();
            StartCoroutine(JumpCooldown());
        }
        else
        {
            vel.y -= gravity * Time.deltaTime;
        }

        _rigidbody.velocity = vel;

        animator.SetFloat("Speed", Mathf.Abs(vel.x));
        animator.SetBool("IsJumping", !(onGround));
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    void CheckVectors()
    {
        string currentDimensionString = levelState.GetTerrainTag(levelState.dimension);

        Vector2 currentPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        RaycastHit2D downLeft = Physics2D.Raycast(leftDown.position, Vector2.down, .1f);
        RaycastHit2D downRight = Physics2D.Raycast(rightDown.position, Vector2.down, .1f);
        if ((downLeft.collider != null && downLeft.collider.CompareTag(currentDimensionString)) ||
            (downRight.collider != null && downRight.collider.CompareTag(currentDimensionString)))
        {
            if (!onGround)
            {
                onGround = true;
                jumps = maxJumps;
                canJump = true;
            }
        }
        else
        {
            onGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GetComponent<DamageController>().takeDamage(other.gameObject.GetComponent<DamageController>().damage);
            StartCoroutine(other.gameObject.GetComponent<LaserMovement>().SetHit());
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            var levelToGoTo = other.gameObject.GetComponent<NextScene>().nextScene;
            SceneManager.LoadScene(levelToGoTo);
        }
        else if (other.gameObject.CompareTag("DeathZone"))
        {
            var spawnPoint = GameObject.FindWithTag("Respawn");
            spawnPoint.GetComponent<MovePlayerToSpawn>().ReturnToStart();
        }
    }
}