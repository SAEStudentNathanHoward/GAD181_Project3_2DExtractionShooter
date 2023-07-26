using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCharacterController : MonoBehaviour
{
    [Header("Character Stats and View Settings")]
    [SerializeField] private float characterSpeed;
    [SerializeField] private LineRenderer aimLine;

    [Header("Character UI Settings")]
    [SerializeField] private TMP_Text characterHP;
    [SerializeField] private TMP_Text characterBullets;

    private void Update()
    {
        // Moves the character with the player input
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * characterSpeed * Time.deltaTime;

        // Rotates the character to look at the mouse
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        // Draws a line to mouse position
        aimLine.SetPosition(0, transform.position);
        aimLine.SetPosition(1, Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)));

        // Checks if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
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
    }

    private void FireGun()
    {
        Debug.Log("gun fired: pew pew");
    }

    private void ChangeWeapon()
    {
        Debug.Log("Weapon changed");
    }

    private void ReloadWeapon()
    {
        Debug.Log("Weapon reloading");
    }
}
