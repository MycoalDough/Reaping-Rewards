using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [Header("Config")]
    public int growthTime;
    public GameObject[] stages;
    public string crop;


    [Header("Settings")]
    public bool hasWater;
    public bool canHarvest;
    public int numOfDaysPlanted;
    public Soil mySoil;
    private TimeManager tm;
    private GlobalVariables gv;
    private NewspaperSystem ns;
    public Inventory inv;
    private int maxDaysNotWatered;
    public bool dead = false;
    private bool canChangePhase = true;

    public GameObject popup;

    public void Start()
    {
        if(!tm)
        {
            tm = GameObject.FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
            ns = GameObject.FindObjectOfType<NewspaperSystem>().GetComponent<NewspaperSystem>();
            gv = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();
            inv = GameObject.FindObjectOfType<Inventory>().GetComponent<Inventory>();

        }
        maxDaysNotWatered = tm.getTime() + Random.Range(5,16); //5-15 days til wilted if not watered
        mySoil.hasPlant = true;

        GameObject t = Instantiate(popup, gameObject.transform.position, Quaternion.identity);


        if (crop == "Corn")
        {
            t.GetComponent<PopupText>().setText = "Corn planted";
        }
        else if (crop == "Wheat")
        {
            t.GetComponent<PopupText>().setText = "Wheat planted";
        }
        else if (crop == "Potato")
        {
            t.GetComponent<PopupText>().setText = "Potato planted";
        }
    }

    public void Harvest()
    {
        mySoil.hasPlant = false;
        mySoil.plant = null;
        GameObject t = Instantiate(popup, gameObject.transform.position, Quaternion.identity);

        AchievementCanvas ac;
        ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
        if (crop == "Corn")
        {
            t.GetComponent<PopupText>().setText = "+1 Corn Plant";
            inv.seeds[0].numberOfPlants++;
            ac.Add("Corny", 1);

        }
        else if(crop == "Wheat")
        {
            t.GetComponent<PopupText>().setText = "+1 Wheat Plant";
            inv.seeds[1].numberOfPlants++;
            ac.Add("Can't Beat Wheat", 1);

        }
        else if (crop == "Potato")
        {
            t.GetComponent<PopupText>().setText = "+1 Potato Plant";
            inv.seeds[2].numberOfPlants++;
            ac.Add("Spudastic", 1);

        }
        Destroy(gameObject);

    }

    public void Update()
    {
        checkIfDead();
    }

    public void checkIfDead()
    {
        if(!hasWater && tm.getTime() >= maxDaysNotWatered)
        {
            setAllSprites(stages.Length - 1);
            hasWater = false;
            dead = true;
            StopCoroutine(growPlant());
        }
    }

    public void harvestDead()
    {
        mySoil.hasPlant = false;
        mySoil.plant = null;
        GameObject t = Instantiate(popup, gameObject.transform.position, Quaternion.identity);
        t.GetComponent<PopupText>().setText = "dead crop :(";
        Destroy(gameObject);
        AchievementCanvas ac;
        ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
        ac.Add("Bruh", 1);
    }

    public void crowAttack()
    {
        mySoil.hasPlant = false;
        mySoil.plant = null;
        GameObject t = Instantiate(popup, gameObject.transform.position, Quaternion.identity);
        t.GetComponent<PopupText>().setText = "Crow!";
        Destroy(gameObject);
    }


    public void watered()
    {
        hasWater = true;
        numOfDaysPlanted = tm.getTime();
        StartCoroutine(growPlant());
        GameObject t = Instantiate(popup, gameObject.transform.position, Quaternion.identity);
        t.GetComponent<PopupText>().setText = "Watered Plant";

    }




    public IEnumerator growPlant()
    {
        int currentSprite = 1;
        if (hasWater)
        {
            setAllSprites(currentSprite);
            for (int i = 2; i < stages.Length - 1; i++)
            {
                if(!dead)
                {
                    yield return new WaitUntil(() => tm.getTime() >= numOfDaysPlanted - ns.weatherBonus);
                    currentSprite++;
                    setAllSprites(currentSprite);
                    numOfDaysPlanted = numOfDaysPlanted + growthTime;
                }
                else
                {
                    checkIfDead();
                }

            }
        }
        canHarvest = true;
    }

    public void setAllSprites(int Override)
    {
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].SetActive(false);
        }

        stages[Override].SetActive(true);
    }

        
}
