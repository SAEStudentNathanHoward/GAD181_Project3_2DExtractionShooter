using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private List<GameObject> weaponList;
    private int weaponListIndex;
    [SerializeField] private GameObject equippedWeapon;
    [SerializeField] private GameObject projectileBullet;
    [SerializeField] private Transform bulletTransform;
    public bool canFire;
    private float timer;
    private Vector3 bulletSpawnPosition;
    private string weaponName;
    private bool weaponIsAutomatic;
    private float weaponFireRate;
    private float weaponClipSize;
    private float weaponMaxAmmo;
    [HideInInspector] public float weaponBulletSpeed;
    [HideInInspector] public int weaponBulletDamage;
    [HideInInspector] public float weaponBulletSpread;

    private AudioSource weaponSFXSource;
    private AudioClip weaponSFXClip;

    private void Start()
    {
        equippedWeapon = weaponList[0];
        weaponName = equippedWeapon.GetComponent<GunPrefabSettings>().weaponName;
        weaponIsAutomatic = equippedWeapon.GetComponent<GunPrefabSettings>().isAutomatic;
        weaponFireRate = equippedWeapon.GetComponent<GunPrefabSettings>().fireRate;
        weaponClipSize = equippedWeapon.GetComponent<GunPrefabSettings>().clipSize;
        weaponBulletDamage = equippedWeapon.GetComponent<GunPrefabSettings>().bulletDamage;
        weaponMaxAmmo = equippedWeapon.GetComponent<GunPrefabSettings>().maxAmmo;
        weaponBulletSpeed = equippedWeapon.GetComponent<GunPrefabSettings>().bulletSpeed;
        weaponBulletSpread = equippedWeapon.GetComponent<GunPrefabSettings>().bulletSpread;
        
        weaponSFXSource = gameObject.GetComponent<PlayerCharacterController>().characterAudioSource;
        weaponSFXClip = equippedWeapon.GetComponent<GunPrefabSettings>().weaponFireSFX;
    }

    void Update()
    {
        // Checks if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && canFire == true)
        {
            FireGun();
        }

        if (Input.GetMouseButton(0) && weaponIsAutomatic == true && canFire == true)
        {
            FireGun();
        }

        // Checks if the scroll wheel is used
        if (Input.mouseScrollDelta.y != 0)
        {
            ChangeWeapon();
        }

        // Checks if the r key is pressed on the keyboard
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadWeapon();
        }

        if (canFire == false)
        {
            timer += Time.deltaTime;
            if (timer > weaponFireRate)
            {
                canFire = true;
                timer = 0;
            }
        }
        //Debug.Log(Time.deltaTime);
    }

    private void FireGun()
    {
        bulletSpawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
        var bullet = Instantiate(projectileBullet, bulletSpawnPosition, Quaternion.identity);
        canFire = false;

        weaponSFXSource.PlayOneShot(weaponSFXClip);
    }

    private void ChangeWeapon()
    {
        Debug.Log("Weapon changed");
        weaponListIndex += 1;

        if (weaponListIndex >= weaponList.Count)
        {
            weaponListIndex = 0;
        }

        equippedWeapon = weaponList[weaponListIndex];

        weaponName = equippedWeapon.GetComponent<GunPrefabSettings>().weaponName;
        weaponIsAutomatic = equippedWeapon.GetComponent<GunPrefabSettings>().isAutomatic;
        weaponFireRate = equippedWeapon.GetComponent<GunPrefabSettings>().fireRate;
        weaponClipSize = equippedWeapon.GetComponent<GunPrefabSettings>().clipSize;
        weaponMaxAmmo = equippedWeapon.GetComponent<GunPrefabSettings>().maxAmmo;
        weaponBulletSpeed = equippedWeapon.GetComponent<GunPrefabSettings>().bulletSpeed;
        weaponBulletSpread = equippedWeapon.GetComponent<GunPrefabSettings>().bulletSpread;
        weaponSFXClip = equippedWeapon.GetComponent<GunPrefabSettings>().weaponFireSFX;

        Debug.Log("current weapon sfx is " + weaponSFXClip);

    }

    private void ReloadWeapon()
    {
        Debug.Log("Weapon reloading");
    }
}
