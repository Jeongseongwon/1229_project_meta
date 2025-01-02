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

    //������� �ҷ������� ���� �ý��� �����ؼ� ����
    //�ϴ� �Ͻ������� inspector â���� �ٷ� Ȱ���ϴ� ������ �����Ͽ���
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