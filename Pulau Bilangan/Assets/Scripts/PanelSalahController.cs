using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelSalahController : MonoBehaviour // Changed class name
{
    public GameObject popup;
    public GameObject bintang1;
    public TextMeshProUGUI textSalah; // Changed variable name to textSalah

    public Button btnMainMenu, btnReplay, btnNext;

    public EasyLevelManager easyLevelManager;


    void Awake()
    {
        if (easyLevelManager == null)
        {
            easyLevelManager = FindObjectOfType<EasyLevelManager>();
        }
    }


    void Start()
    {
        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(() =>
        {
            easyLevelManager.TombolNextSoal(false);
            OnNextSoalDariPanelSalah();
        });
    }



    private void OnEnable()
    {
        popup.transform.localScale = Vector3.zero;
        bintang1.SetActive(false);
        textSalah.gameObject.SetActive(false); // Referencing textSalah

        StartCoroutine(AnimasiPanel());
    }

    IEnumerator AnimasiPanel()
    {
        LeanTween.scale(popup, Vector3.one, 0.5f).setEaseOutBack();
        yield return new WaitForSeconds(0.6f);

        bintang1.SetActive(true);
        LeanTween.scale(bintang1, Vector3.one, 0.3f).setFrom(Vector3.zero).setEaseOutBack();
        yield return new WaitForSeconds(0.6f);

        textSalah.gameObject.SetActive(true); // Referencing textSalah
        LeanTween.scale(textSalah.gameObject, Vector3.one, 0.4f).setFrom(Vector3.zero).setEaseOutBack();
    }

    public void OnNextSoalDariPanelSalah()
    {
        GameStateManager.Instance.currentLevelIndex++; // <– satu-satunya tempat naik index

        var state = GameStateManager.Instance;
        var list = LevelListProvider.Instance.GetLevelList(state.selectedOperation, state.selectedDifficulty);

        if (state.currentLevelIndex < list.sceneNames.Length)
        {
            string nextScene = list.sceneNames[state.currentLevelIndex];
            SceneTransitioner.Instance.LoadSceneAntarSoal(nextScene);
        }
        else
        {
            SceneTransitioner.Instance.LoadSceneWithTransition("FinishPoin");
        }
    }

}