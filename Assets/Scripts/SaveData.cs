using UnityEngine;

public static class SaveData
{
    private const string UnlockedKey = "unlocked";
    private const string BestPrefix = "best";

    public static int UnlockedLevel => PlayerPrefs.GetInt(UnlockedKey, 1);

    public static bool IsUnlocked(int level) => level <= UnlockedLevel;

    public static void Unlock(int level)
    {
        if (level > UnlockedLevel)
        {
            PlayerPrefs.SetInt(UnlockedKey, level);
            PlayerPrefs.Save();
        }
    }

    public static bool TryGetBest(int level, out float time)
    {
        string key = BestPrefix + level;
        if (PlayerPrefs.HasKey(key))
        {
            time = PlayerPrefs.GetFloat(key);
            return true;
        }

        time = 0f;
        return false;
    }

    public static bool Record(int level, float time)
    {
        string key = BestPrefix + level;
        if (!PlayerPrefs.HasKey(key) || time < PlayerPrefs.GetFloat(key))
        {
            PlayerPrefs.SetFloat(key, time);
            PlayerPrefs.Save();
            return true; // New best time
        }

        return false;
    }
}