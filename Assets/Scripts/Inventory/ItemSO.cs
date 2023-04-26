using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public enum ItemType { Generic, Consumable, Weapon, MeleeWeapon }

    [Header("General")]
    public ItemType itemType;
    public Sprite icon;
    public string itemName = "New item";
    public string description = "The best item in the game";

    [Space]
    public bool isStackable = false;

    [Tooltip("How much of this item can be in one stack")]
    [ShowIf("isStackable")]
    public int maxStack = 1;
}
