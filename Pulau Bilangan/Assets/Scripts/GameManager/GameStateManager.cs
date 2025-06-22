using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public MathOperation selectedOperation;
    public Difficulty selectedDifficulty;
    public int currentLevelIndex = 0; // level 1–10

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public string GetProgressKey()
    {
        string op = selectedOperation switch
        {
            MathOperation.Addition => "penjumlahan",
            MathOperation.Subtraction => "pengurangan",
            MathOperation.Multiplication => "perkalian",
            MathOperation.Division => "pembagian",
            _ => "unknown"
        };

        return op.ToLower() + "_" + selectedDifficulty.ToString().ToLower();
    }


}
