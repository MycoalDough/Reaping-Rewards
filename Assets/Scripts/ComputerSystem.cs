using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerSystem : MonoBehaviour
{

    public GameObject screen;
    public GameObject stockScreen;

    public Animator anim;

    public MovementOverrides mo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mo.computerOpen = true;
            anim.Play("OpenPC");
            screen.SetActive(true);
        }
    }

    public void openStocks()
    {
        stockScreen.SetActive(true);
        DictionarySystem ds = GameObject.FindObjectOfType<DictionarySystem>().GetComponent<DictionarySystem>();
        ds.newDefinition("Stock System", "Stocks are like pieces of a company that people can buy. When you own a stock, you own a little part of that company. Companies sell stocks to raise money to grow and do more business. People buy stocks with the hope that the company will do well, and the stock's value will go up, so they can sell it later for a profit. It's a bit like having a share in the success of a business.");
    }

    public void closeStocks()
    {
        stockScreen.SetActive(false);
    }

    public void exitComputer()
    {
        mo.computerOpen = false;
        anim.Play("ClosePC");
    }
}
