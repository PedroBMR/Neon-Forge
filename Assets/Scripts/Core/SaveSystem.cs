using UnityEngine;
using System;
using System.Collections.Generic;

public static class SaveSystem
{
    const string SaveKey = "save_data";

    public static void Save()
    {
        SaveData data = new SaveData
        {
            level = GameManager.I.level,
            credits = GameManager.I.credits,
            manaEssence = GameManager.I.manaEssence,
            tapPower = GameManager.I.tapPower,
            dps = GameManager.I.dps,
            rankChanceBonus = GameManager.I.rankChanceBonus,
            negativeRngReduction = GameManager.I.negativeRngReduction,
            top10 = new List<int>(GameManager.I.top10)
        };

        foreach (WeaponType wt in Enum.GetValues(typeof(WeaponType)))
        {
            if (PlayerPrefs.GetInt($"weapon_{wt}", 0) == 1)
                data.unlockedWeapons.Add(wt);
        }

        foreach (var kvp in GameManager.I.weaponUpgrades)
        {
            data.upgrades.Add(new SaveData.WeaponUpgradeData
            {
                weaponType = kvp.Key,
                stats = kvp.Value
            });
        }

        if (VIPGallery.I != null)
        {
            foreach (var order in VIPGallery.I.Orders)
            {
                data.vips.Add(new SaveData.OrderSaveData
                {
                    weaponType = order.weaponSpec.weaponType,
                    minPayment = order.minPayment,
                    maxPayment = order.maxPayment,
                    requiresAd = order.requiresAd
                });
            }
        }

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public static void Load()
    {
        if (!PlayerPrefs.HasKey(SaveKey)) return;

        string json = PlayerPrefs.GetString(SaveKey);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        GameManager.I.level = data.level;
        GameManager.I.credits = data.credits;
        GameManager.I.manaEssence = data.manaEssence;
        GameManager.I.tapPower = data.tapPower;
        GameManager.I.dps = data.dps;
        GameManager.I.rankChanceBonus = data.rankChanceBonus;
        GameManager.I.negativeRngReduction = data.negativeRngReduction;
        GameManager.I.top10 = data.top10 ?? new List<int>();

        foreach (WeaponType wt in Enum.GetValues(typeof(WeaponType)))
        {
            int unlocked = data.unlockedWeapons != null && data.unlockedWeapons.Contains(wt) ? 1 : 0;
            PlayerPrefs.SetInt($"weapon_{wt}", unlocked);
        }

        GameManager.I.weaponUpgrades.Clear();
        if (data.upgrades != null)
        {
            foreach (var up in data.upgrades)
            {
                GameManager.I.weaponUpgrades[up.weaponType] = up.stats;
            }
        }

        if (VIPGallery.I != null)
        {
            var orders = new List<OrderData>();
            if (data.vips != null)
            {
                foreach (var v in data.vips)
                {
                    WeaponSpec spec = null;
                    if (WeaponDatabase.I != null && WeaponDatabase.I.weaponSpecs != null)
                    {
                        foreach (var ws in WeaponDatabase.I.weaponSpecs)
                        {
                            if (ws.weaponType == v.weaponType)
                            {
                                spec = ws;
                                break;
                            }
                        }
                    }
                    orders.Add(new OrderData
                    {
                        weaponSpec = spec,
                        minPayment = v.minPayment,
                        maxPayment = v.maxPayment,
                        requiresAd = v.requiresAd
                    });
                }
            }
            VIPGallery.I.SetOrders(orders);
        }

        PlayerPrefs.Save();
    }
}
