using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float patrolDistance = 5f;
    private Vector2 startPosition;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        float distanceFromStart = Mathf.Abs(transform.position.x - startPosition.x);
        if (distanceFromStart >= patrolDistance)
        {
            Flip();
        }

        transform.Translate(Vector2.right * speed * (movingRight ? 1 : -1) * Time.deltaTime);
    }

    void Flip()
    {
        movingRight = !movingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX; // Lật sprite
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // Kiểm tra va chạm với tường
        {
            Flip();
        }
    }
}