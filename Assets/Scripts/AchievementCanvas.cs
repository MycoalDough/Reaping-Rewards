using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementCanvas : MonoBehaviour
{

    public string current;

    public Image img;

    public MovementOverrides tm;
    public GameObject dictionary;
    public GameObject canvas;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI stats;
    [Header("animation")]
    public Animator anim;
    public TextMeshProUGUI nameTextAnim;
    public TextMeshProUGUI descTextAnim;
    public Image image;

    public List<AchievementList> achievements = new List<AchievementList>();
    public void Change(string newNumber)
    {
        for (int i = 0; i < achievements.Count; i++)
        {
            if (achievements[i].name == newNumber)
            {
                
                current = newNumber;
                img.sprite = achievements[i].sprite;
                nameText.text = achievements[i].name;
                descriptionText.text = achievements[i].description;
                if(achievements[i].done)
                {
                    stats.text = "Achievement Finished!";
                }
                else
                {
                    stats.text = achievements[i].current + "/" + achievements[i].max;
                }
            }
        }
    }

    public void open()
    {
        canvas.SetActive(true);
        tm.systemOpen = true;
        dictionary.SetActive(false);
    }

    public void exit()
    {
        canvas.SetActive(false);
        tm.systemOpen = false;
        dictionary.SetActive(true);


    }
    public void Update()
    {
        int finished = 0;
        for(int i = 0; i < achievements.Count; i++)
        {
            if(achievements[i].current >= achievements[i].max && !achievements[i].done)
            {
                finished++;
                achievements[i].done = true;
                anim.StopPlayback();
                anim.Play("NoneState", -1, 0f); // The string "NoneState" is just a placeholder
                anim.Play("Achievement", 0, 0f); 
                nameTextAnim.text = achievements[i].name;
                descTextAnim.text = achievements[i].description;
                image.sprite = achievements[i].sprite;
            }

            achievements[i].current = Mathf.Round(achievements[i].current * 100f) / 100f;
        }

        achievements[17].current = finished;
    }
    public void Add(string name, float toAdd)
    {
        for(int i = 0; i < achievements.Count; i++)
        {
            if(achievements[i].name == name)
            {
                achievements[i].current += toAdd;
            }
        }

    }

    private void Start()
    {
        Change("Bruh");
    }
}
