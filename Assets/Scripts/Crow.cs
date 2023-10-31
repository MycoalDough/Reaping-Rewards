using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{

    [Header("Config")]
    public GameObject seed;
    public GameObject soil;
    public bool foundTarget;
    public bool shoo;


    [Header("Lerping")]
    public float timeStartedLerping;
    public float lerpTime;

    private Vector2 spawnPos;
    private Vector2 soilPos;
    private Vector2 leavePos;

    public bool atTarget = false;

    void Start()
    {
        transform.position = new Vector2(36.5f, Random.Range(21.14f, 30));
        spawnPos = transform.position;
    }

    void findTarget()
    {
        if(!foundTarget)
        {
            Seed[] allSeeds = FindObjectsOfType<Seed>();

            int rng = Random.Range(0, allSeeds.Length);

            if (rng != 0 && !allSeeds[rng].mySoil.gameObject.GetComponent<Soil>().hasCrow)
            {
                if(rng == allSeeds.Length + 1)
                {
                    rng = 0;
                }
                Debug.Log(rng);
                seed = allSeeds[rng].gameObject;
                soil = allSeeds[rng].mySoil.gameObject;
                soil.GetComponent<Soil>().hasCrow = true;
                soilPos = soil.transform.position;
                leavePos = new Vector2(soil.transform.position.x -24.64f, soil.transform.position.y + 11);
                foundTarget = true;
                timeStartedLerping = Time.time;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        findTarget();


        if (Vector2.Distance(transform.position, leavePos) < 0.1f)
        {
            soil.GetComponent<Soil>().hasCrow = false;
            Destroy(gameObject);
        }

        if (foundTarget)
        {

            if (seed && Vector2.Distance(transform.position, seed.transform.position) < 0.1f)
            {
                if(!atTarget)
                {
                    StartCoroutine(finish());
                    atTarget = true;
                }
            }
            else
            {
                transform.position = Lerp(transform.position, soilPos, timeStartedLerping, lerpTime);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Scarecrow" || collision.gameObject.layer == 12)
        {
            timeStartedLerping = Time.time;
            soilPos = leavePos;
            shoo = true;
            AchievementCanvas ac;
            ac = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
            ac.Add("Get Outta Here!", 1);

        }
    }

    IEnumerator finish()
    {
        yield return new WaitForSeconds(2);
        if (seed && Vector2.Distance(transform.position, seed.transform.position) < 0.1f)
        {
            soilPos = leavePos;
            timeStartedLerping = Time.time;
            transform.position = Lerp(transform.position, soilPos, timeStartedLerping, lerpTime);
            if (!shoo)
            {
                soil.GetComponent<Soil>().hasCrow = false;
                seed.GetComponent<Seed>().crowAttack();
            }
            else
            {
                transform.position = seed.transform.position + new Vector3(0.1f, 0.1f);
                Debug.Log("Leaving!");
                StopCoroutine(finish());
            }
        }
    }
    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerp, float lerpTime = 1)
    {
        float timeSinceStart = Time.time - timeStartedLerp;
        float percetnageComplete = timeSinceStart / lerpTime;
        var result = Vector3.Lerp(start, end, percetnageComplete);

        return result;
    }
}
