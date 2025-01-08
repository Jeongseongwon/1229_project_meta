using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Manager_Anim_3 : MonoBehaviour
{
    //Common
    public int Content_Seq = 0;

    //Camera
    private GameObject Main_Camera;
    private GameObject Camera_position;
    private Sequence[] Camera_seq;
    private int Number_Camera_seq;


    //Animal
    private GameObject Main_Fruit;
    private GameObject[] Main_Fruit_array;
    private GameObject Fruit_position;

    //p0 �׷� 7��
    //p1 �׷� 5��
    //p2 �׷� 5��

    //���� �ִϸ��̼� ����
    //0~6��
    private Sequence[] Reveal_f_seq;

    //��� transform�� ���� ������ �ְ� �װ� �����ؼ� ����ϴ� �ɷ�
    //���� ��̴� �������� �޶��� �� ����

    private Transform[] p0; //�ʱ� ��ġ 7��
    private Transform[] p1; //�ڽ� �� ��ġ 5��
    private Transform[] p2; //�ڽ� �� ��ġ 5��


    [Header("[ COMPONENT CHECK ]")]
    public GameObject[] Camera_pos_array;
    public GameObject[] Fruit_pos_array;

    void Start()
    {
        Init_Seq_camera();
        Init_Seq_fruit();

        Camera_position = Manager_obj_3.instance.Camera_position;
        Main_Camera = Manager_obj_3.instance.Main_Camera;
        Main_Fruit = Manager_obj_3.instance.Main_Fruit;
        Fruit_position = Manager_obj_3.instance.Fruit_position;
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

    void Init_Seq_fruit()
    {
        Fruit_pos_array = new GameObject[Fruit_position.transform.childCount];
        Main_Fruit_array = new GameObject[Main_Fruit.transform.childCount];
        Reveal_f_seq = new Sequence[Fruit_position.transform.childCount];

        //hide, reveal ������ ���� �ѹ��� �ʱ�ȭ
        //���� �̵� ���� �Ҵ�
        for (int i = 0; i < Fruit_position.transform.childCount; i++)
        {
            Fruit_pos_array[i] = Fruit_position.transform.GetChild(i).gameObject;
            Main_Fruit_array[i] = Main_Fruit.transform.GetChild(i).gameObject;

            Reveal_f_seq[i] = DOTween.Sequence().SetAutoKill(false);

            Transform p0 = Fruit_pos_array[i].transform.GetChild(0).transform;
            Transform p1 = Fruit_pos_array[i].transform.GetChild(1).transform;
            Transform p2 = Fruit_pos_array[i].transform.GetChild(2).transform;
            Transform p3 = Fruit_pos_array[i].transform.GetChild(3).transform;


            //�ִϸ��̼��� ���� �� �ڿ������� ���� �ʿ�� ����


            Reveal_f_seq[i].Append(Main_Fruit_array[i].transform.DOMove(p2.position, 1f).SetEase(Ease.InOutQuad));
            Reveal_f_seq[i].Join(Main_Fruit_array[i].transform.DORotate(p2.rotation.eulerAngles, 1f));
            Reveal_f_seq[i].Append(Main_Fruit_array[i].transform.DOScale(p2.localScale, 1f).SetEase(Ease.InOutQuad));
            Reveal_f_seq[i].Append(Main_Fruit_array[i].transform.DOMove(p3.position, 3f).SetEase(Ease.InOutQuad).SetDelay(5f));

            Reveal_f_seq[i].Pause();


            //Debug.Log("length " + Camera_seq.Length);
        }
    }


    void Move_Seq_camera()
    {
        Camera_seq[Number_Camera_seq].Play();
        Number_Camera_seq++;
        //Debug.Log("C_SEQ = " + Number_Camera_seq);
    }

    //�̹����� �ش� ������Ʈ�� �޾ƿ��� �� ������Ʈ�� ���� ���ǵ��ִ� ��ġ�� �̵��ϰų�
    //�����ؼ� �̵� ��Ű�ų�

    //�ش� �ϴ� ��ü�� �޾ƿ�
    //�̵��ؾ��� ������ 2���� �޾ƿ���
    //�ش� �����ǿ� ���缭 �̵�

    public void In_Seq_fruit(int Num, GameObject plate_Fruit)
    {
        //�������ÿ��� ����, ���� ���� �� ���� �ִϸ��̼� ���
        GameObject plate = plate_Fruit.transform.GetChild(0).gameObject;
        GameObject fruit = plate_Fruit.transform.GetChild(1).gameObject;

        //���ô� �������
        plate.transform.DOScale(0, 1f).SetEase(Ease.OutElastic);

        //������ �ش��ϴ� ���������� �����ؼ� ��
        fruit.transform.DOJump(p1[Num].position, 1f, 1, 1f);
    }
    public void Inactive_Seq_fruit(GameObject fruit)
    {
        //���� ���� �ʴ� ������ ���
        //�ִϸ��̼� ����ϰ� �ı���
        fruit.transform.DOScale(0, 1f).SetEase(Ease.OutElastic).OnComplete(()=> Destroy(fruit));
    }

    public void Reveal_Seq_animal(int Num)
    {
        Reveal_f_seq[Num].Play();
    }


    public void Reveal_All_animal()
    {
        Reveal_f_seq[0].Play();
        Reveal_f_seq[1].Play();
        Reveal_f_seq[2].Play();
        Reveal_f_seq[3].Play();
        Reveal_f_seq[4].Play();
        Reveal_f_seq[5].Play();
        Reveal_f_seq[6].Play();
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
