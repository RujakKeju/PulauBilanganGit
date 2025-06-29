using UnityEngine;
using UnityEngine.UI;

public class DifficultyMenuController_Pengurangan : MonoBehaviour
{
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    void Start()
    {
        var progress = SaveLoadSystem.LoadProgress();

        // Easy selalu aktif
        easyButton.interactable = true;

        // Medium aktif jika pengurangan_easy >= 60
        int skorEasy = GetScore(progress, "pengurangan_easy");
        mediumButton.interactable = skorEasy >= 60;

        // Hard aktif jika pengurangan_medium >= 60
        int skorMedium = GetScore(progress, "pengurangan_medium");
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
        SceneTransitioner.Instance.LoadSceneWithTransition("Level(pengurangan)");
    }

    public void OnBackToOperationMenu()
    {
        SceneTransitioner.Instance.LoadSceneWithTransition("OperationMenu");
    }
}
