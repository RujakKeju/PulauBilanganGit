using UnityEngine;

public class LevelListProvider : MonoBehaviour
{
    public static LevelListProvider Instance;

    public LevelListSO additionEasy;
    public LevelListSO additionMedium;
    public LevelListSO additionHard;
    public LevelListSO subtractionEasy;
    public LevelListSO subtractionMedium;
    public LevelListSO subtractionHard;
    public LevelListSO multiplicationEasy;
    public LevelListSO multiplicationMedium;
    public LevelListSO multiplicationHard;
    public LevelListSO divisionEasy;
    public LevelListSO divisionMedium;
    public LevelListSO divisionHard;
    

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
            (MathOperation.Subtraction, Difficulty.Easy) => subtractionEasy,
            (MathOperation.Subtraction, Difficulty.Medium) => subtractionMedium,
            (MathOperation.Subtraction, Difficulty.Hard) => subtractionHard,
            (MathOperation.Multiplication, Difficulty.Easy) => multiplicationEasy,
            (MathOperation.Multiplication, Difficulty.Medium) => multiplicationMedium,
            (MathOperation.Multiplication, Difficulty.Hard) => multiplicationHard,
            (MathOperation.Division, Difficulty.Easy) => divisionEasy,
            (MathOperation.Division, Difficulty.Medium) => divisionMedium,
            (MathOperation.Division, Difficulty.Hard) => divisionHard,


            _ => null
        };
    }
}
