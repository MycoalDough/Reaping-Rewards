using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    public bool hasPlant;
    public GameObject plant;
    public GameObject soilStats;
    public bool fertilized;
    public bool robotWorker;
    public bool hasCrow;

    public TextMesh textMeshPlant;
    public TextMesh textMeshWater;
    public TextMesh textMeshHarvestable;
    public TextMesh textMeshTime;
    public TextMesh textMeshFertilize;




    public void OnMouseDown()
    {
        soilStats.SetActive(true);
    }

    private void OnMouseEnter()
    {
        soilStats.SetActive(true);
    }

    public void Update()
    {
        changeStat();
    }

    private void OnMouseExit()
    {
        soilStats.SetActive(false);
    }

    public void changeStat()
    {
        //change plant
        if(plant)
        {
            textMeshPlant.text = "Current Plant: " + plant.GetComponent<Seed>().crop;
        }
        else
        {
            textMeshPlant.text = "Current Plant: N/A";
        }

        //change water
        if (plant && plant.GetComponent<Seed>().hasWater)
        {
            textMeshWater.text = "Watered: True";
        }
        else if(plant && !plant.GetComponent<Seed>().hasWater)
        {
            textMeshWater.text = "Watered: False";
        }
        else
        {
            textMeshWater.text = "Watered: N/A";
        }

        //change harvest state
        if (plant && plant.GetComponent<Seed>().canHarvest)
        {
            textMeshHarvestable.text = "Harvestable: True";
        }
        else if (plant && !plant.GetComponent<Seed>().canHarvest)
        {
            textMeshHarvestable.text = "Harvestable: False";
        }
        else
        {
            textMeshHarvestable.text = "Harvestable: N/A";
        }


        ////change time
        //if (plant)
        //{
        //    textMeshTime.text = "Time til Ready: " + plant.GetComponent<Seed>();
        //}
        //else
        //{
        //    textMeshTime.text = "Harvestable: N/A";
        //}

        //change fert
        if (fertilized)
        {
            textMeshFertilize.text = "Fertilized: True";
        }
        else
        {
            textMeshFertilize.text = "Fertilized: False";

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SprinklerWater")
        {
            Debug.Log("what");
            if (plant && !plant.GetComponent<Seed>().hasWater && !plant.GetComponent<Seed>().dead && !plant.GetComponent<Seed>().canHarvest)
            {
                plant.GetComponent<Seed>().watered();
            }
        }
    }
}
