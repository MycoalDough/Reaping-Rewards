using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DictionarySystem : MonoBehaviour
{
    public GameObject define;
    public GameObject achievement;
    public Transform container;
    public Color uncheckedColor;
    public Color checkedColor;
    public TextMeshProUGUI term;
    public TextMeshProUGUI description;
    public ArrayList listOfWords = new ArrayList();
    public GlobalVariables gv;
    public MovementOverrides mo;
    public GameObject parent;
    public Image button;
    public Sprite inc;
    public Sprite seen;
    public void Awake()
    {
        DictionarySystem ds = GameObject.FindObjectOfType<DictionarySystem>().GetComponent<DictionarySystem>();
        ds.newDefinition("Mortgage (!)", "A mortgage is like a special loan for buying a house. It's when the bank lends you money to get a house, and you pay them back bit by bit over a long time, like paying back a big friend who helped you get a special toy. IF YOU DON'T PAY IT OFF, the Bank can take your property (YOU LOSE THE GAME!)");
        ds.newDefinition("Dictionary", "Welcome to the Dictionary! Here, you can review your new terms you've found throughout the game! By viewing a term, you strengthen your knowledge of finances, as well as increasing your chance at winning this game! (and you get +10 dollars !)");
    }
    public void newDefinition(string l_term, string l_def)
    {
        if(!listOfWords.Contains(l_term))
        {
            button.sprite = inc;
            string local = $"{l_term}@{l_def}@1";
            GameObject newDef = Instantiate(define, container.position, Quaternion.identity, container);
            //newDef.GetComponent<Button>().onClick.AddListener(() => checkDef(local));
            newDef.GetComponent<Button>().onClick.AddListener(call: delegate { checkDef(newDef.GetComponent<Button>(), local); });
            newDef.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>().text = l_term;
            listOfWords.Add(l_term);
        }
    }

    public void checkDef(Button button, string parameter)
    {
        string[] temp = parameter.Split('@');
        term.text = temp[0];
        description.text = temp[1];
        if(temp[2] == "1")
        {
            string newDef = temp[0] + "@" + temp[1] + "@0";
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(call: delegate { checkDef(button, newDef); });
            button.gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
            gv.money += 10;
        }
    }
    public void open()
    {
        mo.systemOpen = true;
        parent.SetActive(true);
        achievement.SetActive(false);
        button.sprite = seen;
    }

    public void close()
    {
        mo.systemOpen = false;
        parent.SetActive(false);
        achievement.SetActive(true);
    }
}
