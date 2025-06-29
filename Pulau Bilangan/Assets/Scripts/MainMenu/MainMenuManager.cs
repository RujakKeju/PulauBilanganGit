using UnityEngine;
using System.Collections;


public class MainMenuManager : MonoBehaviour
{
    [Header("UI Refs")]
    public GameObject mainButton;
    public GameObject characterSelectionPanel;
    public GameObject cekAkunPanel;


    void Start()
    {
        mainButton.SetActive(true);
        characterSelectionPanel.SetActive(false);
        cekAkunPanel.SetActive(false);

    }





    public void OnMainButtonPressed()
    {
        var progress = SaveLoadSystem.LoadProgress();
        bool hasName = !string.IsNullOrEmpty(progress.playerName);
        bool hasChar = !string.IsNullOrEmpty(progress.characterName);

        mainButton.SetActive(false);

        if (hasName && hasChar)
        {
            // Langsung masuk ke panel cek akun
            cekAkunPanel.SetActive(true);
            characterSelectionPanel.SetActive(false);
        }
        else
        {
            // Masuk ke panel pilih karakter
            characterSelectionPanel.SetActive(true);
            cekAkunPanel.SetActive(false);
        }
    }
}
