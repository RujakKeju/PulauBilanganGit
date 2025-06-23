using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class NameInputManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject characterPanel;
    public GameObject namePanel;
    public GameObject cekAkunPanel;
    public GameObject confirmDeletePanel;

    [Header("Input & Display")]
    public TMP_InputField nameInput;
    public TextMeshProUGUI displayNameText;
    public Image characterImage;
    public Image pialaFill;
    public TextMeshProUGUI percentageText;

    [Header("Buttons")]
    public Button mainButton;
    public Button confirmDeleteYesButton;
    public Button confirmDeleteNoButton;

    void Start()
    {
        mainButton.onClick.AddListener(OnMainButtonPressed);
        //confirmDeleteYesButton.onClick.AddListener(DeleteAccount);
        //confirmDeleteNoButton.onClick.AddListener(() => confirmDeletePanel.SetActive(false));

        var progress = SaveLoadSystem.LoadProgress();
        if (!string.IsNullOrEmpty(progress.playerName) && progress.characterData != null)
        {
            characterPanel.SetActive(false);
            namePanel.SetActive(false);
            cekAkunPanel.SetActive(true);
        }
        else
        {
            characterPanel.SetActive(true);
            namePanel.SetActive(false);
            cekAkunPanel.SetActive(false);
        }

        LoadAndDisplayAccountData();
    }


    void OnMainButtonPressed()
    {
        var progress = SaveLoadSystem.LoadProgress();

        if (!string.IsNullOrEmpty(progress.playerName) && progress.characterData != null)
        {
            characterPanel.SetActive(false);
            namePanel.SetActive(false);
            cekAkunPanel.SetActive(true);
        }
        else
        {
            characterPanel.SetActive(true);
            namePanel.SetActive(false);
            cekAkunPanel.SetActive(false);
        }
    }

    public void OnConfirmNameButton()
    {
        string inputName = nameInput.text.Trim();
        if (string.IsNullOrEmpty(inputName)) return;

        // Simpan data
        var progress = SaveLoadSystem.LoadProgress();
        progress.playerName = inputName;
        SaveLoadSystem.SaveProgress(progress);

        // Langsung buka panel cek akun
        characterPanel.SetActive(false);
        namePanel.SetActive(false);
        cekAkunPanel.SetActive(true);

        // Refresh tampilan info akun
        LoadAndDisplayAccountData();
    }


    void LoadAndDisplayAccountData()
    {
        var progress = SaveLoadSystem.LoadProgress();

        if (!string.IsNullOrEmpty(progress.playerName))
            displayNameText.text = progress.playerName;

        if (progress.characterData != null && progress.characterData.characterSprite != null)
            characterImage.sprite = progress.characterData.characterSprite;

        float percentage = HitungTotalPersentase(progress);
        percentageText.text = Mathf.RoundToInt(percentage).ToString() + "%";

        if (pialaFill != null)
        {
            pialaFill.fillAmount = percentage / 100f;
        }
    }

    float HitungTotalPersentase(PlayerProgress progress)
    {
        int totalBenar = 0;
        int totalSoal = 0;

        foreach (var entry in progress.levelProgressDict)
        {
            foreach (var lvl in entry.Value.levels)
            {
                if (lvl.isCompleted)
                {
                    totalSoal++;
                    if (lvl.isCorrect) totalBenar++;
                }
            }
        }

        if (totalSoal == 0) return 0f;
        return (float)totalBenar / totalSoal * 100f;
    }

    public void DeleteAccount()
    {
        PlayerProgress newProgress = new PlayerProgress();
        SaveLoadSystem.SaveProgress(newProgress);

        characterPanel.SetActive(true);
        namePanel.SetActive(false);
        cekAkunPanel.SetActive(false);
        confirmDeletePanel.SetActive(false);
    }

    // Fungsi tombol hapus akun
    public void OnDeleteButtonPressed()
    {
        confirmDeletePanel.SetActive(true);
    }

    public void OnClickLanjut()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OperationMenu");
    }

}
