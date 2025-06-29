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
            SFXManager.Instance.PlayCorrect();

        }
        else
        {
            wrongPanel.SetActive(true);
            SFXManager.Instance.PlayWrong();

        }
    }

    public void TombolNextSoal(bool jawabanBenar)
    {
        var state = GameStateManager.Instance;
        string key = state.GetProgressKey();
        int levelIndex = state.currentLevelIndex;

        var progress = SaveLoadSystem.LoadProgress();

        if (!progress.levelProgressDict.ContainsKey(key))
        {
            var lp = new LevelProgress();
            for (int i = 0; i < 10; i++) lp.levels.Add(new LevelEntry());
            progress.levelProgressDict[key] = lp;
        }

        while (progress.levelProgressDict[key].levels.Count <= levelIndex)
        {
            progress.levelProgressDict[key].levels.Add(new LevelEntry());
        }

        var current = progress.levelProgressDict[key].levels[levelIndex];
        current.isCompleted = true;
        current.isCorrect = jawabanBenar;

        SaveLoadSystem.SaveProgress(progress);

        Debug.Log($"[TombolNextSoal] Saved for {key} | index {levelIndex} | benar: {jawabanBenar}");
    }
}
