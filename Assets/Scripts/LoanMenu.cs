using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoanMenu : MonoBehaviour
{
    public LoanSystem ls;
    public TimeManager tm;
    public GlobalVariables gv;

    public bool detectedTimeChange = false;
   
    [Header("Main Variables")]
    int loanedMoney;
    int moneyPayedOff;
    int moneyNeededToPayOff;
    int currentTime;
    bool interestRateApplied = false;
    int maxTime;
    float interestRatePer10Days;
    bool overdue = false;
    [Header("UI")]
    public TextMeshProUGUI T_loanedMoney;
    public TextMeshProUGUI T_moneyCounter;
    public TextMeshProUGUI T_time;
    public TMP_InputField T_depositBox;
    public GameObject T_loanName;
    public GameObject T_overdueLoan;

    public void setUp(int loanedMoney, int L_maxTime, float L_interest)
    {
        T_loanedMoney.text = "Loaned Money: " + loanedMoney;
        maxTime = L_maxTime;
        interestRatePer10Days = L_interest;
        moneyNeededToPayOff = Mathf.CeilToInt(loanedMoney * interestRatePer10Days);
        Debug.Log(moneyNeededToPayOff);
    }
    // Start is called before the first frame update
    void Awake()
    {
        ls = GameObject.FindObjectOfType<LoanSystem>().GetComponent<LoanSystem>();
        gv = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();
        tm = GameObject.FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        T_moneyCounter.text = "Money til paid off: $" + moneyPayedOff + " / $" + moneyNeededToPayOff;
        T_time.text = "Time to pay: " + currentTime + "/" + maxTime;
        paidOff();
        if (tm.timeChanged  && !detectedTimeChange)
        {
            currentTime++;
            detectedTimeChange = true;
        }
        else if (!tm.timeChanged)
        {
            if (detectedTimeChange)
            {
                detectedTimeChange = false;
            }
        }

        if(maxTime <= currentTime)
        {
            overdue = true;
            T_loanName.SetActive(false);
            T_overdueLoan.SetActive(true);
        }
        if(currentTime % 30 == 0 && !interestRateApplied)
        {
            moneyNeededToPayOff = Mathf.CeilToInt(moneyNeededToPayOff * interestRatePer10Days);
            interestRateApplied = true;
            if(overdue)
            {
                ls.creditScore -= 25;
                T_loanName.SetActive(false);
                T_overdueLoan.SetActive(true);
            }
        }
        else if(currentTime % 30 != 0 && interestRateApplied)
        {
            interestRateApplied = false;
        }
    }

    public void Deposit()
    {
        if(T_depositBox.text.ToString() != null && T_depositBox.text == "ALL")
        {
            if(gv.money - (moneyNeededToPayOff - moneyPayedOff) > 0)
            {
                moneyPayedOff += (moneyNeededToPayOff - moneyPayedOff);
                gv.money -= (moneyNeededToPayOff - moneyPayedOff);
                T_depositBox.text = "";
                return;
            }
        }
        if (T_depositBox.text.ToString() != null && int.Parse(T_depositBox.text.ToString()) <= 0)
        {
            T_depositBox.text = "";
            return;
        }

        int depAmount = int.Parse(T_depositBox.text.ToString());
        if (gv.money - depAmount >= 0)
        {
            moneyPayedOff += depAmount;
            gv.money -= depAmount;
            T_depositBox.text = "";
        }
    }

    public void paidOff()
    {
        if(moneyNeededToPayOff <= moneyPayedOff)
        {
            if(loanedMoney <= 400)
            {
                ls.creditScore += 15;
            }
            if (loanedMoney > 400 && loanedMoney <= 1000)
            {
                ls.creditScore += 25;
            }
            else
            {
                ls.creditScore += 50;
            }
            ls.numOfLoans--;
            Destroy(gameObject);
        }

    }
}
