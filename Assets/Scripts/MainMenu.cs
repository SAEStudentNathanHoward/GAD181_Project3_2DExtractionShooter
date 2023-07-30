using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool onlineMultiplayer;
    public static bool localMulitplayer;

    private void Start()
    {
        onlineMultiplayer = false;
        localMulitplayer = false;
    }

    public void LocalMultiplayer()
    {
        localMulitplayer = true;
        SceneManager.LoadScene(2);
    }

    public void OnlineMultiplayer()
    {
        onlineMultiplayer = true;
        SceneManager.LoadScene(1);
    }
}
