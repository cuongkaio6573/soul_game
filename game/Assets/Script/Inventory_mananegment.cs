using UnityEngine;

public class Inventory_mananegment : MonoBehaviour
{
    // ✅ Singleton pattern
    public static Inventory_mananegment Instance { get; private set; }

    public inventory_slot[] slots;   // Kéo thả tất cả slot vào đây
    public Scriptable_object[] startingItems;    // Kéo vài ScriptableObject item để test

    void Awake()
    {
        // ✅ Setup singleton
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("⚠️ Đã có Inventory_mananegment khác, hủy duplicate");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject); // Optional: giữ inventory khi chuyển scene

        Debug.Log("✅ Inventory_mananegment Awake được gọi");

        // ✅ Kiểm tra slots có được gán không
        if (slots == null || slots.Length == 0)
        {
            Debug.LogError("❌ Slots chưa được gán trong Inspector! Hãy kéo các inventory_slot vào mảng 'Slots'");
            return;
        }

        Debug.Log($"✅ Inventory có {slots.Length} slots");

        // ✅ Gán index cho từng slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                slots[i].slotIndex = i;
                slots[i].isSpecialSlot = false;
            }
        }

        // Test: add item vào slot
        for (int i = 0; i < startingItems.Length && i < slots.Length; i++)
        {
            if (startingItems[i] != null)
            {
                slots[i].AddItem(startingItems[i]);
                Debug.Log($"✅ Đã thêm starting item: {startingItems[i].item_name}");
            }
        }
    }

    // ✅ Thêm item vào inventory
    public bool Add(Scriptable_object newItem)
    {
        Debug.Log($"🔍 Đang thêm item vào inventory: {newItem.item_name}");

        // ✅ Kiểm tra input hợp lệ
        if (newItem == null)
        {
            Debug.LogWarning("⚠️ Không thể thêm item NULL!");
            return false;
        }

        // ✅ Kiểm tra slots có sẵn sàng không
        if (slots == null || slots.Length == 0)
        {
            Debug.LogError("❌ Inventory không có slots!");
            return false;
        }

        // ✅ Tìm slot trống và thêm item
        foreach (inventory_slot slot in slots)
        {
            if (slot != null && slot.IsEmpty())
            {
                slot.AddItem(newItem);
                Debug.Log($"✅ Đã thêm {newItem.item_name} vào slot {slot.slotIndex}");
                return true; // ✅ Thành công
            }
        }

        // ✅ Không còn slot trống
        Debug.LogWarning("⚠️ Inventory đầy! Không thể thêm item.");
        return false; // ✅ Thất bại
    }

    // ✅ Dùng item từ slot
    public void UseItem(int slotIndex, bool isSpecial)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length)
        {
            Debug.LogWarning($"⚠️ Slot index {slotIndex} không hợp lệ!");
            return;
        }

        inventory_slot slot = slots[slotIndex];

        if (slot == null || slot.IsEmpty())
        {
            Debug.LogWarning($"⚠️ Slot {slotIndex} trống hoặc NULL!");
            return;
        }

        Scriptable_object item = slot.GetItem();

        Debug.Log($"🎯 Đang dùng item: {item.item_name}");

        // TODO: Thêm logic dùng item ở đây
        // Ví dụ: if (item.itemType == ItemType.Potion) { player.Heal(item.healAmount); }

        // ✅ Giảm số lượng hoặc xóa item
        slot.DecreaseQuantity(1);

        Debug.Log($"✅ Đã dùng {item.item_name}");
    }

    // ✅ Phương thức tiện ích: Kiểm tra inventory có đầy không
    public bool IsFull()
    {
        if (slots == null || slots.Length == 0) return true;

        foreach (inventory_slot slot in slots)
        {
            if (slot != null && slot.IsEmpty())
            {
                return false;
            }
        }
        return true;
    }

    // ✅ Phương thức tiện ích: Đếm số slot trống
    public int GetEmptySlotCount()
    {
        if (slots == null || slots.Length == 0) return 0;

        int count = 0;
        foreach (inventory_slot slot in slots)
        {
            if (slot != null && slot.IsEmpty())
            {
                count++;
            }
        }
        return count;
    }

    // ✅ Xóa toàn bộ inventory
    public void ClearAll()
    {
        if (slots == null) return;

        foreach (inventory_slot slot in slots)
        {
            if (slot != null)
            {
                slot.ClearSlot();
            }
        }

        Debug.Log("🗑️ Đã xóa toàn bộ inventory");
    }
}