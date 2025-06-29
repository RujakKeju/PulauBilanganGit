using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelDisplayController : MonoBehaviour
{
    public Image[] telurImages; // Telur UI: index 0-9
    public Sprite telurDefaultSprite;
    public Sprite[] telurBenarSprites; // 0 = soal 1 benar, dst
    public Sprite[] telurSalahSprites; // 0 = soal 1 salah, dst

    void Start()
    {
        LoadTelurStatus();
    }

    void LoadTelurStatus()
    {
        var key = GameStateManager.Instance.GetProgressKey();
        var progress = SaveLoadSystem.LoadProgress();

        if (!progress.levelProgressDict.ContainsKey(key))
        {
            Debug.Log("Belum ada progress untuk " + key);
            return;
        }

        var levelProgress = progress.levelProgressDict[key];
        List<LevelEntry> levels = levelProgress.levels;

        for (int i = 0; i < telurImages.Length; i++)
        {
            if (i >= levels.Count)
            {
                // Belum dikerjakan
                telurImages[i].sprite = telurDefaultSprite;
            }
            else
            {
                if (levels[i].isCorrect && i < telurBenarSprites.Length)
                {
                    telurImages[i].sprite = telurBenarSprites[i];
                }
                else if (!levels[i].isCorrect && i < telurSalahSprites.Length)
                {
                    telurImages[i].sprite = telurSalahSprites[i];
                }
                else
                {
                    telurImages[i].sprite = telurDefaultSprite; // fallback
                }
            }
        }
    }
}
