using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ShopItem> shopItems;

    [System.Serializable]
    public class ShopItem
    {
        public ItemSO itemSO;
        public int price;
    }
}
