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
    public Image selectedkarakter;


    [Header("Input & Display")]
    public TMP_InputField nameInput;
    public TextMeshProUGUI displayNameText;
    public Image characterImage;
    public Image pialaFill;
    public TextMeshProUGUI percentageText;

    [Header("Cek Akun")]
    public TextMeshProUGUI displayNamaTeks;
    public Image piala_palsu;
    public Image karakterImage;
    public TextMeshProUGUI persenTeks;

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
            characterPanel.SetActive(false);
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
            selectedkarakter.sprite = progress.characterData.characterSprite;
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
            displayNamaTeks.text = progress.playerName;

        if (progress.characterData != null && progress.characterData.characterSprite != null)
            characterImage.sprite = progress.characterData.characterSprite;
        karakterImage.sprite = progress.characterData.characterSprite;

        float percentage = HitungTotalPersentase(progress);
        percentageText.text = Mathf.RoundToInt(percentage).ToString() + "%";
        persenTeks.text = Mathf.RoundToInt(percentage).ToString() + "%";

        if (pialaFill != null)
        {
            pialaFill.fillAmount = percentage / 100f;
        }

        if (piala_palsu != null)
        {
            piala_palsu.fillAmount = percentage / 100f;
        }
    }

    float HitungTotalPersentase(PlayerProgress progress)
    {
        int totalScore = 0;
        int maxScore = 12 * 100;

        Debug.Log($"[Persen] Total Keys in scorePerKey: {progress.scorePerKey.Count}");

        foreach (var entry in progress.scorePerKey)
        {
            Debug.Log($"[Persen] Key: {entry.Key} | Score: {entry.Value}");
            totalScore += entry.Value;
        }

        float percentage = (float)totalScore / maxScore * 100f;
        Debug.Log($"[Persen] totalScore: {totalScore} / {maxScore} => {percentage}%");

        return Mathf.Clamp(percentage, 0f, 100f);
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
