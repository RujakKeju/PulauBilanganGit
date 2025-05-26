using UnityEngine;
using UnityEngine.EventSystems;

public class DragSpawner : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SempoaController controller;
    private GameObject previewIkan;

    public GameObject ikanPreviewPrefab;

    public void OnBeginDrag(PointerEventData eventData)
    {
        previewIkan = Instantiate(ikanPreviewPrefab, transform.root);
        previewIkan.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (previewIkan != null)
            previewIkan.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(
            controller.gridArea.GetComponent<RectTransform>(), eventData.position))
        {
            controller.AddIkan();
        }

        if (previewIkan != null)
            Destroy(previewIkan);
    }
}
