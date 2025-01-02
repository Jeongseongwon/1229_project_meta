using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Text : MonoBehaviour
{
    //Common
    public int Content_Seq = 0;

    public GameObject UI_Text;

    private Animation Anim;

    [Header("[ COMPONENT CHECK ]")]
    public GameObject[] UI_Text_array;

    void Start()
    {
        Anim = UI_Text.GetComponent<Animation>();
        Init_UI_text();
    }

    //텍스트 저장
    void Init_UI_text()
    {
        UI_Text_array = new GameObject[UI_Text.transform.childCount];

        for (int i = 0; i < UI_Text.transform.childCount; i++)
        {
           UI_Text_array [i] = UI_Text.transform.GetChild(i).gameObject;
           //Debug.Log(i+"+"+UI_Text_array[i]);
        }
    }
    
    //텍스트 활성화, 이전 텍스트 비활성화, 애니메이션 재생
    public void Change_UI_text(int Number_seq)
    {
        Content_Seq = Number_seq;
        if (Content_Seq != 0)
        {
            UI_Text_array[Content_Seq - 1].SetActive(false);
        }
        UI_Text_array[Content_Seq].SetActive(true);
        Anim.Play("UI_T_popup");
    }
}
