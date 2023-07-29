using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y, -10);
    }
}
