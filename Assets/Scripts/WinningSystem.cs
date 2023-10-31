using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningSystem : MonoBehaviour
{
    public Text morgage;
    public Text win;
    public LoanSystem ls;
    public GameObject parent;

    public int year;
    public TimeManager tm;

    public GlobalVariables gv;

    public int cost = 75;

    public int days = 60;

    public MovementOverrides mo;

    public Button onward;

    public bool DB;
    public int lives = 3;

    public GameObject heart3;
    public GameObject heart2;
    public GameObject heart1;

    public void Start()
    {
        ls = GameObject.FindObjectOfType<LoanSystem>().GetComponent<LoanSystem>();
    }
    void Update()
    {
        if(tm.getTime() % days == 0 && !DB && tm.getTime() != 0)
        {
            parent.SetActive(true);
            doEVERYTHING();
            mo.systemOpen = true;
            tm.time++;
        }
    }

    public void doEVERYTHING()
    {
        morgage.text = "Morgage #" + year;
        if(gv.money - (year * cost) >= 0)
        {
            gv.money = gv.money - (year * cost * ls.morgageRate);
            onward.gameObject.SetActive(true);
            win.text = "Congrats! You were able to pay off your morgage for $" + (year * cost * ls.morgageRate) + "! See you next year!";
            ls.creditScore += 50;
        }
        else
        {
            lives--;
            if(lives == 2)
            {
                heart3.GetComponent<Animator>().Play("FirstHeartDeath");
            }
            else if(lives == 1)
            {
                heart2.GetComponent<Animator>().Play("FirstHeartDeath");
            }
            else
            {
                heart1.GetComponent<Animator>().Play("FirstHeartDeath");
            }
            if (lives > 0)
            {
                gv.money = gv.money - (year * cost * ls.morgageRate);
                onward.gameObject.SetActive(true);
                win.text = $"Uh oh. Watch out! You have {lives} lives left! You were able to pay $" + (year * cost * ls.morgageRate) + ", but you're in debt! Try taking out a loan and investing.";
                ls.creditScore -= 20;

            }
            else
            {
                win.text = "Nice try! You couldn't pay off your morgage for $" + (year * cost * ls.morgageRate) + ". Refresh to play again!";
                onward.gameObject.SetActive(false);
            }

        }
    }

    public void exit()
    {
        year++;
        mo.systemOpen = false;
        parent.SetActive(false);
    }
}
