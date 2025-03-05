using UnityEngine;

public class Explo : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip explosionSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(explosionSound);
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
