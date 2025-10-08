using UnityEngine;

public class ShopToggler : MonoBehaviour
{
    // Kéo đối tượng ShopCanvas vào trường này trong Inspector
    [SerializeField] private GameObject shopCanvas;

    // Biến trạng thái để biết shop đang mở hay đóng
    private bool isShopOpen = false;

    void Start()
    {
        // Đảm bảo ShopCanvas ẩn khi game bắt đầu
        if (shopCanvas != null)
        {
            shopCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Lắng nghe sự kiện nhấn phím 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Gọi hàm để chuyển đổi trạng thái shop
            ToggleShop();
        }
    }

    public void ToggleShop()
    {
        // Chuyển đổi trạng thái (Mở -> Đóng, Đóng -> Mở)
        isShopOpen = !isShopOpen;

        // Thiết lập trạng thái hiển thị của ShopCanvas
        if (shopCanvas != null)
        {
            shopCanvas.SetActive(isShopOpen);
        }

        // Tùy chọn: Xử lý trạng thái thời gian và con trỏ chuột
        if (isShopOpen)
        {
            // Dừng game khi shop mở (tùy chọn)
            // Time.timeScale = 0f; 

            // Hiện con trỏ chuột
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Tiếp tục game
            // Time.timeScale = 1f; 

            // Ẩn con trỏ chuột và khóa nó vào giữa màn hình (nếu là game FPS/3D)
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}