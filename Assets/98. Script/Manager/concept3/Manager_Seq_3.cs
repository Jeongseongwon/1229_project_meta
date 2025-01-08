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

    //동물 그룹
    private int On_game;


    //과일 게임
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

    //과일의 경우 프리팹으로 무조건 생성이 되는 걸로하고
    //생성된 프리팹을 무조건 스크립트에 연결해놓음


    //선택된 과일 리스트
    // Ex) Orange, Carrot -> 1,0
    public GameObject[] fruitPrefabs; // 과일 프리팹

    private GameObject Box;
    private GameObject Fruitgroups; //테이블에 있는 과일 parent
    private List<int> currentFruits = new List<int>(); // 현재 테이블 위 과일 색깔 리스트
    private List<List<int>> selectedFruits = new List<List<int>>(); // 선택된 과일 리스트
    private FruitColor mainColor; // 메인 색깔
    private int selectedFruitCount = 0;

    private int round = 0; // 현재 게임 회차
    private int maxRounds = 5; // 게임의 최대 회차

    [Header("[ COMPONENT CHECK ]")]

    public int Content_Seq = 0;
    //최초 예외 처리 대신에 0으로 설정
    public float Sequence_timer = 0f;
    //시간, 게임 유무?

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

    //최초로 콘텐츠 실행할 때 인트로 한 다음부터 이게 돌아가게끔 전부 수정 필요

    void Act()
    {
        Manager_Text.Change_UI_text(Content_Seq);
        Manager_Narr.Change_Audio_narr(Content_Seq);
        //Manager_Anim.Change_Animation(Content_Seq);


        //1,3,5,7,9 과일 담기 게임
        //2,4,6,8,10 게임 종료 후 텍스트
        //12 ~ 16 과일 읽어주는 게임
        if (Content_Seq == 1)
        {
            Init_Game_fruit((int)FruitColor.Red);
            StartCoroutine(ResetAfterTime(7f)); // 7초 후에 재설정
        }
        else if (Content_Seq == 3)
        {
            Init_Game_fruit((int)FruitColor.Orange);
            StartCoroutine(ResetAfterTime(7f)); // 7초 후에 재설정
        }
        else if (Content_Seq == 5)
        {
            Init_Game_fruit((int)FruitColor.Yellow);
            StartCoroutine(ResetAfterTime(7f)); // 7초 후에 재설정
        }
        else if (Content_Seq == 7)
        {
            Init_Game_fruit((int)FruitColor.Green);
            StartCoroutine(ResetAfterTime(7f)); // 7초 후에 재설정
        }
        else if (Content_Seq == 9)
        {
            Init_Game_fruit((int)FruitColor.Purple);
            StartCoroutine(ResetAfterTime(7f)); // 7초 후에 재설정
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
        //과일 읽어주기
        Eventsystem.SetActive(true);
        On_game = 0;
    }
    void Timer_set()
    {
        Sequence_timer = 5f;
        //여기 이 부분을 나중에는 지정을 해주던가 아니면 그 특정 부분만 다른 데이터로 넣어주던가 해야함
    }

    void Init_Game_fruit(int colorIndex)
    {
        mainColor = (FruitColor)colorIndex;


        // 7번 실행해서 과일 색깔 리스트 설정
        currentFruits.Clear();
        currentFruits.AddRange(GenerateFruitList(colorIndex));

        // 테이블에 과일 인스턴스화
        for (int i = 0; i < currentFruits.Count; i++)
        {
            //인스턴스화 해놓은걸 내가 관리?
            Instantiate(Box, Box, fruitPrefabs[currentFruits[i]]);
        }
    }
    List<int> GenerateFruitList(int colorIndex)
    {
        List<int> fruitList = new List<int>();

        fruitList.Add(colorIndex * 4 + UnityEngine.Random.Range(0, 4)); // 메인 색깔에서 랜덤으로 2개 추가
        fruitList.Add(colorIndex * 4 + UnityEngine.Random.Range(0, 4)); 

        for (int i = 0; i < 5; i++)
        {
            int randomColor = UnityEngine.Random.Range(0, 5);
            fruitList.Add(randomColor * 4 + UnityEngine.Random.Range(0, 4)); // 다른 색깔에서 랜덤으로 추가
        }
        return fruitList;
    }

    // 과일 클릭 후 처리
    public void Click(int fruitIndex, GameObject plate_Fruit)
    {
        // 선택한 과일 번호 저장
        int selectedFruit = fruitIndex;

        // 메인 색깔 과일을 선택한 경우
        if (selectedFruit / 4 == (int)mainColor)
        {
            selectedFruits.Add(new List<int> { selectedFruit });

            //현재 테이블에 있는 과일 중에 해당 과일 삭제
            Manager_Anim.Inactive_Seq_fruit(plate_Fruit);

            selectedFruitCount++;

            // 5개 선택 시 처리
            if (selectedFruitCount == 5)
            {
                Debug.Log("선택된 과일 5개 완료!");
                selectedFruitCount = 0; // 초기화

                //모든 과일 초기화
                Inactive_All_fruit();

                Content_Seq += 1;
                toggle = true;
                Timer_set();
            }
        }
        else
        {
            // 다른 색깔 과일을 선택한 경우 해당 과일만 애니메이션
            Manager_Anim.Inactive_Seq_fruit(plate_Fruit);
        }
    }

    void Inactive_All_fruit()
    {
        for (int i =0;i< Fruitgroups.transform.childCount; i++)
        {
            //현재 있는 과일 전부 삭제
            GameObject fruit = Fruitgroups.transform.GetChild(i).gameObject;
            Manager_Anim.Inactive_Seq_fruit(fruit);
        }
    }

    IEnumerator ResetAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Init_Game_fruit(UnityEngine.Random.Range(0, 5)); // 새 랜덤 색상으로 초기화
        StartCoroutine(ResetAfterTime(time)); // 계속 반복
    }
}
