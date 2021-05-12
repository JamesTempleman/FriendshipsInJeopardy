using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : Photon.MonoBehaviour, IPunObservable
{
    //gamemanager
    public GameManager gm;

    //quiz manager
    public QuizManager qm;

    //readiedUp
    bool notReady = true;

    //for keeping players synced
    public PhotonView photonView;

    // the players actual body
    public Rigidbody rb;
    public Animator anim;
    public GameObject PlayerCamera;
    public Renderer r;

    //for name tag
    public Text playerNameText;

    //for readying up
    public GameObject readyCanvas;
    public bool ranBefore = true;

    //for looking around
    public Transform character;
    public Vector3 mousePos;


    //for score and actual Gameplay
    public GameObject scorePanel;
    public GameObject ScoreTextObject;
    public Text scoreTxt;
    public int scoreInt;
    Font arial;
    public int selectedAnswer = 0;

    //Just so certain code doesn't run twice on accident
    public bool check = true;

    private void Awake()
    {
        //setting up the Game manager
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log(gm);

        //Setting up Quiz Manager
        qm = gm.qm;

        //setting up the score panel
        scorePanel = gm.ScorePanel;
        Debug.Log(scorePanel);

        //setting up the score txt
        ScoreTextObject = new GameObject("ScoreTxt");

        //setting up fonts for later
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");


        //checking if u are the player who just joined the game
        if (photonView.isMine)
        {
            //turns ur camera on
            PlayerCamera.SetActive(true);

            //sets your player tag to ur name
            playerNameText.text = PhotonNetwork.playerName;
        }
        else
        {
            //turns camera off
            PlayerCamera.SetActive(false);
            playerNameText.text = photonView.owner.NickName;
        }
    }

    void Update()
    {
        //if this is ur player
        if (photonView.isMine)
        {
            //tracking where the mouse is
            mousePos = Input.mousePosition;

            //rotate left
            if (mousePos.x  < 100)
            {
                // Rotate camera and controller.
                transform.Rotate(Vector3.down, 20.0f * Time.deltaTime);
            }

            //rotate right
            else if (mousePos.x > 1820)
            {
                // Rotate camera and controller.
                transform.Rotate(Vector3.up, 20.0f * Time.deltaTime);
            }

            //for readying up
            //if the waiting for players text isnt there
            if (!gm.waitingTxt.gameObject.activeInHierarchy && notReady)
            {

                //ready up canvas
                readyCanvas.SetActive(true);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    readyUp();
                }
            }

            //if not out of time
            if (!qm.outOfTime)
            {
                if (selectedAnswer == 0)
                {
                    //Selecting Answers;
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        selectedAnswer = 1;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        selectedAnswer = 2;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        selectedAnswer = 3;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        selectedAnswer = 4;
                    }
                    
                }

                
            }
            else
            {
                //if answer is right
                if (selectedAnswer == qm.Qs[qm.i-1].correctAnswer)
                {
                    scoreInt++;
                } 

                selectedAnswer = 0;
            }
        }

        //sets the selected answer to yellow to show that the player has picked it
        if(selectedAnswer != 0)
        {
            for (int i = 0; i< 4; i++)
            {
                qm.QuestionAnsTxt[i].color = Color.black;
            }

            qm.QuestionAnsTxt[selectedAnswer - 1].color = Color.yellow;
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                qm.QuestionAnsTxt[i].color = Color.black;
            }
        }

         
        //checks if this code has ran before and if the quiz has started yet
        if (gm.quizStarted && check)
        {
            //Adding the Player to the scoreboard
            ScoreTextObject.transform.SetParent(scorePanel.transform, false);
            ScoreTextObject.AddComponent<Text>();
            scoreTxt = ScoreTextObject.GetComponent<Text>();

            //Formatting
            scoreTxt.resizeTextForBestFit = true;
            scoreTxt.alignment = TextAnchor.MiddleCenter;

            //setting up the score board
            scoreTxt.font = arial;
            scoreTxt.color = Color.black;

            //making sure this doesn't run mutliple times
            check = false;
        }

        //if the code above has run
        if (!check)
        {
            //shows up to date score on screen
            scoreTxt.text = playerNameText.text + ": " + scoreInt.ToString();
        }
    }

    //ready ing up function
    void readyUp()
    {
        //hides ready canvas
        readyCanvas.SetActive(false);

        //makes it so the gamemanager knows that this player is ready
        gm.readiedPlayers++;
        
        //so it wont run again
        notReady = false;
        ranBefore = false;
    }

    //for syncing questions and readying up
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if you are this character
        if (stream.isWriting)
        {
            //ready up
            stream.SendNext(notReady);

            //score
            stream.SendNext(scoreInt);
        }
        
        //if u aint this character
        if (stream.isReading)
        {
            //checks if this player is ready
            bool isready = (bool)stream.ReceiveNext();

            //if they are then ready them up
            if(!isready && ranBefore)
            {
                readyUp();
                ranBefore = false;
            }

            //keep there score up to date
            scoreInt = (int)stream.ReceiveNext();
        }

    }
}