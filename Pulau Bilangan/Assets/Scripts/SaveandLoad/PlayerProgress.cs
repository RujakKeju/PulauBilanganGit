using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerProgress
{
    public string playerName;
    public string characterName;
    public CharacterDataSO characterData;

    public List<LevelProgressEntry> levelProgressList = new();

    [System.NonSerialized]
    public Dictionary<string, LevelProgress> levelProgressDict = new();

    public Dictionary<string, int> scorePerKey = new(); // ✅ nilai per kategori

    public void BuildDictionary()
    {
        levelProgressDict.Clear();
        foreach (var entry in levelProgressList)
        {
            levelProgressDict[entry.key] = entry.progress;
        }
    }

    public void SyncFromDictionary()
    {
        levelProgressList.Clear();
        foreach (var kvp in levelProgressDict)
        {
            levelProgressList.Add(new LevelProgressEntry { key = kvp.Key, progress = kvp.Value });
        }
    }
}

[System.Serializable]
public class LevelProgressEntry
{
    public string key;
    public LevelProgress progress;
}

[Serializable]
public class LevelProgress
{
    public List<LevelEntry> levels = new(); // index 0–9 = level 1–10
    public bool difficultyUnlocked;
}

[Serializable]
public class LevelEntry
{
    public bool isCompleted;  // true = sudah pernah dikerjakan
    public bool isCorrect;    // true = jawabannya benar
}

