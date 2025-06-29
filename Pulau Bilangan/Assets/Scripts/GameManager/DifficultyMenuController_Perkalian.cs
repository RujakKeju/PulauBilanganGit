using UnityEngine;
using UnityEngine.UI;

public class DifficultyMenuController_Perkalian : MonoBehaviour
{
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    void Start()
    {
        var progress = SaveLoadSystem.LoadProgress();

        easyButton.interactable = true;

        int skorEasy = GetScore(progress, "perkalian_easy");
        mediumButton.interactable = skorEasy >= 60;

        int skorMedium = GetScore(progress, "perkalian_medium");
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
        SceneTransitioner.Instance.LoadSceneWithTransition("Level(perkalian)");
    }

    public void OnBackToOperationMenu()
    {
        SceneTransitioner.Instance.LoadSceneWithTransition("OperationMenu");
    }
}
