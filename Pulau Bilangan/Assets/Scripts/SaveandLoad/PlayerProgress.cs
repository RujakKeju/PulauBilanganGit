using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerProgress
{
    public string playerName;
    public string characterName;
    public CharacterDataSO characterData;

    public List<LevelProgressEntry> levelProgressList = new();
    public List<ScoreEntry> scorePerKeyList = new(); // sebagai gantinya untuk serialisasi



    [System.NonSerialized]
    public Dictionary<string, LevelProgress> levelProgressDict = new();

    public Dictionary<string, int> scorePerKey = new(); // ✅ nilai per kategori

    public void BuildDictionary()
    {
        levelProgressDict.Clear();
        foreach (var entry in levelProgressList)
            levelProgressDict[entry.key] = entry.progress;

        scorePerKey.Clear();
        foreach (var entry in scorePerKeyList)
            scorePerKey[entry.key] = entry.score;
    }

    public void SyncFromDictionary()
    {
        levelProgressList.Clear();
        foreach (var kvp in levelProgressDict)
        {
            levelProgressList.Add(new LevelProgressEntry { key = kvp.Key, progress = kvp.Value });
        }

        scorePerKeyList.Clear();
        foreach (var kvp in scorePerKey)
        {
            scorePerKeyList.Add(new ScoreEntry { key = kvp.Key, score = kvp.Value });
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

[System.Serializable]
public class ScoreEntry
{
    public string key;
    public int score;
}


