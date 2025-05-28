using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HardLevelManager : MonoBehaviour
{
    [SerializeField] private MathLevelSO levelData;
    [SerializeField] private Transform spawnAreaBilangan1;
    [SerializeField] private Transform spawnAreaJawaban;
    [SerializeField] private TextMeshProUGUI Bilangan2;
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

    private void CheckAnswer()
    {
        if (fishCollector.CollectedFish == levelData.bilangan2)
        {
            correctPanel.SetActive(true);
        }
        else
        {
            wrongPanel.SetActive(true);
        }
    }
}