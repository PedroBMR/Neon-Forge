using UnityEngine;

public static class SaveSystem
{
    const string SaveKey = "save_data";

    public static void Save()
    {
        SaveData data = new SaveData
        {
            level = GameManager.I.level,
            credits = GameManager.I.credits,
            tapPower = GameManager.I.tapPower,
            dps = GameManager.I.dps
        };

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
        GameManager.I.tapPower = data.tapPower;
        GameManager.I.dps = data.dps;
    }
}
