using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpot : MonoBehaviour
{
    public Transform[] meleeWeaponSpot;
    public Transform[] weaponsSpot;
    public Transform[] etcSpot;
    public GameObject[] meleeWeapon;
    public GameObject[] weapon;
    public GameObject[] etc;

    private void Awake()
    {
        ItemSpawn(meleeWeapon, meleeWeaponSpot);
        ItemSpawn(weapon, weaponsSpot);
        ItemSpawn(etc, etcSpot);
    }

    private void ItemSpawn(GameObject[] prefab, Transform[] spot)
    {
        if (prefab.Length == 0 || spot.Length == 0)
            return;

        for (int i = 0; i < spot.Length; i++)
        {
            if (prefab == meleeWeapon && prefab.Length > 0)
            {
                Instantiate(prefab[i], spot[i].position, spot[i].rotation);
            }

            else if (prefab != meleeWeapon && prefab.Length > 0)
            {
                int randomIndex = Random.Range(0, prefab.Length);
                Instantiate(prefab[randomIndex], spot[i].position, spot[i].rotation);
            }
        }
    }
}