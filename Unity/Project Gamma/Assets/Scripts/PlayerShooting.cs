using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerShooting : MonoBehaviour
{
    private ItemInventory itemInventory;
    public Transform raycastOrigin;

    public uint currentWeaponSlot;
    public GameObject weapon;
    public GameObject firePosition;
    public GameObject bullet;
    public Button shootingBtn;

    public float bulletSpeed;
    [SerializeField] private float shootingCoolTime;
    public float shootingTimer;
    public float bulletDamage;

    public List<GameObject> bulletObjectPool;
    private uint objPoolSize = 6;

    Animator playerAni;
    public Image hitMarker;

    private void Awake()
    {
        bulletObjectPool = new List<GameObject>();

        for (int i = 0; i < objPoolSize; i++)
        {
            GameObject bulletForObjPool = Instantiate(bullet);
            bulletForObjPool.SetActive(false);
            bulletObjectPool.Add(bulletForObjPool);
        }
    }

    private void Start()
    {
        currentWeaponSlot = 0;
        itemInventory = gameObject.GetComponentInChildren<ItemInventory>();
        bulletDamage = 0;

        playerAni = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        switch (currentWeaponSlot)
        {
            case 0:
                if (itemInventory.CurrentPrimaryWeapon != null)
                {
                    shootingCoolTime = itemInventory.CurrentPrimaryWeapon.weaponFireRate;
                    SetActiveWeapon(itemInventory.CurrentPrimaryWeapon.itemName);
                    playerAni.SetBool("Primary_Mode", true);
                    playerAni.SetLayerWeight(1, 1);
                    WeaponShooting(itemInventory.CurrentPrimaryWeapon.weaponDamage);
                }
                else
                {
                    UnActiveWeapon();
                    playerAni.SetBool("Primary_Mode", false);
                    playerAni.SetLayerWeight(1, 0);
                }
                break;
            case 1:
                if (itemInventory.CurrentSecondWeapon != null)
                {
                    shootingCoolTime = itemInventory.CurrentSecondWeapon.weaponFireRate;
                    SetActiveWeapon(itemInventory.CurrentSecondWeapon.itemName);
                    playerAni.SetBool("Second_Mode", true);
                    playerAni.SetLayerWeight(1, 1);
                    WeaponShooting(itemInventory.CurrentSecondWeapon.weaponDamage);
                }
                else
                {
                    UnActiveWeapon();
                    playerAni.SetBool("Second_Mode", false);
                    playerAni.SetLayerWeight(1, 0);
                }
                break;
            case 2:
                if (itemInventory.CurrentMeleeWeapon != null)
                {
                    shootingCoolTime = itemInventory.CurrentMeleeWeapon.weaponFireRate;
                    SetActiveWeapon(itemInventory.CurrentMeleeWeapon.itemName);
                    playerAni.SetBool("Melee_Mode", true);
                    playerAni.SetLayerWeight(1, 1);
                    //WeaponShooting(itemInventory.CurrentMeleeWeapon.weaponDamage);
                    MeleeWeaponSlashing(itemInventory.CurrentMeleeWeapon.weaponDamage);
                }
                else
                {
                    UnActiveWeapon();
                    playerAni.SetBool("Melee_Mode", false);
                    playerAni.SetLayerWeight(1, 0);
                }
                break;
        }
    }

    private void SetActiveWeapon(string itemName)
    {
        if (weapon.activeInHierarchy == false)
        {
            weapon.SetActive(true);

            Transform weapon3dModel = weapon.transform.Find(itemName);
            weapon3dModel.gameObject.SetActive(true);
        }
    }
    private void UnActiveWeapon()
    {
        if (weapon.activeInHierarchy == true)
        {
            weapon.SetActive(false);
        }
    }

    public void SwapWeapon()    //called by OnAction of swapWeaponBtn
    {
        if (currentWeaponSlot == 0)
        {
            if (itemInventory.CurrentPrimaryWeapon != null)
            {
                weapon.transform.Find(itemInventory.CurrentPrimaryWeapon.itemName).gameObject.SetActive(false);
            }
            if (itemInventory.CurrentSecondWeapon != null)
            {
                weapon.transform.Find(itemInventory.CurrentSecondWeapon.itemName).gameObject.SetActive(true);
            }

            currentWeaponSlot = 1;
            playerAni.SetBool("Primary_Mode", false);
            playerAni.SetLayerWeight(1, 0);
        }
        else if (currentWeaponSlot == 1)
        {
            if (itemInventory.CurrentSecondWeapon != null)
            {
                weapon.transform.Find(itemInventory.CurrentSecondWeapon.itemName).gameObject.SetActive(false);
            }
            if (itemInventory.CurrentMeleeWeapon != null)
            {
                weapon.transform.Find(itemInventory.CurrentMeleeWeapon.itemName).gameObject.SetActive(true);
            }

            currentWeaponSlot = 2;
            playerAni.SetBool("Second_Mode", false);
            playerAni.SetLayerWeight(1, 0);
        }
        else if (currentWeaponSlot == 2)
        {
            if (itemInventory.CurrentMeleeWeapon != null)
            {
                weapon.transform.Find(itemInventory.CurrentMeleeWeapon.itemName).gameObject.SetActive(false);
            }
            if (itemInventory.CurrentPrimaryWeapon != null)
            {
                weapon.transform.Find(itemInventory.CurrentPrimaryWeapon.itemName).gameObject.SetActive(true);
            }

            currentWeaponSlot = 0;
            playerAni.SetBool("Melee_Mode", false);
            playerAni.SetLayerWeight(1, 0);
        }

        print("currentWeaponSlot : " + currentWeaponSlot);
    }

    public void WeaponShooting(int bulletDamage)
    {
        shootingTimer += Time.deltaTime;

        if (shootingBtn.GetComponent<ShootingBtn>().isPressed == true && shootingTimer >= shootingCoolTime)
        {
            shootingTimer = 0;

            if (playerAni.GetBool("Primary_Mode") == true)
            {
                playerAni.SetTrigger("Primary_Mode_Shoot");
            }
            if (playerAni.GetBool("Second_Mode") == true)
            {
                playerAni.SetTrigger("Second_Mode_Shoot");
            }
            /*
            if (playerAni.GetBool("Melee_Mode") == true)
            {
                playerAni.SetTrigger("Melee_Mode_Attack");
            }
            */

            GameObject bulletForFire = GetBulletFromBulletObjPool();
            bulletForFire.transform.position = firePosition.transform.position;
            bulletForFire.transform.rotation = firePosition.transform.rotation;
            bulletForFire.SetActive(true);
            bulletForFire.transform.SetParent(null);
            bulletForFire.GetComponent<Bullet>().bulletOwner = gameObject;
            bulletForFire.GetComponent<Rigidbody>().velocity = firePosition.transform.forward * bulletSpeed * Time.deltaTime;

            Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                //Debug.Log("충돌한 객체: " + hitInfo.collider.gameObject.name);
                //Debug.Log("충돌 지점: " + hitInfo.point);

                if (hitInfo.transform.gameObject.CompareTag("Enemy"))
                {
                    HitMarkerBlink();
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    enemy.Hurt(bulletDamage);
                    enemy.targetedHostile = gameObject;
                }

                else if (hitInfo.transform.gameObject.CompareTag("Dragon"))
                {
                    HitMarkerBlink();
                    Dragon dragon = hitInfo.transform.GetComponent<Dragon>();
                    dragon.Hurt(bulletDamage);
                    dragon.targetedHostile = gameObject;
                }
            }
        }
    }

    public void MeleeWeaponSlashing(int weaponDamage)
    {
        float weaponReach = 3f;
        shootingTimer += Time.deltaTime;

        if (shootingBtn.GetComponent<ShootingBtn>().isPressed == true && shootingTimer >= shootingCoolTime)
        {
            shootingTimer = 0;

            if (playerAni.GetBool("Melee_Mode") == true)
            {
                playerAni.SetTrigger("Melee_Mode_Attack");
            }

            Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, weaponReach))
            {
                //Debug.Log("충돌한 객체: " + hitInfo.collider.gameObject.name);
                //Debug.Log("충돌 지점: " + hitInfo.point);

                if (hitInfo.transform.gameObject.CompareTag("Enemy"))
                {
                    HitMarkerBlink();
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    enemy.Hurt(weaponDamage);
                    enemy.targetedHostile = gameObject;
                }

                else if (hitInfo.transform.gameObject.CompareTag("Dragon"))
                {
                    HitMarkerBlink();
                    Dragon dragon = hitInfo.transform.GetComponent<Dragon>();
                    dragon.Hurt(weaponDamage);
                    dragon.targetedHostile = gameObject;
                }
            }
        }
    }

    private GameObject GetBulletFromBulletObjPool()
    {
        for (int i = 0; i < bulletObjectPool.Count; i++)
        {
            if (bulletObjectPool[i].activeInHierarchy == false)
            {
                return bulletObjectPool[i];
            }
        }

        GameObject newBulletIfObjPoolEmpty = Instantiate(bullet);
        newBulletIfObjPoolEmpty.SetActive(false);
        bulletObjectPool.Add(newBulletIfObjPoolEmpty);

        return newBulletIfObjPoolEmpty;
    }

    private void HitMarkerBlink()
    {
        if (hitMarker.IsActive() == false)
        {
            hitMarker.gameObject.SetActive(true);
            StartCoroutine(DisableHitMarker());
        }
    }
    IEnumerator DisableHitMarker()
    {
        yield return new WaitForSeconds(0.1f);
        hitMarker.gameObject.SetActive(false);
    }
}