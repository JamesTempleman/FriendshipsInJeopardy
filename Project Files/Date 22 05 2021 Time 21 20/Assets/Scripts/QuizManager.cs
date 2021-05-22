using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class QuizManager : MonoBehaviour
{

    public player player;

    //SFX
    public AudioSource correctAnswer;
    public AudioSource wrongAnswer;

    //for right and wrong answer notifications
    bool answerCorrect = false;
    bool answerWrong = false;
    float notificationTimer = 1;
    public GameObject correctGO;
    public GameObject wrongGO;

    //Music
    public AudioSource countdownMusic;
    public AudioSource waitingMusic;
    
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

    //UI
    public GameObject endScore;
    public Text endScoreTxt;
    

    // Update is called once per frame
    void Update()
    {
        //if quiz started and quiz isnt done
        if (gm.quizStarted && !quizDone)
        {
            //Quiz here
            Quiz();
            
        }

        //right and wrong answer notifcations
        if (answerCorrect)
        {
            if (notificationTimer > 0)
            {
                notificationTimer -= Time.deltaTime;
            }
            else
            {
                notificationTimer = 1;
                answerCorrect = false;
                correctGO.SetActive(false);
            }
        }
        else if (answerWrong)
        {
            if (notificationTimer > 0)
            {
                notificationTimer -= Time.deltaTime;
            }
            else
            {
                notificationTimer = 1;
                answerWrong = false;
                wrongGO.SetActive(false);
            }
        }
        else
        {
            answerWrong = false;
            answerCorrect = false;
        }
        

    }

    public void Quiz()
    {
        //checks if quiz is done
        if (i <= quizLength - 1 && !quizDone)
        {
            if (i == 0 && !countdownMusic.isPlaying)
            {
                countdownMusic.Play();
            }
            
            //unhides question UI and loads up questions
            QuestionUI.SetActive(true);
            QuestionText.text = Qs[i].question;

            QuestionAnsTxt[0].text = Qs[i].answers[0];
            QuestionAnsTxt[1].text = Qs[i].answers[1];
            QuestionAnsTxt[2].text = Qs[i].answers[2];
            QuestionAnsTxt[3].text = Qs[i].answers[3];
            

            //sets up timer
            Timer.text = String.Format("{0:0.00}", timeRemaining);

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
                    Debug.Log("Quiz Isn't done");
                }
            }
        }
        else
        {
            //tell the console se timeRemaining to 0 and remove the question UI
            timeRemaining = 0;
            Debug.Log("Quiz is done!");
            QuestionUI.SetActive(false);
            endScore.SetActive(true);
            countdownMusic.Stop();
            waitingMusic.Play();
            quizDone = true;
        }

    }

    public void OnePressed()
    {
        player.selectedAnswer = 1;
    }

    public void TwoPressed()
    {
        player.selectedAnswer = 2;
    }
    public void ThreePressed()
    {
        player.selectedAnswer = 3;
    }

    public void FourPressed()
    {
        player.selectedAnswer = 4;
    }

    public void CorrectAnswer()
    {
        answerCorrect = true;
        correctAnswer.Play();
        correctGO.SetActive(true);
    }

    public void WrongAnswer()
    {
        answerWrong = true;
        wrongAnswer.Play();
        wrongGO.SetActive(true);
    }
}