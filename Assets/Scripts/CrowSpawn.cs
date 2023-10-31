using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSpawn : MonoBehaviour
{

    public GameObject crow;

    public float time;
    public float maxTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void spawnCrow(int numberToSpawn)
    {
        for(int i = 0; i < numberToSpawn; i++)
        {
            Instantiate(crow, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;

        if(time > maxTime)
        {
            time = 0;
            spawnCrow(1);
        }
    }
}
