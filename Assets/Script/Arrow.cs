using UnityEngine;

public class Arrow : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip arrowSound;

    public GameObject exploPrefab;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(arrowSound);
        Destroy(gameObject, 2f); // Tự hủy sau 2 giây
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Ban trung ke dich");

            Instantiate(exploPrefab, collision.transform.position, Quaternion.identity);

            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {   

            Destroy(this.gameObject);
        }
    }
}


