using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

public class Manager_Text : MonoBehaviour
{
    //Common
    public int Content_Seq = 0;

    public GameObject UI_Text;
    public GameObject UI_Message;
    public GameObject Panel;

    private Sequence Seq_panel;

    [Header("[ COMPONENT CHECK ]")]
    public GameObject[] UI_Text_array;
    public GameObject[] UI_Message_array;

    void Start()
    {
        Init_UI_text();
        Init_UI_Panel(10f);
    }

    //텍스트 저장
    void Init_UI_text()
    {

        if (UI_Text != null)
        {
            UI_Text_array = new GameObject[UI_Text.transform.childCount];

            for (int i = 0; i < UI_Text.transform.childCount; i++)
            {
                UI_Text_array[i] = UI_Text.transform.GetChild(i).gameObject;
                //Debug.Log(i+"+"+UI_Text_array[i]);
            }
        }

        if (UI_Message != null)
        {
            UI_Message_array = new GameObject[UI_Message.transform.childCount];

            //T - L - E - R - P - Z - F 순서, hide, reveal
            for (int i = 0; i < UI_Message.transform.childCount; i++)
            {
                UI_Message_array[i] = UI_Message.transform.GetChild(i).gameObject;
            }
        }
    }
    public void Init_UI_Panel(float time)
    {
        Seq_panel = DOTween.Sequence().SetAutoKill(false);

        Seq_panel.Append(Panel.transform.DOScale(1, 0.1f).From(0));
        Seq_panel.Append(Panel.transform.DOScale(0, 0.1f).From(1).SetDelay(time));
        //Panel.SetActive(false);
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
        UI_Text_array[Content_Seq].transform.DOScale(1, 1f).From(0).SetEase(Ease.OutElastic);
    }

    public void Active_UI_message(int Number)
    {
        UI_Message_array[Number].SetActive(true);
        UI_Message_array[Number].transform.DOScale(1, 1f).From(0).SetEase(Ease.OutElastic);
    }

    public void Inactive_UI_message(int Number)
    {
        UI_Message_array[Number].SetActive(false);
    }

    public void Inactiveall_UI_message()
    {
        for (int i = 0; i < UI_Message.transform.childCount; i++)
        {
            UI_Message_array[i].SetActive(false);
        }
    }
    public void Active_UI_Panel()
    {
        Panel.SetActive(true);
        Seq_panel.Restart();
    }
    public void Inactive_UI_Panel()
    {
        Panel.SetActive(false);
    }
}
