using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
public class PopupText : MonoBehaviour
{
    public string setText;
    public AudioSource plantSFX;
    public AudioSource waterSFX;
    public AudioSource harvestSFX;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMesh>().text = setText;
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(4,6), ForceMode2D.Impulse);
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(-3, 4), ForceMode2D.Impulse);
        StartCoroutine(delete());

        if(setText.Contains("Planted"))
        {
            plantSFX.Play();
        }
        else if (setText.Contains("Watered"))
        {
            waterSFX.Play();
        }
        else
        {
            harvestSFX.Play();
        }
    }

    IEnumerator delete()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
