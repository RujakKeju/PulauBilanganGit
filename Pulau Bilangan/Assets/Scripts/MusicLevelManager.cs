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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        if (IsSceneInAnyLevelList(sceneName))
        {
            if (GameStateManager.Instance != null)
                PlayMusicByDifficulty(GameStateManager.Instance.selectedDifficulty);
        }
        else
        {
            if (musicSource.isPlaying)
                musicSource.Stop();
        }
    }


    private bool IsSceneInAnyLevelList(string sceneName)
    {
        var allLists = new[]
        {
        LevelListProvider.Instance.additionEasy,
        LevelListProvider.Instance.additionMedium,
        LevelListProvider.Instance.additionHard,
        LevelListProvider.Instance.subtractionEasy,
        LevelListProvider.Instance.subtractionMedium,
        LevelListProvider.Instance.subtractionHard,
        LevelListProvider.Instance.multiplicationEasy,
        LevelListProvider.Instance.multiplicationMedium,
        LevelListProvider.Instance.multiplicationHard,
        LevelListProvider.Instance.divisionEasy,
        LevelListProvider.Instance.divisionMedium,
        LevelListProvider.Instance.divisionHard
    };

        foreach (var levelList in allLists)
        {
            foreach (string s in levelList.sceneNames)
            {
                if (s == sceneName)
                    return true;
            }
        }

        return false;
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

    private void PlayMusicByDifficulty(Difficulty difficulty)
    {
        AudioClip clip = null;

        switch (difficulty)
        {
            case Difficulty.Easy:
                clip = easyClip;
                break;
            case Difficulty.Medium:
                clip = mediumClip;
                break;
            case Difficulty.Hard:
                clip = hardClip;
                break;
        }

        if (clip != null && musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

}
