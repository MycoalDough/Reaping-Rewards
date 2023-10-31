using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBanter : MonoBehaviour
{
    //public bool useCanvas; //if yes, you use a canvas text. if not, you use a 3D text.

    public Text text;

    public string[] dialouges;

    public bool randomTime; //if yes, it displays a new random dialoge after an amount of time

    public float maxTime, currentTime;
    // Start is called before the first frame update
    void Start()
    {
        text.text = dialouges[Random.Range(0, dialouges.Length)];
    }

    private void Awake()
    {
        text.text = dialouges[Random.Range(0, dialouges.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
