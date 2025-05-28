using UnityEngine;
using UnityEngine.UI;

public class MediumLevelManager : MonoBehaviour
{
    public WaterZoneCounter waterZoneCounter;
    public MathLevelSO currentLevel;
    public GameObject correctPanel;
    public GameObject wrongPanel;
    public Button checkAnswerButton;

    void Start()
    {
        checkAnswerButton.onClick.AddListener(CheckAnswer);
    }

   void CheckAnswer()
    {

        if (waterZoneCounter.FishCount == currentLevel.jawaban)
        {
            correctPanel.SetActive(true);
        }
        else
        {
            wrongPanel.SetActive(true);
        }
    }
}
