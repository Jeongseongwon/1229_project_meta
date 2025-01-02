using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Manager_Seq : MonoBehaviour
{


    public int Content_Seq = 0;


    //최초 예외 처리 대신에 0으로 설정
    public float Sequence_timer = 0f;


    private bool toggle = true;

    [Header("[ COMPONENT CHECK ]")]
    public GameObject[] UI_Text_array;
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
        this.GetComponent<Manager_Anim>().Change_Animation(Content_Seq);

        if (Content_Seq == 5)
        {
            //게임 실행
        }
        else if (Content_Seq == 12)
        {
            //2번째 게임 실행
        }
        else if (Content_Seq == 15)
        {
            Debug.Log("CONTENT END");
        }
        else
        {
            Content_Seq += 1;
            toggle = true;
            Timer_set();
        }
    }

    void Game_penquin()
    {

    }

    void Game_bear()
    {

    }

    void Timer_set()
    {
        Sequence_timer = 5f;
        //여기 이 부분을 나중에는 지정을 해주던가 아니면 그 특정 부분만 다른 데이터로 넣어주던가 해야함
    }
}
