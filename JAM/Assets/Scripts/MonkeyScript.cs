using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonkeyScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    BoxCollider2D boxCollider;
    Animator animator;

    bool a_isUp;
    bool a_isUp2;
    bool a_isJumping;
    bool a_isDoble;
    bool a_isDown;
    bool a_isDashing;
    bool a_isCrashed;
    bool a_StillCrashed;
    bool a_isBurned;

    [SerializeField] float thrust;
    [SerializeField] float speed;
    [SerializeField] float dashSpeed;
    [SerializeField] bool isJumping;
    [SerializeField] bool canJump;
    [SerializeField] bool oneMoreJump;
    [SerializeField] bool canDash;
    [SerializeField] float dashCD;

    float timer;

    int newSpeed;

    bool Level1;
    bool Level2;
    bool Level3;

    Vector2 axis;
    Vector2 movement;

    KeyCode UpButton = KeyCode.UpArrow;
    KeyCode RightButton = KeyCode.RightArrow;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        a_isUp = false;
        a_isUp2 = false;
        a_isJumping = false;
        a_isDoble = false;
        a_isDown = false;
        a_isDashing = false;
        a_isCrashed = false;
        a_isBurned = false;
        canDash = true;
        canJump = true;
        oneMoreJump = true;
        axis.x = 1;
        dashCD = 0;
        Level1 = true;
        Level2 = false;
        Level3 = false;
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;

        timer += delta;
        if (timer > 18)
        {
            Level1 = false;
            Level2 = true;
            a_isUp = true;

        }
        if (timer > 36)
        {
            Level2 = false;
            Level3 = true;
            a_isUp2 = true;
        }

        if (a_isBurned)
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.gravityScale = 2000;
            axis.x = 0;
        }

        if (Input.GetKeyDown(UpButton))
        {
            if (canJump) 
            {
                a_StillCrashed = false;
                a_isJumping = true;
                a_isDown = false;
                oneMoreJump = true;
                Jump();
            }
            else if (oneMoreJump)
            {
                a_StillCrashed = false;
                a_isDown = false;
                a_isDoble = true;
                rigidBody.velocity = Vector2.zero;
                oneMoreJump = false;
                Jump();
            }
        }

        if (rigidBody.velocity.y < -10)
        {
            a_isDown = true;
            a_isDoble = false;
        }

        if (rigidBody.velocity.x == 0)
        {
            CheckGameOver();
        }

        if (!canDash)
        {
            dashCD += delta;
            if (dashCD > 0.3)
            {
                a_isDashing = false;
                if (Level1) { newSpeed = 500; }
                else if (Level2) { newSpeed = 600; }
                else if (Level3) { newSpeed = 700; }
                speed = newSpeed;
                //CameraManager.CInstance.speed = 400;
                rigidBody.gravityScale = 180;
            }

            if (dashCD > 2)
            {
                canDash = true;
                dashCD = 0;
            }
        }

        if (Input.GetKeyDown(RightButton))
        {
            if (canDash)
            {
                a_isDashing = true;
                rigidBody.velocity = Vector2.zero;
                rigidBody.gravityScale = 0;
                canDash = false;
                Dash();
            }
        }

        animator.SetBool("Up", a_isUp);
        animator.SetBool("isUp2", a_isUp2);
        animator.SetBool("isJumping", a_isJumping);
        animator.SetBool("isDoble", a_isDoble);
        animator.SetBool("isDown", a_isDown);
        animator.SetBool("isDashing", a_isDashing);
        animator.SetBool("isCrashed", a_isCrashed);
        animator.SetBool("StillCrashed", a_StillCrashed);
        animator.SetBool("isBurned", a_isBurned);

        if(rigidBody.velocity.y > 1000)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 1000);
        }

    }

    private void CheckGameOver()
    {
        bool col1 = false;
        bool col2 = false;
        bool col3 = false;
        float center_y = (boxCollider.bounds.min.y + boxCollider.bounds.max.y) / 2;
        Vector2 centerPosition = new Vector2(boxCollider.bounds.max.x, center_y);
        Vector2 upPosition = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y);
        Vector2 downtPosition = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.max.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, Vector2.right, 2f);
        if (CheckRaycastWithScenario(hits)) { col1 = true; }

        hits = Physics2D.RaycastAll(upPosition, Vector2.right, 2f);
        if (CheckRaycastWithScenario(hits)) { col2 = true; }

        hits = Physics2D.RaycastAll(downtPosition, Vector2.right, 2f);
        if (CheckRaycastWithScenario(hits)) { col3 = true; }

        if (col1 || col2 || col3) { a_isCrashed = true; SoundManager.PlaySound("Crashsound"); }
    }

    private void GameOver() {
        SceneManager.LoadScene("GameOver");
    }

    private void StillCrashed() { a_StillCrashed = true; }

    private void Dash()
    {
        SoundManager.PlaySound("Dashsound");
        //CameraManager.CInstance.speed = 600;
        if (Level1) dashSpeed = 700;
        else if (Level2) dashSpeed = 800;
        else if (Level3) dashSpeed = 900;
        speed = dashSpeed;
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
        //a_isDown = true;
    }

    private void FixedUpdate()
    {
        movement = new Vector2(axis.x * speed, rigidBody.velocity.y);
        rigidBody.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coco")
        {
            SoundManager.PlaySound("Crashsound");
            a_isCrashed = true;
        }
        else if (collision.gameObject.tag == "Bomb")
        {
            SoundManager.PlaySound("Burned");
            a_isBurned = true;
        }
        else if (collision.gameObject.tag == "Bouncer")
        {
            a_isJumping = true;
            a_isDown = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            canJump = true;
            oneMoreJump = false;
            a_isJumping = false; 
            a_isDoble = false; 
            a_isDown = false;

            if (a_isJumping)
            {
                bool col1 = false;
                bool col2 = false;
                bool col3 = false;
                float center_x = (boxCollider.bounds.min.x + boxCollider.bounds.max.x) / 2;
                Vector2 centerPosition = new Vector2(center_x, boxCollider.bounds.min.y);
                Vector2 leftPosition = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y);
                Vector2 rightPosition = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y);

                RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, -Vector2.up, 5f);
                if (CheckRaycastWithScenario(hits)) { col1 = true; }

                hits = Physics2D.RaycastAll(leftPosition, -Vector2.up, 5f);
                if (CheckRaycastWithScenario(hits)) { col2 = true; }

                hits = Physics2D.RaycastAll(rightPosition, -Vector2.up, 5f);
                if (CheckRaycastWithScenario(hits)) { col3 = true; }

                if (col1 || col2 || col3) { a_isJumping = false; a_isDoble = false; a_isDown = false; }
                //else a_isDown = true;
            }
        }
        else if(collision.gameObject.tag == "Scenario")
        {
            canJump = true;
            oneMoreJump = false;
            a_isJumping = false;
            a_isDoble = false;
            a_isDown = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            canJump = false;
            oneMoreJump = true;
        }
        if (collision.gameObject.tag == "Scenario")
        {
            canJump = false;
            oneMoreJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Banana")
        {
            SoundManager.PlaySound("TimeUpsound");
            Destroy(collision.gameObject);
            //cambiar
            GameManager.GInstance.timeRemaining += 5;
        }
        else if (collision.gameObject.tag == "Peel")
        {
            SoundManager.PlaySound("TimeDownsound");
            Destroy(collision.gameObject);
            //cambiar
            GameManager.GInstance.timeRemaining -= 5;
        }
        else if (collision.gameObject.tag == "Moky")
        {
            SoundManager.PlaySound("TimeUpsound");
            Destroy(collision.gameObject);
            //cambiar
            GameManager.GInstance.timeRemaining += 15;
        }
        else if (collision.gameObject.tag == "Water")
        {
            a_isCrashed = true;
        }
    }

    private bool CheckRaycastWithScenario(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Tilemap") { return true; }
            }
        }
        return false;
    }
}
