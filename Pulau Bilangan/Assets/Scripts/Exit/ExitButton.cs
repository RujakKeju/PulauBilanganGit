using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    // Fungsi ini bisa dipanggil oleh tombol
    public void ExitGame()
    {
        Debug.Log("Keluar dari game...");

        // Keluar dari play mode jika di editor Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Keluar dari game jika sudah dibuild (Windows, Android, dll)
        Application.Quit();
#endif
    }
}