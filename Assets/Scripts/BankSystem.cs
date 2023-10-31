using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankSystem : MonoBehaviour
{
    public GlobalVariables gv;
    public MovementOverrides mo;
    public TimeManager tm;

    [Header("COD")]
    public int timeCOD; 
    public InputField CODMoneyField;
    public Image[] CODTimeField;
    public bool slot1open;
    public bool slot2open;
    public bool slot3open;
    public Vector3 slot1Pos;
    public Vector3 slot2Pos;
    public Vector3 slot3Pos;

    public Text bankAccountMoney;
    public Text interestRate;
    public GameObject COD;

    public RectTransform CODFrame;


    private bool timeDB;

    public AudioSource sfx;

    public float bankAmount;

    public GameObject bankCanvas;
    public GameObject CODparent;

    public InputField bankAccountMoneyField;

    public Vector3 codParentOrigin;
    public Vector3 codParentOpen;

    private void Start()
    {
        sfx = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mo.bankOpen = true;
            bankCanvas.SetActive(true);
            CODparent.GetComponent<RectTransform>().localPosition = codParentOpen;

            DictionarySystem ds = GameObject.FindObjectOfType<DictionarySystem>().GetComponent<DictionarySystem>();
            ds.newDefinition("Bank", "A bank is like a safe place for your money. You can save your money there, and they give you an account to keep track of it. When you need your money, you can take it out. It's like a special home for your money!");
            ds.newDefinition("Interest (Bank)", "Interest is like a little extra gift that the bank gives you when you keep your money with them. It's a bit like a thank-you present. So, if you have money saved in the bank, the bank will give you a bit more money over time, just because you left your money there. It's like a little bonus for being a good saver!");
            ds.newDefinition("Certificate of Deposit", "A Certificate of Deposit, or CD, is like a special savings plan at the bank. When you put your money in a CD, you promise not to touch it for a certain amount of time, like a few months or even a few years. In return, the bank gives you even more money than you put in when the time is up. It's like making a deal with the bank to save your money for a while, and they say, 'Thank you!' by giving you extra money when you're done saving.");

        }
    }

    public void setCODTime(int time)
    {
        timeCOD = time;
        setSpriteCOD(time);
    }

    public void buyCOD()
    {


        if (CODMoneyField.text.ToString() != null && int.Parse(CODMoneyField.text.ToString()) <= 0)
        {
            CODMoneyField.text = "";
            return;
        }


        Debug.Log(gv.money - int.Parse(CODMoneyField.text));
        if(timeCOD != 0 && gv.money - int.Parse(CODMoneyField.text) >= 0 && (slot1open || slot2open || slot3open))
        {
            sfx.Play();
            gv.money -= int.Parse(CODMoneyField.text);

            if (slot1open == true)
            {
                GameObject CODClone = Instantiate(COD, slot1Pos, Quaternion.identity, CODparent.transform);
                CertificateOfDeposit CD = CODClone.GetComponent<CertificateOfDeposit>();
                //CD.GetComponent<RectTransform>().position = slot1Pos;
                CD.money = int.Parse(CODMoneyField.text);
                CD.timeStart = timeCOD;
                CD.slot1 = true;
                slot1open = false;
            }
            else if (slot2open == true)
            {
                GameObject CODClone = Instantiate(COD, slot2Pos, Quaternion.identity, CODparent.transform);
                CertificateOfDeposit CD = CODClone.GetComponent<CertificateOfDeposit>();
                //CD.GetComponent<RectTransform>().position = slot2Pos;
                CD.money = int.Parse(CODMoneyField.text);
                CD.timeStart = timeCOD;
                CD.slot2 = true;
                slot2open = false;

            }
            else if (slot3open == true)
            {
                GameObject CODClone = Instantiate(COD, slot3Pos, Quaternion.identity, CODparent.transform);
                CertificateOfDeposit CD = CODClone.GetComponent<CertificateOfDeposit>();
                CD.money = int.Parse(CODMoneyField.text);
                //CD.GetComponent<RectTransform>().position = slot3Pos;
                CD.timeStart = timeCOD;
                CD.slot3 = true;
                slot3open = false;

            }


            CODMoneyField.text = "";

        }
    }

    public void setSpriteCOD(int time)
    {
        for (int i = 0; i < CODTimeField.Length; i++)
        {
            CODTimeField[i].color = new Color32(255, 255, 255, 255);
        }
        if (time == 1)
        {
            CODTimeField[0].color = new Color32(131, 255, 107, 255);
        }
        if (time == 3)
        {
            CODTimeField[1].color = new Color32(131, 255, 107, 255);
        }
        if (time == 5)
        {
            CODTimeField[2].color = new Color32(131, 255, 107, 255);
        }
        if (time == 8)
        {
            CODTimeField[3].color = new Color32(131, 255, 107, 255);
        }
    }

    public void Update()
    {
        bankAccountMoney.text = "Bank Account Funds: $" + bankAmount;
        bankAmount = Mathf.Round(bankAmount * 100f) / 100f;
        interestRate.text = "Interest Rate: " + gv.interestRate + "x";

        if (tm.getTime() % 52 == 0 && !timeDB && tm.getTime() != 0)
        {
            timeDB = true;
            bankAmount = bankAmount * gv.interestRate;
        }
        else if (tm.getTime() % 52 != 0)
        {
            timeDB = false;
        }

    }

    public void Deposit()
    {
        if (bankAccountMoneyField.text.ToString() != null && int.Parse(bankAccountMoneyField.text.ToString()) <= 0)
        {
            bankAccountMoneyField.text = "";
            return;
        }

        int depAmount = int.Parse(bankAccountMoneyField.text.ToString());
        if (gv.money - depAmount >= 0)
        {
            bankAmount += depAmount;
            gv.money -= depAmount;
            bankAccountMoneyField.text = "";
            sfx.Play();

        }
    }

    public void Withdraw()
    {
        if (bankAccountMoneyField.text.ToString() != null && int.Parse(bankAccountMoneyField.text.ToString()) <= 0)
        {
            bankAccountMoneyField.text = "";
            return;
        }

        int depAmount = int.Parse(bankAccountMoneyField.text.ToString());
        if (bankAmount - depAmount >= 0)
        {
            gv.money += depAmount;
            bankAmount -= depAmount;
            bankAccountMoneyField.text = "";
            sfx.Play();

        }
    }

    public void exit()
    {
        mo.bankOpen = false;
        bankCanvas.SetActive(false);
        CODparent.GetComponent<RectTransform>().localPosition = codParentOrigin;
        //player.transform.position = player.transform.position + new Vector3(player.transform.position.x, -2);
    }
}
