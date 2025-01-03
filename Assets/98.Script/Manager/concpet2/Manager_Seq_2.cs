using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Manager_Seq_2 : MonoBehaviour
{


    public int Content_Seq = 0;


    //���� ���� ó�� ��ſ� 0���� ����
    public float Sequence_timer = 0f;

    private bool toggle = true;

    //���� �׷�
    public GameObject Animal;
    private int On_game;

    [Header("[ COMPONENT CHECK ]")]
    public GameObject[] UI_Message_array;     
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
                //Debug.Log("timer done");
            }
        }

        
    }


    //���ʷ� ������ ������ �� ��Ʈ�� �� �������� �̰� ���ư��� �ǰ�
    //�ƴϸ� ��Ʈ�� �����ϰ� �����ϴ� �ɷ� �ϵ�,


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
        //���� �����
        On_game = 0;

        //���� Ŭ�� �� �� �ֵ��� Ȱ��ȭ ��Ű��
        //���� Ŭ���ϸ� �ش��ϴ� ���� �ؽ�Ʈ, �����̼�, �����Ҹ� ���
        //�׸��� �ð� ������ ���� ���� �ִϸ��̼� ���

    }
    void Init_Game_reveal()
    {
        //���� ã��
        On_game = 0;


        //���� Ŭ�� �� �� �ֵ��� Ȱ��ȭ ��Ű��
        //���� Ŭ���ϸ� �ش��ϴ� ���� �ؽ�Ʈ, �����̼� ���
        //�׸��� �ð� ������ ����� ������ �ִϸ��̼� �� �����Ҹ� ���
    }
    void Init_Game_read()
    {
        //���� ã��
        On_game = 0;


        //�׳� ���������� ���� �����̴� �ִϸ��̼� ��
        //�ش��ϴ� ���� �ؽ�Ʈ, �����̼� ���
        
    }
    void Timer_set()
    {
        Sequence_timer = 5f;
        //���� �� �κ��� ���߿��� ������ ���ִ��� �ƴϸ� �� Ư�� �κи� �ٸ� �����ͷ� �־��ִ��� �ؾ���
    }

}
