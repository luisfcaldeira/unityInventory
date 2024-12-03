using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public InventorySlot slot;
    public RectTransform rectTransform;
    [HideInInspector] public Image itemDrag;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(slot.item.isEmpty)
        {
            return;
        }

        itemDrag = Core.Instance.inventory.itemDrag;
        itemDrag.sprite = slot.item.itemSprite;
        itemDrag.enabled = true;
        itemDrag.rectTransform.position = transform.position;
        itemDrag.GetComponent<CanvasGroup>().blocksRaycasts = false; // pra saber onde soltou o item

        Core.Instance.inventory.slotDrag = slot;
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemDrag.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemDrag.enabled = false;
        itemDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
