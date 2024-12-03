using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    public SlotType slotType;
    public int idSlot;
    public Item item;
    public Image itemImage;
    public Text txtItemAmount;
    public bool isShowAmount;

    public void CreateSlot(int id, Item i)
    {
        idSlot = id;
        item = i;
    }

    public void UpdateSlot()
    {
        if(item.isEmpty)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
            if(isShowAmount)
            {
                txtItemAmount.text = "";
            }
        } 
        else
        {
            itemImage.sprite = item.itemSprite;
            itemImage.enabled = true;
            if (isShowAmount)
            {
                txtItemAmount.text = Core.Instance.inventory.GetItemAmount(item);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Core.Instance.inventory.OnDropDone(this);
    }
}
