using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Manager_Anim_2 : MonoBehaviour
{
    //Common
    public int Content_Seq = 0;

    //Camera
    public GameObject Main_Camera;
    public GameObject Camera_position;
    private Sequence[] Camera_seq;
    private int Number_Camera_seq;


    //Animal
    public GameObject Main_Animal;
    private GameObject[] Main_Animal_array;
    public GameObject Animal_position;

    //����, Hide, Reveal �迭�� ����
    //0~6��
    private Sequence[] Hide_a_seq;
    private Sequence[] Reveal_a_seq;

    [Header("[ COMPONENT CHECK ]")]
    public GameObject[] Camera_pos_array;
    public GameObject[] Animal_pos_array;

    void Start()
    {
        Init_Seq_camera();
        Init_Seq_animal();
    }
    //�������� Ȱ���� �κ�
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

    void Init_Seq_animal()
    {
        Animal_pos_array = new GameObject[Animal_position.transform.childCount];
        Main_Animal_array = new GameObject[Main_Animal.transform.childCount];
        Hide_a_seq = new Sequence[Animal_position.transform.childCount];
        Reveal_a_seq = new Sequence[Animal_position.transform.childCount];

        //hide, reveal ������ ���� �ѹ��� �ʱ�ȭ
        //���� �̵� ���� �Ҵ�
        for (int i = 0; i < Animal_position.transform.childCount; i++)
        {
            Animal_pos_array[i] = Animal_position.transform.GetChild(i).gameObject;
            Main_Animal_array[i] = Main_Animal.transform.GetChild(i).gameObject;

            Hide_a_seq[i] = DOTween.Sequence().SetAutoKill(false);
            Reveal_a_seq[i] = DOTween.Sequence().SetAutoKill(false);

            Transform p0 = Animal_pos_array[i].transform.GetChild(0).transform;
            Transform p1 = Animal_pos_array[i].transform.GetChild(1).transform;
            Transform p2 = Animal_pos_array[i].transform.GetChild(2).transform;
            Transform p3 = Animal_pos_array[i].transform.GetChild(3).transform;


            //�ִϸ��̼��� ���� �� �ڿ������� ���� �ʿ�� ����
            Hide_a_seq[i].Append(Main_Animal_array[i].transform.DOMove(p1.position, 1f).SetEase(Ease.InOutQuad));
            Hide_a_seq[i].Join(Main_Animal_array[i].transform.DORotate(p1.rotation.eulerAngles, 1f));


            Reveal_a_seq[i].Append(Main_Animal_array[i].transform.DOMove(p2.position, 1f).SetEase(Ease.InOutQuad));
            Reveal_a_seq[i].Join(Main_Animal_array[i].transform.DORotate(p2.rotation.eulerAngles, 1f));
            Reveal_a_seq[i].Append(Main_Animal_array[i].transform.DOScale(p2.localScale, 1f).SetEase(Ease.InOutQuad));
            Reveal_a_seq[i].Append(Main_Animal_array[i].transform.DOMove(p3.position, 3f).SetEase(Ease.InOutQuad).SetDelay(5f));

            Hide_a_seq[i].Pause();
            Reveal_a_seq[i].Pause();


            //Debug.Log("length " + Camera_seq.Length);
        }
    }


    void Move_Seq_camera()
    {
        //�Լ��� ���� �����ϴ°� ����
        //Ŭ���� �Ǹ� �ش��ϴ� ����� ������ �ſ����ϱ�
        Camera_seq[Number_Camera_seq].Play();
        Number_Camera_seq++;
        //Debug.Log("C_SEQ = " + Number_Camera_seq);
    }

    public void Hide_Seq_animal(int Num)
    {
        Hide_a_seq[Num].Play();
    }
    public void Reveal_Seq_animal(int Num)
    {
        Reveal_a_seq[Num].Play();
    }

    public void Hide_All_animal()
    {
        Hide_a_seq[0].Play();
        Hide_a_seq[1].Play();
        Hide_a_seq[2].Play();
        Hide_a_seq[3].Play();
        Hide_a_seq[4].Play();
        Hide_a_seq[5].Play();
        Hide_a_seq[6].Play();

    }

    public void Reveal_All_animal()
    {
        Reveal_a_seq[0].Play();
        Reveal_a_seq[1].Play();
        Reveal_a_seq[2].Play();
        Reveal_a_seq[3].Play();
        Reveal_a_seq[4].Play();
        Reveal_a_seq[5].Play();
        Reveal_a_seq[6].Play();
    }


    public void Change_Animation(int Number_seq)
    {
        Content_Seq = Number_seq;
        if (Content_Seq == 1 || Content_Seq == 3 || Content_Seq == 5)
        {
            Move_Seq_camera();
            //Debug.Log("SEQ = " + Content_Seq);
        }
    }

    //public void Shake_Seq_sleigh()
    //{
    //    Sequence Shake = DOTween.Sequence();
    //    Shake.Append(Sleigh.transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad));
    //    //Shake.Append(Sleigh.transform.DOShakePosition(1,1,10,1,false,true).SetEase(Ease.InOutQuad));
    //    //��鸮�� �ִϸ��̼�
    //    //���ư��� �ִϸ��̼�
    //}

    //public void Fly_Seq_sleigh()
    //{
    //    Sequence Fly = DOTween.Sequence();

    //    Transform p1 = Sleigh_position.transform.GetChild(0).transform;
    //    Transform p2 = Sleigh_position.transform.GetChild(1).transform;
    //    Transform p3 = Sleigh_position.transform.GetChild(2).transform;


    //    //��� ���� ���� �ִϸ��̼�
    //    Fly.Append(Sleigh.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 1f).SetEase(Ease.InOutQuad));
    //    Fly.Append(Sleigh.transform.DOMove(p1.transform.position, 1f).SetEase(Ease.InOutQuad));
    //    Fly.Join(Sleigh.transform.DORotate(p1.transform.rotation.eulerAngles, 1f));
    //    Fly.Append(Sleigh.transform.DOJump(p2.transform.position, 1f, 1, 1f));
    //    Fly.Join(Sleigh.transform.DORotate(p2.transform.rotation.eulerAngles, 1f));
    //    Fly.Append(Sleigh.transform.DOJump(p3.transform.position, 1f, 1, 1f));
    //    Fly.Join(Sleigh.transform.DORotate(p3.transform.rotation.eulerAngles, 1f));
    //}
}