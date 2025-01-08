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
    
    //�ٱ��� ���� ���س���
    //�׸��� �ش� �ٱ��ϰ� ���� �ٱ��ϰ� �ȴٸ� �ش� �ٱ����� p0, p1, p2�� �ҷ���
    //�ٱ��ϴ� �� 5��
    //�ش� �ٱ��� ������� ä������ �״�� Ȱ���ϱ����
    //���࿡ ������ �ٽ� �����ؾ��ϸ�?
    //�ٽ��ϱ� ������ ���� �ٽ� �ε�?
    //�װ� �� �׷��� �׳� ���� �� ���� �ٽ� ���� �����ϵ��� �ؾ���

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
