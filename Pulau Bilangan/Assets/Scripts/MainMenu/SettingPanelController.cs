using UnityEngine;
using UnityEngine.UI;

public class SettingPanelController : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panelSetting;
    public RectTransform panelPapanSetting;

    [Header("Buttons & Switchbutton")]
    public Button buttonSetting;
    public Button buttonClose;
    public CustomSwitchButton switchSound;
    public CustomSwitchButton switchMusic;


    [Header("Colors")]
    public Color onColor = Color.green;
    public Color offColor = Color.red;

    void Start()
    {
        // Load setting dari PlayerPrefs
        bool isSoundOn = PlayerPrefs.GetInt("SoundSwitch", 1) == 1;
        bool isMusicOn = PlayerPrefs.GetInt("MusicSwitch", 1) == 1;

        if (switchSound != null)
        {
            switchSound.SetState(isSoundOn); // Set posisi awal
            switchSound.OnToggleChanged += SetSound; // Tambah listener
        }

        if (switchMusic != null)
        {
            switchMusic.SetState(isMusicOn);
            switchMusic.OnToggleChanged += SetMusic;
        }

        buttonSetting.onClick.AddListener(ShowPanel);
        buttonClose.onClick.AddListener(ClosePanel);
        panelSetting.SetActive(false);
    }

    void SetSound(bool isOn)
    {
        PlayerPrefs.SetInt("SoundSwitch", isOn ? 1 : 0);

        foreach (var obj in GameObject.FindGameObjectsWithTag("Sound"))
        {
            if (obj.TryGetComponent<AudioSource>(out var audio))
                audio.mute = !isOn;
            else
                obj.SetActive(isOn);
        }
    }

    public void SetMusic(bool isOn)
    {
        PlayerPrefs.SetInt("MusicSwitch", isOn ? 1 : 0);

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
