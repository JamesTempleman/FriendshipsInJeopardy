using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuizManager : MonoBehaviour
{

    /*
     * ToDo:
     * Impliment Quiz UI
     * 
     */
     

    //the questions themselves
    public Questions[] Qs;

    //game manager script
    public GameManager gm;

    //quiz variables
    public int quizLength;
    public int RandomNumber;
    public float timeRemaining = 60;

    public GameObject QuestionUI;
    public Text QuestionText;
    public Text Timer;
    public Text[] QuestionAnsTxt;

    //for iterating through questions
    public int i = 0;

    //if out of time
    public bool outOfTime = false;

    //if quiz is done
    public bool quizDone = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.quizStarted)
        {
            //Quiz here
            Quiz();
        }
    }

    public void Quiz()
    {
        //checks if quiz is done
        if (i <= quizLength-1 && !quizDone) {

            //unhides question UI and loads up questions
            QuestionUI.SetActive(true);
            QuestionText.text = Qs[i].question;
            
            QuestionAnsTxt[0].text = Qs[i].answers[0];
            QuestionAnsTxt[1].text = Qs[i].answers[1];
            QuestionAnsTxt[2].text = Qs[i].answers[2];
            QuestionAnsTxt[3].text = Qs[i].answers[3];
            
            //sets up timer
            Timer.text = timeRemaining.ToString();

            //makes timer work
            if (timeRemaining > 0)
            {
                outOfTime = false;
                timeRemaining -= Time.deltaTime;
            }
            //if out of time
            else
            {
                outOfTime = true;
                timeRemaining = 10;
                if (i != quizLength)
                {
                    i++;
                }
                else
                {
                    quizDone = true;
                }
            }
        }
        //if quiz is done
        else
        {
            timeRemaining = 0;
            Debug.Log("Quiz is done!");

            /*    Impliment End of Quiz Screen   */
        }
    }
}
