using UnityEngine;
using TMPro;

public class MathLevelManager : MonoBehaviour
{
    [SerializeField] private MathLevelSO levelData;
    [SerializeField] private Transform spawnAreaBilangan1;
    [SerializeField] private Transform spawnAreaBilangan2;
    [SerializeField] private TextMeshProUGUI jawabanText;
    [SerializeField] private TextMeshProUGUI operasiText;
    [SerializeField] private TextMeshProUGUI difficultyText;

    void Start()
    {
        GenerateUI();
    }

    private void GenerateUI()
    {
        // Spawn prefab untuk bilangan1 dan bilangan2 menggunakan prefab hewan yang telah di-assign
        SpawnAnimal(levelData.bilangan1, spawnAreaBilangan1, levelData.animalPrefab1);
        SpawnAnimal(levelData.bilangan2, spawnAreaBilangan2, levelData.animalPrefab2);

        // Tampilkan jawaban, simbol operasi, dan tingkat kesulitan di UI
        jawabanText.text = levelData.jawaban.ToString();
        operasiText.text = levelData.GetOperationSymbol();
        difficultyText.text = "";
    }

    private void SpawnAnimal(int count, Transform parent, GameObject prefab)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, parent);
        }
    }
}
