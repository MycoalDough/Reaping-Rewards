using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class RNGEventSystem : MonoBehaviour
{

    public MovementOverrides mo;
    public GlobalVariables gv;
    public TimeManager tm;
    public List<RNGEventQuestion> Q = new List<RNGEventQuestion>();

    public int chosenTime = 40;

    public int numQuestionsCorrect = 0;
    public int numQuestionsIncorrect = 0;
    private bool db;
    private bool canPress = false;
    public int chosenRNG;

    public GameObject Xbutton;

    public GameObject parent;
    public int chosenAnswer;

    public Text nameText;
    public Text description;

    public Text a1;
    public Text a2;
    public Text a3;
    public Text a4;

    public Text explaination;

    void Update()
    {
        if(tm.getTime() % chosenTime == 0 && !db && tm.getTime() != 0)
        {
            parent.SetActive(true);
            db = true;
            chosenRNG = Random.Range(0, Q.Count);
            SetUp();
        }
        else if (tm.getTime() % chosenTime != 0)
        {
            db = false;
        }
    }

    public void leave()
    {
        parent.SetActive(false);
        canPress = true;
        mo.rngOpen = false;
    }

    public void SetUp()
    {
        mo.rngOpen = true;
        Xbutton.SetActive(false);
        canPress = true;
        explaination.gameObject.SetActive(false);

        nameText.text = Q[chosenRNG].name;
        description.text = Q[chosenRNG].description;
        explaination.text = Q[chosenRNG].explaination;

        a1.text = Q[chosenRNG].answers[0];
        a2.text = Q[chosenRNG].answers[1];
        a3.text = Q[chosenRNG].answers[2];
        a4.text = Q[chosenRNG].answers[3];

    }

    public void clickAnswer(int number)
    {
        if(canPress)
        {
            canPress = false;
            if (number == Q[chosenRNG].correctAnswer + 1)
            {
                correctAnswer();


            }
            else
            {
                wrongAnswer();
                Debug.Log(Q[chosenRNG].correctAnswer + 1);
            }

           
            explaination.gameObject.SetActive(true);
            Xbutton.SetActive(true);
            questionAnalytics();
        }

    }

    public void correctAnswer()
    {
        numQuestionsCorrect++;
        gv.money += Q[chosenRNG].reward;
        explaination.text = "You got the question correct!  " + Q[chosenRNG].explaination;
    }

    public void wrongAnswer()
    {
        numQuestionsIncorrect++;
        //gv.money += Q[chosenRNG].reward;
        explaination.text = "Nice try. The correct answer was #" + (Q[chosenRNG].correctAnswer + 1) + ".  " + Q[chosenRNG].explaination;
    }

    public void questionAnalytics() {

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "sparklingInt", numQuestionsCorrect },
           
        };

        UnityEngine.Analytics.Analytics.CustomEvent("numQuestion", parameters);
    }
}
