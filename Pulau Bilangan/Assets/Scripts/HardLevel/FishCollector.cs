using UnityEngine;

public class FishCollector : MonoBehaviour
{
    public MathUIManager uiManager;
    private int collectedFish = 0;

    public int CollectedFish => collectedFish; // Getter untuk HardLevelManager

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            collectedFish++;
            uiManager.UpdateAnswer(collectedFish);
            Destroy(collision.gameObject);
        }
    }
}
