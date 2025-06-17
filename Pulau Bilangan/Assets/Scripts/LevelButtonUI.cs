using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonUI : MonoBehaviour
{
    public Image statusOverlay;
    public Sprite lockedSprite;     // Image B
    public Sprite completedSprite;  // Image A
    public Sprite currentSprite;    // Image C
    public TextMeshProUGUI levelText;

    public Button button;

    public void Setup(int levelIndex, int unlockedUntil)
    {
        levelText.text = (levelIndex + 1).ToString();

        if (levelIndex < unlockedUntil)
        {
            statusOverlay.sprite = completedSprite;
            button.interactable = true;
        }
        else if (levelIndex == unlockedUntil)
        {
            statusOverlay.sprite = currentSprite;
            button.interactable = true;
        }
        else
        {
            statusOverlay.sprite = lockedSprite;
            button.interactable = false;
        }
    }
}
