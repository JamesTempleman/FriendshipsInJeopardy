using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToMenu : MonoBehaviour
{
    public void LoadMenu()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
