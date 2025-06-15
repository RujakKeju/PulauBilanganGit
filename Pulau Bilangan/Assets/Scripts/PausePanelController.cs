using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 
public class PausePanelController : MonoBehaviour
{
    // Variabel publik yang akan diisi di Inspector Unity
    public GameObject overlayPanel; 
    public RectTransform boardPanel; 
    public Button buttonPause;
    public Button buttonClose; 
    public Button buttonLanjut; 
    public Button buttonSetting;
    public Button buttonMainMenu;

    // Variabel privat untuk posisi animasi
    private Vector2 offscreenTopPos; 
    private Vector2 onscreenCenterPos;

    void Awake() // Gunakan Awake untuk memastikan setup awal sebelum Start
    {
        
        offscreenTopPos = new Vector2(boardPanel.anchoredPosition.x, Screen.height + boardPanel.rect.height);
        onscreenCenterPos = Vector2.zero;

        // Pastikan overlay dan papan tersembunyi di awal
        overlayPanel.SetActive(false);
        boardPanel.anchoredPosition = offscreenTopPos; // Atur posisi awal offscreen

        // Tambahkan listener untuk klik tombol
        buttonPause.onClick.AddListener(OpenPauseMenu);
        buttonClose.onClick.AddListener(ClosePauseMenu); 
        buttonLanjut.onClick.AddListener(ResumeGame); 
        buttonSetting.onClick.AddListener(OpenSettingsMenu); 
        buttonMainMenu.onClick.AddListener(ReturnToMainMenu);
    }

    
    public void OpenPauseMenu()
    {
        if (overlayPanel.activeSelf) return; // Mencegah membuka jika sudah terbuka

        overlayPanel.SetActive(true);
        Time.timeScale = 0f; // Jeda waktu game

        // Animasikan panel papan ke tengah layar
        LeanTween.move(boardPanel, onscreenCenterPos, 0.5f)
            .setEaseOutBack()
            .setIgnoreTimeScale(true); // Animasi harus tetap berjalan meskipun waktu diskalakan ke 0
    }

    
    public void ClosePauseMenu()
    {
        // Animasikan panel papan keluar layar (ke atas)
        LeanTween.move(boardPanel, offscreenTopPos, 0.5f)
            .setEaseInBack()
            .setIgnoreTimeScale(true) // Animasi harus tetap berjalan meskipun waktu diskalakan ke 0
            .setOnComplete(() => {
                overlayPanel.SetActive(false);
                Time.timeScale = 1f; // Lanjutkan waktu game
            });
    }

    
    public void ResumeGame()
    {
        ClosePauseMenu(); // Menggunakan kembali logika ClosePauseMenu
    }

    public void OpenSettingsMenu()
    {
        Debug.Log("Membuka Menu Pengaturan...");
        ClosePauseMenu();
    }

   
    public void ReturnToMainMenu()
    {
        Debug.Log("Kembali ke Menu Utama...");
        Time.timeScale = 1f; // Pastikan skala waktu diatur ulang sebelum memuat scene baru
        SceneManager.LoadScene("MainMenu"); // Ganti "MainMenu" dengan nama scene menu utama Anda yang sebenarnya
    }
}