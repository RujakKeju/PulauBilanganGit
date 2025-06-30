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
    public EasyLevelManager easyLevelManager;
    public MediumLevelManager mediumLevelManager;
    public HardLevelManager hardLevelManager;
    public SharkLevelManager sharkLevelManager;


    public Button btnHome, btnMenu, btnNext;



    void Awake()
    {
        if (easyLevelManager == null)
            easyLevelManager = FindObjectOfType<EasyLevelManager>();
        if (mediumLevelManager == null)
            mediumLevelManager = FindObjectOfType<MediumLevelManager>();
        if (hardLevelManager == null)
            hardLevelManager = FindObjectOfType<HardLevelManager>();
        if (sharkLevelManager == null)
            sharkLevelManager = FindObjectOfType<SharkLevelManager>();


    }
    void Start()
    {


        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(() =>
        {
            if (easyLevelManager != null)
                easyLevelManager.TombolNextSoal(true);

            if (mediumLevelManager != null)
                mediumLevelManager.TombolNextSoal(true);

            if (hardLevelManager != null)
                hardLevelManager.TombolNextSoal(true);
            if (sharkLevelManager != null)
                sharkLevelManager.TombolNextSoal(true);

            OnNextLevelBenar();
        });

    }



    private void OnEnable()
    {
        var state = GameStateManager.Instance;

        string key = state.GetProgressKey();

        var progress = SaveLoadSystem.LoadProgress();

        popup.transform.localScale = Vector3.zero;
        bintang1.SetActive(false);
        bintang2.SetActive(false);
        bintang3.SetActive(false);
        textBenar.gameObject.SetActive(false);

        // ▶️ Putar suara menang saat panel muncul
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayWin();
        }

        StartCoroutine(AnimasiPanel());

        btnHome.onClick.RemoveAllListeners();
        btnHome.onClick.AddListener(OnMainMenu);

        btnMenu.onClick.RemoveAllListeners();
        btnMenu.onClick.AddListener(() =>
        {
            string op = ConvertOperationToBahasa(state.selectedOperation);
            SceneTransitioner.Instance.LoadSceneWithTransition("Level(" + op + ")");
        });

        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(OnNextLevelBenar);
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
        SceneTransitioner.Instance.LoadSceneWithTransition("MainMenu");
    }

    public void OnLevel()
    {



    }

    public void OnNextLevelBenar()
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
            string finishSceneName = "FinishScene(" + ConvertOperationToBahasa(state.selectedOperation) + ")";
            SceneTransitioner.Instance.LoadSceneWithTransition(finishSceneName);
        }
    }

    public string ConvertOperationToBahasa(MathOperation op)
    {
        return op switch
        {
            MathOperation.Addition => "penjumlahan",
            MathOperation.Subtraction => "pengurangan",
            MathOperation.Multiplication => "perkalian",
            MathOperation.Division => "pembagian",
            _ => "unknown"
        };
    }

}
