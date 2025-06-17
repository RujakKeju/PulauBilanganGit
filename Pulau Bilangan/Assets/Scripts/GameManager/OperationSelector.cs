using UnityEngine;
using UnityEngine.SceneManagement;

public class OperationSelector : MonoBehaviour
{
    public void SelectOperation(int operationIndex)
    {
        GameStateManager.Instance.selectedOperation = (MathOperation)operationIndex;

        string sceneName = "";

        switch ((MathOperation)operationIndex)
        {
            case MathOperation.Addition:
                sceneName = "DifficultyMenu(penjumlahan)";
                break;
            case MathOperation.Subtraction:
                sceneName = "DifficultyMenu(pengurangan)";
                break;
            case MathOperation.Multiplication:
                sceneName = "DifficultyMenu(perkalian)";
                break;
            case MathOperation.Division:
                sceneName = "DifficultyMenu(pembagian)";
                break;
        }

        SceneManager.LoadScene(sceneName);
    }
}
