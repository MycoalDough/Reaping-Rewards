using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public float interval;

    [SerializeField] public int time;
    private bool debounce;

    public bool timeScale;
    public Text text;
    public bool timeChanged;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int getTime()
    {
        return time;
    }

    void Update()
    {
        if(!timeScale)
        {
            if (!debounce)
            {
                StartCoroutine(changeTime());
            }
        }

        setText();
    }

    public void setText()
    {
        text.text = "Time: " + time;
    }

    IEnumerator changeTime()
    {
        if(!debounce)
        {
            debounce = true;
            yield return new WaitForSeconds(interval);
            if(!timeScale)
            {
                time = time + 1;
                timeChanged = true;
                yield return new WaitForSeconds(0.01f);
                timeChanged = false;
            }
            debounce = false;
        }
    }

    IEnumerator timeChange()
    {
        yield return new WaitForEndOfFrame();
        timeChanged = false;
    }
}
