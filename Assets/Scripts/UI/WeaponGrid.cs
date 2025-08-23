using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGrid : MonoBehaviour
{
    [Serializable]
    public class WeaponEntry
    {
        public string name;
        public WeaponType weaponType;
        public int cost;
        public bool isPremium;
    }

    public Transform container;
    public WeaponCard cardPrefab;
    public WeaponEntry[] weapons;

    void Start()
    {
        if (weapons == null || weapons.Length == 0)
        {
            weapons = new WeaponEntry[]
            {
                new WeaponEntry{ name = WeaponType.Sword.ToString(), weaponType = WeaponType.Sword, cost = 100, isPremium = false },
                new WeaponEntry{ name = WeaponType.Axe.ToString(), weaponType = WeaponType.Axe, cost = 150, isPremium = false },
                new WeaponEntry{ name = WeaponType.Dagger.ToString(), weaponType = WeaponType.Dagger, cost = 200, isPremium = false },
                new WeaponEntry{ name = WeaponType.Spear.ToString(), weaponType = WeaponType.Spear, cost = 250, isPremium = false },
                new WeaponEntry{ name = WeaponType.WarHammer.ToString(), weaponType = WeaponType.WarHammer, cost = 300, isPremium = false },
                new WeaponEntry{ name = "Pacote 1", weaponType = WeaponType.Sword, cost = 0, isPremium = true },
                new WeaponEntry{ name = "Pacote 2", weaponType = WeaponType.Axe, cost = 0, isPremium = true }
            };
        }

        foreach (var w in weapons)
        {
            var card = Instantiate(cardPrefab, container);
            card.Setup(this, w);
        }
    }

    public bool IsUnlocked(WeaponType type)
    {
        return PlayerPrefs.GetInt($"weapon_{type}", 0) == 1;
    }

    public int GetGold()
    {
        return PlayerPrefs.GetInt("gold", 0);
    }

    public void TryUnlock(WeaponEntry entry)
    {
        if (entry.isPremium) return;
        if (IsUnlocked(entry.weaponType)) return;
        int gold = GetGold();
        if (gold < entry.cost) return;
        gold -= entry.cost;
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt($"weapon_{entry.weaponType}", 1);
        PlayerPrefs.Save();
        SaveSystem.Save();
    }
}
