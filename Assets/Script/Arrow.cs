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
            Debug.Log("Bắn trúng mục tiêu!");
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Wall") || collision.CompareTag("Ladder"))
        {
            Debug.Log("Mũi tên chạm vào tường, nền hoặc thang!");
            Destroy(gameObject);
        }
    }
}


