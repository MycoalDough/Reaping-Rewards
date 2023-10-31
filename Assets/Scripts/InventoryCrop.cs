using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InventoryCrop
{
    public int numberOfSeeds;
    public int numberOfPlants;
    public GameObject seedPrefab;
    public string name;

    public InventoryCrop(int NumberOfSeeds, GameObject SeedPrefab, string Name, int NumberOfPlants)
    {
        seedPrefab = SeedPrefab;
        numberOfSeeds = NumberOfSeeds;
        name = Name;
        numberOfPlants = NumberOfPlants;

    }
}
