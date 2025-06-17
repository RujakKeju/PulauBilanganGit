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
        accountNameText.text = PlayerPrefs.GetString("PlayerName", "No Name");
        // Gunakan data yang telah disimpan di CharacterSelectionManager untuk menampilkan sprite
        if (CharacterSelectionManager.SelectedCharacterData != null)
        {
            accountCharacterImage.sprite = CharacterSelectionManager.SelectedCharacterData.characterSprite;
        }
        else
        {
            Debug.LogError("SelectedCharacterData is null!");
        }
    }

    public void OnClickLanjut()
    {
        SceneManager.LoadScene("OperationMenu"); // Pastikan nama scene sesuai
    }
}
