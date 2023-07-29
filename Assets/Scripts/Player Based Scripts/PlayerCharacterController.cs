using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Unity.Netcode;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.Tilemaps;
using System.Runtime.CompilerServices;

public class PlayerCharacterController : NetworkBehaviour
{
    [Header("Character Stats and View Settings")]
    [SerializeField] private GameObject characterSprite;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float characterSpeed;
    [SerializeField] public LineRenderer aimLine;

    [Header("Character UI Settings")]
    [SerializeField] private TMP_Text characterHP;
    [SerializeField] private TMP_Text characterBullets;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
        }
    }

    private void Update()
    {

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            // Moves the character with the player input
            transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * characterSpeed * Time.deltaTime;
        }

        // Rotates the character to look at the mouse
        Vector3 diff = mainCamera.ScreenToWorldPoint(Input.mousePosition) - characterSprite.transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        characterSprite.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        // Draws a line to mouse position
        aimLine.SetPosition(0, characterSprite.transform.position);
        aimLine.SetPosition(1, mainCamera.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)));
    }
}
