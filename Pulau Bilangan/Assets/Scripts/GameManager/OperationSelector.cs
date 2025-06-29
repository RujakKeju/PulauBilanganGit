using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OperationSelector : MonoBehaviour
{
    public Button tambahButton;
    public Button kurangButton;
    public Button kaliButton;
    public Button bagiButton;

    void Start()
    {
        var progress = SaveLoadSystem.LoadProgress();

        // Buka tambah selalu
        tambahButton.interactable = true;

        // Cek syarat untuk unlock pengurangan
        int skorPenjumlahanHard = GetScore(progress, "penjumlahan_hard");
        kurangButton.interactable = skorPenjumlahanHard >= 60;

        // Cek syarat untuk unlock perkalian
        int skorPenguranganHard = GetScore(progress, "pengurangan_hard");
        kaliButton.interactable = skorPenguranganHard >= 60;

        // Cek syarat untuk unlock pembagian
        int skorPerkalianHard = GetScore(progress, "perkalian_hard");
        bagiButton.interactable = skorPerkalianHard >= 60;
    }

    int GetScore(PlayerProgress progress, string key)
    {
        if (progress.scorePerKey.TryGetValue(key, out int score))
            return score;
        return 0;
    }

    public void SelectOperation(int operationIndex)
    {
        GameStateManager.Instance.selectedOperation = (MathOperation)operationIndex;

        string sceneName = operationIndex switch
        {
            0 => "DifficultyMenu(penjumlahan)",
            1 => "DifficultyMenu(pengurangan)",
            2 => "DifficultyMenu(perkalian)",
            3 => "DifficultyMenu(pembagian)",
            _ => ""
        };

        SceneTransitioner.Instance.LoadSceneWithTransition(sceneName);
    }

    public void OnBackToMainMenu()
    {
        SceneTransitioner.Instance.LoadSceneWithTransition("MainMenu");
    }
}
