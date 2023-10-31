using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialougeSystem : MonoBehaviour
{

    public TextMeshProUGUI personName;
    public TextMeshProUGUI talking;

    public YarnController yc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chat(string s)
    {
        string[] temp = s.Split('/');
        personName.text = temp[0];
        talking.text = temp[1];

    }
}
