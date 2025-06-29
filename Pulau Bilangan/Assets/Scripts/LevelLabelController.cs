using TMPro;
using UnityEngine;

public class LevelLabelController : MonoBehaviour
{
    public TextMeshProUGUI labelText;

    void Start()
    {
        var state = GameStateManager.Instance;
        if (state == null || labelText == null) return;

        string operasi = state.selectedOperation switch
        {
            MathOperation.Addition => "Penjumlahan",
            MathOperation.Subtraction => "Pengurangan",
            MathOperation.Multiplication => "Perkalian",
            MathOperation.Division => "Pembagian",
            _ => "Operasi"
        };

        string tingkat = state.selectedDifficulty switch
        {
            Difficulty.Easy => "Mudah",
            Difficulty.Medium => "Sedang",
            Difficulty.Hard => "Sulit",
            _ => "Tingkat"
        };

        int nomor = state.currentLevelIndex + 1;
        labelText.text = $"{operasi} - {tingkat} - {nomor}";
    }
}
