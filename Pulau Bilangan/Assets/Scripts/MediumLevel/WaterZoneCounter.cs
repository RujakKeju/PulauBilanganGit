using UnityEngine;
using TMPro;

public class WaterZoneCounter : MonoBehaviour
{
    public TMP_Text fishCountText;
    private int fishCount = 0;

    public int FishCount => fishCount; // Getter buat ambil jumlah ikan di script lain

    void Start()
    {
        UpdateFishCount();
    }

    void UpdateFishCount()
    {
        fishCountText.text = fishCount.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            fishCount++;
            UpdateFishCount();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            fishCount--;
            UpdateFishCount();
        }
    }
}
