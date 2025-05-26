using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SempoaController : MonoBehaviour
{
    public GameObject panel_key;
    public GameObject panelSempoa;
    public GameObject ikanPrefab; // prefab kecil untuk ditaruh di grid
    public Transform gridArea; // parent slot tempat ikan diletakkan
    public TextMeshProUGUI countText;
    public Button buttonKunci, buttonClose;
    public int maxIkan = 30;


    private RectTransform sempoaRect;
    private Vector2 offscreenPos;
    private Vector2 onscreenPos;


    private List<GameObject> currentIkan = new List<GameObject>();
    private int jumlahIkan => currentIkan.Count;

    void Start()
    {
        panel_key.SetActive(false);
        panelSempoa.SetActive(false);

        sempoaRect = panelSempoa.GetComponent<RectTransform>();

        offscreenPos = new Vector2(0, Screen.height + 500);
        onscreenPos = Vector2.zero;

        sempoaRect.anchoredPosition = offscreenPos;

        buttonKunci.onClick.AddListener(() =>
        {
            panel_key.SetActive(true);
            panelSempoa.SetActive(true);

            // Transisi dari atas ke tengah
            LeanTween.move(sempoaRect, onscreenPos, 0.5f).setEaseOutBack();
        });

        buttonClose.onClick.AddListener(CloseSempoa);
    }


    public void AddIkan()
    {
        if (jumlahIkan >= maxIkan) return;

        GameObject ikan = Instantiate(ikanPrefab, gridArea);
        currentIkan.Add(ikan);
        UpdateCount();
    }

    void CloseSempoa()
    {
        // Transisi keluar ke atas, lalu matikan panel
        LeanTween.move(sempoaRect, offscreenPos, 0.5f).setEaseInBack().setOnComplete(() =>
        {
            panelSempoa.SetActive(false);
            panel_key.SetActive(false);

            // Reset sempoa
            foreach (var ikan in currentIkan)
                Destroy(ikan);
            currentIkan.Clear();
            UpdateCount();
        });
    }


    void UpdateCount()
    {
        countText.text =  jumlahIkan.ToString();
    }
}
