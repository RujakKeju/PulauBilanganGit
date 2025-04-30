using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainButton; // Tombol "Main" di layar awal
    [SerializeField] private GameObject characterSelectionPanel; // Panel pilihan karakter

    void Start()
    {
        // Pastikan di awal hanya tombol "Main" yang muncul
        mainButton.SetActive(true);
        characterSelectionPanel.SetActive(false);
    }

    // Panggil fungsi ini saat tombol "Main" ditekan (via OnClick)
    public void OnMainButtonPressed()
    {
        mainButton.SetActive(false);
        characterSelectionPanel.SetActive(true);
    }
}
