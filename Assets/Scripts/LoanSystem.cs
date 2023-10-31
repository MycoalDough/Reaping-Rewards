using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoanSystem : MonoBehaviour
{
    public GlobalVariables gv;
    public TimeManager tm;
    public TMP_InputField moneyField;
    public int creditScore; //starts at 600
    public int savedCreditScore;
    //a creditscore below 550
    public GameObject main;
    //decreases chance of getting a loan to 50%, and increases loan interest to 1.3, increases morgage by x1.2, numOfLoans = 1
    //anything between 300-650 makes loans = 80%, loan interest = 1.2, increases morgage by x1, numOfLoans = 2
    //651-800 = lans = 100%, loan interest 1.1, increases morgage by x.9, numOfLoans = 3
    public GameObject loanParent;
    public GameObject loan;
    public int numOfLoans;
    public int maxLoans;
    public int loanAmount;
    public float morgageRate;
    public float chance;
    public float interestRate;
    public int daysTilDue;
    public GameObject loanLock;
    public bool b_loanLock;
    public int dayTilunLock;
    public bool timeDB;
    public TextMeshProUGUI loanLockText;
    public TextMeshProUGUI creditSCore;
    public TextMeshProUGUI intRateText;
    public TextMeshProUGUI chanceText;
    public TextMeshProUGUI maxText;

    public RectTransform parent;
    public Vector3 originalPos = new Vector3(-813.665f, -57.35345f, 0);
    public Vector3 hiddenPos = new Vector3(0, -1000, 0);

    public int free;
    public int savedCredit;
    public GameObject hidden;
    public TextMeshProUGUI freeonot;

    //formula: (CS - 300) / (800 - 300) * (1500 - LA) / (1500 - 100) * 100 //cs is credit score, la is loan ammount
    private void Start()
    {
        creditScore = 600;
        tm = GameObject.FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        gv = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();
    }
    public void creditCheck()
    {
        intRateText.text = "Interest Rate (per 30 days): " + interestRate + "%";
        chanceText.text = "Chance to get a loan: " + Mathf.Round(chance* 100f) / 100f + "%";
        maxText.text = "Max number of loans: " + maxLoans;
        if (creditScore <= 400)
        {
            interestRate = 1.1f;
            maxLoans = 1;
            morgageRate = 1.1f;
        }
        else if(creditScore <= 600 && creditScore > 400)
        {
            interestRate = 1.08f;
            maxLoans = 2;
            morgageRate = 1f;
        }
        else
        {
            interestRate = 1.05f;
            maxLoans = 3;
            morgageRate = 0.95f;
        }
    }


    public void unhide()
    {
        if(free - 1 >= 0)
        {
            hidden.SetActive(false);
            savedCreditScore = creditScore;
        }
        else if(gv.money - 20 >= 0)
        {
            hidden.SetActive(false);
            savedCreditScore = creditScore;
            gv.money -= 20;
        }
    }

    public void Update()
    {
        creditSCore.text = "Credit Score: " + creditScore;
        if (savedCreditScore != creditScore)
        {
            savedCreditScore = 0;
            hidden.SetActive(true);
        }
        creditCheck();
        int c;
        if ((moneyField.text.ToString() != null || moneyField.text.ToString() != "") && int.TryParse(moneyField.text.ToString(), out c)) 
        { 
            loanAmount = int.Parse(moneyField.text.ToString()); 
        }
        chance = (creditScore - 300) / 5 + (1500 - loanAmount) / 20;
        chance = Mathf.Min(100, Mathf.Max(0, chance));
        Debug.Log(chance);
        if (b_loanLock && tm.time >= dayTilunLock)
        {
            b_loanLock = false;
            dayTilunLock = 0;
            loanLock.SetActive(false);
        }


        if (tm.getTime() % 30 == 0 && !timeDB && tm.getTime() != 0)
        {
            timeDB = true;
            free++;
        }
        else if (tm.getTime() % 30 != 0)
        {
            timeDB = false;
        }
        
        if(free > 0)
        {
            freeonot.text = "(Free checks left: " + free + ")";
        }
        else
        {
            freeonot.text = "(Cost: $20.00)";
        }

    }

    public void openLoan()
    {
        if(numOfLoans + 1 <= maxLoans)
        {
            int rng = Random.Range(0, 101);
            if(rng <= chance)
            {
                if (moneyField.text.ToString() != null && int.Parse(moneyField.text.ToString()) <= 0)
                {
                    moneyField.text = "";
                    return;
                }

                int loanNum = int.Parse(moneyField.text.ToString());
                if (loanNum >= 100 && loanNum <= 1500)
                {
                    gv.money += loanNum;
                    GameObject l_loan = Instantiate(loan, loanParent.transform.position, Quaternion.identity, loanParent.transform);
                    LoanMenu lm = l_loan.GetComponent<LoanMenu>();
                    lm.setUp(loanNum, daysTilDue, interestRate);
                    l_loan.SetActive(true);
                    numOfLoans++;
                    moneyField.text = "";
                }
            }
            else
            {
                loanLocked();
            }
        }
    }

    public void loanLocked()
    {
        loanLock.SetActive(true);
        dayTilunLock = tm.time + 15;
        loanLockText.text = "(Day #" + dayTilunLock + ")";
        b_loanLock = true;

    }

    public void enter()
    {
        parent.localPosition = originalPos;
        main.SetActive(true);
        DictionarySystem ds = GameObject.FindObjectOfType<DictionarySystem>().GetComponent<DictionarySystem>();
        ds.newDefinition("Loan", "A loan is like borrowing money from someone, usually a bank. You promise to pay it back, but you can use the money for important things like buying a house or going to college. It's like getting help when you need it.");
        ds.newDefinition("Credit Score (!)", "Your credit score is like a report card for how good you are at borrowing and paying back money. If you pay back your loans on time, your credit score goes up, and it's easier to borrow more money. If you don't, it goes down, and it can be harder to borrow.");
        ds.newDefinition("Interest (Loans)", "When you borrow money, you usually have to pay back more than you borrowed. The extra money you pay is called interest. It's like a small fee for borrowing money. The interest rate is how much that fee costs.");
        ds.newDefinition("Debt (Loan)", "Debt is like the money you owe when you borrow.If you borrowed money for a bike, the bike is yours, but you still owe the money.It's a bit like a promise to pay back the loan, and you need to make payments to clear your debt.");
    }
    public void exit()
    {
        parent.localPosition = hiddenPos;
        main.SetActive(false);
    }
}
