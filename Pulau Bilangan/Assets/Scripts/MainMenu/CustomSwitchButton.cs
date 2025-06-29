using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomSwitchButton : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Color onColor = Color.green;
    public Color offColor = Color.red;

    public delegate void SwitchChanged(bool isOn);
    public event SwitchChanged OnToggleChanged;

    private bool isOn = true;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Toggle);
    }

    void Start()
    {
        isOn = PlayerPrefs.GetInt(gameObject.name, 1) == 1;
        ApplyVisual();
    }

    void Toggle()
    {
        isOn = !isOn;
        PlayerPrefs.SetInt(gameObject.name, isOn ? 1 : 0);
        ApplyVisual();
        OnToggleChanged?.Invoke(isOn);
    }

    public void SetState(bool state)
    {
        isOn = state;
        ApplyVisual();
    }

    private void ApplyVisual()
    {
        if (label != null)
            label.text = isOn ? "ON" : "OFF";

        if (button != null)
        {
            var colors = button.colors;
            colors.normalColor = isOn ? onColor : offColor;
            colors.selectedColor = colors.normalColor;
            button.colors = colors;
        }
    }
}
