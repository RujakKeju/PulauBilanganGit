using UnityEngine;

public enum Difficulty { Easy, Medium, Hard } //daftar pilihan memilih tingkat kesulitan
public enum MathOperation { Addition, Subtraction, Multiplication, Division } //jenis operasi bilangan

[CreateAssetMenu(fileName = "NewMathLevel", menuName = "MathGame/Level")]
public class MathLevelSO : ScriptableObject
{
    public int bilangan1;
    public int bilangan2;
    public int jawaban;
    public MathOperation operation;
    public Difficulty difficulty;

    [Header("Animal Prefabs")]
    public GameObject animalPrefab1; // Prefab untuk bilangan1
    public GameObject animalPrefab2; // Prefab untuk bilangan2

    public string GetOperationSymbol()
    {
        return operation switch
        {
            MathOperation.Addition => "+",
            MathOperation.Subtraction => "-",
            MathOperation.Multiplication => "x",
            MathOperation.Division => ":",
            _ => "?"
        };
    }
}
