using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewspaperSystem : MonoBehaviour
{

    [Header("Settings & Config")]
    [SerializeField] private TimeManager tm;
    [SerializeField] private MovementOverrides mo;
    [SerializeField] private int timeRotation = 12;
    [SerializeField] private GameObject button;
    [SerializeField] private bool dbTime = false;
    [SerializeField] private Animator anim;
    public Text newspaper;
    public Text weather;
    public Text comp;

    public CrowSpawn cs;
    public StockSystem ss;


    [Header("Newspaper Icon")]
    [SerializeField] private Sprite noIcon;
    [SerializeField] private Sprite Icon;

    [Header("All Statistics")]
    public string chosenPlant;
    public int cPlant;
    public int chosenPriceRange;
    public int weatherBonus;
    public int competitor;

    void Update()
    {
        tryIcon();

        if(mo.nameOfPlayer == "")
        {
            changeNewspaperRotation();
        }
    }

    public void changeNewspaperRotation()
    {
        newspaper.text = "";
        changePlant();
        changePriceRange();
        changeWeather();
        competitorChange();
    }

    public void openNewspaper()
    {
        anim.Play("NewspaperOpen");
        mo.newspaperOpen = true;
        button.GetComponent<Image>().sprite = noIcon;
        DictionarySystem ds = GameObject.FindObjectOfType<DictionarySystem>().GetComponent<DictionarySystem>();
        ds.newDefinition("Newspaper (!)", "The newspaper shows you everything you need to know to win in this game! It shows you the best crop and price range for it, allowing you to have a better chance of selling. It also influences the STOCK MARKET!");
    }

    public void exitNewspaper()
    {
        anim.Play("NewspaperClose");
        mo.newspaperOpen = false;
    }

    public void changePlant()
    {
        int rng = Random.Range(1, 4);
        
        if(rng == 1) //corn
        {
            cPlant = 0;
            chosenPlant = "Corn";
            newspaper.text += "Currently, the best selling plant is CORN.  ";
            ss.SSL[0].min = -1;
            ss.SSL[0].max = 3.5f;

            //wheat
            ss.SSL[1].min = -3;
            ss.SSL[1].max = 2.3f;
            //potato
            ss.SSL[2].min = -4.44f;
            ss.SSL[2].max = 4.11f;
        }
        else if(rng == 2) //wheat
        {
            cPlant = 1;
            chosenPlant = "Wheat";
            newspaper.text += "Currently, the best selling plant is WHEAT.  ";

            //corn
            ss.SSL[0].min = -2.5f;
            ss.SSL[0].max = 1.5f;
            //potato
            ss.SSL[2].min = -4.44f;
            ss.SSL[2].max = 4.11f;

            ss.SSL[1].min = -2;
            ss.SSL[1].max = 3;

        }
        else if(rng == 3) //potato
        {
            {
                cPlant = 2;
                chosenPlant = "Potato";
                newspaper.text += "Currently, the best selling plant are POTATOES.  ";

                ss.SSL[2].min = -3.33f;
                ss.SSL[2].max = 5;

                //corn
                ss.SSL[0].min = -2.5f;
                ss.SSL[0].max = 1.5f;
                //wheat
                ss.SSL[1].min = -3;
                ss.SSL[1].max = 2.3f;


            }
        }
    }

    public void changePriceRange()
    {
        int rng = Random.Range(2, 8);
        chosenPriceRange = rng;
        newspaper.text += "Also, the best price for this would be around $" + rng + ".  ";
    }

    public void changeWeather()
    {
        weather.text = "";
        int rng = Random.Range(0, 6);

        if(rng == 0 || rng == 1) //normal
        {
            weatherBonus = 0;
            weather.text += "Looks like the weather is clear! (-0 farming time).";
        }
        else if (rng == 2) //rainy
        {
            weatherBonus = 1;
            weather.text += "Looks like it's going to be rainy! (-1 farming time).";
        }
        else if (rng == 3)
        {
            weatherBonus = -2;
            weather.text += "Aw man. It's going to snow! Crows are migrating to a farm in the north, so watch out farmers! It's also a good time to invest! (+2 farming time).";
            if (tm.time > 30)
            {
                cs.spawnCrow(3);
            }
        }
        else if (rng == 4) //cloudy
        {
            weatherBonus = -1;
            weather.text += "It's going to be cloudy folks! (+1 farming time).";
        }
        else if (rng == 5) //drizzle
        {
            weatherBonus = 2;
            weather.text += "Looks like we're going to have a soft drizzle! (-2 farming time).";
        }
    }

    public void competitorChange()
    {
        int rng = Random.Range(-10, 11);
        competitor = rng;
        if (rng > 0)
        {
            comp.text = "" + mo.nameOfPlayer + " has some competition! Their competitor has a reputation of " + competitor + ".  ";
        }
        else
        {
            comp.text = "" + mo.nameOfPlayer + " seems to be in the clear! Their competitor has a reputation of " + competitor + ".  ";
        }
    }
    public void tryIcon()
    {
        if (tm.getTime() % timeRotation == 0 && !dbTime)
        {
            changeNewspaperRotation();
            button.GetComponent<Image>().sprite = Icon;
            dbTime = true;
        }
        else if(tm.getTime() % timeRotation != 0)
        {
            dbTime = false;
        }
    }
}
