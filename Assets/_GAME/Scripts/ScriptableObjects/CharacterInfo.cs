using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Objects/Character")]
public class CharacterInfo : ScriptableObject
{
    [Header("Character Info")]
    public float moveSpeed;
    public int health;
}
