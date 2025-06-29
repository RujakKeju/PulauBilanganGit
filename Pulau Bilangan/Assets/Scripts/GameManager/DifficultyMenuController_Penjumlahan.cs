using UnityEngine;
using UnityEngine.UI;

public class DifficultyMenuController_Penjumlahan : MonoBehaviour
{
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    void Start()
    {
        var progress = SaveLoadSystem.LoadProgress();

        // Easy selalu aktif
        easyButton.interactable = true;

        // Medium aktif jika skor easy >= 60
        int skorEasy = GetScore(progress, "penjumlahan_easy");
        mediumButton.interactable = skorEasy >= 60;

        // Hard aktif jika skor medium >= 60
        int skorMedium = GetScore(progress, "penjumlahan_medium");
        hardButton.interactable = skorMedium >= 60;
    }

    int GetScore(PlayerProgress progress, string key)
    {
        if (progress.scorePerKey.TryGetValue(key, out int score))
            return score;
        return 0;
    }

    public void SelectDifficulty(int difficultyIndex)
    {
        GameStateManager.Instance.selectedDifficulty = (Difficulty)difficultyIndex;
        SceneTransitioner.Instance.LoadSceneWithTransition("Level(penjumlahan)");
    }

    public void OnBackToOperationMenu()
    {
        SceneTransitioner.Instance.LoadSceneWithTransition("OperationMenu");
    }
}
