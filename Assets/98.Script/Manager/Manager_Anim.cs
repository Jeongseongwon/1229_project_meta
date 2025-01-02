using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Manager_Anim : MonoBehaviour
{
    //Common
    public int Content_Seq = 0;

    //Camera
    public GameObject Main_Camera;
    public GameObject Camera_position;
    private Sequence [] Camera_seq;
    private int Number_Camera_seq;

    //Animal
    public GameObject Main_Penguin;
    private GameObject [] Main_Penguin_array;
    public GameObject Penguin_position;
    //펭귄, in sequence 배열, out sequence 배열로 관리
    //0~6번
    private Sequence[] In_p_seq;
    private Sequence[] Out_p_seq;

    [Header("[ COMPONENT CHECK ]")]
    public GameObject[] Camera_pos_array;
    public GameObject[] Penguin_pos_array;

    void Start()
    {
        Init_Seq_camera();
        Init_Seq_penguin();
    }
    void Init_Seq_camera()
    {
        Camera_pos_array = new GameObject[Camera_position.transform.childCount];
        Camera_seq = new Sequence[Camera_position.transform.childCount];
        Number_Camera_seq = 0;

        for (int i = 0; i < Camera_position.transform.childCount; i++)
        {
            Camera_pos_array[i] = Camera_position.transform.GetChild(i).gameObject;

            Camera_seq[i] = DOTween.Sequence();

            Transform pos = Camera_position.transform.GetChild(i).transform;
            Camera_seq[i].Append(Main_Camera.transform.DORotate(pos.transform.rotation.eulerAngles, 1f));
            Camera_seq[i].Join(Main_Camera.transform.DOMove(pos.transform.position, 1f).SetEase(Ease.InOutQuad));
            Camera_seq[i].Pause();


            //Debug.Log("length " + Camera_seq.Length);
        }
    }

    void Init_Seq_penguin()
    {
        Penguin_pos_array = new GameObject[Penguin_position.transform.childCount];
        Main_Penguin_array = new GameObject[Main_Penguin.transform.childCount];
        In_p_seq = new Sequence[Penguin_position.transform.childCount];
        Out_p_seq = new Sequence[Penguin_position.transform.childCount];

        //in.out 시퀀스 각각 한번씩 초기화
        //메인 이동 펭귄 할당
        for (int i = 0; i < Penguin_position.transform.childCount; i++)
        {
            Penguin_pos_array[i] = Penguin_position.transform.GetChild(i).gameObject;
            Main_Penguin_array[i] = Main_Penguin.transform.GetChild(i).gameObject;

            In_p_seq[i] = DOTween.Sequence().SetAutoKill(false);
            Out_p_seq[i] = DOTween.Sequence().SetAutoKill(false);

            Transform p1 = Penguin_pos_array[i].transform.GetChild(0).transform;
            Transform p2 = Penguin_pos_array[i].transform.GetChild(1).transform;
            Transform p3 = Penguin_pos_array[i].transform.GetChild(2).transform;
            Transform p4 = Penguin_pos_array[i].transform.GetChild(3).transform;

            In_p_seq[i].Append(Main_Penguin_array[i].transform.DORotate(p2.transform.rotation.eulerAngles, 1f));
            In_p_seq[i].Append(Main_Penguin_array[i].transform.DOMove(p2.transform.position, 1f).SetEase(Ease.InOutQuad));
            In_p_seq[i].Append(Main_Penguin_array[i].transform.DOJump(p3.transform.position, 1f, 1, 1f));
            In_p_seq[i].Append(Main_Penguin_array[i].transform.DORotate(p3.transform.rotation.eulerAngles, 1f));

            Out_p_seq[i].Append(Main_Penguin_array[i].transform.DORotate(p4.transform.rotation.eulerAngles, 1f));
            Out_p_seq[i].Append(Main_Penguin_array[i].transform.DOMove(p4.transform.position, 1f).SetEase(Ease.InOutQuad));
            Out_p_seq[i].Append(Main_Penguin_array[i].transform.DOJump(p1.transform.position, 1f, 1, 1f));
            Out_p_seq[i].Append(Main_Penguin_array[i].transform.DORotate(p1.transform.rotation.eulerAngles, 1f));

            In_p_seq[i].Pause();
            Out_p_seq[i].Pause();


            //Debug.Log("length " + Camera_seq.Length);
        }
    }
    void Move_Seq_camera()
    {
        Camera_seq[Number_Camera_seq].Play();
        Number_Camera_seq++;
        //Debug.Log("C_SEQ = " + Number_Camera_seq);
    }
    void Move_Seq_penguin()
    {
        //안으로 들어가는 애니메이션 일괄 하고
        //잠시 뒤 몇 마리 돌아오고
        //다시 집어넣고
        //다음으로 진행

        In_p_seq[0].Play();
        In_p_seq[1].Play();
        In_p_seq[2].Play();
        In_p_seq[3].Play();
        In_p_seq[4].Play();
        In_p_seq[5].Play();
        In_p_seq[6].Play();

        Number_Camera_seq++;
        //Debug.Log("C_SEQ = " + Number_Camera_seq);
    }

    //펭귄 애니메이션의 경우 총 10번 하는걸로 하고, 각각 한번씩 돌아온 다음에 랜덤으로 3번 더 하고 출발하는 걸로
    //썰매 애니메이션의 경우 잠깐 흔들어주는걸로 하고, 게임 종료 된 이후에 한번만 함
    //그리고 카메라 이동하고 각각 텍스트 보여줌
    //눈썰매 애니메이션이 하나 더 잇구만
    //곰 애니메이션의 경우 하나도 없고 물고기만 튀어오른 다음에 곰까지 하는걸로

    //해당하는 seq 번호에 호출 필요
    public void Change_Animation(int Number_seq)
    {
        Content_Seq = Number_seq;
        if (Content_Seq == 1 || Content_Seq == 2)
        {
            Move_Seq_camera();
            //Debug.Log("SEQ = " + Content_Seq);
        }else if (Content_Seq == 4)
        {
            Move_Seq_penguin();
        }
    }
}
