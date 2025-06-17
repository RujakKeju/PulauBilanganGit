using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelBenarController : MonoBehaviour
{
    public GameObject popup;
    public GameObject bintang1, bintang2, bintang3;
    public TextMeshProUGUI textBenar;

    public Button btnMainMenu, btnReplay, btnNext;

    private void OnEnable()
    {
        popup.transform.localScale = Vector3.zero;
        bintang1.SetActive(false);
        bintang2.SetActive(false);
        bintang3.SetActive(false);
        textBenar.gameObject.SetActive(false);

        StartCoroutine(AnimasiPanel());

        // Assign listener (pastikan tidak dobel)
        btnMainMenu.onClick.RemoveAllListeners();
        btnMainMenu.onClick.AddListener(OnMainMenu);

        btnReplay.onClick.RemoveAllListeners();
        btnReplay.onClick.AddListener(OnReplay);

        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(OnNextLevel);
    }

    IEnumerator AnimasiPanel()
    {
        LeanTween.scale(popup, Vector3.one, 0.5f).setEaseOutBack();
        yield return new WaitForSeconds(0.6f);

        bintang1.SetActive(true);
        LeanTween.scale(bintang1, Vector3.one, 0.3f).setFrom(Vector3.zero).setEaseOutBack();
        yield return new WaitForSeconds(0.3f);

        bintang2.SetActive(true);
        LeanTween.scale(bintang2, Vector3.one, 0.3f).setFrom(Vector3.zero).setEaseOutBack();
        yield return new WaitForSeconds(0.3f);

        bintang3.SetActive(true);
        LeanTween.scale(bintang3, Vector3.one, 0.3f).setFrom(Vector3.zero).setEaseOutBack();
        yield return new WaitForSeconds(0.3f);

        textBenar.gameObject.SetActive(true);
        LeanTween.scale(textBenar.gameObject, Vector3.one, 0.4f).setFrom(Vector3.zero).setEaseOutBack();
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("Level(Penjumlahan)");
    }

    public void OnReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnNextLevel()
    {
        var state = GameStateManager.Instance;
        var op = state.selectedOperation;
        var diff = state.selectedDifficulty;

        state.currentLevelIndex++;

        // Ambil level list berdasarkan operasi & difficulty
        LevelListSO list = LevelListProvider.Instance.GetLevelList(op, diff);

        if (state.currentLevelIndex < list.sceneNames.Length)
        {
            string nextScene = list.sceneNames[state.currentLevelIndex];
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("Semua level selesai!");
            SceneManager.LoadScene("MainMenu"); // atau scene 'You Win' kalau ada
        }
    }
}
