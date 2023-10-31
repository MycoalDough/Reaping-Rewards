using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class AchievementList
{
    public string name;
    public string description;
    public float current;
    public float max;
    public bool done;

    public Sprite sprite;

    public AchievementList(string Name, string Description, float Current, float Max, bool Done, Sprite Sprite)
    {
        name = Name;
        description = Description;
        current = Current;
        max = Max;
        done = Done;
        sprite = Sprite;

    }
}
