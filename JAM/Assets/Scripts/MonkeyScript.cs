using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    BoxCollider2D boxCollider;
    Animator animator;
    [SerializeField] float thrust;
    [SerializeField] float speed;
    [SerializeField] bool isJumping;
    [SerializeField] bool canJump;
    [SerializeField] bool oneMoreJump;

    [SerializeField] bool canDash;
    private GameObject limitdash;
    public GameObject dashtrigger;
    public LayerMask LayerDashLimit;
    public float dashCD = 0;
    public bool dashing;
    public float dashspeed;

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
        canJump = true;
        oneMoreJump = true;
        axis.x = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(UpButton))
        {
            if (canJump)
            {
                //canJump = false;
                oneMoreJump = true;
                Jump();
            }
            else if (oneMoreJump)
            {
                rigidBody.velocity = Vector2.zero;
                oneMoreJump = false;
                Jump();
            }
        }

        if (Input.GetKeyDown(RightButton))
        {
            if (canDash)
            {
                Dash();
            }
        }


    }

    private void Dash()
    {
        float centery = (boxCollider.bounds.min.y + boxCollider.bounds.max.y) / 2;
        Vector2 positioncenter = new Vector2(boxCollider.bounds.max.x, centery);
        Vector2 positionup = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.max.y);
        Vector2 positiondown = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y);

        RaycastHit2D hitscenter = Physics2D.Raycast(positioncenter, Vector2.right, 200, LayerDashLimit);
        RaycastHit2D hitsup = Physics2D.Raycast(positionup, Vector2.right, 200, LayerDashLimit);
        RaycastHit2D hitsdown = Physics2D.Raycast(positiondown, Vector2.right, 200, LayerDashLimit);
        RaycastHit2D[] array = new RaycastHit2D[] { hitsup, hitscenter, hitsdown };

        Vector2 LimitDashPosition = transform.position + transform.right * 200;
        for (int i = 0; i < 3; i++)
        {
            if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
            {
                LimitDashPosition = array[i].point;
            }
        }
        limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);

        rigidBody.velocity = new Vector2(dashspeed, 0);

        rigidBody.gravityScale = 0;
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        movement = new Vector2(axis.x * speed, rigidBody.velocity.y);
        rigidBody.velocity = movement;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            canJump = true;
            oneMoreJump = false;

            if (isJumping)
            {
                bool col1 = false;
                bool col2 = false;
                bool col3 = false;
                float center_x = (boxCollider.bounds.min.x + boxCollider.bounds.max.x) / 2;
                Vector2 centerPosition = new Vector2(center_x, boxCollider.bounds.min.y);
                Vector2 leftPosition = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y);
                Vector2 rightPosition = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y);

                RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, -Vector2.up, 0.1f);
                if (CheckRaycastWithScenario(hits)) { col1 = true; }

                hits = Physics2D.RaycastAll(leftPosition, -Vector2.up, 0.1f);
                if (CheckRaycastWithScenario(hits)) { col2 = true; }

                hits = Physics2D.RaycastAll(rightPosition, -Vector2.up, 0.1f);
                if (CheckRaycastWithScenario(hits)) { col3 = true; }

                if (col1 || col2 || col3) { isJumping = false; }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            canJump = false;
            oneMoreJump = true;
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
