using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    public void SelectDifficulty(int difficultyIndex)
    {
        GameStateManager.Instance.selectedDifficulty = (Difficulty)difficultyIndex;

        // Dapatkan operation & difficulty yang sudah disimpan sebelumnya
        MathOperation op = GameStateManager.Instance.selectedOperation;
        Difficulty diff = GameStateManager.Instance.selectedDifficulty;

        // Format scene tujuan, contoh: "ChoiceLevel_Addition_Easy"
        string sceneName = $"ChoiceLevel_{op}_{diff}";

        SceneManager.LoadScene(sceneName);
    }
}
