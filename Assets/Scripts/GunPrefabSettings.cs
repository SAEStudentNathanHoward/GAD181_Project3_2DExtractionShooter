using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GunPrefabSettings : NetworkBehaviour
{
    [Header("Basic Weapon Settings")]
    [SerializeField] public string weaponName;
    [SerializeField] public bool isAutomatic;

    [Header("Ammo/Bullet Settings")]
    [SerializeField] public float fireRate;
    [SerializeField] public float clipSize;
    [SerializeField] public float maxAmmo;
    [SerializeField] public int bulletDamage;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletSpread;

    [SerializeField] public AudioClip weaponFireSFX;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
        }
    }
}
