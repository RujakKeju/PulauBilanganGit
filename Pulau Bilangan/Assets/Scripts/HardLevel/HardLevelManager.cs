using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HardLevelManager : MonoBehaviour
{
    [SerializeField] private MathLevelSO levelData;
    [SerializeField] private Transform spawnAreaBilangan1;
    [SerializeField] private Transform spawnAreaBilangan2;
    [SerializeField] private TextMeshProUGUI jawabanText;
    [SerializeField] private TextMeshProUGUI operasiText;
    [SerializeField] private TextMeshProUGUI difficultyText;

    [Header("UI Panels")]
    public GameObject correctPanel;
    public GameObject wrongPanel;
    public Button checkAnswerButton;

    [Header("Collector Reference")]
    public FishCollector fishCollector;

    private void Start()
    {
        GenerateLevelUI();
        checkAnswerButton.onClick.AddListener(CheckAnswer);
    }

    private void GenerateLevelUI()
    {
        SpawnFish(levelData.bilangan1, spawnAreaBilangan1, levelData.animalPrefab1);
        SpawnFish(levelData.bilangan2, spawnAreaBilangan2, levelData.animalPrefab2);

        jawabanText.text = levelData.jawaban.ToString();
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

    private void CheckAnswer()
    {
        if (fishCollector.CollectedFish == levelData.jawaban)
        {
            correctPanel.SetActive(true);
        }
        else
        {
            wrongPanel.SetActive(true);
        }
    }
}
