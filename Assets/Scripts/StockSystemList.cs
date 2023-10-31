using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class StockSystemList
{
    public float originalPrice;
    public float previousPrice;
    public float currentPrice;
    public int currentShares;

    public float min;
    public float max;

    public GameObject downArrow;
    public GameObject upArrow;

    public Text OP;
    public Text PP;
    public Text CP;
    public Text CS;
    public StockSystemList(GameObject DA, GameObject UA, float Min, float Max, float OriginalPrice, float PreviousPrice, float CurrentPrice, int CurrentShares, Text oOP, Text oPP, Text oCP, Text oCS)
    {
        downArrow = DA;
        upArrow = UA;
        originalPrice = OriginalPrice;
        previousPrice = PreviousPrice;
        currentPrice = CurrentPrice;
        currentShares = CurrentShares;
        min = Min;
        max = Max;
        OP = oOP;
        PP = oPP;
        CP = oCP;
        CS = oCS;
    }
}
