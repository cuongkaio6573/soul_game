using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Kéo InventoryPanel (GameObject UI trong Canvas) vào đây")]
    public GameObject inventoryPanel;  // ✅ Đổi tên rõ ràng hơn

    private PlayerInputActions inputActions;
    private InputAction inventoryAction;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        // ✅ Kiểm tra ngay khi Awake
        if (inventoryPanel == null)
        {
            Debug.LogError("❌ INVENTORY PANEL chưa được gán trong Inspector!\n" +
                           "Hãy:\n" +
                           "1. Chọn GameObject có script InventoryUI (temp_player hoặc Canvas)\n" +
                           "2. Tìm component InventoryUI\n" +
                           "3. Kéo GameObject 'InventoryPanel' từ Canvas vào ô 'Inventory Panel'");
        }
        else
        {
            Debug.Log($"✅ InventoryPanel đã được gán: '{inventoryPanel.name}'");
            // ✅ Bắt đầu với inventory đóng
            inventoryPanel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        inventoryAction = inputActions.Player.Inventory;
        inputActions.Player.Enable();

        Debug.Log("✅ InventoryUI Input Actions enabled");
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        inputActions?.Dispose();
    }

    void Update()
    {

        // ✅ Kiểm tra null trước khi dùng
        if (inventoryAction == null) return;

        if (inventoryAction.WasPressedThisFrame())
        {
            Debug.Log("📥 Tab pressed -> ToggleInventory()");
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("❌ Inventory Panel NULL! Không thể toggle.");
            return;
        }

        bool newState = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(newState);

        Debug.Log($"✅ Inventory: {(newState ? "MỞ 📂" : "ĐÓNG 📁")}");

        // ✅ Optional: Tạm dừng game khi mở inventory
        // Time.timeScale = newState ? 0f : 1f;
    }

    // ✅ Public methods để code khác có thể gọi
    public void OpenInventory()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(true);
            Debug.Log("📂 Inventory mở");
        }
    }

    public void CloseInventory()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
            Debug.Log("📁 Inventory đóng");
        }
    }

    public bool IsInventoryOpen()
    {
        return inventoryPanel != null && inventoryPanel.activeSelf;
    }
}