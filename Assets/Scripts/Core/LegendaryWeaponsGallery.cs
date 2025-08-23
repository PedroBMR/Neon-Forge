using System.Collections.Generic;
using UnityEngine;

public class LegendaryWeaponsGallery : MonoBehaviour
{
    public static LegendaryWeaponsGallery I;

    [System.Serializable]
    public class Entry
    {
        public WeaponSpec spec;
        public Rank rank;
        public double value;
    }

    readonly List<Entry> entries = new List<Entry>();
    public IReadOnlyList<Entry> Entries => entries;

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Add(WeaponSpec spec, Rank rank, double value)
    {
        if (spec == null || rank < Rank.SPlus)
            return;
        entries.Add(new Entry { spec = spec, rank = rank, value = value });
        entries.Sort((a, b) => b.value.CompareTo(a.value));
        if (entries.Count > 10)
            entries.RemoveRange(10, entries.Count - 10);
    }
}

