using System;
using UnityEngine;

[Serializable]
public class OrderData
{
    public WeaponSpec weaponSpec;
    public int minPayment;
    public int maxPayment;
    // Se verdadeiro, o pedido exige assistir a um anúncio antes de ser aceito
    public bool requiresAd;
}
