using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SharkLevelManager : MonoBehaviour
{
    [SerializeField] private MathLevelSO levelData;
    [SerializeField] private Transform spawnAreaBilangan1; // Soal diam
    [SerializeField] private Transform spawnAreaJawaban;   // Ikan jawaban
    [SerializeField] private TextMeshProUGUI Bilangan2;
    [SerializeField] private TextMeshProUGUI operasiText;
    [SerializeField] private TextMeshProUGUI difficultyText;

    [Header("UI Panels")]
    public GameObject correctPanel;
    public GameObject wrongPanel;
    public Button checkAnswerButton;

    [Header("Collector Reference")]
    public SharkCollector sharkCollector;

    private void Start()
    {
        GenerateLevelUI();
        checkAnswerButton.onClick.AddListener(CheckAnswer);
    }

    private void GenerateLevelUI()
    {
        // Spawn ikan diam sebagai bilangan1
        SpawnFish(levelData.bilangan1, spawnAreaBilangan1, levelData.animalPrefab1);

        // Spawn ikan yang bisa dimakan sebagai bilangan2
        SpawnFish(levelData.jawaban, spawnAreaJawaban, levelData.animalPrefab2);

        Bilangan2.text = "0";
        operasiText.text = levelData.GetOperationSymbol();
        difficultyText.text = levelData.difficulty.ToString();
    }

    private void SpawnFish(int count, Transform parent, GameObject prefab)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, parent);
        }
    }

    public void UpdateBilangan2UI(int value)
    {
        Bilangan2.text = value.ToString();
    }

    private void CheckAnswer()
    {
        if (sharkCollector.CollectedFish == levelData.bilangan2)
        {
            correctPanel.SetActive(true);
        }
        else
        {
            wrongPanel.SetActive(true);
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
