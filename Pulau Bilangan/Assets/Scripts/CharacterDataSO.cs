using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/Create New Character")]
public class CharacterDataSO : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public string description; //deskripsi mengenai karakter
}
