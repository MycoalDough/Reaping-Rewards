using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoboticWorkerAI : MonoBehaviour
{
    [Header("In Game Variables")]
    public bool poh = true; //plant = true, harvest = false
    public GlobalVariables gv;
    public GameObject tile;
    public Inventory inv;
    public bool isInTask;
    public float moveSpeed;
    public float timer;
    public float maxTimer;
    public GameObject soilParent;
    public NewspaperSystem ns;
    public Transform target;
    public TimeManager tm;
    public int daysRunning = 0;
    public int costPer25Days = 30;
    public bool timeDB = false;
    Coroutine runningC = null;
    private float phTime;
    private float phMaxTime = 3;
    public int level;
    public bool isActive = true;
    public MovementOverrides mo;
    //-1 = maxTimer = null ($400)
    //0 = maxTimer = 10 secs between moves ($10 every 25 days)
    //1 = maxTimer = 5 secs ($15 every 25)
    //2 = maxTimer = 3 secs ($20 every 25) isSmart = true
    public bool isSmart; //will 1) farm the highest paying crop and 2) plant the highest crop and will plant at the clostest tile
    [Header("Canvas")]
    public GameObject canvas;
    public string[] names;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI currentPriorityText;
    public TextMeshProUGUI costPer25;
    public TextMeshProUGUI lvlCost;
    public TextMeshProUGUI deActivate;
    public TextMeshProUGUI changePriority;
    private Vector3 prevPos;
    public bool nameOverride;
    public bool detectedTimeChange = false;
    // Start is called before the first frame update-
    void Start()
    {
        inv = GameObject.FindObjectOfType<Inventory>().GetComponent<Inventory>();
        soilParent = GameObject.Find("SoilParent");
        ns = GameObject.FindObjectOfType<NewspaperSystem>().GetComponent<NewspaperSystem>();
        gv = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();
        tm = GameObject.FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        mo = GameObject.FindObjectOfType<MovementOverrides>().GetComponent<MovementOverrides>();
        if (!nameOverride) { nameText.text = "Robot: " + names[Random.Range(0, names.Length)]; }
        StartCoroutine(changeSprite());
        //startPlant();
    }

    IEnumerator changeSprite()
    {
        while(true)
        {
            prevPos = transform.position;
            yield return new WaitForEndOfFrame();
            Vector3 current = transform.position - prevPos;
            if(current.x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = transform;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            StopAllCoroutines();
        }
        if(tm.timeChanged && isActive && !mo.isActive && !detectedTimeChange)
        {
            daysRunning++;
            detectedTimeChange = true;
        }
        else if(!tm.timeChanged)
        {
            if (detectedTimeChange)
            {
                detectedTimeChange = false;
            }
        }
        if (daysRunning >= 25)
        {
            gv.money -= costPer25Days;
            daysRunning = 0;
        }

        if (mo.isActive)
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = 0.5f;
        }

        if (!isInTask && target == null && isActive)
        {
            isInTask = true;
            if(poh && (inv.seeds[0].numberOfSeeds != 0 || inv.seeds[1].numberOfSeeds != 0 || inv.seeds[2].numberOfSeeds != 0))
            {
                startPlant();
                return;
            }
            else
            {
                StartCoroutine(timerReset());
            }

            if(!poh)
            {
                List<Transform> all_tiles = new List<Transform>();

                foreach (Transform child in soilParent.transform)
                {
                    if (child.gameObject.GetComponent<Soil>().hasPlant && child.gameObject.GetComponent<Soil>().plant.gameObject.GetComponent<Seed>().canHarvest && !child.gameObject.GetComponent<Soil>().robotWorker)
                    {
                        all_tiles.Add(child);
                    }

                }
                if(all_tiles.Count == 0)
                {
                    StartCoroutine(timerReset());
                    return;
                }
                StartCoroutine(harvest());
                return;
            }
            StartCoroutine(timerReset());
        }
    }

    IEnumerator plant()
    {
        StopCoroutine(timerReset());
        int val = 0;
        Transform[] all_tiles = new Transform[soilParent.transform.childCount];

        foreach (Transform child in soilParent.transform)
        {
            all_tiles[val] = child.transform;
            val++;
        }

        if (level != 3)
        {
            target = all_tiles[Random.Range(0, soilParent.transform.childCount - 1)];
            while (target.GetComponent<Soil>().hasPlant)
            {
                target = all_tiles[Random.Range(0, soilParent.transform.childCount - 1)];

                yield return null;
            }

            Debug.Log("Test Case #1: Tile");
            while (Vector2.Distance(transform.position, target.transform.position) > 0.01)
            {
                transform.position = Vector2.Lerp(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
            target.GetComponent<Soil>().robotWorker = true;
            Debug.Log("Test Case #2: Move");

            int randomPlant = Random.Range(0, 3);
            while (inv.seeds[randomPlant].numberOfSeeds == 0) //maxtamp. wrong
            {
                randomPlant = Random.Range(0, 3);
                yield return null;
            }

            Debug.Log("Test Case #3: Seed");


            while (phMaxTime > phTime)
            {
                phTime += Time.deltaTime;
                yield return null;
            }
            Debug.Log("Test Case #4: Timer");

            phTime = 0;
            if(!target.GetComponent<Soil>().hasPlant)
            {
                GameObject plant = Instantiate(inv.seeds[randomPlant].seedPrefab, transform.position, Quaternion.identity);
                Soil mySoil = target.GetComponent<Soil>();
                plant.GetComponent<Seed>().mySoil = mySoil;
                mySoil.hasPlant = true;
                mySoil.plant = plant;
                inv.seeds[randomPlant].numberOfSeeds--;
                target.GetComponent<Soil>().robotWorker = false;
                target = null;
            }

            yield return null;

        }
        else
        {
            target = getClosestOfObjects(all_tiles);
            target.GetComponent<Soil>().robotWorker = true;

            while (Vector2.Distance(transform.position, target.transform.position) > 0.5)
            {
                transform.position = Vector2.Lerp(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            int randomPlant = ns.cPlant;
            while (inv.seeds[randomPlant].numberOfSeeds == 0)
            {
                randomPlant = Random.Range(0, 3);
                yield return null;
            }
            if (!target.GetComponent<Soil>().hasPlant)
            {
                GameObject plant = Instantiate(inv.seeds[randomPlant].seedPrefab, transform.position, Quaternion.identity);
                Soil mySoil = target.GetComponent<Soil>();
                mySoil.hasPlant = true;
                plant.GetComponent<Seed>().mySoil = mySoil;
                mySoil.plant = plant;
                inv.seeds[randomPlant].numberOfSeeds--;
                target.GetComponent<Soil>().robotWorker = false;
                target = null;
            }


            yield return null;
        }

            StartCoroutine(timerReset());
    }

    public void startPlant()
    {
        runningC = StartCoroutine(plant());
    }

    IEnumerator timerReset()
    {
        yield return new WaitForSeconds(maxTimer);
        isInTask = false;
    }

    IEnumerator harvest()
    {
        if (!target)
        {
            StopCoroutine(timerReset());
            List<Transform> all_tiles = new List<Transform>();

            foreach (Transform child in soilParent.transform)
            {
                if (child.gameObject.GetComponent<Soil>().hasPlant && child.gameObject.GetComponent<Soil>().plant.gameObject.GetComponent<Seed>().canHarvest && !child.gameObject.GetComponent<Soil>().robotWorker)
                {
                    all_tiles.Add(child);
                }

            }
            Transform target = null;

            if (level != 3)
            {
                target = all_tiles[Random.Range(0, all_tiles.Count - 1)];
                while (!target.gameObject.GetComponent<Soil>().hasPlant && (!target.GetComponent<Soil>().plant.GetComponent<Seed>().canHarvest || target.GetComponent<Soil>().plant.GetComponent<Seed>().dead) && target.GetComponent<Soil>().robotWorker)
                {
                    target = all_tiles[Random.Range(0, soilParent.transform.childCount - 1)];
                    yield return null;
                }

            }
            else
            {
                target = getClosestOfBestPlant(all_tiles);
                while (!target.gameObject.GetComponent<Soil>().hasPlant && !target.GetComponent<Soil>().plant.GetComponent<Seed>().canHarvest && target.GetComponent<Soil>().robotWorker)
                {
                    target = all_tiles[Random.Range(0, soilParent.transform.childCount - 1)];
                    yield return null;
                }

            }
            target.GetComponent<Soil>().robotWorker = true;

            while (Vector2.Distance(transform.position, target.transform.position) > 0.01)
            {
                transform.position = Vector2.Lerp(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                yield return null;
            }



            while (phMaxTime > phTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            phTime = 0;
            isInTask = false;
            if (target.GetComponent<Soil>().hasPlant)
            {
                target.GetComponent<Soil>().robotWorker = false;
                if (target.GetComponent<Soil>().plant.GetComponent<Seed>().dead)
                {
                    target.GetComponent<Soil>().plant.GetComponent<Seed>().harvestDead();
                }
                else
                {
                    target.GetComponent<Soil>().plant.GetComponent<Seed>().Harvest();
                }
                target = null;
            }
            StartCoroutine(timerReset());
        }
        else
        {
            StartCoroutine(timerReset());
            yield return null;
        }
    }

    Transform getClosestOfObjects(Transform[] objects)
    {
        Transform tMin = objects[0];
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in objects)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist && !tMin.gameObject.GetComponent<Soil>().hasPlant)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    Transform getClosestOfBestPlant(List<Transform> objects)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in objects)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist && tMin.GetComponent<Soil>().plant.GetComponent<Seed>().canHarvest && tMin.GetComponent<Soil>().plant.GetComponent<Seed>().crop == ns.chosenPlant && !tMin.GetComponent<Soil>().robotWorker)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }


    private void OnMouseDown()
    {
        mo.botOpen = true;
        canvas.SetActive(true);
    }
    public void exit()
    {
        mo.botOpen = false;
        canvas.SetActive(false);
    }

    public void levelUp()
    {
        if(level == 1 && gv.money >= 100)
        {
            level++;
            levelText.text = "LEVEL: 2";
            gv.money -= 100;
            lvlCost.text = "Cost: $300";
            costPer25.text = "Cost: $40 per 25 days";
            costPer25Days = 40;
        }
        else if (level == 2 && gv.money >= 300)
        {
            level++;
            gv.money -= 300;
            lvlCost.text = "Cost: MAX";
            levelText.text = "LEVEL: MAX";
            costPer25.text = "Cost: $50 per 25 days";
            costPer25Days = 50;
        }
    }

    public void changeActiveState()
    {
        if(isActive)
        {
            isActive = false;
            deActivate.text = "ACTIVATE";
        }
        else
        {
            isActive = true;
            deActivate.text = "DEACTIVATE";
            StartCoroutine(timerReset());
            isInTask = false;
            target = null;
        }
    }

    public void changePriorities()
    {
        if (poh)
        {
            poh = false;
            changePriority.text = "PLANT";
            currentPriorityText.text = "Priority: HARVEST";
        }
        else
        {
            poh = true;
            changePriority.text = "HARVEST";
            currentPriorityText.text = "Priority: PLANT";

        }
    }
}
