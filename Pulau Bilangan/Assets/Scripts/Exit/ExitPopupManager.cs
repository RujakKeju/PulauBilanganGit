using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPopupManager : MonoBehaviour
{
    public GameObject exitPopupPanel;

    // Tampilkan popup
    public void ShowExitPopup()
    {
        exitPopupPanel.SetActive(true);
    }

    // Sembunyikan popup
    public void HideExitPopup()
    {
        exitPopupPanel.SetActive(false);
    }

    // Keluar dari game
    public void ExitGame()
    {
        Debug.Log("Keluar dari game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
