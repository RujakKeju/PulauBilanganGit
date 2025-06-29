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

    public AudioSource audioSource;
    public AudioClip panelBenarSound;
    public AudioClip panelSalahSound;


    private bool jawabanBenar; // tambahkan di atas


    private List<int> answerOptions = new List<int>();

    void Start()
    {
        GenerateQuestion();
        GenerateAnswers();

        // Pastikan panel benar dan salah tidak langsung muncul
        panelBenar.SetActive(false);
        panelSalah.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    private void GenerateQuestion()
    {
        SpawnAnimalSoal(levelData.bilangan1, spawnAreaBilangan1, levelData.animalPrefab1); //membutuhkan count, parent, pefabs
        SpawnAnimalSoal(levelData.bilangan2, spawnAreaBilangan2, levelData.animalPrefab2);

        jawabanText.text = "?";
        operasiText.text = levelData.GetOperationSymbol();
        difficultyText.text = "";
    }

    private void GenerateAnswers()
    {
        answerOptions.Clear(); // reset list biar nggak numpuk dari sebelumnya
        answerOptions.Add(levelData.jawaban);

        while (answerOptions.Count < 3)
        {
            int randomWrongAnswer = Random.Range(levelData.jawaban - 3, levelData.jawaban + 4); // +4 karena upper bound eksklusif

            // Tambahan: Pastikan jawaban salah > 0 dan bukan jawaban yang benar, dan belum ada di daftar
            if (randomWrongAnswer > 0 && randomWrongAnswer != levelData.jawaban && !answerOptions.Contains(randomWrongAnswer))
            {
                answerOptions.Add(randomWrongAnswer);
            }
        }

        answerOptions.Shuffle();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answerValue = answerOptions[i];

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(answerValue));

            Transform spawnPoint = answerButtons[i].transform.Find("SpawnPoint");
            if (spawnPoint != null)
            {
                ClearSpawnedAnimals(spawnPoint);
                SpawnAnimalSoal(answerValue, spawnPoint, levelData.animalPrefab1);
            }

            //  Tambahan di sini: set jumlah teks
            TextMeshProUGUI jumlahText = answerButtons[i].transform.Find("JumlahText")?.GetComponent<TextMeshProUGUI>();
            if (jumlahText != null)
            {
                jumlahText.text = answerValue.ToString();
            }
            else
            {
                Debug.LogWarning("JumlahText not found in " + answerButtons[i].name);
            }
        }
    }

    void CheckAnswer(int input)
    {
        bool jawabanBenar = input == levelData.jawaban;

        if (jawabanBenar)
        {
            panelBenar.SetActive(true); // TAMPILKAN panel, tidak load scene
            SFXManager.Instance.PlayCorrect();

        }
        else
        {
            panelSalah.SetActive(true); // TAMPILKAN panel, tidak load scene
            SFXManager.Instance.PlayWrong();

        }
    }



    private void SpawnAnimalSoal(int count, Transform parent, GameObject prefab) //soal
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
