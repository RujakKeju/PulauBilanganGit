using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] private LevelListSO easyLevels;
    [SerializeField] private LevelListSO mediumLevels;
    [SerializeField] private LevelListSO hardLevels;

    [SerializeField] private Button[] levelButtons; // Assign dari 1–10 tombol

    [SerializeField] private Button btnBack;

    private LevelListSO currentLevelList;

    void Start()
    {
        Difficulty selectedDiff = GameStateManager.Instance.selectedDifficulty;

        // Tentukan level list sesuai difficulty
        switch (selectedDiff)
        {
            case Difficulty.Easy:
                currentLevelList = easyLevels;
                break;
            case Difficulty.Medium:
                currentLevelList = mediumLevels;
                break;
            case Difficulty.Hard:
                currentLevelList = hardLevels;
                break;
        }

        SetupButtons();

        // Tambahkan listener untuk tombol kembali
        btnBack.onClick.AddListener(() =>
        {
            string operationScene = GetDifficultyMenuSceneName(GameStateManager.Instance.selectedOperation);
            SceneTransitioner.Instance.LoadSceneWithTransition(operationScene);
        });
    }

    // Fungsi pembantu untuk nama scene berdasarkan operasi
    string GetDifficultyMenuSceneName(MathOperation op)
    {
        switch (op)
        {
            case MathOperation.Addition:
                return "DifficultyMenu(Penjumlahan)";
            case MathOperation.Subtraction:
                return "DifficultyMenu(Pengurangan)";
            case MathOperation.Multiplication:
                return "DifficultyMenu(Perkalian)";
            case MathOperation.Division:
                return "DifficultyMenu(Pembagian)";
            default:
                return "OperationMenu"; // fallback
        }

    }

        void SetupButtons()
    {
        string key = GameStateManager.Instance.selectedOperation + "_" + GameStateManager.Instance.selectedDifficulty;
        var progress = SaveLoadSystem.LoadProgress();

        List<LevelEntry> completedLevels = new List<LevelEntry>();
        if (progress.levelProgressDict.ContainsKey(key))
            completedLevels = progress.levelProgressDict[key].levels;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;
            bool unlocked = (i == 0) || (i > 0 && completedLevels.Count > i - 1 && completedLevels[i - 1].isCompleted);
            levelButtons[i].interactable = unlocked;

            levelButtons[i].onClick.AddListener(() =>
            {
                GameStateManager.Instance.currentLevelIndex = index;
                SceneTransitioner.Instance.LoadSceneWithTransition(currentLevelList.sceneNames[index]);
            });
        }

    }





}
