using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOverrides : MonoBehaviour
{
    public bool gardenerOpen = false;
    public bool isPlanting = false;
    public bool rngOpen = false;
    public bool newspaperOpen = false;
    public bool sellOpen = false;
    public bool bankOpen = false;
    public bool computerOpen = false;
    public bool dialougeOpen = false;
    public bool systemOpen = false;
    public bool botOpen = false;
    public string nameOfPlayer;
    public TimeManager tm;

    public bool isActive;

    public void Update()
    {
        //uh, spaghetti code who? (me)
        //Movement Overrides
        if(!gardenerOpen && !isPlanting && !rngOpen && !newspaperOpen && !sellOpen && !bankOpen && !computerOpen && !dialougeOpen && !systemOpen && !botOpen)
        {
            gameObject.GetComponent<PlayerController>().canMove = true;
        }
        else
        {
            gameObject.GetComponent<PlayerController>().canMove = false;
        }


        //Time Overrides
        if(!gardenerOpen && !rngOpen && !newspaperOpen && !sellOpen && !bankOpen && !computerOpen && !dialougeOpen && !systemOpen && !botOpen)
        {
            tm.timeScale = false;
            isActive = false;

        }
        else
        {
            tm.timeScale = true;
            isActive = true;

        }
    }
}
