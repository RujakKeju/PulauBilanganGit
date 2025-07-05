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

    private void Awake()
    {
        if (buttonContainer == null) return;

        foreach (Transform child in buttonContainer)
        {
            var highlight = child.Find("Highlight");
            if (highlight != null)
                highlight.gameObject.SetActive(false);
        }
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

            Image bgImage = btnObj.GetComponent<Image>();
            if (bgImage != null)
                bgImage.enabled = false;


            // Cari Image karakter
            Image characterImage = btnObj.transform.Find("CharacterImage")?.GetComponent<Image>();
            if (characterImage != null)
            {
                characterImage.sprite = characterData[i].characterSprite;
            }

            // HIGHLIGHT – ambil dari btnObj YANG SAMA, bukan instansi baru
            GameObject highlight = btnObj.transform.Find("Highlight")?.gameObject;
            if (highlight != null)
            {
                highlight.SetActive(false);
                highlightObjects.Add(highlight);
            }
            else
            {
                highlightObjects.Add(null);
            }

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
        foreach (var obj in highlightObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // Aktifkan yang dipilih
        if (highlightObjects.Count > index && highlightObjects[index] != null)
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
