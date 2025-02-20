using UnityEngine;

public class Arrow : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f); // Tự hủy sau 2 giây
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Ban trung ke dich");
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Ladder"))
        {
            Destroy(this.gameObject);
        }
    }
}


