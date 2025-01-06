using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;



public class Manager_Seq_3 : MonoBehaviour
{

    public static Manager_Seq_3 instance = null;

    private Manager_Anim_3 Manager_Anim;
    private Manager_Text Manager_Text;
    private Manager_Narr Manager_Narr;

    public GameObject Eventsystem;

    private bool toggle = true;

    //���� �׷�
    private int On_game;


    //���� ����
    public enum FruitColor
    {
        Red, Orange, Yellow, Green, Purple
    }

    public enum Fruit
    {
        Strawberry, Apple, Tomato, Cherry,
        Carrot, Pumpkin, Orange, Onion,
        Banana, Lemon, Corn, Pineapple,
        Watermelon, Cucumber, Avocado, GreenOnion,
        Grapes, Blueberry, Eggplant, Beetroot
    }

    private Dictionary<FruitColor, HashSet<Fruit>> fruitGroups = new Dictionary<FruitColor, HashSet<Fruit>>()
    {
        { FruitColor.Red, new HashSet<Fruit> { Fruit.Strawberry, Fruit.Apple, Fruit.Tomato, Fruit.Cherry } },
        { FruitColor.Orange, new HashSet<Fruit> { Fruit.Carrot, Fruit.Pumpkin, Fruit.Orange, Fruit.Onion } },
        { FruitColor.Yellow, new HashSet<Fruit> { Fruit.Banana, Fruit.Lemon, Fruit.Corn, Fruit.Pineapple } },
        { FruitColor.Green, new HashSet<Fruit> { Fruit.Watermelon, Fruit.Cucumber, Fruit.Avocado, Fruit.GreenOnion } },
        { FruitColor.Purple, new HashSet<Fruit> { Fruit.Grapes, Fruit.Blueberry, Fruit.Eggplant, Fruit.Beetroot } }
    };

    private Dictionary<FruitColor, List<Fruit>> Selected_fruitGroups = new Dictionary<FruitColor, List<Fruit>>()
    {
        { FruitColor.Red, new List<Fruit>() },
        { FruitColor.Orange, new List<Fruit>() },
        { FruitColor.Yellow, new List<Fruit>() },
        { FruitColor.Green, new List<Fruit>() },
        { FruitColor.Purple, new List<Fruit>() }
    };

    private List<FruitColor> AllKeyDictionary; // ���� ���� ����Ʈ
    private List<Fruit> currentFruitDisplay = new List<Fruit>(); // ���� ȭ�鿡 ������ ���� ����Ʈ
    private FruitColor mainColor; // ���� ����
    private int round = 0; // ���� ���� ȸ��
    private int maxRounds = 5; // ������ �ִ� ȸ��

    [Header("[ COMPONENT CHECK ]")]

    public int Content_Seq = 0;
    //���� ���� ó�� ��ſ� 0���� ����
    public float Sequence_timer = 0f;
    //�ð�, ���� ����?

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Manager_Text = this.gameObject.GetComponent<Manager_Text>();
        Manager_Anim = this.gameObject.GetComponent<Manager_Anim_3>();
        Manager_Narr = this.gameObject.GetComponent<Manager_Narr>();
        AllKeyDictionary = new List<FruitColor>(fruitGroups.Keys); // ���� ���� ����Ʈ
    }

    // Update is called once per frame
    void Update()
    {
        Sequence_timer -= Time.deltaTime;
        if (Sequence_timer < 0)
        {
            if (toggle == true)
            {
                toggle = false;
                Act();
                //Debug.Log("timer done");
            }
        }
    }

    //���ʷ� ������ ������ �� ��Ʈ�� �� �������� �̰� ���ư��Բ� ���� ���� �ʿ�

    void Act()
    {
        Manager_Text.Change_UI_text(Content_Seq);
        Manager_Narr.Change_Audio_narr(Content_Seq);
        //Manager_Anim.Change_Animation(Content_Seq);

        Content_Seq += 1;
        toggle = true;
        Timer_set();

        //1,3,5,7,9 ���� ��� ����
        //2,4,6,8,10 ���� ���� �� �ؽ�Ʈ
        //12 ~ 16 ���� �о��ִ� ����
        if (Content_Seq == 1 || Content_Seq == 3 || Content_Seq == 5 || Content_Seq == 7 || Content_Seq == 9)
        {
            Init_Game_fruit(AllKeyDictionary[round]);
        }
        else if (Content_Seq == 12 || Content_Seq == 13 || Content_Seq == 14 || Content_Seq == 15 || Content_Seq == 16)
        {

        }
        else
        {
            Content_Seq += 1;
            toggle = true;
            Timer_set();
        }
    }
    void Init_Game_read()
    {
        //���� ã��
        //���� Ŭ�� �� �� �ֵ��� Ȱ��ȭ ��Ű��
        Eventsystem.SetActive(true);
        On_game = 0;
    }
    void Timer_set()
    {
        Sequence_timer = 5f;
        //���� �� �κ��� ���߿��� ������ ���ִ��� �ƴϸ� �� Ư�� �κи� �ٸ� �����ͷ� �־��ִ��� �ؾ���
    }

    void Init_Game_fruit(FruitColor mainColorInput)
    {
        ////����� ó�� �κ�
        //Eventsystem.SetActive(true);
        //On_game = 0;

        if (round >= maxRounds)
        {
            Debug.Log("���� ����");
            return;
        }

        round++;
        mainColor = mainColorInput; // �Ű������� ���� ���� ������ ����

        Debug.Log($"Round {round}: Main color is {mainColor}");

        // ���� ������ ������ 5���� ������ �������� ����
        currentFruitDisplay.Clear();
        currentFruitDisplay.Add(GetRandomMainColorFruit()); // ���� ���� �ִ� ������ �ݵ�� ����
        
        FillFruitsWithRandomColors();

        // ���� ����Ʈ ���
        Debug.Log("Current Fruit Display:");
        foreach (Fruit fruit in currentFruitDisplay)
        {
            Debug.Log(fruit);
        }
    }

    // ���� ����Ʈ�� �������� ������ �߰��ϴ� �Լ�
    void FillFruitsWithRandomColors()
    {
        List<Fruit> availableFruits = new List<Fruit>();

        // ���� �׷쿡�� ������ �����ϰ� ������
        foreach (var group in fruitGroups)
        {
            availableFruits.AddRange(group.Value);
        }

        // ���� ������ ������ ������ 4�� �� ����
        for (int i = 0; i < 4; i++)
        {
            Fruit randomFruit = availableFruits[UnityEngine.Random.Range(0, availableFruits.Count)];
            currentFruitDisplay.Add(randomFruit);
        }

        // ������ �����ϰ� ����
        for (int i = 0; i < currentFruitDisplay.Count; i++)
        {
            Fruit temp = currentFruitDisplay[i];
            int randomIndex = UnityEngine.Random.Range(i, currentFruitDisplay.Count);
            currentFruitDisplay[i] = currentFruitDisplay[randomIndex];
            currentFruitDisplay[randomIndex] = temp;
        }
    }

    // ���� Ŭ�� �� ó��
    public void OnFruitClick(Fruit clickedFruit)
    {
        // ����ڰ� ������ ������ ���� ������ ���ϰ� ���ٸ�
        if (fruitGroups[mainColor].Contains(clickedFruit))
        {
            // ������ ������ Selected_fruitGroups�� ����
            Selected_fruitGroups[mainColor].Add(clickedFruit);
            Debug.Log($"Added {clickedFruit} to Selected_fruitGroups.");
        }
        else
        {
            // ���� ������ ���ϰ� �ٸ��� Debug.Log ���
            Debug.Log($"The selected fruit {clickedFruit} is not of the main color {mainColor}.");
        }

        // ����ڰ� ������ ������ currentFruitDisplay���� ����
        currentFruitDisplay.Remove(clickedFruit);

        // ���� ���� ����� üũ�Ͽ� ���� ���� ������ �ִ��� Ȯ��
        bool hasMainColorFruit = currentFruitDisplay.Exists(fruit => fruitGroups[mainColor].Contains(fruit));

        if (!hasMainColorFruit)
        {
            // ���� ���� ������ ���ٸ� ���� ������ ������ �ٽ� �������� �߰�
            Fruit mainFruit = GetRandomMainColorFruit();
            currentFruitDisplay.Add(mainFruit);
            Debug.Log($"Added {mainFruit} (Main Color) back to the list.");
        }
        else
        {
            // ���� ���� ������ �ִٸ� �������� ������ �ϳ� �̾Ƽ� �߰�
            FillFruitsWithRandomColors();
            Debug.Log("Main color fruit found, re-randomizing fruits.");
        }

        // ���� ���� ��� ���
        Debug.Log("Updated Fruit Display:");
        foreach (Fruit fruit in currentFruitDisplay)
        {
            Debug.Log(fruit);
        }
    }
    // ���� ���� �ش��ϴ� ������ �������� ��ȯ�ϴ� �Լ�
    private Fruit GetRandomMainColorFruit(FruitColor mainColorInput)
    {
        //�ش��ϴ� ���� ����Ʈ �޾ƿ���
        //�ű⿡�� �������� �ϳ��� �̴´�

        var fruitsInMainColor = fruitGroups[AllKeyDictionary[round]];

        List<Fruit> availableFruits = new List<Fruit>();

        // ���� �׷쿡�� ������ �����ϰ� ������
        foreach (var group in fruitsInMainColor)
        {
            availableFruits.AddRange(group.Value);
        }

        // ���� ������ ������ ������ 4�� �� ����
        for (int i = 0; i < 4; i++)
        {
            Fruit randomFruit = availableFruits[UnityEngine.Random.Range(0, availableFruits.Count)];
            currentFruitDisplay.Add(randomFruit);
        }

        return fruitsInMainColor[UnityEngine.Random.Range(0, 4)];
    }

    public void fruit_button(int Num_button)
    {
        //�ش��ϴ� ������ �����س���
        //�ش��ϴ� ������ �޽���, �ִϸ��̼� ���
        if (Content_Seq == 2)
        {
            Manager_Text.Active_UI_message(Num_button);
            Manager_Anim.Hide_Seq_animal(Num_button);
            //���߿� ��ġ ������ �ʿ� ����
            //1�� ���� �����ð��� �ΰ� ���� ���� Ŭ���� �� �ֵ��� ��
            On_game += 1;
        }
        else if (Content_Seq == 4)
        {
            Manager_Text.Active_UI_message(Num_button+7);
            Manager_Anim.Reveal_Seq_animal(Num_button);


            //�� ������ �ִϸ��̼��� ���� ���� �� �� ���� �����ð��� �ΰ� ���� ���� Ŭ���� �� �ֵ��� ��
            Manager_Text.Active_UI_Panel();
            On_game += 1;
        }

        if (On_game == 5)
        {

            //ȿ���� ���, ����Ʈ ����
            Eventsystem.SetActive(false);
            Content_Seq += 1;
            toggle = true;
            Timer_set();
        }
    }
}
