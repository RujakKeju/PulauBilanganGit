using UnityEngine;
using System.Collections;


public class MainMenuManager : MonoBehaviour
{
    [Header("UI Refs")]
    public GameObject mainButton;
    public GameObject characterSelectionPanel;
    public GameObject cekAkunPanel;

    [Header("Audio")]
    public AudioSource bgmSource;
    public AudioClip bgmClip;
    public float fadeDuration = 1.5f;


    void Start()
    {
        mainButton.SetActive(true);
        characterSelectionPanel.SetActive(false);
        cekAkunPanel.SetActive(false);
        bgmSource = GetComponent<AudioSource>();

        // Setup BGM
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.volume = 0f;
            bgmSource.Play();
            StartCoroutine(FadeInBGM());
        }
    }
    IEnumerator FadeInBGM()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
        bgmSource.volume = 1f;
    }

    public IEnumerator FadeOutBGMAndChangeScene(string sceneName)
    {
        float startVol = bgmSource.volume;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(startVol, 0f, t / fadeDuration);
            yield return null;
        }

        bgmSource.Stop();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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
