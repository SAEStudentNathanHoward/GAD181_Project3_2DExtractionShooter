using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    private float bulletSpread;
    private int bulletDamage;

    private float timer;

    private Transform characterSprite;

    void Start()
    { 
        mainCam = GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        force = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>().weaponBulletSpeed;
        bulletSpread = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>().weaponBulletSpread;
        bulletDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>().weaponBulletDamage;

        if (MainMenu.localMulitplayer == false)
        {
            Vector3 direction = mousePos - transform.position;
            rb.velocity = new Vector2(Random.Range(direction.x - bulletSpread, direction.x + bulletSpread), Random.Range(direction.y - bulletSpread, direction.y + bulletSpread)).normalized * force;
        }
        else
        {
            characterSprite = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpriteRenderer>().transform;
            Vector2 direction = new Vector2(characterSprite.transform.up.x, characterSprite.transform.up.y);
            rb.velocity = new Vector2(Random.Range(direction.x - bulletSpread, direction.x + bulletSpread), Random.Range(direction.y - bulletSpread, direction.y + bulletSpread)).normalized * force;

            Debug.Log(characterSprite.transform.forward);
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCharacterController>().characterHP -= bulletDamage;
        }

        Destroy(this.gameObject);
    }
}
