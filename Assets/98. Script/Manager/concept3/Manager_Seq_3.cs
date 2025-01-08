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

    private GameObject Eventsystem;

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

    //������ ��� ���������� ������ ������ �Ǵ� �ɷ��ϰ�
    //������ �������� ������ ��ũ��Ʈ�� �����س���


    //���õ� ���� ����Ʈ
    // Ex) Orange, Carrot -> 1,0
    public GameObject[] fruitPrefabs; // ���� ������

    private GameObject Box;
    private GameObject Fruitgroups; //���̺� �ִ� ���� parent
    private List<int> currentFruits = new List<int>(); // ���� ���̺� �� ���� ���� ����Ʈ
    private List<List<int>> selectedFruits = new List<List<int>>(); // ���õ� ���� ����Ʈ
    private FruitColor mainColor; // ���� ����
    private int selectedFruitCount = 0;

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

        Eventsystem = Manager_obj_3.instance.Eventsystem;
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


        //1,3,5,7,9 ���� ��� ����
        //2,4,6,8,10 ���� ���� �� �ؽ�Ʈ
        //12 ~ 16 ���� �о��ִ� ����
        if (Content_Seq == 1)
        {
            Init_Game_fruit((int)FruitColor.Red);
            StartCoroutine(ResetAfterTime(7f)); // 7�� �Ŀ� �缳��
        }
        else if (Content_Seq == 3)
        {
            Init_Game_fruit((int)FruitColor.Orange);
            StartCoroutine(ResetAfterTime(7f)); // 7�� �Ŀ� �缳��
        }
        else if (Content_Seq == 5)
        {
            Init_Game_fruit((int)FruitColor.Yellow);
            StartCoroutine(ResetAfterTime(7f)); // 7�� �Ŀ� �缳��
        }
        else if (Content_Seq == 7)
        {
            Init_Game_fruit((int)FruitColor.Green);
            StartCoroutine(ResetAfterTime(7f)); // 7�� �Ŀ� �缳��
        }
        else if (Content_Seq == 9)
        {
            Init_Game_fruit((int)FruitColor.Purple);
            StartCoroutine(ResetAfterTime(7f)); // 7�� �Ŀ� �缳��
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
        //���� �о��ֱ�
        Eventsystem.SetActive(true);
        On_game = 0;
    }
    void Timer_set()
    {
        Sequence_timer = 5f;
        //���� �� �κ��� ���߿��� ������ ���ִ��� �ƴϸ� �� Ư�� �κи� �ٸ� �����ͷ� �־��ִ��� �ؾ���
    }

    void Init_Game_fruit(int colorIndex)
    {
        mainColor = (FruitColor)colorIndex;


        // 7�� �����ؼ� ���� ���� ����Ʈ ����
        currentFruits.Clear();
        currentFruits.AddRange(GenerateFruitList(colorIndex));

        // ���̺� ���� �ν��Ͻ�ȭ
        for (int i = 0; i < currentFruits.Count; i++)
        {
            //�ν��Ͻ�ȭ �س����� ���� ����?
            Instantiate(Box, Box, fruitPrefabs[currentFruits[i]]);
        }
    }
    List<int> GenerateFruitList(int colorIndex)
    {
        List<int> fruitList = new List<int>();

        fruitList.Add(colorIndex * 4 + UnityEngine.Random.Range(0, 4)); // ���� ���򿡼� �������� 2�� �߰�
        fruitList.Add(colorIndex * 4 + UnityEngine.Random.Range(0, 4)); 

        for (int i = 0; i < 5; i++)
        {
            int randomColor = UnityEngine.Random.Range(0, 5);
            fruitList.Add(randomColor * 4 + UnityEngine.Random.Range(0, 4)); // �ٸ� ���򿡼� �������� �߰�
        }
        return fruitList;
    }

    // ���� Ŭ�� �� ó��
    public void Click(int fruitIndex, GameObject plate_Fruit)
    {
        // ������ ���� ��ȣ ����
        int selectedFruit = fruitIndex;

        // ���� ���� ������ ������ ���
        if (selectedFruit / 4 == (int)mainColor)
        {
            selectedFruits.Add(new List<int> { selectedFruit });

            //���� ���̺� �ִ� ���� �߿� �ش� ���� ����
            Manager_Anim.Inactive_Seq_fruit(plate_Fruit);

            selectedFruitCount++;

            // 5�� ���� �� ó��
            if (selectedFruitCount == 5)
            {
                Debug.Log("���õ� ���� 5�� �Ϸ�!");
                selectedFruitCount = 0; // �ʱ�ȭ

                //��� ���� �ʱ�ȭ
                Inactive_All_fruit();

                Content_Seq += 1;
                toggle = true;
                Timer_set();
            }
        }
        else
        {
            // �ٸ� ���� ������ ������ ��� �ش� ���ϸ� �ִϸ��̼�
            Manager_Anim.Inactive_Seq_fruit(plate_Fruit);
        }
    }

    void Inactive_All_fruit()
    {
        for (int i =0;i< Fruitgroups.transform.childCount; i++)
        {
            //���� �ִ� ���� ���� ����
            GameObject fruit = Fruitgroups.transform.GetChild(i).gameObject;
            Manager_Anim.Inactive_Seq_fruit(fruit);
        }
    }

    IEnumerator ResetAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Init_Game_fruit(UnityEngine.Random.Range(0, 5)); // �� ���� �������� �ʱ�ȭ
        StartCoroutine(ResetAfterTime(time)); // ��� �ݺ�
    }
}
