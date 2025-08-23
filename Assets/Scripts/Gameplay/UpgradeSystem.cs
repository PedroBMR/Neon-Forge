using UnityEngine;
using System;
using System.Collections.Generic;

public class UpgradeSystem : MonoBehaviour
{
    public UpgradeCard cardPrefab;
    public Transform content;

    List<UpgradeData> upgrades = new List<UpgradeData>();

    void Start()
    {
        BuildUpgrades();
        foreach (var up in upgrades)
        {
            var card = Instantiate(cardPrefab, content);
            card.Setup(this, up);
        }
    }

    void BuildUpgrades()
    {
        upgrades.Add(new UpgradeData
        {
            category = UpgradeCategory.General,
            generalType = GeneralUpgradeType.Tap,
            baseCost = 15,
            growth = 1.15,
            amount = 1
        });
        upgrades.Add(new UpgradeData
        {
            category = UpgradeCategory.General,
            generalType = GeneralUpgradeType.HighRankChance,
            baseCost = 50,
            growth = 1.30,
            amount = 0.02
        });
        upgrades.Add(new UpgradeData
        {
            category = UpgradeCategory.General,
            generalType = GeneralUpgradeType.NegativeRng,
            baseCost = 30,
            growth = 1.25,
            amount = 0.01
        });

        foreach (WeaponType wt in Enum.GetValues(typeof(WeaponType)))
        {
            upgrades.Add(new UpgradeData { category = UpgradeCategory.Weapon, weaponType = wt, weaponAttribute = WeaponAttribute.Tap, baseCost = 20, growth = 1.20, amount = 1 });
            upgrades.Add(new UpgradeData { category = UpgradeCategory.Weapon, weaponType = wt, weaponAttribute = WeaponAttribute.Dps, baseCost = 25, growth = 1.20, amount = 0.5 });
            upgrades.Add(new UpgradeData { category = UpgradeCategory.Weapon, weaponType = wt, weaponAttribute = WeaponAttribute.Time, baseCost = 30, growth = 1.25, amount = -0.05 });
            upgrades.Add(new UpgradeData { category = UpgradeCategory.Weapon, weaponType = wt, weaponAttribute = WeaponAttribute.Stability, baseCost = 35, growth = 1.30, amount = 0.05 });
            upgrades.Add(new UpgradeData { category = UpgradeCategory.Weapon, weaponType = wt, weaponAttribute = WeaponAttribute.Value, baseCost = 40, growth = 1.35, amount = 1 });
        }
    }

    public bool TryBuy(UpgradeData up)
    {
        double cost = up.GetCost();
        if (GameManager.I.credits < cost)
            return false;

        GameManager.I.credits -= cost;

        if (up.category == UpgradeCategory.General)
        {
            switch (up.generalType)
            {
                case GeneralUpgradeType.Tap:
                    GameManager.I.tapPower += up.amount;
                    break;
                case GeneralUpgradeType.HighRankChance:
                    GameManager.I.rankChanceBonus += up.amount;
                    break;
                case GeneralUpgradeType.NegativeRng:
                    GameManager.I.negativeRngReduction += up.amount;
                    break;
            }
        }
        else
        {
            var stats = GameManager.I.GetWeaponStats(up.weaponType);
            switch (up.weaponAttribute)
            {
                case WeaponAttribute.Tap:
                    stats.tapBonus += up.amount;
                    GameManager.I.tapPower += up.amount;
                    break;
                case WeaponAttribute.Dps:
                    stats.dpsBonus += up.amount;
                    GameManager.I.dps += up.amount;
                    break;
                case WeaponAttribute.Time:
                    stats.timeModifier += (float)up.amount;
                    break;
                case WeaponAttribute.Stability:
                    stats.stabilityBonus += (float)up.amount;
                    break;
                case WeaponAttribute.Value:
                    stats.valueBonus += up.amount;
                    break;
            }
        }

        up.level++;
        UIController.I?.UpdateTapDps(GameManager.I.tapPower, GameManager.I.dps);
        UIController.I?.UpdateCredits(GameManager.I.credits);
        return true;
    }

    public enum UpgradeCategory { General, Weapon }
    public enum GeneralUpgradeType { Tap, HighRankChance, NegativeRng }
    public enum WeaponAttribute { Tap, Dps, Time, Stability, Value }

    [Serializable]
    public class UpgradeData
    {
        public UpgradeCategory category;
        public GeneralUpgradeType generalType;
        public WeaponType weaponType;
        public WeaponAttribute weaponAttribute;
        public double baseCost;
        public double growth = 1.1;
        public double amount = 1;
        public int level = 0;

        public double GetCost() => baseCost * System.Math.Pow(growth, level);

        public string GetTitle()
        {
            if (category == UpgradeCategory.General)
                return generalType.ToString();
            return $"{weaponType} {weaponAttribute}";
        }

        public string GetDescription()
        {
            string sign = amount >= 0 ? "+" : "";
            if (category == UpgradeCategory.General)
            {
                switch (generalType)
                {
                    case GeneralUpgradeType.Tap: return $"{sign}{amount:0} Tap";
                    case GeneralUpgradeType.HighRankChance: return $"{sign}{amount:P0} Chance";
                    case GeneralUpgradeType.NegativeRng: return $"{sign}{amount:P0} RNG";
                }
            }
            else
            {
                switch (weaponAttribute)
                {
                    case WeaponAttribute.Tap: return $"{sign}{amount:0} Tap";
                    case WeaponAttribute.Dps: return $"{sign}{amount:0.##} DPS";
                    case WeaponAttribute.Time: return $"{sign}{amount:P0} Time";
                    case WeaponAttribute.Stability: return $"{sign}{amount:P0} Stability";
                    case WeaponAttribute.Value: return $"{sign}{amount:0} Value";
                }
            }
            return string.Empty;
        }
    }
}
