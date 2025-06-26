using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public static class FirestoreSync
{
    public static void SaveProgressToFirestore(PlayerProgress progress)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        string playerID = GetOrCreatePlayerID();

        Dictionary<string, object> dataToSave = new Dictionary<string, object>
        {
            { "playerName", progress.playerName },
            { "characterName", progress.characterName },
            { "savedAt", Timestamp.GetCurrentTimestamp() }
        };

        // Build nested progress
        Dictionary<string, object> levelDataDict = new Dictionary<string, object>();
        foreach (var entry in progress.levelProgressList)
        {
            Dictionary<string, object> levelInfo = new Dictionary<string, object>
            {
                { "difficultyUnlocked", entry.progress.difficultyUnlocked }
            };

            List<Dictionary<string, object>> levelList = new List<Dictionary<string, object>>();
            foreach (var level in entry.progress.levels)
            {
                levelList.Add(new Dictionary<string, object>
                {
                    { "isCompleted", level.isCompleted },
                    { "isCorrect", level.isCorrect }
                });
            }

            levelInfo["levels"] = levelList;
            levelDataDict[entry.key] = levelInfo;
        }

        // Simpan skor per kategori
        Dictionary<string, object> scoreDict = new();
        foreach (var pair in progress.scorePerKey)
        {
            scoreDict[pair.Key] = pair.Value;
        }
        dataToSave["scores"] = scoreDict;

        dataToSave["progress"] = levelDataDict;

        db.Collection("players").Document(playerID).SetAsync(dataToSave).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
                Debug.Log("[FIRESTORE] Data berhasil disimpan!");
            else
                Debug.LogError("[FIRESTORE] Gagal simpan: " + task.Exception);
        });
    }

    private static string GetOrCreatePlayerID()
    {
        if (!PlayerPrefs.HasKey("playerID"))
        {
            string newID = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString("playerID", newID);
            PlayerPrefs.Save();
            Debug.Log("[PLAYER ID] Baru dibuat: " + newID);
            return newID;
        }

        string existingID = PlayerPrefs.GetString("playerID");
        Debug.Log("[PLAYER ID] Ditemukan: " + existingID);
        return existingID;
    }
}
