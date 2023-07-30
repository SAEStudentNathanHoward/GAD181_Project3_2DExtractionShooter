using TMPro;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerCharacterController : NetworkBehaviour
{
    [Header("Character View Settings")]
    [SerializeField] private GameObject characterSprite;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float characterSpeed;
    [SerializeField] public LineRenderer aimLine;

    [Header("Charcter Stats")]
    [SerializeField] public int characterHP = 100;

    [Header("Character UI Settings")]
    [SerializeField] private TMP_Text characterHPDisplay;
    [SerializeField] private TMP_Text characterBullets;
    [SerializeField] public AudioSource characterAudioSource;

    [Header("Character Controller Input Settings")]
    [SerializeField] private InputActionAsset inputActionAssetFile;

    private Vector2 playerControllerMove;
    private Vector2 playerControllerRotate;
    private PlayerInputManager playerInputManager;
    private GameObject playerCharacter;
    private GameObject secondPlayerCharacter;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            mainCamera.enabled = false;
            enabled = false;
        }
    }

    private void Start()
    {
        if (MainMenu.localMulitplayer == true)
        {
            playerCharacter = GameObject.Find("Player");
            secondPlayerCharacter = GameObject.Find("Player 2");
            Destroy(secondPlayerCharacter.GetComponent<AudioListener>());
            playerInputManager = GameObject.Find("PlayerManager").GetComponent<PlayerInputManager>();
        }
        else
        {
            gameObject.GetComponent<PlayerInput>().enabled = false;
        }
    }

    private void Update()
    {
        if (MainMenu.localMulitplayer == false)
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
            aimLine.SetPosition(1, mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)));
        }
        else
        {
            transform.position += new Vector3(playerControllerMove.x, playerControllerMove.y, 0) * characterSpeed * Time.deltaTime;
            
            if (playerControllerRotate != Vector2.zero )
            {
                float rot_z = Mathf.Atan2(playerControllerRotate.y, playerControllerRotate.x) * Mathf.Rad2Deg;
                characterSprite.transform.rotation = Quaternion.Euler(0, 0, rot_z - 90);
            }
        }

        if (characterHP == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx) => playerControllerMove = ctx.ReadValue<Vector2>();

    public void OnRotate(InputAction.CallbackContext ctx) => playerControllerRotate = ctx.ReadValue<Vector2>();
}
