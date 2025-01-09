using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Manager_Seq_3;
using static UnityEditor.PlayerSettings;

public class Manager_Anim_3 : MonoBehaviour
{
    //Common
    public int Content_Seq = 0;

    //Camera
    private GameObject Main_Camera;
    private GameObject Camera_position;
    private Sequence[] Camera_seq;
    private int Number_Camera_seq;


    //fruit
    private GameObject Fruit_position;

    private GameObject Main_Box;
    private GameObject[] Main_Box_array;
    private GameObject Box_position;


    private GameObject Selected_fruit;
    private int fruit_number;

    //p0 �׷� 7��
    //p1 �׷� 5��
    //p2 �׷� 5��

    //���� �ִϸ��̼� ����
    //0~6��
    private Sequence[] Reveal_f_seq;

    //��� transform�� ���� ������ �ְ� �װ� �����ؼ� ����ϴ� �ɷ�
    //���� ��̴� �������� �޶��� �� ����

    private Transform[] B_p0; //�ڽ� �ʱ� ��ġ 5��
    private Transform B_p1; //���� ���� �ڽ� ��ġ
    private Transform B_p2; //���� �б� �ڽ� ��ġ

    public Transform[] F_p0; //���� ���� ���� ��ġ
    private Transform[] F_p1; //�ڽ��� ���� ��ġ
    private Transform[] F_p2; //���� �б� ���� ��ġ

    [Header("[ COMPONENT CHECK ]")]
    //��ġ �Է°� Ȯ�ο�
    public GameObject[] Camera_pos_array;
    public GameObject[] Fruit_pos_array;
    public GameObject[] Box_pos_array;

    void Start()
    {
        Camera_position = Manager_obj_3.instance.Camera_position;
        Main_Camera = Manager_obj_3.instance.Main_Camera;
        Fruit_position = Manager_obj_3.instance.Fruit_position;
        Box_position = Manager_obj_3.instance.Box_position;
        Main_Box = Manager_obj_3.instance.Main_Box;

        Init_Seq_camera();
        Init_Seq_fruit();
        Init_Seq_box();
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

    //�׷��ٸ� �̰� ũ�� �ǹ̰� ������?
    //�׳� �׶� �׶� �ʱ�ȭ�ؼ� ����� �����ص� ũ�� ������� �� ���⵵�ϰ�
    //�׳� transform���� ���� �� ������ �ִ°ɷ�?
    void Init_Seq_fruit()
    {
        Fruit_pos_array = new GameObject[Fruit_position.transform.childCount];


        F_p0 = new Transform[7]; //���� ���� ���� ��ġ
        F_p1 = new Transform[5]; //�ڽ��� ���� ��ġ
        F_p2 = new Transform[5]; //���� �б� ���� ��ġ

        for (int i = 0; i < Fruit_position.transform.childCount; i++)
        {
            Fruit_pos_array[i] = Fruit_position.transform.GetChild(i).gameObject;
        }
        //FP
        F_p0[0] = Fruit_position.transform.GetChild(0);
        F_p0[1] = Fruit_position.transform.GetChild(1);
        F_p0[2] = Fruit_position.transform.GetChild(2);
        F_p0[3] = Fruit_position.transform.GetChild(3);
        F_p0[4] = Fruit_position.transform.GetChild(4);
        F_p0[5] = Fruit_position.transform.GetChild(5);
        F_p0[6] = Fruit_position.transform.GetChild(6);
        F_p1[0] = Fruit_position.transform.GetChild(7);
        F_p1[1] = Fruit_position.transform.GetChild(8);
        F_p1[2] = Fruit_position.transform.GetChild(9);
        F_p1[3] = Fruit_position.transform.GetChild(10);
        F_p1[4] = Fruit_position.transform.GetChild(11);
        F_p2[0] = Fruit_position.transform.GetChild(12);
        F_p2[1] = Fruit_position.transform.GetChild(13);
        F_p2[2] = Fruit_position.transform.GetChild(14);
        F_p2[3] = Fruit_position.transform.GetChild(15);
        F_p2[4] = Fruit_position.transform.GetChild(16);


    }
    void Init_Seq_box()
    {

        B_p0 = new Transform[5];  //�ڽ� �ʱ� ��ġ 5��

        Main_Box_array = new GameObject[Main_Box.transform.childCount];
        Box_pos_array = new GameObject[Box_position.transform.childCount];

        for (int i = 0; i < Box_position.transform.childCount; i++)
        {
            Box_pos_array[i] = Box_position.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < Main_Box.transform.childCount; i++)
        {
            Main_Box_array[i] = Main_Box.transform.GetChild(i).gameObject;
        }
        //BP
        B_p0[0] = Box_position.transform.GetChild(0);
        B_p0[1] = Box_position.transform.GetChild(1);
        B_p0[2] = Box_position.transform.GetChild(2);
        B_p0[3] = Box_position.transform.GetChild(3);
        B_p0[4] = Box_position.transform.GetChild(4);
        B_p1 = Box_position.transform.GetChild(5);
        B_p2 = Box_position.transform.GetChild(6);

    }
    public void Popup_fruit(GameObject fruit)
    {
        Sequence seq = DOTween.Sequence();

        //2�� �� �˾� �ִϸ��̼� �� ���� Ȱ��ȭ
        seq.Append(fruit.transform.DOScale(1, 1f).From(0).SetEase(Ease.OutElastic).OnStart(() => fruit.SetActive(true))).SetDelay(2f);
        //seq.Append(fruit.transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad));
    }

    //�ش��ϴ� ������ ��� ���������� �̵�����
    public void Jump_fruit(GameObject fruit, Transform pos, float timer)
    {
        Sequence seq = DOTween.Sequence();

        if (timer != 0f)
        {
            seq.Append(fruit.transform.DOJump(pos.position, 1f, 1, 1f)).SetDelay(timer);
            seq.Append(fruit.transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad));
        }
        else
        {
            seq.Append(fruit.transform.DOJump(pos.position, 1f, 1, 1f));
            seq.Append(fruit.transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad));
        }

    }

    public void Jump_box_bp1(int round)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(Main_Box_array[round].transform.DOJump(B_p1.position, 1f, 1, 1f));
        seq.Append(Main_Box_array[round].transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad));
    }
    public void Jump_box_bp0(int round)
    {
        Sequence seq = DOTween.Sequence();

        //���� �ð� ���� ������ �����ϴ� �ִϸ��̼�
        seq.Append(Main_Box_array[round].transform.DOJump(B_p0[round].position, 1f, 1, 1f)).SetDelay(3f);
        seq.Join(Main_Box_array[round].transform.DOShakeScale(0.5f, 1, 10, 90, true).SetEase(Ease.OutQuad));
        seq.Append(Main_Box_array[round].transform.DOScale(B_p0[round].localScale, 1f));


        //seq.Append(Main_Box_array[round].transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad));

    }
    public void Move_box_bp2(int round)
    {
        Sequence seq = DOTween.Sequence();

        if (round != 0)
        {
            //���� �ٱ��ϸ� ����־��ְ� �� ������ ���� ���� �ٱ��ϸ� ����
            seq.Append(Main_Box_array[round - 1].transform.DOMove(B_p0[round - 1].position, 1f).SetEase(Ease.InOutQuad));
            seq.Append(Main_Box_array[round].transform.DOMove(B_p2.position, 1f).SetEase(Ease.InOutQuad));
        }
        else
        {
            seq.Append(Main_Box_array[round].transform.DOMove(B_p2.position, 1f).SetEase(Ease.InOutQuad));
        }

        seq.Join(Main_Box_array[round].transform.DOScale(B_p2.localScale, 1f));
    }

    public void Move_Seq_camera()
    {
        Camera_seq[Number_Camera_seq].Play();
        Number_Camera_seq++;
        //Debug.Log("C_SEQ = " + Number_Camera_seq);
    }
    public void Inactive_Seq_fruit(GameObject fruit, float timer)
    {
        //���� ������ ������°� ���� ���� �� ���� ���
        //(�̽�) ������ �������� �ѹ��� ������� �Ŷ� ������ ���� ���� ���� ������� �ϴ°Ŷ� ���ϰ� �浹 �߻��ϴ� �� ����
        if (timer != 0f)
        {
            fruit.transform.DOScale(0, 0.5f).SetEase(Ease.InOutQuint).OnComplete(() => Destroy(fruit)).SetDelay(timer);
        }
        else
        {
            fruit.transform.DOScale(0, 0.5f).SetEase(Ease.InOutQuint).OnComplete(() => Destroy(fruit));
        }
    }
    public void Devide_Seq_fruit(GameObject plate_Fruit,int number)
    {
        //���ô� ��Ȱ��ȭ �Ǵ� �ı�
        //������ �ٱ��� ������//�������ÿ��� ����, ���� ���� �� ���� �ִϸ��̼� ���
        GameObject plate = plate_Fruit.transform.GetChild(0).gameObject;
        GameObject fruit = plate_Fruit.transform.GetChild(1).gameObject;

        //���ô� �������
        plate.transform.DOScale(0, 1f).SetEase(Ease.OutElastic);

        Jump_fruit(fruit, F_p1[number],0f);
    }

    public void Read_Seq_fruit(int round)
    {
        GameObject Selected_fruit;
        int fruit_number;

        //������ ���� ����
        //3�� ��ٸ���
        //����, �ؽ�Ʈ
        //3�� ��ٸ���
        //����, �ؽ�Ʈ
        //3�� ��ٸ���
        //����, �ؽ�Ʈ
        //3�� ��ٸ���
        //����, �ؽ�Ʈ
        //3�� ��ٸ���
        //����, �ؽ�Ʈ


        for (int i = 0; i < 5; i++)
        {
            Selected_fruit = Main_Box_array[round].transform.GetChild(i).gameObject;
            fruit_number = Selected_fruit.GetComponent<Clicked_fruit>().Number_fruit;

            Jump_fruit(Selected_fruit, Get_Fp2(i), 1.5f);
            Manager_Text.Changed_UI_message_c3(i + 7, fruit_number);
        }
        Sequence seq = DOTween.Sequence();


        seq.Append(Main_Box_array[round].transform.GetChild(0).transform.DOJump(F_p2[round].position, 1f, 1, 1f)).SetDelay(2f);
        seq.Append(Main_Box_array[round].transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad)).OnComplete(() =>
            Manager_Text.Changed_UI_message_c3(i + 7, fruit_number);
        
        );

    }

    void Read_func()
    {

    }

    public Transform Get_Fp0(int num)
    {
        return F_p0[num];
    }

    public Transform Get_Fp2(int num)
    {
        return F_p2[num];
    }

    public void Change_Animation(int Number_seq)
    {
        Content_Seq = Number_seq;
        if (Content_Seq == 11 || Content_Seq == 12)
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
