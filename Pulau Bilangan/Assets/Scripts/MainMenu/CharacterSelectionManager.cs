using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    // Panel yang ditampilkan
    [SerializeField] private GameObject nameInputPanel;
    [SerializeField] private GameObject characterSelectionPanel;

    // Gambar yang ditampilkan di panel input nama (untuk menunjukkan karakter terpilih)
    [SerializeField] private Image selectedCharacterImage;

    // Data karakter (dari ScriptableObject CharacterDataSO)
    [SerializeField] private CharacterDataSO[] characterData;

    // Prefab tombol karakter dan container untuk instansiasi tombol-tombol tersebut
    [SerializeField] private GameObject characterButtonPrefab;
    [SerializeField] private Transform buttonContainer;

    [SerializeField] private NameInputManager nameInputManager;

    // Variabel untuk menyimpan pilihan
    private int selectedCharacterIndex = 0;
    public static CharacterDataSO SelectedCharacterData { get; private set; }

    void Start()
    {
        GenerateCharacterButtons();
    }

    // Instansiasi tombol berdasarkan data karakter secara dinamis
    void GenerateCharacterButtons()
    {
        // Hapus tombol lama jika ada
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // Buat tombol untuk tiap karakter
        for (int i = 0; i < characterData.Length; i++)
        {
            GameObject btnObj = Instantiate(characterButtonPrefab, buttonContainer);
            Button btn = btnObj.GetComponent<Button>();
            Image btnImage = btnObj.GetComponent<Image>();

            if (btnImage != null)
            {
                btnImage.sprite = characterData[i].characterSprite;
            }

            // Tangkap index di variabel lokal agar listener bekerja dengan benar
            int index = i;
            btn.onClick.AddListener(() => SelectCharacter(index));
        }
    }

    // Saat tombol karakter ditekan
    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
        SelectedCharacterData = characterData[index];
        selectedCharacterImage.sprite = characterData[index].characterSprite;
        Debug.Log("Selected character: " + characterData[index].characterName);

    }

    // Konfirmasi pilihan karakter dan lanjut ke panel input nama
    public void ConfirmCharacterSelection()
    {
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacterIndex);
        PlayerPrefs.SetString("SelectedCharacterName", characterData[selectedCharacterIndex].characterName);

        // Panggil method update melalui referensi nameInputManager
        if (nameInputManager != null)
            nameInputManager.UpdateSelectedCharacterImage();
        else
            Debug.LogError("NameInputManager reference is missing!");

        characterSelectionPanel.SetActive(false);
        nameInputPanel.SetActive(true);
    }
}
