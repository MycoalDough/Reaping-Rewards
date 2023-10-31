using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scarecrow : MonoBehaviour
{

    public int uses;

    public GameObject parent;

    public void Start()
    {
        AchievementCanvas ac;
        ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
        ac.Add("Laziness", 1);
    }

    public TextMeshPro txt;

    private void OnMouseEnter()
    {
        txt.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        txt.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Crow")
        {
            uses--;
        }
    }

    private void Update()
    {
        txt.text = "Uses:" + uses;

        if(uses <= 0)
        {
            Destroy(parent);
        }
    }
}
