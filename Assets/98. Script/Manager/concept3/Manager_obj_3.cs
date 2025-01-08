using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Manager_obj_3 : MonoBehaviour
{

    public static Manager_obj_3 instance = null;
    // Start is called before the first frame update

    //Camera
    public GameObject Main_Camera;
    public GameObject Camera_position;

    //Fruit
    public GameObject Main_Fruit;
    public GameObject Fruit_position;

    public GameObject Box;
    public GameObject [] Plate_fruit;
    
    //바구니 갯수 정해놓음
    //그리고 해당 바구니가 메인 바구니가 된다면 해당 바구니의 p0, p1, p2를 불러옴
    //바구니는 총 5개
    //해당 바구니 순서대로 채워놓고 그대로 활용하기로함
    //만약에 게임을 다시 시작해야하면?
    //다시하기 누르면 씬을 다시 로드?
    //그건 좀 그렇고 그냥 전부 다 비우고 다시 게임 시작하도록 해야지

    //Eventsystem
    public GameObject Eventsystem;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
    }
    public GameObject GetPlatefruit(int num)
    {
        return Box.transform.GetChild(num).gameObject;
    }
}
