using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPrefabSettings : MonoBehaviour
{
    [Header("Basic Weapon Settings")]
    [SerializeField] public string weaponName;
    [SerializeField] public bool isAutomatic;

    [Header("Ammo/Bullet Settings")]
    [SerializeField] public float fireRate;
    [SerializeField] public float clipSize;
    [SerializeField] public float maxAmmo;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletSpread;
}
