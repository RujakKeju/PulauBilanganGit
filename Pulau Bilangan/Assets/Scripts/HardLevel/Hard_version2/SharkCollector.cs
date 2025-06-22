using UnityEngine;

public class SharkCollector : MonoBehaviour
{
    public MathUIManager uiManager;
    public GameObject fishBonePrefab;
    public AudioClip eatSound; // <--- Tambahkan ini

    private AudioSource audioSource; // <--- Tambahkan ini
    private int collectedFish = 0;
    public int CollectedFish => collectedFish;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Ambil AudioSource dari GameObject
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            collectedFish++;
            uiManager.UpdateAnswer(collectedFish);

            Vector3 spawnPoint = transform.position + new Vector3(0.5f, 0f, 0); // Mulut hiu
            Instantiate(fishBonePrefab, spawnPoint, Quaternion.identity);

            // 🔊 Mainkan suara makan
            if (eatSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(eatSound);
            }

            Destroy(collision.gameObject);
        }
    }
}
