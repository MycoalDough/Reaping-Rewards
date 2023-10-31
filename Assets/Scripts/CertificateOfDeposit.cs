using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CertificateOfDeposit : MonoBehaviour
{
    public bool slot1;
    public bool slot2;
    public bool slot3;

    public float money;
    public int timeStart;
    public int time;
    public float earlyMoney;

    private bool timeDB;
    private bool finished;

    public GlobalVariables gv;
    public BankSystem bs;
    public TimeManager tm;

    public Text timeLimit;
    public Text currentMoney;
    public Text earlyMoneyText;
    public Text finalMoney;

    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        tm = GameObject.FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        gv = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();

        bs = GameObject.FindObjectOfType<BankSystem>().GetComponent<BankSystem>();
        earlyMoney = money * 0.9f;

        AchievementCanvas ac;
        ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
        ac.Add("Money Comes Easy", 1);

    }

    // Update is called once per frame
    void Update()
    {
        changeAllTexts();

        if(slot1)
        {
            //gameObject.GetComponent<RectTransform>().position = bs.slot1Pos;
        }
        if (slot2)
        {
            //gameObject.GetComponent<RectTransform>().position = bs.slot2Pos;
        }
        if (slot3)
        {
           // gameObject.GetComponent<RectTransform>().position = bs.slot3Pos;
        }
        money = Mathf.Round(money * 100f) / 100f;

        if (!timeDB && tm.getTime() % 25 == 0 && !finished)
        {
            timeDB = true;
            time++;
            money *= gv.interestRate;
        }
        else if (tm.getTime() % 25 != 0)
        {
            timeDB = false;
        }


        if (time >= timeStart)
        {
            parent.SetActive(false);
            finished = true;
            finalMoney.gameObject.SetActive(true);
        }
    }

    public void changeAllTexts()
    {
        timeLimit.text = time + "/" + timeStart;
        currentMoney.text = "$" + money;
        earlyMoneyText.text = "Click here to claim early for $" + earlyMoney;
        finalMoney.text = "Click to claim $" + money + "!";
    }

    public void claim(bool isEarly)
    {
        if (finished && !isEarly)
        {
            AchievementCanvas ac;
            ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
            ac.Add("Making Dough", money);
            ac.Add("A Small Fortune", money);
            ac.Add("One Grand", money);
            ac.Add("Millonaire", money);
            gv.money += money;
            if (slot1)
            {
                bs.slot1open = true;
            }
            else if (slot2)
            {
                bs.slot2open = true;
            }
            else if (slot3)
            {
                bs.slot3open = true;
            }
            Destroy(gameObject);
        }
        else if(isEarly)
        {
            AchievementCanvas ac;
            ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
            ac.Add("Making Dough", earlyMoney);
            ac.Add("A Small Fortune", earlyMoney);
            ac.Add("One Grand", earlyMoney);
            ac.Add("Millonaire", earlyMoney);
            gv.money += earlyMoney;
            if (slot1)
            {
                bs.slot1open = true;
            }
            else if (slot2)
            {
                bs.slot2open = true;
            }
            else if (slot3)
            {
                bs.slot3open = true;
            }
            Destroy(gameObject);
        }


    }


    
}
