using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //version and the actual connection panel
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject ConnectPanel;

    //input fields
    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreatGameInput;
    [SerializeField] private InputField JoingGameInput;

    //start the game
    [SerializeField] private GameObject StartButton;


    private void Awake()
    {
        //connects the right server
        PhotonNetwork.ConnectUsingSettings(VersionName);

    }


    //when a connection has esstablished
    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void exitGame()
    {
        Application.Quit();
    }


    public void ChangeUserNameInput()
    {
        if(UsernameInput.text.Length>= 3)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }

    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreatGameInput.text, new RoomOptions() { maxPlayers = 4 }, null);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(JoingGameInput.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGame");
    }
}
