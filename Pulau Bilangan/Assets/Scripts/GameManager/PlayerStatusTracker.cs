using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatusTracker : MonoBehaviour
{
    public static PlayerStatusTracker Instance;

    public string currentScene => SceneManager.GetActiveScene().name;

    public MathOperation currentOperation => GameStateManager.Instance.selectedOperation;
    public Difficulty currentDifficulty => GameStateManager.Instance.selectedDifficulty;
    public int currentLevelIndex => GameStateManager.Instance.currentLevelIndex;

    public string currentKey => $"{currentOperation}_{currentDifficulty}";

    public bool IsCurrentLevelCompleted()
    {
        var progress = SaveLoadSystem.LoadProgress();
        if (progress.levelProgressDict.TryGetValue(currentKey, out var levelData))
        {
            if (currentLevelIndex >= 0 && currentLevelIndex < levelData.levels.Count)
                return levelData.levels[currentLevelIndex].isCompleted;
        }
        return false;
    }

    public bool IsNextLevelUnlocked()
    {
        var progress = SaveLoadSystem.LoadProgress();
        int nextLevel = currentLevelIndex + 1;
        if (nextLevel >= 10) return false;

        if (progress.levelProgressDict.TryGetValue(currentKey, out var levelData))
        {
            return levelData.levels[nextLevel - 1].isCompleted;
        }
        return false;
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
