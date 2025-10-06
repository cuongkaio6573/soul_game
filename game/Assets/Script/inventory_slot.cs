using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class inventory_slot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI References")]
    public Image iconUI;
    public TextMeshProUGUI quantityText;

    [Header("Slot Data")]
    private Scriptable_object currentItem;
    private int currentQuantity = 0;

    [HideInInspector] public int slotIndex;
    [HideInInspector] public bool isSpecialSlot = false;

    private Inventory_mananegment inventoryManager;

    void Awake()
    {
        Debug.Log($"[Slot {gameObject.name}] Awake - Đang tìm references...");

        // Tự động tìm Icon nếu chưa gán
        if (iconUI == null)
        {
            Transform iconTransform = transform.Find("Icon");
            if (iconTransform != null)
            {
                iconUI = iconTransform.GetComponent<Image>();
                Debug.Log($"[Slot {gameObject.name}] Tìm thấy Icon tự động");
            }
            else
            {
                Debug.LogError($"❌ Slot '{gameObject.name}' KHÔNG có child GameObject tên 'Icon'!");
            }
        }

        // Tự động tìm Quantity nếu chưa gán
        if (quantityText == null)
        {
            Transform qtyTransform = transform.Find("Quantity");
            if (qtyTransform != null)
            {
                quantityText = qtyTransform.GetComponent<TextMeshProUGUI>();
            }
        }

        // Kiểm tra cuối cùng
        if (iconUI == null)
        {
            Debug.LogError($"❌ Slot '{gameObject.name}' iconUI vẫn NULL sau Awake!");
        }
        else
        {
            Debug.Log($"✅ Slot '{gameObject.name}' iconUI OK: {iconUI.gameObject.name}");
        }

        inventoryManager = Inventory_mananegment.Instance;
        if (inventoryManager == null)
        {
            inventoryManager = FindObjectOfType<Inventory_mananegment>();
        }

        ClearSlot();
    }

    public bool IsEmpty()
    {
        return currentItem == null;
    }

    public Scriptable_object GetItem()
    {
        return currentItem;
    }

    public int GetQuantity()
    {
        return currentQuantity;
    }

    public void AddItem(Scriptable_object newItem)
    {
        Debug.Log($"🔵 [Slot {slotIndex}] AddItem được gọi với item: {newItem?.item_name}");

        if (newItem == null)
        {
            Debug.LogWarning("⚠️ Không thể thêm item NULL vào slot!");
            return;
        }

        if (IsEmpty())
        {
            Debug.Log($"🔵 [Slot {slotIndex}] Slot trống, đang gọi UpdateSlot...");
            UpdateSlot(newItem, 1);
            Debug.Log($"✅ Slot {slotIndex} nhận item: {newItem.item_name}");
        }
        else if (currentItem == newItem)
        {
            IncreaseQuantity(1);
            Debug.Log($"✅ Slot {slotIndex} tăng số lượng {newItem.item_name}: {currentQuantity}");
        }
        else
        {
            Debug.LogWarning($"⚠️ Slot {slotIndex} đã có item khác!");
        }
    }

    public void UpdateSlot(Scriptable_object newItem, int newQuantity)
    {
        Debug.Log($"🟢 [Slot {slotIndex}] UpdateSlot BẮT ĐẦU - item: {newItem?.item_name}, qty: {newQuantity}");

        currentItem = newItem;
        currentQuantity = Mathf.Max(0, newQuantity);

        // Kiểm tra iconUI
        if (iconUI == null)
        {
            Debug.LogError($"❌ [Slot {slotIndex}] iconUI NULL trong UpdateSlot!");
            return;
        }

        Debug.Log($"🟢 [Slot {slotIndex}] iconUI OK, đang update sprite...");

        if (newItem != null)
        {
            if (newItem.itemIcon == null)
            {
                Debug.LogError($"❌ [Slot {slotIndex}] Item '{newItem.item_name}' không có itemIcon!");
                iconUI.sprite = null;
                iconUI.enabled = false;
            }
            else
            {
                iconUI.sprite = newItem.itemIcon;
                iconUI.color = Color.white;
                iconUI.enabled = true;
                Debug.Log($"✅ [Slot {slotIndex}] ĐÃ SET SPRITE: {newItem.itemIcon.name}");
            }
        }
        else
        {
            iconUI.sprite = null;
            iconUI.enabled = false;
            Debug.Log($"🗑️ [Slot {slotIndex}] Cleared");
        }

        // Update quantity text
        if (quantityText != null)
        {
            if (currentQuantity > 1)
            {
                quantityText.text = currentQuantity.ToString();
                quantityText.enabled = true;
            }
            else
            {
                quantityText.text = "";
                quantityText.enabled = false;
            }
        }

        Debug.Log($"🟢 [Slot {slotIndex}] UpdateSlot HOÀN TẤT");
    }

    public void ClearSlot()
    {
        currentItem = null;
        currentQuantity = 0;

        if (iconUI != null)
        {
            iconUI.sprite = null;
            iconUI.enabled = false;
        }

        if (quantityText != null)
        {
            quantityText.text = "";
            quantityText.enabled = false;
        }
    }

    public void IncreaseQuantity(int amount)
    {
        if (currentItem == null) return;
        UpdateSlot(currentItem, currentQuantity + Mathf.Max(1, amount));
    }

    public void DecreaseQuantity(int amount)
    {
        if (currentItem == null) return;
        int newQty = currentQuantity - Mathf.Max(1, amount);

        if (newQty > 0)
        {
            UpdateSlot(currentItem, newQty);
        }
        else
        {
            ClearSlot();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsEmpty() || inventoryManager == null) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventoryManager.UseItem(slotIndex, isSpecialSlot);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log($"Right-click slot {slotIndex}");
        }
    }
}