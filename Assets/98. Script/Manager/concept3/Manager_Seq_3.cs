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


    //���� ����
    public enum FruitColor
    {
        Red, Purple, Green, Orange, Yellow
    }

    public enum Fruit
    {
        Strawberry, Apple, Tomato, Cherry,
        Grapes, Blueberry, Eggplant, Beetroot,
        Watermelon, Cucumber, Avocado, GreenOnion,
        Carrot, Pumpkin, Orange, Onion,
        Banana, Lemon, Corn, Pineapple
    }

    //������ ��� ���������� ������ ������ �Ǵ� �ɷ��ϰ�
    //������ �������� ������ ��ũ��Ʈ�� �����س���


    //���õ� ���� ����Ʈ
    // Ex) Orange, Carrot -> 1,0
    public GameObject[] fruitPrefabs; // ���� ������

    private GameObject Main_Box;
    private GameObject Boxgroups;


    private GameObject[] Main_Box_array;
    private GameObject Fruitgroups; //���̺� �ִ� ���� parent

    private List<int> currentFruits = new List<int>(); // ���� ���̺� �� ���� ���� ����Ʈ
    //private List<List<int>> selectedFruits = new List<List<int>>(); // ���õ� ���� ����Ʈ
    public FruitColor mainColor; // ���� ����
    public int selectedFruitCount = 0;

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
        Main_Box = Manager_obj_3.instance.Main_Box;

        Main_Box_array = new GameObject[Main_Box.transform.childCount];

        for (int i = 0; i < Main_Box.transform.childCount; i++)
        {
            //���� �ִ� ���� ���� ����
            //��� �ٱ��� �Ҵ� �޾Ұ�
            //������� R - P -G -O -Y
            Main_Box_array[i] = Main_Box.transform.GetChild(i).gameObject;
        }
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
                //Init_Game_fruit((int)FruitColor.Red);

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
        Manager_Anim.Change_Animation(Content_Seq);


        //1,3,5,7,9 ���� ��� ����
        //2,4,6,8,10 ���� ���� �� �ؽ�Ʈ
        //12 ~ 16 ���� �о��ִ� ����
        if (Content_Seq == 0)
        {
            Init_Game_fruit((int)FruitColor.Red);
            Eventsystem.SetActive(false);

            Content_Seq += 1;
            toggle = true;
            Timer_set();
        }
        else if (Content_Seq == 1)
        {
            //���⿡�� ��ġ Ȱ��ȭ
            Eventsystem.SetActive(true);
        }
        else if (Content_Seq == 3)
        {
            Init_Game_fruit((int)FruitColor.Orange);
            Eventsystem.SetActive(true);
            //StartCoroutine(ResetAfterTime(7f)); // 7�� �Ŀ� �缳��
        }
        else if (Content_Seq == 5)
        {
            Init_Game_fruit((int)FruitColor.Yellow);
            Eventsystem.SetActive(true);
            //StartCoroutine(ResetAfterTime(7f)); // 7�� �Ŀ� �缳��
        }
        else if (Content_Seq == 7)
        {
            Init_Game_fruit((int)FruitColor.Green);
            Eventsystem.SetActive(true);
            //StartCoroutine(ResetAfterTime(7f)); // 7�� �Ŀ� �缳��
        }
        else if (Content_Seq == 9)
        {
            Init_Game_fruit((int)FruitColor.Purple);
            Eventsystem.SetActive(true);
            //StartCoroutine(ResetAfterTime(7f)); // 7�� �Ŀ� �缳��
        }
        else if (Content_Seq == 2 || Content_Seq == 4 || Content_Seq == 6 || Content_Seq == 8 || Content_Seq == 10)
        {
            End_Game_fruit();
            Eventsystem.SetActive(false);

            Content_Seq += 1;
            toggle = true;
            Timer_set();
        }
        else if (Content_Seq == 11)
        {
            Init_Game_read();

            Content_Seq += 1;
            toggle = true;
            Timer_set();
        }
        else if (Content_Seq == 12 || Content_Seq == 13 || Content_Seq == 14 || Content_Seq == 15 || Content_Seq == 16)
        {
            Read_fruit(round);
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
        round = 0;
        Eventsystem.SetActive(false);
    }
    void Timer_set()
    {
        Sequence_timer = 5f;
        //���� �� �κ��� ���߿��� ������ ���ִ��� �ƴϸ� �� Ư�� �κи� �ٸ� �����ͷ� �־��ִ��� �ؾ���
    }

    void Init_Game_fruit(int colorIndex)
    {
        //�ڽ��� �����ؼ� ���󰡾���
        mainColor = (FruitColor)colorIndex;

        Manager_Anim.Jump_box_bp1(round);
        //�� ������ ������ �Ʒ��� ����

        //���� ���򿡼� 2��
        Generate_fruit(colorIndex * 4 + UnityEngine.Random.Range(0, 4), 0);
        Generate_fruit(colorIndex * 4 + UnityEngine.Random.Range(0, 4), 1);

        for (int i = 0; i < 5; i++)
        {
            //��ü �������� 5��
            Generate_fruit(UnityEngine.Random.Range(0, 16), i + 2);
        }
    }

    void End_Game_fruit()
    {
        Manager_Anim.Jump_box_bp0(round);
        Inactive_All_fruit();

        round += 1;
    }
    void Read_fruit(int round)
    {
        //ù��°�� �ƴ϶�� �ٱ��� �ٲٴ� �ִϸ��̼� �ʿ�
        //�ش� �ٱ��� ������ �о��
        Manager_Anim.Move_box_bp2(round);

        GameObject Selected_fruit; 
        int fruit_number;

        //������ ���� ����
        //3�� ��ٸ��ٰ�
        //�Ʒ� for�� ����
        for (int i = 0; i < 5; i++)
        {
            DOVirtual.DelayedCall();


            Selected_fruit = Main_Box_array[round].transform.GetChild(i).gameObject;
            fruit_number = Selected_fruit.GetComponent<Clicked_fruit>().Number_fruit;

            Manager_Anim.Jump_fruit(Selected_fruit, Manager_Anim.Get_Fp2(i), 1.5f);
            Manager_Text.Changed_UI_message_c3(i + 7, fruit_number);
        }

        round += 1;
    }


    //���� ���� ����� �� �������ų�, Ŭ���� ���ų�
    public void Click(GameObject plate_Fruit, int num_fruit, int num_table)
    {
        
        if (num_fruit / 4 == (int)mainColor)
        {
            //���� ���̺� �ִ� ���� �߿� �ش� ���� ����
            Manager_Anim.Devide_Seq_fruit(plate_Fruit, selectedFruitCount);
            plate_Fruit.transform.SetSiblingIndex(selectedFruitCount);
            //���⿡�� �ش� ���� ������Ʈ �ε��� �ø�
            Manager_Text.Changed_UI_message_c3(num_table, num_fruit);
            //�ش� �ϴ� ����, ä�� �ؽ�Ʈ, �����̼ǵ� ���;���

            //�׳� ��ü �������� �ϳ� �ٽ� �߰�
            Generate_fruit(UnityEngine.Random.Range(0, 16), num_table);

            selectedFruitCount++;

            // 5�� ���� �� ó��
            if (selectedFruitCount == 5)
            {
                Debug.Log("���õ� ���� 5�� �Ϸ�!");
                //���⿡�� �������� �ٷ� �Ѿ���� ���ڴµ�?
                selectedFruitCount = 0; // �ʱ�ȭ

                Content_Seq += 1;
                toggle = true;
            }
        }
        //���� ���򿡼� ���� ���� ���
        else
        {
            //���� ���̺� �ִ� ���� �߿� �ش� ���� ����
            Manager_Anim.Inactive_Seq_fruit(plate_Fruit,0f);

            //���� ���򿡼� ����
            Generate_fruit((int)mainColor * 4 + UnityEngine.Random.Range(0, 4), num_table);

        }
    }

    public void Inactive_All_fruit()
    {
        for (int i =5;i< Main_Box_array[round].transform.childCount; i++)
        {
            GameObject fruit = Main_Box_array[round].transform.GetChild(i).gameObject;
            Manager_Anim.Inactive_Seq_fruit(fruit,2f);
        }
    }
    
    void Generate_fruit(int num_fruit, int num_table)
    {
        //������ ��Ȱ��ȭ ��ä�� �޾ƿ��� �˾� �ִϸ��̼ǿ��� Ȱ��ȭ��
        Transform pos = Manager_Anim.Get_Fp0(num_table);
        Transform fruit_group = Manager_obj_3.instance.Fruit_position.transform;

        GameObject fruit = Instantiate(Manager_obj_3.instance.Fruit_prefabs[num_fruit]);
        fruit.transform.SetParent(Main_Box_array[round].transform);
        fruit.transform.localPosition = pos.localPosition;

        fruit.GetComponent<Clicked_fruit>().Set_Number_fruit(num_fruit);
        fruit.GetComponent<Clicked_fruit>().Set_Number_table(num_table);

        Manager_Anim.Popup_fruit(fruit);
    }



    IEnumerator ResetAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Init_Game_fruit(UnityEngine.Random.Range(0, 5)); // �� ���� �������� �ʱ�ȭ
        StartCoroutine(ResetAfterTime(time)); // ��� �ݺ�
    }
}
