using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SprinklerManager : MonoBehaviour
{
    public int uses = 20;
    public bool DB = false;
    public int config = 1;

    public TimeManager tm;

    public TextMeshPro ui;
    public GameObject screen;

    public bool playerIsOn;
    public GameObject water;
    //1 = every 8 days
    //2 = every 13 days
    //3 = every 20 days
    //4 = click to work

    private void OnMouseEnter()
    {
        screen.SetActive(true);
    }

    private void OnMouseExit()
    {
        screen.SetActive(false);
    }
    public int timer;

    public void Start()
    {
        AchievementCanvas ac;
        ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
        ac.Add("It's Raining", 1);
        tm = GameObject.FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
    }
    public void OnMouseDown()
    {
        if (config == 4)
        {
            StartCoroutine(spawnWater());
            uses--;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        playerIsOn = true;
    }



    void OnTriggerExit2D(Collider2D col)
    {
        playerIsOn = false;
    }
    public void changeTimer()
    {
        if (config == 1)
        {
            timer = 8;
        }
        else if (config == 2)
        {
            timer = 13;
        }
        else if (config == 3)
        {
            timer = 20;
        }
    }

    public void Update()
    {
        ui.text = "Uses: " + uses + "\n\n";
        if (playerIsOn)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                config++;
                if (config > 4)
                {
                    config = 1;
                    changeTimer();
                }
            }
        }
        changeDB();
        if (config == 1)
        {
            configOne();
            ui.text += "Deploys Every: 8 Days";
        }
        else if (config == 2)
        {
            configTwo();
            ui.text += "Deploys Every: 13 Days";
        }
        else if (config == 3)
        {
            configThree();
            ui.text += "Deploys Every: 20 Days";
        }
        else
        {
            ui.text += "Deploys Every: Mouse Click";
        }
        if (uses <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void configOne()
    {
        if (!DB && tm.getTime() % 8 == 0)
        {
            //shoot water
            DB = true;
            uses--;
            StartCoroutine(spawnWater());
        }
    }

    public void configTwo()
    {
        if (!DB && tm.getTime() % 13 == 0)
        {
            //shoot water
            DB = true;
            uses--;
            StartCoroutine(spawnWater());
        }
    }

    public void configThree()
    {
        if (!DB && tm.getTime() % 20 == 0)
        {
            //shoot water
            DB = true;
            uses--;
            StartCoroutine(spawnWater());
        }
    }

    public void changeDB()
    {
        if (tm.getTime() % 8 != 0 && tm.getTime() % 13 != 0 && tm.getTime() % 20 != 0)
        {
            DB = false;
        }
    }

    IEnumerator spawnWater()
    {
        GameObject clone = Instantiate(water, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(clone);

    }
}
