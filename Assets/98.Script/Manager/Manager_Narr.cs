using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Narr : MonoBehaviour
{
    //Common
    public int Content_Seq = 0;

    private AudioSource Audio;

    [Header("[ COMPONENT CHECK ]")]
    public AudioClip[] Audio_Narration_array;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    //어떤식으로 불러올지는 기존 시스템 참고해서 저장
    //일단 일시적으로 inspector 창에서 바로 활용하는 것으로 구현하였음
    void Init_Audio_narr()
    {

    }

    public void Change_Audio_narr(int Number_seq)
    {

        Content_Seq = Number_seq;
        //if (Content_Seq != 0)
        //{

        //}
        Audio.clip = Audio_Narration_array[Content_Seq];
        Audio.Play();
    }
}