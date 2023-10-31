using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UtilityController : MonoBehaviour
{
    public int sprinkerCount;
    public int scarecrowCount;
    public int robotCount;

    public Animator anim;
    public Transform player;
    public GameObject sprink;
    public GameObject sc;
    public GameObject rob;

    public Sprite spri;
    public Sprite sci;
    public Sprite robotSp;

    public Image img;

    public Text Name;
    public Text sprinkText;
    public Text scareText;
    public Text robotText;
    public Text Total;

    public bool open;

    public string change;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void openCLose()
    {
        if(!open)
        {
            anim.Play("UtilityUp");
            open = true;
        }
        else
        {
            open = false;
            anim.Play("UtilityDown");
        }
    }
    // Update is called once per frame
    void Update()
    {
        Name.text = change;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(change == "Sprinkler" && sprinkerCount >= 1)
            {
                sprinkerCount--;
                Instantiate(sprink, player.position, Quaternion.identity);
            }
            if (change == "Scarecrow" && scarecrowCount >= 1)
            {
                scarecrowCount--;
                Instantiate(sc, player.position, Quaternion.identity);
            }
            if (change == "Robot" && robotCount >= 1)
            {
                robotCount--;
                Instantiate(rob, player.position, Quaternion.identity);
            }
        }

        if(change == "Sprinkler")
        {
            Total.text = "Utility: " + sprinkerCount;
        }
        if (change == "Scarecrow")
        {
            Total.text = "Utility: " + scarecrowCount;
        }
        if (change == "Robot")
        {
            Total.text = "Utility: " + robotCount;
        }

        sprinkText.text = "Total: " + sprinkerCount;
        scareText.text = "Total: " + scarecrowCount;
        robotText.text = "Total: " + robotCount;

    }

    public void changeSprinkler()
    {
        img.sprite = spri;
        change = "Sprinkler";
    }

    public void changeScarecrow()
    {
        img.sprite = sci;
        change = "Scarecrow";
    }

    public void changeRobot()
    {
        img.sprite = robotSp;
        change = "Robot";
    }
}
