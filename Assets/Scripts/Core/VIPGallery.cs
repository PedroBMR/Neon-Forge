using System.Collections.Generic;
using UnityEngine;

public class VIPGallery : MonoBehaviour
{
    public static VIPGallery I;
    readonly List<OrderData> vipOrders = new List<OrderData>();

    public int Count => vipOrders.Count;
    public IEnumerable<OrderData> Orders => vipOrders;

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Add(OrderData order)
    {
        if (order != null)
            vipOrders.Add(order);
    }
}

