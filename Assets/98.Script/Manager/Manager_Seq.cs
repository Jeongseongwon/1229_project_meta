using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Manager_Seq : MonoBehaviour
{


    public int Content_Seq = 0;


    //���� ���� ó�� ��ſ� 0���� ����
    public float Sequence_timer = 0f;


    private bool toggle = true;

    [Header("[ COMPONENT CHECK ]")]
    public GameObject[] UI_Text_array;
    //�ð�, ���� ����?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //�ٵ� ������Ʈ�� ��� �װ� ������ ��� ���ư��ٵ�?

        Sequence_timer -= Time.deltaTime;
        if (Sequence_timer < 0)
        {
            if (toggle == true)
            {
                toggle = false;
                Act();
                Debug.Log("timer done");
            }
        }
    }


    //���ʷ� ������ ������ �� ��Ʈ�� �� �������� �̰� ���ư��� �ǰ�
    //�ƴϸ� ��Ʈ�� �����ϰ� �����ϴ� �ɷ� �ϵ�,


    void Act()
    {
        

        //�ؽ�Ʈ �Ŵ��� �Լ� ����
        //�����̼� �Ŵ��� �Լ� ����
        //�ִϸ��̼� �Ŵ��� �Լ� ����
        this.GetComponent<Manager_Text>().Change_UI_text();

        if (Content_Seq == 4)
        {
            //���� ����
        }
        else if (Content_Seq == 12)
        {
            //2��° ���� ����
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
        //���� �� �κ��� ���߿��� ������ ���ִ��� �ƴϸ� �� Ư�� �κи� �ٸ� �����ͷ� �־��ִ��� �ؾ���
    }
}
