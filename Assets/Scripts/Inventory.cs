using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Config")]
    public int selected = 0;

    public Image[] allImages;

    public Text cornSeedAmount;
    public Text cornPlantAmount;

    public Text wheatSeedAmount;
    public Text wheatPlantAmount;

    public Text potatoSeedAmount;
    public Text potatoPlantAmount;
    public List<InventoryCrop> seeds = new List<InventoryCrop>();

    public void changeAmounts() //changes GUI
    {
        cornSeedAmount.text = "Total Seeds: " + seeds[0].numberOfSeeds;
        cornPlantAmount.text = "Total Plants: " + seeds[0].numberOfPlants;

        wheatSeedAmount.text = "Total Seeds: " + seeds[1].numberOfSeeds;
        wheatPlantAmount.text = "Total Plants: " + seeds[1].numberOfPlants;

        potatoSeedAmount.text = "Total Seeds: " + seeds[2].numberOfSeeds;
        potatoPlantAmount.text = "Total Plants: " + seeds[2].numberOfPlants;

    }



    public void setSelected(int i)
    {
        selected = i;
        changeColor(i);
    }
    private void Start()
    {
        setSelected(0);
    }
    public void changeColor(int saved)
    {
        for (int i = 0; i < allImages.Length; i++)
        {
            allImages[i].color = new Color32(226, 226, 226, 225);
        }
        //Debug.Log(saved);
        allImages[saved].color = new Color32(226, 160, 160, 225);

    }

    public void Update()
    {
        changeAmounts();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            setSelected(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setSelected(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            setSelected(2);
        }
    }

}
