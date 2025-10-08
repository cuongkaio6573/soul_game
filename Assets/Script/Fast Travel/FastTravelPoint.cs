using UnityEngine;

public class FastTravelPoint : MonoBehaviour
{
    public string pointName;       // Tên hiển thị
    public bool unlocked = false;  // Chưa mở khóa

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            unlocked = true;
            Debug.Log(pointName + " đã được mở khóa!");
        }
    }
}
