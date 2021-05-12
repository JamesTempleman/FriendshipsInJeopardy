using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Photon.MonoBehaviour
{
    //player prefab
    public GameObject PlayerPrefab;

    //to join the game 
    public GameObject SpawnCanvas;
    Vector3 OrignalPos = new Vector3(-1.5f, 1.88f, -7.74f);
    readonly float offset = -2.3f;
    public GameObject TempCam;
    public GameObject[] Positions;

    //In game UI
    public Text waitingTxt;
    public GameObject GameCanvas;
   
    //Quiz Specific Variables
    public bool quizStarted = false;

    //Quiz Manager
    public QuizManager qm;


    //checking if the players are ready to play
    public int readiedPlayers;

    //Score
    public GameObject ScorePanel;

    // Update is called once per frame
    void Update()
    {
        if (checkPlayers() >= 5)
        {
            SpawnCanvas.SetActive(false);
        }
        else if(checkPlayers() > 1)
        {
            waitingTxt.gameObject.SetActive(false);
        }
        /*
         * 
         * impliment a spectator mode here
         * 
         */

        //checks if the game is ready to begin
        if (readiedPlayers == checkPlayers() && checkPlayers() >=2)
        { 
            quizStarted = true;
        }

    }

    public void SpawnPlayer()
    {
        //figures out how muc hto offset the next player
        float AdjustedOffset = offset * checkPlayers();

        //spawns the player in at the correct podium
        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(OrignalPos.x + AdjustedOffset, OrignalPos.y, OrignalPos.z), Quaternion.identity, 0);

        //destroys the un-needed camera
        Destroy(TempCam);

        //turns off the spawn button canvas
        SpawnCanvas.SetActive(false);
        GameCanvas.SetActive(true);
    }

    //checks how many players are in the lobby
    public int checkPlayers()
    {
        //how many players
        int PlayerCount = 0;

        //runs through the podiums checking if anyone is there
        for (int i = 0; i < Positions.Length; i++)
        {
            //the someoneThere script contains a variable that tracks if there is a player there
            someoneThere check = Positions[i].GetComponent<someoneThere>();

            //if there is someone there increase player count by 1
            if (check.SomeonesHere)
            {
                PlayerCount++;
            }
        }
        //returns how many players there is
        return PlayerCount;
    }
    
}
