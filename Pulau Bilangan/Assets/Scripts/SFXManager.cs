using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Sound Clips")]
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip winSound;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton dan persisten antar scene
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    public void PlayWin()
    {   
    if (winSound != null)
        audioSource.PlayOneShot(winSound);
    }
    public void PlayCorrect()
    {
        if (correctSound != null)
            audioSource.PlayOneShot(correctSound);
    }

    public void PlayWrong()
    {
        if (wrongSound != null)
            audioSource.PlayOneShot(wrongSound);
    }
}
