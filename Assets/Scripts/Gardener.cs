using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gardener : MonoBehaviour
{
    public MovementOverrides mo;
    public UtilityController uc;
    public GameObject gardenerCanvas;
    public GlobalVariables g;
    public Inventory inv;

    public GameObject player;

    public AudioSource sfx;

    public double costPerSeed;
    public float cost;
    public string crop;

    public GameObject decrease;
    public GameObject increase;

    public int numberOfSeeds;

    public Text description;
    public Text price;
    public Text amountOfSeeds;


    [Header("All Crop Texts")]
    public Text cornPriceText;
    public Text wheatPriceText;
    public Text PotatoPriceText;


    //NEED TO MAKE ALL 3 CROPS WORK WITH THIS SYSTEM

    // public 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            mo.gardenerOpen = true;
            gardenerCanvas.SetActive(true);
        }
    }

    public void exit()
    {
        mo.gardenerOpen = false;
        gardenerCanvas.SetActive(false);
        crop = "";
        //player.transform.position = player.transform.position + new Vector3(player.transform.position.x, -2);
    }

    public void swapCrop(string cropName)
    {
        crop = cropName;
        if (cropName == "Corn")
        {
            costPerSeed = g.cornPriceInMarket;
        }
        if (cropName == "Wheat")
        {
            costPerSeed = g.wheatPriceInMarket;
        }
        if (cropName == "Potato")
        {
            costPerSeed = g.potatoPriceInMarket;
        }
        if (cropName == "Sprinkler")
        {
            costPerSeed = 30;
        }
        if (cropName == "Scarecrow")
        {
            costPerSeed = 30;
        }
        if (cropName == "Robot")
        {
            costPerSeed = 500;
            crop = "a Robot Worker";
        }
    }

    public void increaseSeedCount()
    {
        Debug.Log((cost / g.taxRate + costPerSeed) * g.taxRate);
        if((cost/g.taxRate + costPerSeed)*g.taxRate <= g.money)
        {
            numberOfSeeds++;
        }
    }

    public void decreaseSeedCount()
    {
        if(numberOfSeeds - 1 >= 1)
        {
            numberOfSeeds--;
        }
    }

    public void Update()
    {
        cost = (float)(numberOfSeeds * costPerSeed) * g.taxRate;
        cost = Mathf.Round(cost * 100f) / 100f;
        if(crop != "")
        {
            description.text = "Hello! Would you like to buy " + crop + " for $" + costPerSeed.ToString() + "? (Plus Tax!)";

        }
        price.text = "$" + cost.ToString();
        amountOfSeeds.text = numberOfSeeds.ToString();

        //poggers
        
        cornPriceText.text = "$1.50" + " per seed";
        wheatPriceText.text = "$" + g.wheatPriceInMarket + " per seed";
        PotatoPriceText.text = "$" + g.potatoPriceInMarket + " per seed";

        if(crop == "")
        {
            decrease.SetActive(false);
            increase.SetActive(false); 
        }
        else
        {
            decrease.SetActive(true);
            increase.SetActive(true);
        }


    }

    public void Buy()
    {
        if(g.money - cost > 0)
        {
            sfx.Play();
            g.money -= (float)cost;

            if (crop == "Corn")
            {
                inv.seeds[0].numberOfSeeds = inv.seeds[0].numberOfSeeds + numberOfSeeds;
            }
            if (crop == "Wheat")
            {
                inv.seeds[1].numberOfSeeds = inv.seeds[1].numberOfSeeds + numberOfSeeds;
            }
            if (crop == "Potato")
            {
                inv.seeds[2].numberOfSeeds = inv.seeds[2].numberOfSeeds + numberOfSeeds;
            }
            if (crop == "Sprinkler")
            {
                uc.sprinkerCount += numberOfSeeds;
            }
            if (crop == "Scarecrow")
            {
                uc.scarecrowCount += numberOfSeeds;
            }
            if (crop == "a Robot Worker")
            {
                uc.robotCount += numberOfSeeds;
            }
        }


    }

    public void resetSeedCount()
    {
        numberOfSeeds = 1;
    }


}
