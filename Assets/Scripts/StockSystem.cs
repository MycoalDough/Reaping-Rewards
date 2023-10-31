using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockSystem : MonoBehaviour
{
    public Image[] arrows;
    public GlobalVariables gv;

    public List<StockSystemList> SSL = new List<StockSystemList>();

    public TimeManager tm;
    private bool timeDB;
    public int payday;

    private void Start()
    {
        changeShare();
    }

    void Update()
    {
        changeAllTexts();
        if (tm.getTime() % payday == 0 && !timeDB && tm.getTime() != 0)
        {
            timeDB = true;
            changeShare();
        }
        else if(tm.getTime() % payday != 0)
        {
            timeDB = false;
        }
    }

    public void changeShare()
    {
        for (int i = 0; i < SSL.Count; i++)
        {
            float save = SSL[i].currentPrice;
            SSL[i].currentPrice = SSL[i].currentPrice + Random.Range(SSL[i].min, SSL[i].max + 1);
            if(SSL[i].currentPrice < 0)
            {
                SSL[i].currentPrice = 0;
            }
            SSL[i].currentPrice = Mathf.Round(SSL[i].currentPrice * 100f) / 100f;

            SSL[i].previousPrice = save;

            if (SSL[i].previousPrice > SSL[i].currentPrice)
            {
                SSL[i].upArrow.SetActive(false);
                SSL[i].downArrow.SetActive(true);

            }
            else
            {
                SSL[i].upArrow.SetActive(true);
                SSL[i].downArrow.SetActive(false);
            }

        }
    }

    public void changeAllTexts()
    {
        for (int i = 0; i < SSL.Count; i++)
        {
            SSL[i].OP.text = "$" + SSL[i].originalPrice;
            SSL[i].PP.text = "$" + SSL[i].previousPrice;
            SSL[i].CP.text = "$" + SSL[i].currentPrice;
            SSL[i].CS.text = "Current Shares: " + SSL[i].currentShares;
        }
    }

    public void buy1(int i)
    {
        if(gv.money - SSL[i].currentPrice >= 0)
        {
            gv.money -= SSL[i].currentPrice;
            SSL[i].currentShares++;
            AchievementCanvas ac;
            ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
            ac.Add("Newbie Investor", SSL[i].currentPrice);
            ac.Add("Average investor", SSL[i].currentPrice);
            ac.Add("Investing God", SSL[i].currentPrice);

        }
    }

    public void buy5(int i)
    {
        if (gv.money - (SSL[i].currentPrice * 5) >= 0)
        {
            gv.money -= (SSL[i].currentPrice * 5);
            SSL[i].currentShares += 5;
            AchievementCanvas ac;
            ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
            ac.Add("Newbie Investor", SSL[i].currentPrice *5);
            ac.Add("Average investor", SSL[i].currentPrice *5);
            ac.Add("Investing God", SSL[i].currentPrice* 5);
        }
    }

    public void sell1(int i)
    {
        if(SSL[i].currentShares - 1 >= 0)
        {
            SSL[i].currentShares--;
            gv.money += SSL[i].currentPrice;
            AchievementCanvas ac;
            ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
            ac.Add("A Small Fortune", SSL[i].currentPrice);
            ac.Add("One Grand", SSL[i].currentPrice);
            ac.Add("Millonaire", SSL[i].currentPrice);
            ac.Add("Making Dough", SSL[i].currentPrice);

        }
    }

    public void sellAll(int i)
    {
        gv.money += SSL[i].currentPrice * SSL[i].currentShares;
        SSL[i].currentShares = 0;
        AchievementCanvas ac;
        ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
        ac.Add("A Small Fortune", SSL[i].currentPrice * SSL[i].currentShares);
        ac.Add("One Grand", SSL[i].currentPrice * SSL[i].currentShares);
        ac.Add("Millonaire", SSL[i].currentPrice * SSL[i].currentShares);
        ac.Add("Making Dough", SSL[i].currentPrice * SSL[i].currentShares);


    }
}
