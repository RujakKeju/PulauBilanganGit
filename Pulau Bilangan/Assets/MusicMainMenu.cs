using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMainMenu : MonoBehaviour
{

    [Header("Audio")]
    public AudioSource bgmSource;
    public AudioClip bgmClip;
    public float fadeDuration = 1.5f;


    private void Start()
    {
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
}
