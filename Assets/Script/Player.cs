using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float climbSpeed = 3f;
    public float jumpHeight = 3f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 move;
    private bool isClimbing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        move.x = Input.GetAxis("Horizontal");

        if (isClimbing)
        {
            move.y = Input.GetAxis("Vertical");
        }
        else
        {
            move.y = 0;
        }

        move.Normalize();

        if (move.x > 0) Flip(false);
        else if (move.x < 0) Flip(true);

        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Attacking");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("Rolling");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jumping");
            rb.linearVelocity = new Vector2(move.x * speed, jumpHeight);
        }
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(move.x * speed, move.y * climbSpeed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
            rb.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isClimbing = true;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }

    void Flip(bool facingLeft)
    {
        Vector2 newScale = transform.localScale;
        newScale.x = facingLeft ? -1 : 1;
        transform.localScale = newScale;
    }

    void UpdateAnimation()
    {
        animator.SetBool("Running", move.x != 0);
        animator.SetBool("Climbing", isClimbing && move.y != 0);
    }
}
