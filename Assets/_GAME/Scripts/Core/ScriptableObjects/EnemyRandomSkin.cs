using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRandomSkin", menuName = "Scriptable Objects/Skin")]
public class EnemyRandomSkin : ScriptableObject
{
    // set random skin for enemy
    [Header("Skin Color")]
    public List<Material> EnemyColor;
}
