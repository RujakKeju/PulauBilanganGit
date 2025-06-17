using UnityEngine;

public class LevelListProvider : MonoBehaviour
{
    public static LevelListProvider Instance;

    public LevelListSO additionEasy;
    public LevelListSO additionMedium;
    public LevelListSO additionHard;
    // dst untuk pengurangan, perkalian, pembagian...

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public LevelListSO GetLevelList(MathOperation op, Difficulty diff)
    {
        return (op, diff) switch
        {
            (MathOperation.Addition, Difficulty.Easy) => additionEasy,
            (MathOperation.Addition, Difficulty.Medium) => additionMedium,
            (MathOperation.Addition, Difficulty.Hard) => additionHard,
            // Tambahkan lainnya...
            _ => null
        };
    }
}
