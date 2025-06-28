using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon",menuName ="Scriptable Objects/Weapon")]
public class WeaponInfo : ScriptableObject
{
    // Change appropriate header names and descriptions as needed
    [Header("Weapon Type")]
    public GameObject[] WeaponType;

    [Header("Add Attack Range")]
    public float[] AddAttackRange;

    [Header("Add Attack Speed")]
    public float[] AddAttackSpeed;

    [Header("Arrow Materials")]
    public Material[] ArrowDefaultMaterials;

    [Header("Axe0 Materials")]
    public Material[] Axe0DefaultMaterials;

    [Header("Axe1 Materials")]
    public Material[] Axe1DefaultMaterials;

    [Header("Boomerang Materials")]
    public Material[] BoomerangDefaultMaterials;

    [Header("Candy0 Materials")]
    public Material[] Candy0DefaultMaterials;

    [Header("Candy1 Materials")]
    public Material[] Candy1DefaultMaterials;

    [Header("Candy2 Materials")]
    public Material[] Candy2DefaultMaterials;

    [Header("Candy4 Materials")]
    public Material[] Candy4DefaultMaterials;

    [Header("Hammer Materials")]
    public Material[] HammerDefaultMaterials;

    [Header("Knife Materials")]
    public Material[] KnifeDefaultMaterials;

    [Header("Uzi Materials")]
    public Material[] UziDefaultMaterials;

    [Header("Z Materials")]
    public Material[] ZDefaultMaterials;

    [Header("Custom Materials")]
    public Material[] CustomMaterials;
}
