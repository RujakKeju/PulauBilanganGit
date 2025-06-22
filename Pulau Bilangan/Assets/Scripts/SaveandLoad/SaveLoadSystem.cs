using UnityEngine;
using System.IO;

public static class SaveLoadSystem
{
    static string savePath => Application.persistentDataPath + "/progress.json";

    public static void SaveProgress(PlayerProgress progress)
    {
        progress.SyncFromDictionary(); // tambahkan ini
        string json = JsonUtility.ToJson(progress, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"[SaveProgress] Path: {savePath}");

        foreach (var kvp in progress.levelProgressDict)
            Debug.Log($"[SaveProgress] Key: {kvp.Key} | Level Count: {kvp.Value.levels.Count}");
    }



    public static PlayerProgress LoadProgress()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Progress belum ada, buat baru.");
            return new PlayerProgress();
        }

        string json = File.ReadAllText(savePath);
        var progress = JsonUtility.FromJson<PlayerProgress>(json);
        progress.BuildDictionary(); // tambahkan ini
        Debug.Log($"[LoadProgress] Isi JSON:\n{json}");
        return progress;
    }




    public static void MarkCurrentLevelAsIncorrect()
    {
        var state = GameStateManager.Instance;
        var progress = LoadProgress();
        string key = state.GetProgressKey();


        if (!progress.levelProgressDict.ContainsKey(key))
            progress.levelProgressDict[key] = new LevelProgress();

        var list = progress.levelProgressDict[key].levels;
        int idx = state.currentLevelIndex;

        if (idx >= 0 && idx < list.Count)
        {
            list[idx].isCompleted = true;
            list[idx].isCorrect = false;
        }

        SaveProgress(progress);
    }

    public static void MarkCurrentLevelAsCorrect()
    {
        var state = GameStateManager.Instance;
        var progress = LoadProgress();

        string key = state.GetProgressKey();

        if (!progress.levelProgressDict.ContainsKey(key))
            progress.levelProgressDict[key] = new LevelProgress();

        var list = progress.levelProgressDict[key].levels;
        int idx = state.currentLevelIndex;

        if (idx >= 0 && idx < list.Count)
        {
            list[idx].isCompleted = true;
            list[idx].isCorrect = true;
        }

        SaveProgress(progress);
    }


}
