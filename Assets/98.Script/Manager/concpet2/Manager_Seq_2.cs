using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Manager_Seq_2 : MonoBehaviour
{


    public int Content_Seq = 0;


    //최초 예외 처리 대신에 0으로 설정
    public float Sequence_timer = 0f;

    private bool toggle = true;

    //동물 그룹
    public GameObject Animal;
    private int On_game;

    [Header("[ COMPONENT CHECK ]")]
    public GameObject[] UI_Message_array;     
    //시간, 게임 유무?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //근데 업데이트에 계속 그게 있으면 계속 돌아갈텐데?

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


    //최초로 콘텐츠 실행할 때 인트로 한 다음부터 이게 돌아가도 되고
    //아니면 인트로 포함하고 실행하는 걸로 하되,


    void Act()
    {
        this.GetComponent<Manager_Text>().Change_UI_text(Content_Seq);
        this.GetComponent<Manager_Narr>().Change_Audio_narr(Content_Seq);
        this.GetComponent<Manager_Anim_2>().Change_Animation(Content_Seq);

        if (Content_Seq == 2)
        {

        }
        else if(Content_Seq == 4)
        {
            
        }
        else if (Content_Seq == 5)
        {
           
        }
        else
        {
            Content_Seq += 1;
            toggle = true;
            Timer_set();
        }
    }

    void Init_Game_hide()
    {
        //동물 숨기기
        On_game = 0;

        //동물 클릭 할 수 있도록 활성화 시키고
        //동물 클릭하면 해당하는 동물 텍스트, 나레이션, 울음소리 재생
        //그리고 시간 지나면 동물 숨는 애니메이션 재생

    }
    void Init_Game_reveal()
    {
        //동물 찾기
        On_game = 0;


        //동물 클릭 할 수 있도록 활성화 시키고
        //동물 클릭하면 해당하는 동물 텍스트, 나레이션 재생
        //그리고 시간 지나면 가운데로 나오는 애니메이션 및 울음소리 재생
    }
    void Init_Game_read()
    {
        //동물 찾기
        On_game = 0;


        //그냥 순차적으로 동물 움직이는 애니메이션 및
        //해당하는 동물 텍스트, 나레이션 재생
        
    }
    void Timer_set()
    {
        Sequence_timer = 5f;
        //여기 이 부분을 나중에는 지정을 해주던가 아니면 그 특정 부분만 다른 데이터로 넣어주던가 해야함
    }

}
