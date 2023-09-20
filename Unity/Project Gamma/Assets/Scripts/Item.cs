using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Potion,
        BodyArmor,
        Bullet,
        Ect,
    }
    public string itemName;
    public Sprite itemImage;
    public ItemType itemType;

    //if item is potion
    public int hpRecoveryAmount;

    //if item is bullet
    public int bulletAmount;

    //if item is weapon
    public enum WeaponType
    {
        Melee,
        Pistol,
        Shotgun,
        SMG,
        AR,
        SR,
        ect,
    }

    public GameObject itemShape3D;
    public WeaponType weaponType;
    public int weaponDamage;
    public float weaponFireRate;
    public float weaponReloadingDelay;
    public int weaponRecoil;
    public int maxAmmo;
}
