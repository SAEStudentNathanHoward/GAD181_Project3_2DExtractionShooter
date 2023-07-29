using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    private float bulletSpread;

    private float timer;

    void Start()
    { 
        mainCam = GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        force = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>().weaponBulletSpeed;
        bulletSpread = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>().weaponBulletSpread;

        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(Random.Range(direction.x - bulletSpread, direction.x + bulletSpread), Random.Range(direction.y - bulletSpread, direction.y + bulletSpread)).normalized * force;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            Destroy(this.gameObject);
        }
    }
}
