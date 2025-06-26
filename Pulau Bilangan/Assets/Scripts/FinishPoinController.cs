using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinishPoinController : MonoBehaviour
{
    [Header("UI Refs")]
    public TextMeshProUGUI poinText;
    public GameObject[] bintangList; // 3 GameObject bintang
    public RectTransform papanGoyang;
    public Button closeButton;

    public void Start()
    {
        // Goyangkan papan!
        LeanTween.rotateZ(papanGoyang.gameObject, 5f, 0.6f).setEaseInOutSine().setLoopPingPong();

        var state = GameStateManager.Instance;

        string key = state.GetProgressKey();

        var progress = SaveLoadSystem.LoadProgress();

        // FIX: gunakan TryGetValue untuk aman
        LevelProgress levelProgress;
        if (!progress.levelProgressDict.TryGetValue(key, out levelProgress))
        {
            Debug.LogWarning("Belum ada data progres untuk: " + key);
            levelProgress = new LevelProgress(); // default kosong
        }

        // Hitung skor benar
        int benar = 0;
        foreach (var lvl in levelProgress.levels)
        {
            if (lvl.isCorrect) benar++;
        }

        float persentase = (benar / 10f) * 100f;
        poinText.text = Mathf.RoundToInt(persentase).ToString();

        // Tampilkan bintang berdasarkan skor
        SetBintangByScore(persentase);

        // Tombol kembali ke scene level pemilihan
        closeButton.onClick.AddListener(() =>
        {
            string op = ConvertOperationToBahasa(state.selectedOperation);
            SceneTransitioner.Instance.LoadSceneWithTransition("Level(" + op + ")");
        });

        Debug.Log($"[FinishPoin] Key: {key} | Data count: {levelProgress.levels.Count}");
        progress.scorePerKey[key] = Mathf.RoundToInt(persentase);
        SaveLoadSystem.SaveProgress(progress);
        FirestoreSync.SaveProgressToFirestore(progress);

    }

    void SetBintangByScore(float score)
    {
        for (int i = 0; i < bintangList.Length; i++)
            bintangList[i].SetActive(false);

        if (score >= 70)
        {
            bintangList[0].SetActive(true);
            bintangList[1].SetActive(false);
            bintangList[2].SetActive(false);
            bintangList[3].SetActive(false);
        }
        else if (score >= 30)
        {
            bintangList[0].SetActive(false);
            bintangList[1].SetActive(true);
            bintangList[2].SetActive(false);
            bintangList[3].SetActive(false);
        }
        else if (score >= 0)
        {
            bintangList[0].SetActive(false);
            bintangList[1].SetActive(false);
            bintangList[2].SetActive(true);
            bintangList[3].SetActive(false);
        }
        else
        {
            bintangList[0].SetActive(false);
            bintangList[1].SetActive(false);
            bintangList[2].SetActive(false);
            bintangList[3].SetActive(true);
        }
    }

    public string ConvertOperationToBahasa(MathOperation op)
    {
        return op switch
        {
            MathOperation.Addition => "penjumlahan",
            MathOperation.Subtraction => "pengurangan",
            MathOperation.Multiplication => "perkalian",
            MathOperation.Division => "pembagian",
            _ => "unknown"
        };
    }

}
