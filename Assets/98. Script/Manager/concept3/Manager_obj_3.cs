using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Manager_obj_3 : MonoBehaviour
{

    public static Manager_obj_3 instance = null;
    // Start is called before the first frame update

    //Camera
    public GameObject Main_Camera;
    public GameObject Camera_position;

    //Fruit
    public GameObject Fruit_position;

    //public GameObject Main_Box;
    public GameObject Main_Box;
    public GameObject Box_position;

    public GameObject [] Plate_fruit;


    public GameObject[] Fruit_prefabs;
    public Sprite[] Fruit_text;
    public AudioClip[] Fruit_narration;

    //Eventsystem
    public GameObject Eventsystem;


    //기능 테스트 시작
    //바구니 및 과일 정상적으로 전부 나오는지 테스트

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

    //메인 박스도 갈아끼우는게 필요함
    //public GameObject GetPlatefruit(int num)
    //{
    //    return Box_groups.transform.GetChild(num).gameObject;
    //}
}
