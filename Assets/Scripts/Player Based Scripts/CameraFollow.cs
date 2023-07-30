using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private Camera mainCamera;
    private GameObject secondPlayerCharacter;
    private Camera secondCamera;
    

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
        }
    }

    private void Start()
    {
        if (MainMenu.localMulitplayer == true)
        {
            mainCamera.rect = new Rect(-0.5f, 0, 1, 1);

            secondPlayerCharacter = GameObject.Find("Player 2");
            secondCamera = secondPlayerCharacter.GetComponent<Camera>();
            secondCamera.rect = new Rect(0.5f, 0, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y, -10);
    }
}
