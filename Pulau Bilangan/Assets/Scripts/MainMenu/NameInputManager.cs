using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class NameInputManager : MonoBehaviour
{
    public static NameInputManager Instance { get; private set; }


    [SerializeField] private TMP_InputField nameInputField; // Field untuk input nama
    [SerializeField] private GameObject nameInputPanel;       // Panel input nama
    [SerializeField] private Image selectedCharacterImage;    // Image di panel input nama untuk menampilkan karakter yang dipilih

    [SerializeField] private GameObject accountPanel;         // Panel "Akun" untuk menampilkan data tersimpan
    [SerializeField] private TextMeshProUGUI accountNameText;   // Teks untuk nama pemain di panel akun
    [SerializeField] private Image accountCharacterImage;       // Gambar karakter di panel "Akun"

    public Image fillBar;
    public TMP_Text percentText;

    void Start()
    {
        UpdateSelectedCharacterImage();
    }

    // Jika panel input nama ditampilkan secara manual, panggil method ini
    public void UpdateSelectedCharacterImage()
    {
        if (CharacterSelectionManager.SelectedCharacterData != null)
        {
            selectedCharacterImage.sprite = CharacterSelectionManager.SelectedCharacterData.characterSprite;
            Debug.Log("Sprite di InputName di-update: " + selectedCharacterImage.sprite.name);
        }
        else
        {
            Debug.LogError("SelectedCharacterData is null! Pastikan karakter sudah dipilih.");
        }
    }

    public void ConfirmName()
    {
        string playerName = nameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("PlayerName", playerName);

            // SIMPAN ke PlayerProgress
            var progress = SaveLoadSystem.LoadProgress();
            progress.playerName = playerName;
            progress.characterName = CharacterSelectionManager.SelectedCharacterData.characterName;
            SaveLoadSystem.SaveProgress(progress);

            ShowAccountPanel();
        }
        else
        {
            Debug.Log("Nama tidak boleh kosong!");
        }
    }

    private void ShowAccountPanel()
    {
        accountPanel.SetActive(true);
        var progress = SaveLoadSystem.LoadProgress();

        accountNameText.text = progress.playerName;

        // Karakter
        var charSO = Resources.Load<CharacterDataSO>("Characters/" + progress.characterName);
        if (charSO != null)
        {
            accountCharacterImage.sprite = charSO.characterSprite;
        }

        // Hitung persentase progres
        float totalBenar = 0;
        float totalSoal = 0;

        foreach (var entry in progress.levelProgressDict.Values)
        {
            foreach (var level in entry.levels)
            {
                if (level.isCompleted)
                {
                    totalSoal++;
                    if (level.isCorrect) totalBenar++;
                }
            }
        }

        float persen = (totalSoal > 0) ? (totalBenar / totalSoal) * 100f : 0f;

        fillBar.fillAmount = persen / 100f;
        percentText.text = Mathf.RoundToInt(persen) + "%";

        Debug.Log($"[Akun] Progres total: {persen}%");

        // TODO: tampilkan di UI piala
    }


    public void OnClickLanjut()
    {
        if (SceneTransitioner.Instance != null)
        {
            SceneTransitioner.Instance.LoadSceneWithTransition("OperationMenu");
        }
        else
        {
            Debug.LogWarning("SceneTransitionUI belum ada di scene ini!");
            SceneManager.LoadScene("OperationMenu"); // fallback
        }
    }


}
