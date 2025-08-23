using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int level;
    public double credits;
    public double manaEssence;
    public double tapPower;
    public double dps;
    public double rankChanceBonus;
    public double negativeRngReduction;
    public List<WeaponType> unlockedWeapons = new List<WeaponType>();
    public List<WeaponUpgradeData> upgrades = new List<WeaponUpgradeData>();
    public List<OrderSaveData> vips = new List<OrderSaveData>();
    public List<int> top10 = new List<int>();

    [Serializable]
    public class WeaponUpgradeData
    {
        public WeaponType weaponType;
        public GameManager.WeaponUpgradeStats stats;
    }

    [Serializable]
    public class OrderSaveData
    {
        public WeaponType weaponType;
        public int minPayment;
        public int maxPayment;
        public bool requiresAd;
    }
}
