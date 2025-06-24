using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CharacterSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject nameInputPanel;
    [SerializeField] private GameObject characterSelectionPanel;

    [SerializeField] private Image selectedCharacterImage;

    [SerializeField] private CharacterDataSO[] characterData;
    [SerializeField] private GameObject characterButtonPrefab;
    [SerializeField] private Transform buttonContainer;

    private List<GameObject> characterButtons = new List<GameObject>();
    private List<GameObject> highlightObjects = new List<GameObject>();



    private int selectedCharacterIndex = 0;
    public static CharacterDataSO SelectedCharacterData { get; private set; }

    void Start()
    {
        GenerateCharacterButtons();


    }

    void GenerateCharacterButtons()
    {
        characterButtons.Clear();
        highlightObjects.Clear();

        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < characterData.Length; i++)
        {
            GameObject btnObj = Instantiate(characterButtonPrefab, buttonContainer);
            Button btn = btnObj.GetComponent<Button>();

            // Cari Image karakter
            Image characterImage = btnObj.transform.Find("CharacterImage")?.GetComponent<Image>();
            if (characterImage != null)
            {
                characterImage.sprite = characterData[i].characterSprite;
            }

            // Cari highlight (boleh null, tapi kita simpan saja)
            GameObject highlight = btnObj.transform.Find("Highlight")?.gameObject;
            if (highlight != null)
            {
                highlight.SetActive(false); // default off
            }
            highlightObjects.Add(highlight); // walau null, tetap disimpan

            characterButtons.Add(btnObj);

            int index = i;
            btn.onClick.AddListener(() => SelectCharacter(index));
        }
    }


    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
        SelectedCharacterData = characterData[index];
        selectedCharacterImage.sprite = characterData[index].characterSprite;

        // Matikan semua highlight
        for (int i = 0; i < highlightObjects.Count; i++)
        {
            if (highlightObjects[i] != null)
                highlightObjects[i].SetActive(false);
        }

        // Aktifkan highlight tombol yang dipilih
        if (highlightObjects[index] != null)
            highlightObjects[index].SetActive(true);

        Debug.Log("Selected character: " + characterData[index].characterName);
    }


    public void ConfirmCharacterSelection()
    {
        PlayerProgress progress = SaveLoadSystem.LoadProgress();
        progress.characterData = characterData[selectedCharacterIndex];
        SaveLoadSystem.SaveProgress(progress);

        characterSelectionPanel.SetActive(false);
        nameInputPanel.SetActive(true);
    }
}
