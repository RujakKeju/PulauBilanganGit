using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelpPanelController : MonoBehaviour
{
    public GameObject overlayPanel; // Panel hitam transparan
    public RectTransform boardPanel; // Panel papan (RectTransform agar bisa di-animasiin)
    public TextMeshProUGUI helpText;
    public Button buttonHelp, buttonClose, buttonSound;
    public AudioSource audioSource;

    [TextArea] public string helpContent;
    public AudioClip voiceClip;
    public AudioClip panelSound;

    private Vector3 offscreenPos;
    private Vector3 onscreenPos;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        // Sembunyikan di awal
        overlayPanel.SetActive(false);
        offscreenPos = new Vector3(0, Screen.height + 500, 0); // posisi di luar layar atas
        onscreenPos = Vector3.zero; // tengah layar
        boardPanel.anchoredPosition = offscreenPos;

        buttonHelp.onClick.AddListener(OpenHelp);
        buttonClose.onClick.AddListener(CloseHelp);
        buttonSound.onClick.AddListener(PlayVoice);
    }

    void OpenHelp()
    {
        overlayPanel.SetActive(true);
        helpText.text = helpContent;
        Time.timeScale = 0f; // pause game

        if (panelSound != null && audioSource != null)
            audioSource.PlayOneShot(panelSound);

        // Pindahkan papan ke tengah (animasi sederhana)
        LeanTween.move(boardPanel, onscreenPos, 0.5f).setEaseOutBack().setIgnoreTimeScale(true);
    }

    void CloseHelp()
    {

        if (panelSound != null && audioSource != null)
            audioSource.PlayOneShot(panelSound);
        // Keluarkan papan ke atas
        LeanTween.move(boardPanel, offscreenPos, 0.5f).setEaseInBack().setIgnoreTimeScale(true)
        .setOnComplete(() => {
            overlayPanel.SetActive(false);
            Time.timeScale = 1f; // resume game
        });
    }

    void PlayVoice()
    {
        if (audioSource != null && voiceClip != null)
        {
            audioSource.PlayOneShot(voiceClip);
        }
    }
}
