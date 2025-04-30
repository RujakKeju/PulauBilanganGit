using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EasyLevelManager : MonoBehaviour
{
    [SerializeField] private MathLevelSO levelData;
    [SerializeField] private Transform spawnAreaBilangan1;
    [SerializeField] private Transform spawnAreaBilangan2;
    [SerializeField] private TextMeshProUGUI jawabanText;
    [SerializeField] private TextMeshProUGUI operasiText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private Button[] answerButtons;

    [Header("UI Panels")]
    [SerializeField] private GameObject panelBenar;
    [SerializeField] private GameObject panelSalah;

    private List<int> answerOptions = new List<int>();

    void Start()
    {
        GenerateQuestion();
        GenerateAnswers();

        // Pastikan panel benar dan salah tidak langsung muncul
        panelBenar.SetActive(false);
        panelSalah.SetActive(false);
    }

    private void GenerateQuestion()
    {
        SpawnAnimal(levelData.bilangan1, spawnAreaBilangan1, levelData.animalPrefab1);
        SpawnAnimal(levelData.bilangan2, spawnAreaBilangan2, levelData.animalPrefab2);

        jawabanText.text = "?";
        operasiText.text = levelData.GetOperationSymbol();
        difficultyText.text = levelData.difficulty.ToString();
    }

    private void GenerateAnswers()
    {
        // Masukkan jawaban benar
        answerOptions.Add(levelData.jawaban);

        // Tambahkan 2 jawaban acak
        while (answerOptions.Count < 3)
        {
            int randomWrongAnswer = Random.Range(levelData.jawaban - 3, levelData.jawaban + 3);
            if (randomWrongAnswer != levelData.jawaban && !answerOptions.Contains(randomWrongAnswer))
            {
                answerOptions.Add(randomWrongAnswer);
            }
        }

        // Acak jawaban
        answerOptions.Shuffle();

        // Set jawaban ke tombol
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answerValue = answerOptions[i];

            // Pastikan kita reset listener agar tidak menumpuk
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(answerValue));

            // Cari child "SpawnPoint"
            Transform spawnPoint = answerButtons[i].transform.Find("SpawnPoint");
            if (spawnPoint != null)
            {
                // Bersihkan ikan lama kalau ada
                ClearSpawnedAnimals(spawnPoint);

                // Spawn prefab sesuai angka jawaban
                SpawnAnimal(answerValue, spawnPoint, levelData.animalPrefab1);
            }
            else
            {
                Debug.LogWarning("SpawnPoint not found in " + answerButtons[i].name);
            }
        }
    }
    private void CheckAnswer(int selectedAnswer)
    {
        if (selectedAnswer == levelData.jawaban)
        {
            Debug.Log("Jawaban Benar!");
            panelBenar.SetActive(true);
            panelSalah.SetActive(false);
        }
        else
        {
            Debug.Log("Jawaban Salah!");
            panelSalah.SetActive(true);
            panelBenar.SetActive(false);
        }
    }

    private void SpawnAnimal(int count, Transform parent, GameObject prefab)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, parent);
        }
    }

    private void ClearSpawnedAnimals(Transform spawnPoint)
    {
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
        }
    }
}
