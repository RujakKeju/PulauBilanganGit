using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomSwitchButton : MonoBehaviour
{
    public RectTransform handle;        // lingkaran ●
    public TextMeshProUGUI label;       // teks ON/OFF
    public Color onColor = Color.green;
    public Color offColor = Color.red;
    public float offset = 60f;          // jarak geser handle

    private bool isOn = true;
    private Button button;
    private Image bgImage;

    void Awake()
    {
        button = GetComponent<Button>();
        bgImage = GetComponent<Image>();
        button.onClick.AddListener(Toggle);
    }

    void Start()
    {
        // Load status dari PlayerPrefs
        isOn = PlayerPrefs.GetInt(gameObject.name, 1) == 1;
        ApplyVisual(false);
    }

    void Toggle()
    {
        isOn = !isOn;
        PlayerPrefs.SetInt(gameObject.name, isOn ? 1 : 0);
        ApplyVisual(true);
    }

    void ApplyVisual(bool animate)
    {
        label.text = isOn ? "ON" : "OFF";
        bgImage.color = isOn ? onColor : offColor;

        float targetX = isOn ? offset : -offset;

        if (animate)
        {
            LeanTween.moveLocalX(handle.gameObject, targetX, 0.15f).setEaseOutQuad();
        }
        else
        {
            var pos = handle.localPosition;
            pos.x = targetX;
            handle.localPosition = pos;
        }
    }

    public bool IsOn() => isOn;
}
