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

    //p0 그룹 7개
    //p1 그룹 5개
    //p2 그룹 5개

    //과일 애니메이션 관리
    //0~6번
    private Sequence[] Reveal_f_seq;

    //대신 transform을 전부 가지고 있고 그걸 조합해서 사용하는 걸로
    //과일 어레이는 언제든지 달라질 수 있음

    private Transform[] p0; //초기 위치 7개
    private Transform[] p1; //박스 안 위치 5개
    private Transform[] p2; //박스 밖 위치 5개


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

    void Init_Seq_fruit()
    {
        Fruit_pos_array = new GameObject[Fruit_position.transform.childCount];
        Main_Fruit_array = new GameObject[Main_Fruit.transform.childCount];
        Reveal_f_seq = new Sequence[Fruit_position.transform.childCount];

        //hide, reveal 시퀀스 각각 한번씩 초기화
        //메인 이동 동물 할당
        for (int i = 0; i < Fruit_position.transform.childCount; i++)
        {
            Fruit_pos_array[i] = Fruit_position.transform.GetChild(i).gameObject;
            Main_Fruit_array[i] = Main_Fruit.transform.GetChild(i).gameObject;

            Reveal_f_seq[i] = DOTween.Sequence().SetAutoKill(false);

            Transform p0 = Fruit_pos_array[i].transform.GetChild(0).transform;
            Transform p1 = Fruit_pos_array[i].transform.GetChild(1).transform;
            Transform p2 = Fruit_pos_array[i].transform.GetChild(2).transform;
            Transform p3 = Fruit_pos_array[i].transform.GetChild(3).transform;


            //애니메이션을 조금 더 자연스럽게 만들 필요는 있음


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
        fruit.transform.DOJump(p1[Num].position, 1f, 1, 1f);
    }
    public void Inactive_Seq_fruit(GameObject fruit)
    {
        //색깔에 맞지 않는 과일일 경우
        //애니메이션 재생하고 파괴함
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
