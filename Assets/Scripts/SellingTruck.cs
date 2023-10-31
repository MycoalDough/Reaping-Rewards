using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingTruck : MonoBehaviour
{

    public NewspaperSystem ns;
    public MovementOverrides mo;
    public GlobalVariables gv;
    public Inventory inv;
    public Animator anim;

    public Text sellRate;

    public Text reciptCrop;
    public Text reciptSold;
    public Text reciptFee;
    public Text reciptTotal;

    public Text cropText;

    public int actualAmountSold;


    public string crop;
    public double price;
    public int numberOfCrops;
    public int percentage;
    public double offset;
    public GameObject parent;

    public Text amountOfCropsText;
    public Text priceText;

    private void Start()
    {
        changeCrop("Corn");

    }
    private void Update()
    {
        amountOfCropsText.text = "" + numberOfCrops;
        priceText.text = "$" + price;
        checkPercentage();
        cropText.text = "Selected Crop: " + crop;
    }

    private void checkPercentage()
    {
        percentage = 13;
        percentage -= ns.competitor;

        if (crop == "Corn")
        {
            if(ns.chosenPlant == "Corn")
            {
                percentage += 30;

                if(price < ns.chosenPriceRange + offset)
                {
                    percentage += 40;
                }
            }
            else
            {
                if (price < ns.chosenPriceRange + offset)
                {
                    percentage += 25;
                }
            }
        }
        if (crop == "Wheat")
        {
            if (ns.chosenPlant == "Wheat")
            {
                percentage += 30;

                if (price < ns.chosenPriceRange + offset)
                {
                    percentage += 40;
                }
            }
            else
            {
                if (price < ns.chosenPriceRange + offset)
                {
                    percentage += 25;
                }
            }
        }
        if (crop == "Potato")
        {
            if (ns.chosenPlant == "Potato")
            {
                percentage += 30;

                if (price < ns.chosenPriceRange + offset)
                {
                    percentage += 40;
                }
            }
            else
            {
                if (price < ns.chosenPriceRange + offset)
                {
                    percentage += 25;
                }
            }
        }

        if(price > 9.5) //height check!1
        {
            percentage -= 20;
        }

        if(percentage < 0)
        {
            percentage = 0;
        }
       
        if(percentage > 100)
        {
            percentage = 100;
        }
        changeSellRateText();
    }

    public void changeSellRateText()
    {
        if(percentage > 100)
        {
            percentage = 100;
        }
        sellRate.text = percentage + "%";

        if(percentage > 70)
        {
            sellRate.color = new Color32(28, 255, 0, 255);
        }
        else if(percentage > 45 && percentage <= 70)
        {
            sellRate.color = new Color32(255, 192, 0, 255);
        }
        else
        {
            sellRate.color = new Color32(255, 0, 20, 255);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mo.sellOpen = true;
            parent.SetActive(true);
            DictionarySystem ds = GameObject.FindObjectOfType<DictionarySystem>().GetComponent<DictionarySystem>();
            ds.newDefinition("Selling in Bulk", "Selling in bulk means selling a lot of something all at once, which is a good idea for a few reasons. When you buy in bulk, you can often get a lower price per item, so it can save you money. It's also convenient because you have more of what you need, and you don't have to shop as often. Businesses like selling in bulk too because they can reach more customers and reduce their costs, making it a win-win for both buyers and sellers.");
        }
    }

    public void changeCrop(string cropName)
    {
        crop = cropName;
        price = 1;
        numberOfCrops = 1;
    }

    public void ChangePrice(float add)
    {
        if(add > 0)
        {
            if(price + add < 20.5)
            {
                price = price + add;

            }
        }
        else
        {
            if (price - add > 0.5)
            {
                price = price + add;
            }

        }
    }

    public void sell()
    {
        double moneyEarned = 0;

        anim.Play("SellingAnimation");

        for (int i = 0; i < numberOfCrops; i++)
        {
            int rng = Random.Range(0, 101);

            if(rng < percentage)
            {
                actualAmountSold++;

                if (crop == "Corn")
                {
                    inv.seeds[0].numberOfPlants--;
                }
                else if (crop == "Wheat")
                {
                    inv.seeds[1].numberOfPlants--;
                }
                else if (crop == "Potato")
                {
                    inv.seeds[2].numberOfPlants--;
                }
                gv.money += (float)price;
                moneyEarned += price;
            }




        }
        reciptCrop.text = "Crop Sold: " + crop;
        reciptSold.text = "Amount Sold: " + actualAmountSold + "/" + numberOfCrops;

        if(actualAmountSold >= 1)
        {
            AchievementCanvas ac;
            ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
            ac.Add("First Sale", 1);
        }
        price = 1;
        numberOfCrops = 1;
        int fee = 2 + (2 * (int)(numberOfCrops / 6));
        float feeTax = fee * gv.taxRate;
        feeTax = Mathf.Round(feeTax * 100f) / 100f;
        float totals = (float)moneyEarned - feeTax;
        totals = Mathf.Round(totals * 100f) / 100f;


        gv.money -= feeTax;
        reciptFee.text = "Shipping Fee (+Tax): $" + feeTax;
        
        if(totals < 0)
        {
            reciptTotal.text = "Total Earnings: -$" + Mathf.Abs(totals);
        }
        else
        {
            reciptTotal.text = "Total Earnings: $" + totals;

            AchievementCanvas ac;
            ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
            ac.Add("Making Dough", totals);
            ac.Add("A Small Fortune", totals);
            ac.Add("One Grand", totals);
            ac.Add("Millonaire", totals);

        }

        actualAmountSold = 0;
    }

    public void exitRecipt()
    {
        anim.Play("ExitSelling");
    }

    public void Exit()
    {
        mo.sellOpen = false;
        parent.SetActive(false);
    }

    public void changeNumberCrop(bool increase)
    {
        if(increase)
        {
            if(checkIfCrop())
            {
                numberOfCrops++;
            }
        }
        else
        {
            if(numberOfCrops - 1 > 0)
            {
                numberOfCrops--;
            }
        }
    }

    public void resetNumber(bool isPrice)
    {
        if(isPrice)
        {
            price = 1;
        }
        else
        {
            numberOfCrops = 1;
        }
    }

    public void maxCrops()
    {
        if (crop == "Corn")
        {
            numberOfCrops = inv.seeds[0].numberOfPlants;
        }
        else if (crop == "Wheat")
        {
            numberOfCrops = inv.seeds[1].numberOfPlants;
        }
        else if (crop == "Potato")
        {
            numberOfCrops = inv.seeds[2].numberOfPlants;
        }
    }
    public bool checkIfCrop()
    {
        if(crop == "Corn" && (inv.seeds[0].numberOfPlants >= numberOfCrops + 1))
        {
            return true;
        }
        else if (crop == "Wheat" && (inv.seeds[1].numberOfPlants >= numberOfCrops + 1))
        {
            return true;
        }
        else if (crop == "Potato" && (inv.seeds[2].numberOfPlants >= numberOfCrops + 1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
