using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour
{
    public float money;
    public float allMoney;
    public float taxRate;
    public float interestRate;

    public int shipFee;

    public Text moneyText;

    [Header("Corn Variables")]
    public double cornPriceInMarket;

    [Header("Wheat Variables")]
    public double wheatPriceInMarket;

    [Header("Potato Variables")]
    public double potatoPriceInMarket;

    public void Update()
    {
        money = Mathf.Round(money * 100f) / 100f;
        moneyText.text = "$" + money.ToString();
    }
}
