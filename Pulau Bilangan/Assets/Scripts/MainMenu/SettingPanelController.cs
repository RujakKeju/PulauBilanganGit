using UnityEngine;
using UnityEngine.UI;

public class SettingPanelController : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panelSetting;
    public RectTransform panelPapanSetting;

    [Header("Buttons & Toggles")]
    public Button buttonSetting;
    public Button buttonClose;
    public Toggle toggleSound;
    public Toggle toggleMusic;

    [Header("Colors")]
    public Color onColor = Color.green;
    public Color offColor = Color.red;

    void Start()
    {
        // Load setting dari PlayerPrefs
        bool isSoundOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        bool isMusicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;

        toggleSound.isOn = isSoundOn;
        toggleMusic.isOn = isMusicOn;

        UpdateToggleVisual(toggleSound, isSoundOn);
        UpdateToggleVisual(toggleMusic, isMusicOn);

        toggleSound.onValueChanged.AddListener(SetSound);
        toggleMusic.onValueChanged.AddListener(SetMusic);

        buttonSetting.onClick.AddListener(ShowPanel);
        buttonClose.onClick.AddListener(ClosePanel);

        // Panel disembunyikan di awal
        panelSetting.SetActive(false);
    }

    void SetSound(bool isOn)
    {
        PlayerPrefs.SetInt("SoundEnabled", isOn ? 1 : 0);
        UpdateToggleVisual(toggleSound, isOn);

        foreach (var obj in GameObject.FindGameObjectsWithTag("Sound"))
        {
            if (obj.TryGetComponent<AudioSource>(out var audio))
                audio.mute = !isOn;
            else
                obj.SetActive(isOn);
        }
    }

    void SetMusic(bool isOn)
    {
        PlayerPrefs.SetInt("MusicEnabled", isOn ? 1 : 0);
        UpdateToggleVisual(toggleMusic, isOn);

        foreach (var obj in GameObject.FindGameObjectsWithTag("Music"))
        {
            if (obj.TryGetComponent<AudioSource>(out var audio))
                audio.mute = !isOn;
            else
                obj.SetActive(isOn);
        }
    }

    void UpdateToggleVisual(Toggle toggle, bool isOn)
    {
        var bg = toggle.targetGraphic;
        if (bg != null)
            bg.color = isOn ? onColor : offColor;
    }

    public void ShowPanel()
    {
        panelSetting.SetActive(true);
        panelPapanSetting.localScale = Vector3.zero;

        LeanTween.scale(panelPapanSetting, Vector3.one, 0.4f).setEaseOutBack();
    }

    public void ClosePanel()
    {
        LeanTween.scale(panelPapanSetting, Vector3.zero, 0.3f).setEaseInBack().setOnComplete(() =>
        {
            panelSetting.SetActive(false);
        });
    }
}
