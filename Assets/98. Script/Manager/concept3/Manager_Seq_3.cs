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

    private List<FruitColor> AllKeyDictionary; // 메인 과일 리스트
    private List<Fruit> currentFruitDisplay = new List<Fruit>(); // 현재 화면에 나열된 과일 리스트
    private FruitColor mainColor; // 메인 색깔
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
        AllKeyDictionary = new List<FruitColor>(fruitGroups.Keys); // 메인 과일 리스트
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

        Content_Seq += 1;
        toggle = true;
        Timer_set();

        //1,3,5,7,9 과일 담기 게임
        //2,4,6,8,10 게임 종료 후 텍스트
        //12 ~ 16 과일 읽어주는 게임
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
        //동물 찾기
        //동물 클릭 할 수 있도록 활성화 시키고
        Eventsystem.SetActive(true);
        On_game = 0;
    }
    void Timer_set()
    {
        Sequence_timer = 5f;
        //여기 이 부분을 나중에는 지정을 해주던가 아니면 그 특정 부분만 다른 데이터로 넣어주던가 해야함
    }

    void Init_Game_fruit(FruitColor mainColorInput)
    {
        ////여기는 처리 부분
        //Eventsystem.SetActive(true);
        //On_game = 0;

        if (round >= maxRounds)
        {
            Debug.Log("게임 종료");
            return;
        }

        round++;
        mainColor = mainColorInput; // 매개변수로 받은 메인 색깔을 설정

        Debug.Log($"Round {round}: Main color is {mainColor}");

        // 메인 색깔을 포함한 5개의 과일을 랜덤으로 선택
        currentFruitDisplay.Clear();
        currentFruitDisplay.Add(GetRandomMainColorFruit()); // 메인 색깔에 있는 과일을 반드시 포함
        
        FillFruitsWithRandomColors();

        // 과일 리스트 출력
        Debug.Log("Current Fruit Display:");
        foreach (Fruit fruit in currentFruitDisplay)
        {
            Debug.Log(fruit);
        }
    }

    // 과일 리스트에 랜덤으로 과일을 추가하는 함수
    void FillFruitsWithRandomColors()
    {
        List<Fruit> availableFruits = new List<Fruit>();

        // 과일 그룹에서 과일을 랜덤하게 가져옴
        foreach (var group in fruitGroups)
        {
            availableFruits.AddRange(group.Value);
        }

        // 메인 색깔을 제외한 과일을 4개 더 뽑음
        for (int i = 0; i < 4; i++)
        {
            Fruit randomFruit = availableFruits[UnityEngine.Random.Range(0, availableFruits.Count)];
            currentFruitDisplay.Add(randomFruit);
        }

        // 과일을 랜덤하게 섞기
        for (int i = 0; i < currentFruitDisplay.Count; i++)
        {
            Fruit temp = currentFruitDisplay[i];
            int randomIndex = UnityEngine.Random.Range(i, currentFruitDisplay.Count);
            currentFruitDisplay[i] = currentFruitDisplay[randomIndex];
            currentFruitDisplay[randomIndex] = temp;
        }
    }

    // 과일 클릭 후 처리
    public void OnFruitClick(Fruit clickedFruit)
    {
        // 사용자가 선택한 과일이 메인 색깔의 과일과 같다면
        if (fruitGroups[mainColor].Contains(clickedFruit))
        {
            // 선택한 과일을 Selected_fruitGroups에 저장
            Selected_fruitGroups[mainColor].Add(clickedFruit);
            Debug.Log($"Added {clickedFruit} to Selected_fruitGroups.");
        }
        else
        {
            // 메인 색깔의 과일과 다르면 Debug.Log 출력
            Debug.Log($"The selected fruit {clickedFruit} is not of the main color {mainColor}.");
        }

        // 사용자가 선택한 과일을 currentFruitDisplay에서 삭제
        currentFruitDisplay.Remove(clickedFruit);

        // 현재 과일 목록을 체크하여 메인 색깔 과일이 있는지 확인
        bool hasMainColorFruit = currentFruitDisplay.Exists(fruit => fruitGroups[mainColor].Contains(fruit));

        if (!hasMainColorFruit)
        {
            // 메인 색깔 과일이 없다면 메인 색깔의 과일을 다시 랜덤으로 추가
            Fruit mainFruit = GetRandomMainColorFruit();
            currentFruitDisplay.Add(mainFruit);
            Debug.Log($"Added {mainFruit} (Main Color) back to the list.");
        }
        else
        {
            // 메인 색깔 과일이 있다면 랜덤으로 과일을 하나 뽑아서 추가
            FillFruitsWithRandomColors();
            Debug.Log("Main color fruit found, re-randomizing fruits.");
        }

        // 현재 과일 목록 출력
        Debug.Log("Updated Fruit Display:");
        foreach (Fruit fruit in currentFruitDisplay)
        {
            Debug.Log(fruit);
        }
    }
    // 메인 색깔에 해당하는 과일을 랜덤으로 반환하는 함수
    private Fruit GetRandomMainColorFruit(FruitColor mainColorInput)
    {
        //해당하는 벨류 리스트 받아오고
        //거기에서 랜덤으로 하나를 뽑는다

        var fruitsInMainColor = fruitGroups[AllKeyDictionary[round]];

        List<Fruit> availableFruits = new List<Fruit>();

        // 과일 그룹에서 과일을 랜덤하게 가져옴
        foreach (var group in fruitsInMainColor)
        {
            availableFruits.AddRange(group.Value);
        }

        // 메인 색깔을 제외한 과일을 4개 더 뽑음
        for (int i = 0; i < 4; i++)
        {
            Fruit randomFruit = availableFruits[UnityEngine.Random.Range(0, availableFruits.Count)];
            currentFruitDisplay.Add(randomFruit);
        }

        return fruitsInMainColor[UnityEngine.Random.Range(0, 4)];
    }

    public void fruit_button(int Num_button)
    {
        //해당하는 과일을 구분해내고
        //해당하는 과일의 메시지, 애니메이션 재생
        if (Content_Seq == 2)
        {
            Manager_Text.Active_UI_message(Num_button);
            Manager_Anim.Hide_Seq_animal(Num_button);
            //나중에 위치 맞춰줄 필요 있음
            //1초 정도 지연시간을 두고 다음 것을 클릭할 수 있도록 함
            On_game += 1;
        }
        else if (Content_Seq == 4)
        {
            Manager_Text.Active_UI_message(Num_button+7);
            Manager_Anim.Reveal_Seq_animal(Num_button);


            //각 동물의 애니메이션이 전부 종료 될 때 까지 지연시간을 두고 다음 것을 클릭할 수 있도록 함
            Manager_Text.Active_UI_Panel();
            On_game += 1;
        }

        if (On_game == 5)
        {

            //효과음 재생, 이펙트 출현
            Eventsystem.SetActive(false);
            Content_Seq += 1;
            toggle = true;
            Timer_set();
        }
    }
}
