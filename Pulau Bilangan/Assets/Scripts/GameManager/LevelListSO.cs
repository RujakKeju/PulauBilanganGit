using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelList", menuName = "MathGame/Level List")]
public class LevelListSO : ScriptableObject
{
    public Difficulty difficulty;
    public string[] sceneNames = new string[10]; // 10 level per difficulty
}
