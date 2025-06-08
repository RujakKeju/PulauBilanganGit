using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
}
