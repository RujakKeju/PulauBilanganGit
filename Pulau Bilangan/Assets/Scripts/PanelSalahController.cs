using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelSalahController : MonoBehaviour // Changed class name
{
    public GameObject popup;
    public GameObject bintang1;
    public TextMeshProUGUI textSalah; // Changed variable name to textSalah

    public Button btnHome, btnMenu, btnNext;

    public EasyLevelManager easyLevelManager;
    public MediumLevelManager mediumLevelManager;
    public HardLevelManager hardLevelManager;
    public SharkLevelManager sharkLevelManager;


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
                easyLevelManager.TombolNextSoal(false);

            if (mediumLevelManager != null)
                mediumLevelManager.TombolNextSoal(false);

            if (hardLevelManager != null)
                hardLevelManager.TombolNextSoal(false);

            if (sharkLevelManager != null)
                sharkLevelManager.TombolNextSoal(false);


            OnNextSoalDariPanelSalah();
        });
    }



    private void OnEnable()
    {
        var state = GameStateManager.Instance;

        string key = state.GetProgressKey();

        var progress = SaveLoadSystem.LoadProgress();

        popup.transform.localScale = Vector3.zero;
        bintang1.SetActive(false);
        textSalah.gameObject.SetActive(false); // Referencing textSalah

        StartCoroutine(AnimasiPanel());

        btnHome.onClick.RemoveAllListeners();
        btnHome.onClick.AddListener(OnMainMenu);

        btnMenu.onClick.RemoveAllListeners();
        btnMenu.onClick.AddListener(() =>
        {
            string op = ConvertOperationToBahasa(state.selectedOperation);
            SceneTransitioner.Instance.LoadSceneWithTransition("Level(" + op + ")");
        });
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
            string finishSceneName = "FinishScene(" + ConvertOperationToBahasa(state.selectedOperation) + ")";
            SceneTransitioner.Instance.LoadSceneWithTransition(finishSceneName);
        }
    }
    public void OnMainMenu()
    {
        SceneTransitioner.Instance.LoadSceneWithTransition("MainMenu");
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