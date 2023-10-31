using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Config")]
    public Inventory inv;
    [Header("Movement Settings")]
    public float playerSpeed = 8; //speed player moves
    public bool canMove = true;
    public Transform cam;
    private MovementOverrides mo;

    public bool canDoTasks;

    public Animator anim;

    public Inventory inventory;



    [Header("Planting")]
    public GameObject currentSoil;
    public GameObject cornSeed;

    private void Start()
    {
        mo = gameObject.GetComponent<MovementOverrides>();
    }

    void Update()
    {
        if(canMove)
        {
            MoveForward(); // Player Movement 
            if(Input.GetKeyDown(KeyCode.Space) && canDoTasks)
            {
                StartCoroutine(plantSeed());
            }
        }

        if(canDoTasks)
        {
            tag = "Player";
        }
        else
        {
            tag = "PlayerDialougeOnly";
        }
        cameraMovement();
    }

    public void cameraMovement()
    {
        cam.position = new Vector3(transform.position.x, transform.position.y, -10);
    }


    public void changeSoil(GameObject soil)
    {
        currentSoil = soil;
    }

    public IEnumerator plantSeed()
    {
        if (currentSoil && currentSoil.GetComponent<Soil>() && currentSoil.GetComponent<Soil>().robotWorker == false)
        {
            if (currentSoil.GetComponent<Soil>().hasPlant == false && currentSoil.GetComponent<Soil>().plant == false)
            {
                mo.isPlanting = true;
                anim.Play("PlayerPlant");
                if (inventory.selected == 0 && inventory.seeds[0].numberOfSeeds > 0)
                {
                    yield return new WaitForSeconds(2); //time to plant
                    inventory.seeds[0].numberOfSeeds--;
                    GameObject seed = Instantiate(inventory.seeds[0].seedPrefab, currentSoil.transform.position, Quaternion.identity); //need to change in the future to match with array
                    seed.GetComponent<Seed>().mySoil = currentSoil.GetComponent<Soil>();
                    currentSoil.GetComponent<Soil>().hasPlant = true;
                    currentSoil.GetComponent<Soil>().plant = seed;
                }
                else if (inventory.selected == 1 && inventory.seeds[1].numberOfSeeds > 0)
                {
                    yield return new WaitForSeconds(2); //time to plant

                    inventory.seeds[1].numberOfSeeds--;
                    GameObject seed = Instantiate(inventory.seeds[1].seedPrefab, currentSoil.transform.position, Quaternion.identity); //need to change in the future to match with array
                    seed.GetComponent<Seed>().mySoil = currentSoil.GetComponent<Soil>();
                    currentSoil.GetComponent<Soil>().hasPlant = true;
                    currentSoil.GetComponent<Soil>().plant = seed;
                }
                else if (inventory.selected == 2 && inventory.seeds[2].numberOfSeeds > 0)
                {
                    yield return new WaitForSeconds(2); //time to plant

                    inventory.seeds[2].numberOfSeeds--;
                    GameObject seed = Instantiate(inventory.seeds[2].seedPrefab, currentSoil.transform.position, Quaternion.identity); //need to change in the future to match with array
                    seed.GetComponent<Seed>().mySoil = currentSoil.GetComponent<Soil>();
                    currentSoil.GetComponent<Soil>().hasPlant = true;
                    currentSoil.GetComponent<Soil>().plant = seed;
                }
                anim.Play("PlayerIdle");
                mo.isPlanting = false;
            }
            else
            {
                Seed s = currentSoil.GetComponent<Soil>().plant.GetComponent<Seed>();
                if (s && !s.hasWater && !s.dead)
                {
                    anim.Play("PlayerWater");
                    Seed currentPlant = s;
                    mo.isPlanting = true;
                    yield return new WaitForSeconds(2); //time to water
                    currentPlant.watered();
                    mo.isPlanting = false;
                }
                else if (s && s.dead)
                {
                    anim.Play("PlayerHarvest");
                    mo.isPlanting = true;
                    yield return new WaitForSeconds(2); //time to water
                    Seed currentPlant = s;
                    currentPlant.harvestDead();
                    mo.isPlanting = false;
                }
                else if (s && s.canHarvest && !s.dead)
                {
                    anim.Play("PlayerHarvest");
                    mo.isPlanting = true;
                    yield return new WaitForSeconds(2); //time to water
                    Seed currentPlant = s;
                    currentPlant.Harvest();
                    mo.isPlanting = false;
                }
                anim.Play("PlayerIdle");
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == 10)//soil layer
        {
            currentSoil = collision.gameObject;
        }
        else
        {
            currentSoil = null;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)//soil layer
        {
            currentSoil = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == 10)//soil layer
        {
            currentSoil = collision.gameObject;
        }
        else
        {
            currentSoil = null;
        }
    }

    void MoveForward()
    {
        if(canMove)
        {
            //anim.speed = 1;
            if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && !mo.isPlanting)
            {
                anim.speed = 0;
            }
            else if(mo.dialougeOpen)
            {
                anim.speed = 0;
            }
            else
            {
                anim.speed = 1;
            }
            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))//Press up arrow key to move forward on the Y AXIS
            {
                transform.Translate(0, playerSpeed * Time.deltaTime, 0);
 //               anim.Play("PlayerUp");
            }
            if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))//Press up arrow key to move forward on the Y AXIS
            {
                transform.Translate(0, -playerSpeed * Time.deltaTime, 0);
                //anim.Play("PlayerDown");

            }
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))//Press up arrow key to move forward on the Y AXIS
            {
                transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
               // anim.Play("PlayerLeft");

            }
            if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))//Press up arrow key to move forward on the Y AXIS
            {
                transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
                //anim.Play("PlayerRight");
            }


            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))//Press up arrow key to move forward on the Y AXIS
            {
                anim.Play("PlayerLeft");
            }
            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))//Press up arrow key to move forward on the Y AXIS
            {
                anim.Play("PlayerRight");
            }
            else if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))//Press up arrow key to move forward on the Y AXIS
            {
                anim.Play("PlayerUp");
            }
            else if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))//Press up arrow key to move forward on the Y AXIS
            {
                anim.Play("PlayerDown");
            }


        }
        else
        {
            
        }
        


    }
}
