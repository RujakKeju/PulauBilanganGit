using UnityEngine;

public class ClickSoundPlayer : MonoBehaviour
{
    public static ClickSoundPlayer Instance;

    [Header("Sound Settings")]
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton & DontDestroy
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Auto-attach AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // klik kiri mouse
        {
            PlayClick();
        }
    }

    public void PlayClick()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
