using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject nameInputPanel;
    [SerializeField] private GameObject characterSelectionPanel;

    [SerializeField] private Image selectedCharacterImage;

    [SerializeField] private CharacterDataSO[] characterData;
    [SerializeField] private GameObject characterButtonPrefab;
    [SerializeField] private Transform buttonContainer;

    private int selectedCharacterIndex = 0;
    public static CharacterDataSO SelectedCharacterData { get; private set; }

    void Start()
    {
        GenerateCharacterButtons();
    }

    void GenerateCharacterButtons()
    {
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < characterData.Length; i++)
        {
            GameObject btnObj = Instantiate(characterButtonPrefab, buttonContainer);
            Button btn = btnObj.GetComponent<Button>();
            Image btnImage = btnObj.GetComponent<Image>();

            if (btnImage != null)
            {
                btnImage.sprite = characterData[i].characterSprite;
            }

            int index = i;
            btn.onClick.AddListener(() => SelectCharacter(index));
        }
    }

    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
        SelectedCharacterData = characterData[index];
        selectedCharacterImage.sprite = characterData[index].characterSprite;
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
