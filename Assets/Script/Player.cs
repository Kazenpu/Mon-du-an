using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float climbSpeed = 3f;
    public float jumpHeight = 3f;

    public GameObject prefabArrow;
    public Transform shootPoint;

    public GameObject gameOverCanvas;
    public GameObject WinCanvas;
    public TextMeshProUGUI scoreText;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 move;
    private bool isOnLadder = false;  // Để kiểm tra có đang ở trên thang không
    private int score = 0;
    private bool facingRight = true;

    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        UpdateScoreUI();
    }

    void Update()
    {
        move.x = Input.GetAxis("Horizontal");

        if (isOnLadder)
        {
            move.y = Input.GetAxis("Vertical");
        }
        else
        {
            move.y = 0;
        }

        move.Normalize();

        if (move.x > 0) Flip(true);
        else if (move.x < 0) Flip(false);

        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Attacking");
            StartCoroutine(PrefabArrows());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("Rolling");
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            animator.SetTrigger("Jumping");
            rb.linearVelocity = new Vector2(move.x * speed, jumpHeight);
            canJump = false;  // Không thể nhảy lần nữa cho đến khi chạm đất
        }
    }

    void FixedUpdate()
    {
        if (isOnLadder)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Ladder"))
        {
            canJump = true;  // Khi chạm đất hoặc thang, cho phép nhảy lại
        }
    }

    IEnumerator PrefabArrows()
    {
        Vector3 direction = transform.localScale.x > 0 ? transform.right : -transform.right;

        for (int i = 0; i < 1; i++)
        {
            Vector3 spawnPos = shootPoint.position + direction * 1 * i;
            GameObject createdArrow = Instantiate(prefabArrow, spawnPos, Quaternion.identity);

            createdArrow.transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);

            Rigidbody2D rbArrow = createdArrow.GetComponent<Rigidbody2D>();
            if (rbArrow != null)
            {
                rbArrow.linearVelocity = direction * 5;
            }
            yield return new WaitForSeconds(1);
        }
    }

    void Flip(bool faceRight)
    {
        if (facingRight != faceRight)
        {
            facingRight = faceRight;
            transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
        }
    }

    void UpdateAnimation()
    {
        animator.SetBool("Running", move.x != 0);

        //if (isOnLadder)
        //{
        //    animator.SetTrigger("Climbing");
        //}
        animator.SetBool("Climbing", isOnLadder);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = true;
            animator.SetBool("Climbing", false);
            animator.SetBool("Idle", false);
            //animator.CrossFade("Player Climb Animation", 0.01f);
            rb.linearVelocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Enemy"))
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
            isOnLadder = false;
            animator.SetBool("Climbing", false);
            animator.SetBool("Idle", true);
            //animator.Play("Player Idle Animation");
        }
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
        score += 10;
        UpdateScoreUI();
        Destroy(coin);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
