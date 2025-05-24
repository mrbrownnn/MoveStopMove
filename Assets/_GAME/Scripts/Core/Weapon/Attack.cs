using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack: MonoBehaviour
{
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private Transform enemyPosition;
    private int OwnerID, OpponentID;
    void _HideWeapon()
    {
        
        foreach (Transform weapon in weaponPosition)
        {
            weapon.GetComponent<MeshRenderer>().enabled = false;
            Material[] BulletMaterial = weapon.GetComponent<Renderer>().sharedMaterials;
            if (weapon.gameObject.activeSelf)
            {
                weapon.GetComponent<BulletSpawner>().CreateBullet(weaponPosition.position, OwnerID, OpponentID, BulletMaterial);
            }
        }
        StartCoroutine(_ShowWeapon(0.18f));
    }

    IEnumerator _ShowWeapon(float _timeCounting)
    {
        yield return new WaitForSeconds(_timeCounting);
        foreach (Transform weapon in weaponPosition)
        {
            weapon.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    public void SetID(int _ownerID, int _opponentID)
    {
        OwnerID = _ownerID;
        OpponentID = _opponentID;
    }
}
