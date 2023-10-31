using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class YarnController : MonoBehaviour
{
    // Start is called before the first frame 
    public MovementOverrides mo;
    [SerializeField] public static string playerName;

    public Animator anim;
    public Text pn;
    public TextMeshProUGUI name1;
    public GameObject inputCanvas;
    public TextMeshProUGUI talking;
    public Inventory inv;
    public NewspaperSystem ns;

    public AchievementCanvas am;
    public Animator thisanim;
    public GameObject dialouge;

    public int currentLine;
    public int phase;

    public bool isCollided;
    public GameObject player;
    public void Start()
    {
        Time.timeScale = 0f;
        player.tag = "PlayerDialougeOnly";
        inv = GameObject.FindObjectOfType<Inventory>().GetComponent<Inventory>();
        ns = GameObject.FindObjectOfType<NewspaperSystem>().GetComponent<NewspaperSystem>();
        am = GameObject.FindObjectOfType<AchievementCanvas>().GetComponent<AchievementCanvas>();
    }


    public string[] conversation1 = //line 4 sfx
    {
        $"Stranger/Hey, are you {playerName} ?",
        $"{playerName}@Yes . . .who are you?",
        $"Stranger/Hi, I'm your financial advisor.",
        $"Financial Advisor@I was looking for you all over town.",
        $"Financial Advisor@I've noticed that you seem to be in quite a bit of debt!",
        $"{playerName}@You didn't have to rub it in'",
        $"Financial Advisor@Well'at least you decided to invest in this property! It has the potential to make a lot of profit.",
        $"Financial Advisor@You just need to learn how to make the most out of the <b>American Enterprise System</b>.",
        $"{playerName}@The American Enterprise System?",
        $"Financial Advisor@It's where a person like you can make your own business decisions. Essentially, you can make money without restrictions!",
        $"Financial Advisor@By the way, how did you get the money to purchase this property?",
        $"{playerName}@I took out a <b>loan</b>.",
        $"Financial Advisor@Oh, so you borrowed money! If that's the case, you'll have to <b>pay your mortgage every 60 days.</b>",
        $"{playerName}@What if I don't pay it in time?",
        $"Financial Advisor@Then I'm afraid you'll lose your property to the bank.",
        $"Financial Advisor@So, make sure that you are spending your time and money wisely. You should aim to pay off your mortgage and make a profit as well!",
        $"Financial Advisor@Alright, enough chit chat! Follow me to the farmland below.",
        $"{playerName}@Hey, wait up!"
    };

    public string[] conversation2 = //after index = 6, give seeds + sfx
    {
        $"Financial Advisor@You finally made it! Anyways, you can grow crops on pieces of soil.",
        $"Financial Advisor@Once you're done growing them, you can sell your crops at a shipping truck for some cash.",
        $"Financial Advisor@So, let's start by growing some crops! Press <b>space</b> to plant your seeds and to water the plants.",
        $"{playerName}@<b>Seeds</b>?",
        $"Financial Advisor@Yep!",
        $"{playerName}@What seeds?",
        $"Financial Advisor@Oops, you don't have any? Here, take a <b>Corn Seed</b>, on the house!",
        $"Financial Advisor@Check the bottom of your screen! Click on the crop that you wish to plant; you can switch between crops at any time.",
        $"Financial Advisor@If you run out of seeds, or just want more, you'll have to buy more from the gardener shop.",
        $"Financial Advisor@Anyways, after you plant your seeds, press space to water it. If you don't water your seeds, they will die!",
        $"{playerName}@Is that the only thing I need to worry about?",
        $"Financial Advisor@Oh, right! Watch out for crows that might come by your crops! Walking near them would scare them away, but you can also use the scarecrow utility.",
        $"Financial Advisor@How do I use <b>utilities</b>?",
        $"Financial Advisor@To the right of your seeds, there should be a slot labeled utilities! Press the red arrow on the slot to choose which one you use. There are two utilities available for you to use.",
        $"Financial Advisor@To use utilities, press the drop-down and choose which one you want. Once ready, press <b>\"Z\"</b> to place it (Hover over the sprinkler for more info about it)!",
        $"Financial Advisor@What's the difference between each one?",
        $"Financial Advisor@Well, like I mentioned earlier, the scarecrow can protect your crops from crows. The Sprinkler can automatically water your crops for you!",
        $"Financial Advisor@Once you're done growing your plant(s), meet me at the <b>shipping truck</b>, any time!"
    };

    public string[] conversation3 =
    {
        $"Financial Advisor@Welcome to the <b>shipping truck</b>! This is where you can sell the crops you've grown!",
        $"Financial Advisor@The shipping truck sends the crops you produce to the <b>market</b>.",
        $"{playerName}@What exactly is the market?",
        $"Financial Advisor@Well, a market is a place where products and services are bought or sold.",
        $"{playerName}@Who's buying and who's selling, then?",
        $"Financial Advisor@The market is made up of two groups/the <b>producers</b> and the <b>consumers</b>.",
        $"Financial Advisor@Producers create and supply the goods and services. Meanwhile, consumers purchase the products made from the producer.",
        $"{playerName}@So does that make me a producer?",
        $"Financial Advisor@Exactly! After you grow your crops, you can sell them using the shipping truck to make money.",
        $"Financial Advisor@The shipping truck sends the products to retailers and customers, who are your consumers.",
        $"Financial Advisor@This market is one part of the United States economic system, also known as the <b>American Enterprise System</b>, which also consists of businesses that compete for customers free of government control.",
        $"{playerName}@Wait, <b>competitors</b>?",
        $"Financial Advisor@Yup, you can hear about how they're doing by reading the <b>newspaper</b> on the bottom right of your screen.",
        $"Financial Advisor@Since you are now a business in the American Enterprise system, you'll be trying to sell more of your products than your competitors' products to consumers.",
        $"playerName/How do I know what price I need to sell my crops for?",
        $"Financial Advisor@When setting a price there is always <b>risk</b> and <b>reward</b>.",
        $"Financial Advisor@What if the price is too high and no one buys?",
        $"Financial Advisor@What if the price is too low and you don't make a profit?",
        $"Financial Advisor@That's where <b>supply and <b>demand</b> comes in handy.",
        $"Financial Advisor@Supply is the amount of product that producers can offer. Demand is how much consumers are willing to pay for said product.",
        $"Financial Advisor@Additionally, you must keep your competitors in mind when choosing a price.",
        $"playerName/What do you mean by that?",
        $"Financial Advisor@Ideally, you want a lower price than your competitors so more people will buy your crops.",
        $"Financial Advisor@But make sure the price isn't too low ' you still need to profit!",
        $"Financial Advisor@Remember that you can check on your competitors using the <b>newspaper</b>. It also shows you supply and demand statistics and the weather.",
        $"Financial Advisor@Anyways, since you don't have many crops yet, it's probably not a good idea to sell.",
        $"Financial Advisor@Oh look, the <b>bank</b> finally opened! This gives us the opportunity for another way to make money/",
        $"Financial Advisor@Follow me to the bank!"
    };

    public string[] conversation4 =
    {
        $"Financial Advisor@Welcome to the <b>bank</b>!",
        $"{playerName}@You said you need to tell me something?",
        $"Financial Advisor@Your goal is to make enough profit from selling crops and investments to pay off your loan.",
        $"Financial Advisor@Whenever you produce a crop and sell it, be aware of the <b>costs of producing</b> the crop.",
        $"Financial Advisor@If it costs more money to make the crop and pay for shipping than the actual price of the crop, you're losing money!",
        $"Financial Advisor@Oh, look at the time! I have to go. Keep working hard, I believe in you.",
        $"playerName/Wait, you're just going to leave?",
        $"Financial Advisor@I might visit you again in the future. But for now, good luck and remember my advice! See you soon!"
    };

    public void changeName()
    {
        if (pn.text.Length >= 1)
        {
            Time.timeScale = 1;
            playerName = pn.text;
            mo.nameOfPlayer = playerName;
            anim.Play("Introduction");
            print(playerName);
            spagettticode();
            ns.changeNewspaperRotation();
        }
    }

    public void spagettticode()
    {
        string[] one = {$"Stranger@Hey, are you {playerName} ?",
        $"{playerName}@Yes . . .who are you?",
        $"Stranger@Hi, I'm your financial advisor.",
        $"Financial Advisor@I was looking for you all over town.",
        $"Financial Advisor@I've noticed that you seem to be in quite a bit of debt!",
        $"{playerName}@You didn't have to rub it in...",
        $"Financial Advisor@Well'at least you decided to invest in this property! It has the potential to make a lot of profit.",
        $"Financial Advisor@You just need to learn how to make the most out of the <b>American Enterprise System</b>.",
        $"{playerName}@The American Enterprise System?",
        $"Financial Advisor@It's where a person like you can make your own business decisions. Essentially, you can make money without restrictions!",
        $"Financial Advisor@By the way, how did you get the money to purchase this property?",
        $"{playerName}@I took out a <b>loan</b>.",
        $"Financial Advisor@Oh, so you borrowed money! If that's the case, you'll have to <b>pay your mortgage every 60 days.</b>",
        $"{playerName}@What if I don't pay it in time?",
        $"Financial Advisor@Then I'm afraid you'll lose your property to the bank.",
        $"Financial Advisor@So, make sure that you are spending your time and money wisely. You should aim to pay off your mortgage and make a profit as well!",
        $"Financial Advisor@Alright, enough chit chat! Follow me to the farmland below.",
        $"{playerName}@Hey, wait up!"};

        string[] two = {$"Financial Advisor@You finally made it! Anyways, you can grow crops on pieces of soil.",
        $"Financial Advisor@Once you're done growing them, you can sell your crops at a shipping truck for some cash.",
        $"Financial Advisor@So, let's start by growing some crops! Press <b>space</b> to plant your seeds and to water the plants.",
        $"{playerName}@<b>Seeds</b>?",
        $"Financial Advisor@Yep!",
        $"{playerName}@What seeds?",
        $"Financial Advisor@Oops, you don't have any? Here, take a <b>Corn Seed</b>, on the house!",
        $"Financial Advisor@Check the bottom of your screen! Click on the crop that you wish to plant; you can switch between crops at any time.",
        $"Financial Advisor@If you run out of seeds, or just want more, you'll have to buy more from the gardener shop.",
        $"Financial Advisor@Anyways, after you plant your seeds, press space to water it. If you don't water your seeds, they will die!",
        $"{playerName}@Is that the only thing I need to worry about?",
        $"Financial Advisor@Oh, right! Watch out for crows that might come by your crops! Walking near them would scare them away, but you can also use the scarecrow utility.",
        $"Financial Advisor@How do I use <b>utilities</b>?",
        $"Financial Advisor@To the right of your seeds, there should be a slot labeled utilities! Press the red arrow on the slot to choose which one you use. There are two utilities available for you to use.",
        $"Financial Advisor@To use utilities, press the drop-down and choose which one you want. Once ready, press <b>\"Z\"</b> to place it (Hover over the sprinkler for more info about it)!",
        $"Financial Advisor@What's the difference between each one?",
        $"Financial Advisor@Well, like I mentioned earlier, the scarecrow can protect your crops from crows. The Sprinkler can automatically water your crops for you!",
        $"Financial Advisor@Once you're done growing your plant(s), meet me at the <b>shipping truck</b>, any time!"};
        string[] three = {  $"Financial Advisor@Welcome to the <b>shipping truck</b>! This is where you can sell the crops you've grown!",
        $"Financial Advisor@The shipping truck sends the crops you produce to the <b>market</b>.",
        $"{playerName}@What exactly is the market?",
        $"Financial Advisor@Well, a market is a place where products and services are bought or sold.",
        $"{playerName}@Who's buying and who's selling, then?",
        $"Financial Advisor@The market is made up of two groups/the <b>producers</b> and the <b>consumers</b>.",
        $"Financial Advisor@Producers create and supply the goods and services. Meanwhile, consumers purchase the products made from the producer.",
        $"{playerName}@So does that make me a producer?",
        $"Financial Advisor@Exactly! After you grow your crops, you can sell them using the shipping truck to make money.",
        $"Financial Advisor@The shipping truck sends the products to retailers and customers, who are your consumers.",
        $"Financial Advisor@This market is one part of the United States economic system, also known as the <b>American Enterprise System</b>, which also consists of businesses that compete for customers free of government control.",
        $"{playerName}@Wait, <b>competitors</b>?",
        $"Financial Advisor@Yup, you can hear about how they're doing by reading the <b>newspaper</b> on the bottom right of your screen.",
        $"Financial Advisor@Since you are now a business in the American Enterprise system, you'll be trying to sell more of your products than your competitors' products to consumers.",
        $"{playerName}@How do I know what price I need to sell my crops for?",
        $"Financial Advisor@When setting a price there is always <b>risk</b> and <b>reward</b>.",
        $"Financial Advisor@What if the price is too high and no one buys?",
        $"Financial Advisor@What if the price is too low and you don't make a profit?",
        $"Financial Advisor@That's where <b>supply and <b>demand</b> comes in handy.",
        $"Financial Advisor@Supply is the amount of product that producers can offer. Demand is how much consumers are willing to pay for said product.",
        $"Financial Advisor@Additionally, you must keep your competitors in mind when choosing a price.",
        $"{playerName}@What do you mean by that?",
        $"Financial Advisor@Ideally, you want a lower price than your competitors so more people will buy your crops.",
        $"Financial Advisor@But make sure the price isn't too low ' you still need to profit!",
        $"Financial Advisor@Remember that you can check on your competitors using the <b>newspaper</b>. It also shows you supply and demand statistics and the weather.",
        $"Financial Advisor@Anyways, since you don't have many crops yet, it's probably not a good idea to sell.",
        $"Financial Advisor@Oh look, the <b>bank</b> finally opened! This gives us the opportunity for another way to make money/",
        $"Financial Advisor@Follow me to the bank!"};
        string[] four =
    {
        $"Financial Advisor@Welcome to the <b>bank</b>!",
        $"{playerName}@You said you need to tell me something?",
        $"Financial Advisor@Your goal is to make enough profit from selling crops and investments to pay off your loan.",
        $"Financial Advisor@Whenever you produce a crop and sell it, be aware of the <b>costs of producing</b> the crop.",
        $"Financial Advisor@If it costs more money to make the crop and pay for shipping than the actual price of the crop, you're losing money!",
        $"Financial Advisor@Oh, look at the time! I have to go. Keep working hard, I believe in you.",
        $"{playerName}@Wait, you're just going to leave?",
        $"Financial Advisor@I might visit you again in the future. But for now, good luck and remember my advice! See you soon!"
    };
        conversation1 = one;
        conversation2 = two;
        conversation3 = three;
        conversation4 = four;
        StartCoroutine(disable());
    }

    IEnumerator disable()
    {
        yield return new WaitForSeconds(10);
        inputCanvas.SetActive(false);
        Time.timeScale = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isCollided)
        {
            isCollided = true;
            mo.dialougeOpen = true;
            dialouge.SetActive(true);
            speech();
        }
    }


    public void buttonPress()
    {
        while(Time.timeScale == 0)
        {
            Time.timeScale = 1f;
        }
    }

    public void speech()
    {
        DictionarySystem ds = GameObject.FindObjectOfType<DictionarySystem>().GetComponent<DictionarySystem>();

        switch (phase)
        {
            case 0:
                set(conversation1);
                break;
            case 1:
                ds.newDefinition("Debt", "Debt is when you owe someone money. It's like borrowing money from someone or a bank. You promise to pay back the money you borrowed, often with a little extra called 'interest.' It's like borrowing a toy from a friend and saying, 'I'll give it back, and I'll also give you some extra toys as a 'thank you' for letting me borrow it.'");
                ds.newDefinition("American Enterprise System", "The American Enterprise System is like the way grown-ups work together to make things and do business. In this system, people can start their own companies and come up with ideas for products or services. They can sell these things to other people who want them. It's all about people having the freedom to create businesses, make money, and buy things they need or want. It's like a big teamwork where everyone gets to play a part in making and trading things in the country.");
                set(conversation2);
                break;
            case 2:
                set(conversation3);
                ds.newDefinition("Market", "In the business world, a market is like a playground where companies and customers meet to trade products and services. Companies make things, and the market is where they try to sell those things to people who want them. It's a bit like a big store where everything is for sale, from toys to food to clothes.");
                ds.newDefinition("Producer(You!)", "A producer is like a magician who makes the things you see in the market. They create toys, clothes, food, and more. They work hard to make sure their products are good and wanted by people.");
                ds.newDefinition("Competitor", "Competitors are like friendly rivals. They are other producers who also make similar things. It's a bit like a race to see who can make the best product and sell the most. Competition makes things exciting!");
                ds.newDefinition("Risk vs Reward", "Imagine you're on an adventure. Risk is like the tricky parts of your adventure, like climbing a big hill. But if you make it to the top, the reward is like finding a treasure chest! In the business world, taking risks can lead to big rewards, but sometimes things can be tricky.");
                ds.newDefinition("Supply and Demand", "Supply is like a big box of cookies, and demand is how much people want those cookies. If everyone wants cookies, the price might go up because they're in high demand. If there are too many cookies and not enough people who want them, the price might go down. It's like a seesaw between what's available and what people want.");
                

                break;
            case 3:
                set(conversation4);
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
        
    }

    public void set(string[] conversation)
    {
        if(talking.text == "I might visit you again in the future. But for now, good luck and remember my advice! See you soon!")
        {
            dialouge.SetActive(false);
            mo.dialougeOpen = false;
            thisanim.Play("NPCMove4");
            am.achievements[18].current++;

        }
        if (currentLine == conversation.Length)
        {
            currentLine = 0;
            if (phase == 0) { thisanim.Play("NPCMove1"); }
            else if (phase == 1) { thisanim.Play("NPCMove2"); }
            else if (phase == 2) { thisanim.Play("NPCMove3"); }
            phase++;
            dialouge.SetActive(false);
            mo.dialougeOpen = false;
            StartCoroutine(canColllideCheck());   
        }
        else
        {
            dialouge.SetActive(true);
            Debug.Log(conversation[currentLine]);
            string[] temp = conversation[currentLine].Split('@');
            name1.text = temp[0];
            talking.text = temp[1];
            currentLine++;
        }

        if(phase == 1)
        {
            player.GetComponent<PlayerController>().canDoTasks = true;
        }
        if(phase == 1 && currentLine == 5)
        {
            inv.seeds[0].numberOfSeeds++;
        }
    }

    private IEnumerator canColllideCheck()
    {
        yield return new WaitForSeconds(2);
        isCollided = false;
    }

}