using UnityEngine;


[CreateAssetMenu(fileName ="Item", menuName = "Scriptable/item", order = 1)]
public class Item : ScriptableObject
{
    public bool isEmpty;
    public string itemName;
    public Sprite itemSprite;
    public bool isSingleSlot;
}
