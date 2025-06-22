using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner Instance;

    [Header("UI Refs")]
    public RectTransform panel;
    public TextMeshProUGUI loadingText;
    public Image fishImage;
    public Sprite[] fishSprites;

    [Header("Audio")]
    public AudioSource transitionSound;

    private int fishIndex = 0;
    private bool isLoading = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void LoadSceneWithTransition(string sceneName)
    {
        if (isLoading) return;
        isLoading = true;

        // WAJIB aktifkan GameObject parent (biasanya ini TransisiManager)
        gameObject.SetActive(true);
        panel.gameObject.SetActive(true); // <- aktifkan PanelLoading juga!

        StartCoroutine(AnimateLoadingText());

        // Reset posisi di luar layar
        panel.anchoredPosition = new Vector2(0, 1080);

        LeanTween.moveY(panel, 0f, 0.8f).setEaseOutBounce().setOnComplete(() =>
        {
            AnimateFish();
            StartCoroutine(LoadSceneCoroutine(sceneName));
        });
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(2f); // Pastikan anim selesai

        // Load scene tapi transition panel tetap aktif karena DontDestroyOnLoad
        SceneManager.LoadScene(sceneName);

        // Tunggu setengah detik agar scene baru stabil
        yield return new WaitForSeconds(1f);

        // Animasi keluar (naik ke atas)
        LeanTween.moveY(panel, Screen.height, 0.8f).setEaseInCubic().setOnComplete(() =>
        {
            isLoading = false;
            gameObject.SetActive(false);
        });


    }    public void LoadSceneAntarSoal(string sceneName)
    {
        if (isLoading) return;
        isLoading = true;

        // WAJIB aktifkan GameObject parent (biasanya ini TransisiManager)
        gameObject.SetActive(true);
        panel.gameObject.SetActive(true); // <- aktifkan PanelLoading juga!

        StartCoroutine(AnimateLoadingText());

        // Reset posisi di luar layar
        panel.anchoredPosition = new Vector2(0, 1080);

        LeanTween.moveY(panel, 0f, 0.8f).setEaseOutBounce().setOnComplete(() =>
        {
            AnimateFish();
            StartCoroutine(LoadSceneCorotinSoal(sceneName));
        });
    }

    IEnumerator LoadSceneCorotinSoal(string sceneName)
    {
        yield return new WaitForSeconds(0.5f); // Pastikan anim selesai

        // Load scene tapi transition panel tetap aktif karena DontDestroyOnLoad
        SceneManager.LoadScene(sceneName);

        // Tunggu setengah detik agar scene baru stabil
        yield return new WaitForSeconds(1f);

        // Animasi keluar (naik ke atas)
        LeanTween.moveY(panel, Screen.height, 0.8f).setEaseInCubic().setOnComplete(() =>
        {
            isLoading = false;
            gameObject.SetActive(false);
        });
    }


    void AnimateFish()
    {
        PulseFish();
    }

    void PulseFish()
    {
        if (!isLoading) return;

        // Scale up
        LeanTween.scale(fishImage.rectTransform, Vector3.one * 1.15f, 0.2f).setEaseOutSine().setOnComplete(() =>
        {
            // Scale down
            LeanTween.scale(fishImage.rectTransform, Vector3.one, 0.2f).setEaseInSine().setOnComplete(() =>
            {
                // Ganti sprite setelah detak
                fishIndex = (fishIndex + 1) % fishSprites.Length;
                fishImage.sprite = fishSprites[fishIndex];

                // Ulangi pulse lagi
                PulseFish(); // recursive call
            });
        });
    }


    IEnumerator AnimateLoadingText()
    {
        string baseText = "Memuat";
        int dot = 0;
        while (isLoading)
        {
            loadingText.text = baseText + new string('.', dot);
            dot = (dot + 1) % 4;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
