using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Scriptable_object : ScriptableObject
{
    [Header("Item Basic Info")]
    public string item_name = "New Item";
    public Sprite itemIcon;  // Icon hiển thị trong inventory

    [TextArea(3, 5)]
    public string description = "Item description here";

    [Header("Item Properties")]
    public ItemType itemType;
    public bool IsUseableAnywhere = false;
    public int maxStack = 99;

    [Header("Item Stats (Optional)")]
    public int healAmount = 0;
    public int damage = 0;
    public int defense = 0;
}

// ✅ Enum cho loại item
public enum ItemType
{
    Consumable,    // Đồ tiêu hao (potion, food)
    Weapon,        // Vũ khí
    Armor,         // Giáp
    Material,      // Nguyên liệu
    Quest,         // Quest item
    Gem,           // Đá quý
    UpgradeItem    // Đồ nâng cấp
}