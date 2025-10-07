using UnityEngine;
using System.Collections.Generic;

public class FastTravelSystem : MonoBehaviour
{
    public static FastTravelSystem instance;
    public List<FastTravelPoint> travelPoints = new List<FastTravelPoint>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void TravelTo(string pointName, GameObject player)
    {
        foreach (FastTravelPoint p in travelPoints)
        {
            if (p.pointName == pointName && p.unlocked)
            {
                player.transform.position = p.transform.position;
                Debug.Log("Dịch chuyển tới: " + p.pointName);
                return;
            }
        }
        Debug.Log("Điểm này chưa được mở khóa!");
    }
}
