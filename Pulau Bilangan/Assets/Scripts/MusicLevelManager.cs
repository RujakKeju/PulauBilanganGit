using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLevelManager : MonoBehaviour
{
    public static MusicLevelManager Instance;

    public AudioSource musicSource;
    public AudioClip easyClip;
    public AudioClip mediumClip;
    public AudioClip hardClip;

    private string[] levelScenePrefixes = new string[] {
        "EasyLevel", "MediumLevel", "HardLevel"
    };

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        if (sceneName.StartsWith("Level(") || IsLevelScene(sceneName))
        {
            var state = GameStateManager.Instance;
            PlayMusicByDifficulty(state.selectedDifficulty);
        }
        else
        {
            // Stop music in non-level scenes
            if (musicSource.isPlaying)
                musicSource.Stop();
        }
    }

    bool IsLevelScene(string sceneName)
    {
        foreach (var prefix in levelScenePrefixes)
        {
            if (sceneName.StartsWith(prefix))
                return true;
        }
        return false;
    }

    void PlayMusicByDifficulty(Difficulty diff)
    {
        if (musicSource == null) return;

        AudioClip clipToPlay = null;
        switch (diff)
        {
            case Difficulty.Easy:
                clipToPlay = easyClip;
                break;
            case Difficulty.Medium:
                clipToPlay = mediumClip;
                break;
            case Difficulty.Hard:
                clipToPlay = hardClip;
                break;
        }

        if (clipToPlay != null && musicSource.clip != clipToPlay)
        {
            musicSource.clip = clipToPlay;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}
