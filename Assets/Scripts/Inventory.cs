using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum SlotType
{
    Inventory,
    Hotbar,
}

public class Inventory : MonoBehaviour
{

    public Item empty;

    [Header("Itens")]
    public RectTransform itensPanel;
    public GameObject itemSlotPrefab;
    public int itensAmount = 44;

    [Header("Hotbar")]
    public RectTransform hotbarPanel;
    public GameObject hotbarSlotPrefab;
    public int hotbarSlotsAmount = 10;

    [Header("Drag and Drop")]
    public Image itemDrag;
    [HideInInspector] public InventorySlot slotDrag;
    [HideInInspector] public InventorySlot slotDrop;

    public Dictionary<int, InventorySlot> inventorySlots = new Dictionary<int, InventorySlot>();
    public Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    public Dictionary<int, InventorySlot> hotbar = new Dictionary<int, InventorySlot>();

    private bool isInventoryCreated;

    // Start is called before the first frame update
    void Start()
    {
        CreateItensSlot();
    }

    public void CreateItensSlot()
    {
        if (isInventoryCreated)
            return;

        for (int i = 0; i < itensAmount; i++)
        {
            GameObject slotInstance  = Instantiate(itemSlotPrefab, itensPanel);
            InventorySlot inventorySlot = slotInstance.GetComponent<InventorySlot>();
            inventorySlot.CreateSlot(i, empty);

            inventorySlots.Add(i, inventorySlot);
        }

        for (int i = 0; i < hotbarSlotsAmount; i++)
        {
            GameObject slotInstance = Instantiate(hotbarSlotPrefab, hotbarPanel);
            InventorySlot inventorySlot = slotInstance.GetComponent< InventorySlot>();
            inventorySlot.CreateSlot(i, empty);

            hotbar.Add(i, inventorySlot);
        }

        isInventoryCreated = true;

        UpdateInventorySlots();
        UpdateHotbarSlots();
    }

    public void GetItem(Item item, int amount)
    {
        
        if(inventory.ContainsKey(item) && !item.isSingleSlot)
        {
            inventory[item] += amount;
            UpdateInventorySlots();
        }
        else
        {
            if(!IsInventoryFull())
            {
                int firstEmptySlot = inventorySlots.First(x => x.Value.item == empty).Key;
                inventorySlots[firstEmptySlot].item = item;
                if(!item.isSingleSlot)
                {
                    inventory.Add(item, amount);
                }
                UpdateInventorySlots();
            }
        }
    }

    public string GetItemAmount(Item item)
    {
        if (inventory.ContainsKey(item))
        {
            return inventory[item].ToString();
        }

        return "";
    }

    public bool IsInventoryFull()
    {
        bool isFull = true;

        int busy = inventorySlots.Values.Count(x => x.item != empty);
        if(busy < itensAmount)
        {
            isFull = false;
        }

        return isFull;
    }

    public void UpdateInventorySlots()
    {
        foreach(var slot in inventorySlots)
        {
            slot.Value.UpdateSlot();
        }
    }

    public void UpdateHotbarSlots()
    {
        foreach(var slot in hotbar)
        {
            slot.Value.UpdateSlot();
        }
    }

    public void OnDropDone(InventorySlot drop)
    {
        slotDrop = drop;

        Item iDrag = slotDrag.item;
        Item iDrop = slotDrop.item;

        if(slotDrag.slotType == slotDrop.slotType)
        {
            slotDrag.item = iDrop;
            slotDrop.item = iDrag;
        }
        else
        {
            if(slotDrop.slotType == SlotType.Hotbar)
            {
                slotDrop.item = iDrag;
            }
        }

        UpdateInventorySlots();
        UpdateHotbarSlots();
    }
}
