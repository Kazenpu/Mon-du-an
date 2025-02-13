using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float climbSpeed = 3f;
    public float jumpHeight = 3f;

    public GameObject gameOverCanvas;
    public GameObject WinCanvas;
    public TextMeshProUGUI scoreText;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 move;
    private bool isClimbing = false;
    private int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        UpdateScoreUI();
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
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Exit"))
        {
            Win();
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            CollectCoin(collision.gameObject);
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

    void Die()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        Destroy(gameObject);
    }
    void Win()
    {
        if (WinCanvas != null)
        {
            WinCanvas.SetActive(true);
        }

        Time.timeScale = 0;
    }
    void CollectCoin(GameObject coin)
    {
        score += 10;  // Tăng điểm khi thu thập coin
        UpdateScoreUI();  // Cập nhật UI
        Destroy(coin);  // Hủy đối tượng coin sau khi thu thập
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score.ToString();
        }
    }
}
