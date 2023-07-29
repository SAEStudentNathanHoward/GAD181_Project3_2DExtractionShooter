using TMPro;
using UnityEngine;
using Unity.Netcode;


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

        if (characterHP == 0)
        {
            Destroy(this.gameObject);
        }
    }

}
