using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenuController_Pembagian : MonoBehaviour
{
    public void SelectDifficulty(int difficultyIndex)
    {
        // Simpan difficulty ke GameStateManager
        GameStateManager.Instance.selectedDifficulty = (Difficulty)difficultyIndex;

        // Karena ini DifficultyMenu(Penjumlahan), langsung load scene Level-nya
        
        SceneTransitioner.Instance.LoadSceneWithTransition("Level(pembagian)");
    }

    public void OnBackToOperationMenu()
    {
        SceneTransitioner.Instance.LoadSceneWithTransition("OperationMenu");
    }

}
