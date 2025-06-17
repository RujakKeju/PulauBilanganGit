using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] private LevelListSO easyLevels;
    [SerializeField] private LevelListSO mediumLevels;
    [SerializeField] private LevelListSO hardLevels;

    [SerializeField] private Button[] levelButtons; // Assign dari 1–10 tombol

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
    }

    void SetupButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i; // buat closure aman
            string sceneToLoad = currentLevelList.sceneNames[i];

            levelButtons[i].onClick.AddListener(() => SceneManager.LoadScene(sceneToLoad));
        }
    }
}
