using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool isOut;
    public Transform tpOut;
    public Transform tpIn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (isOut)
            {
                collision.gameObject.transform.position = tpIn.position;
            }
            else
            {
                collision.gameObject.transform.position = tpOut.position;
            }
        }
    }
}
