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


    //fruit
    private GameObject Fruit_position;

    private GameObject Main_Box;
    private GameObject[] Main_Box_array;
    private GameObject Box_position;

    //p0 그룹 7개
    //p1 그룹 5개
    //p2 그룹 5개

    //과일 애니메이션 관리
    //0~6번
    private Sequence[] Reveal_f_seq;

    //대신 transform을 전부 가지고 있고 그걸 조합해서 사용하는 걸로
    //과일 어레이는 언제든지 달라질 수 있음

    private Transform[] B_p0; //박스 초기 위치 5개
    private Transform B_p1; //과일 게임 박스 위치
    private Transform B_p2; //과일 읽기 박스 위치

    public Transform[] F_p0; //과일 게임 과일 위치
    private Transform[] F_p1; //박스내 과일 위치
    private Transform[] F_p2; //과일 읽기 과일 위치

    [Header("[ COMPONENT CHECK ]")]
    //위치 입력값 확인용
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
    //공통으로 활용할 부분
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

    //그렇다면 이게 크게 의미가 없을듯?
    //그냥 그때 그때 초기화해서 만들고 실행해도 크게 상관없을 것 같기도하고
    //그냥 transform값만 전부 잘 가지고 있는걸로?
    void Init_Seq_fruit()
    {
        Fruit_pos_array = new GameObject[Fruit_position.transform.childCount];


        F_p0 = new Transform[7]; //과일 게임 과일 위치
        F_p1 = new Transform[5]; //박스내 과일 위치
        F_p2 = new Transform[5]; //과일 읽기 과일 위치

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

        B_p0 = new Transform[5];  //박스 초기 위치 5개

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

        //2초 뒤 팝업 애니메이션 및 과일 활성화
        seq.Append(fruit.transform.DOScale(1, 1f).From(0).SetEase(Ease.OutElastic).OnStart(() => fruit.SetActive(true))).SetDelay(2f);
        //seq.Append(fruit.transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad));
    }

    //해당하는 과일을 어느 포지션으로 이동할지
    public void Jump_fruit(GameObject fruit, Transform pos)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(fruit.transform.DOJump(pos.position, 1f, 1, 1f));
        seq.Append(fruit.transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad));
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

        //조금 시간 지난 다음에 점프하는 애니메이션
        seq.Append(Main_Box_array[round].transform.DOJump(B_p0[round].position, 1f, 1, 1f)).SetDelay(3f);
        seq.Join(Main_Box_array[round].transform.DOShakeScale(0.5f, 1, 10, 90, true).SetEase(Ease.OutQuad));
        //seq.Append(Main_Box_array[round].transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad));

    }
    public void Move_box_bp2(int round)
    {
        Sequence seq = DOTween.Sequence();

        //클릭한 후 지연시간 있고 바구니 이동하게됨
        seq.Append(Main_Box_array[round].transform.DOMove(B_p2.position, 1f).SetEase(Ease.InOutQuad).SetDelay(5f));
        //seq.Append(Main_Box_array[round].transform.DOShakeScale(1, 1, 10, 90, true).SetEase(Ease.OutQuad));
    }

    public void Move_Seq_camera()
    {
        Camera_seq[Number_Camera_seq].Play();
        Number_Camera_seq++;
        //Debug.Log("C_SEQ = " + Number_Camera_seq);
    }

    //이번에는 해당 오브젝트를 받아오고 그 오브젝트를 사전 정의되있는 위치로 이동하거나
    //세팅해서 이동 시키거나

    //해당 하는 물체를 받아옴
    //이동해야할 포지션 2개를 받아오고
    //해당 포지션에 맞춰서 이동

    public void In_Seq_fruit(int Num, GameObject plate_Fruit)
    {
        //과일접시에서 과일, 접시 분해 후 각각 애니메이션 재생
        GameObject plate = plate_Fruit.transform.GetChild(0).gameObject;
        GameObject fruit = plate_Fruit.transform.GetChild(1).gameObject;

        //접시는 사라지고
        plate.transform.DOScale(0, 1f).SetEase(Ease.OutElastic);

        //과일은 해당하는 포지션으로 점프해서 들어감
        //fruit.transform.DOJump(p1[Num].position, 1f, 1, 1f);
    }
    public void Inactive_Seq_fruit(GameObject fruit, float timer)
    {
        //뭔가 과일이 사라지는게 조금 늦은 것 같은 기분
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
        //접시는 비활성화 또는 파괴
        //과일은 바구니 안으로//과일접시에서 과일, 접시 분해 후 각각 애니메이션 재생
        GameObject plate = plate_Fruit.transform.GetChild(0).gameObject;
        GameObject fruit = plate_Fruit.transform.GetChild(1).gameObject;

        //접시는 사라지고
        plate.transform.DOScale(0, 1f).SetEase(Ease.OutElastic);

        Jump_fruit(fruit, F_p1[number]);
    }

    public void Move_Seq_box(int Round)
    {
        //해당 하는 라운드의 바구니를 p1으로 이동

    }

    public Transform Get_Fp0(int num)
    {
        return F_p0[num];
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
    //    //흔들리는 애니메이션
    //    //날아가는 애니메이션
    //}

    //public void Fly_Seq_sleigh()
    //{
    //    Sequence Fly = DOTween.Sequence();

    //    Transform p1 = Sleigh_position.transform.GetChild(0).transform;
    //    Transform p2 = Sleigh_position.transform.GetChild(1).transform;
    //    Transform p3 = Sleigh_position.transform.GetChild(2).transform;


    //    //펭귄 원상 복구 애니메이션
    //    Fly.Append(Sleigh.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 1f).SetEase(Ease.InOutQuad));
    //    Fly.Append(Sleigh.transform.DOMove(p1.transform.position, 1f).SetEase(Ease.InOutQuad));
    //    Fly.Join(Sleigh.transform.DORotate(p1.transform.rotation.eulerAngles, 1f));
    //    Fly.Append(Sleigh.transform.DOJump(p2.transform.position, 1f, 1, 1f));
    //    Fly.Join(Sleigh.transform.DORotate(p2.transform.rotation.eulerAngles, 1f));
    //    Fly.Append(Sleigh.transform.DOJump(p3.transform.position, 1f, 1, 1f));
    //    Fly.Join(Sleigh.transform.DORotate(p3.transform.rotation.eulerAngles, 1f));
    //}
}
