using UnityEngine;

public class SharkCollector : MonoBehaviour
{
    public MathUIManager uiManager;
    public GameObject fishBonePrefab;

    private int collectedFish = 0;
    public int CollectedFish => collectedFish;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            collectedFish++;
            uiManager.UpdateAnswer(collectedFish);

            Vector3 spawnPoint = transform.position + new Vector3(0.5f, 0f, 0); // Mulut hiu
            Instantiate(fishBonePrefab, spawnPoint, Quaternion.identity);

            Destroy(collision.gameObject);
        }
    }
}
