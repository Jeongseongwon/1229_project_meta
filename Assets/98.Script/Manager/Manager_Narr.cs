using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Narr : MonoBehaviour
{
    //Common
    public int Content_Seq = 0;


    public GameObject Audio_Narration;

    [Header("[ COMPONENT CHECK ]")]
    public AudioClip[] Audio_Narration_array;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //������� �ҷ������� ���� �ý��� �����ؼ� ����
    void Init_Audio_narr()
    {
        
    }

    public void Change_Audio_narr()
    {
        
    }

    /*
     *  void Start()
    {
        Audio = GetComponent<AudioSource>();
        Audio.clip = AudioFiles[0];
        Audio.PlayDelayed(FirstNarrationDelay);
        NarrationEnd = Manager_audio.instance.Narration_End;
        NextBtn = GameObject.Find("NextEffect");
        NarrationEnd.PlayDelayed(Audio.clip.length + FirstNarrationDelay);
        Invoke("NextBtnEffect", Audio.clip.length + FirstNarrationDelay);
        Invoke("NextBtnEffect", 5f); //�߰� �򰡿����� ����, �� �κ��� ���� ȿ��
    }

    // Update is called once per frame
    void Update()
    {
        PlayNarrationWithCount();
    }
    void PlayNarrationWithCount()//When NextButton Pressed, Trigger Narration and Effect
    {
        BtnCount = gameObject.GetComponent<Script_controller>().btnCount;
        if (AudioFiles.Length > BtnCount)
        {
            if (PostCount != BtnCount)
            {
                EffectReset();
                Audio.clip = AudioFiles[BtnCount];
                if(Audio.clip!=null)
                {
                    Audio.PlayDelayed(1f);

                    NarrationEnd.PlayDelayed(Audio.clip.length + 1f);
                    Invoke("NextBtnEffect", Audio.clip.length + 1f);
                    Prev_Status = true;

                }
            }
        }
        PostCount = BtnCount;
    }
    public void EffectReset()// Reset All Effect and Delay
    {
        CancelInvoke();
        NextBtn.GetComponent<Animation>().Stop();
        NextBtn.SetActive(false);
        Audio.Stop();
        NarrationEnd.Stop();
        Invoke("NextBtnEffect", 10f); //�߰� �򰡿����� ����
    }
    void NextBtnEffect()// UI Effect
    {
        NextBtn.SetActive(true);
        NextBtn.GetComponent<Animation>().Play("NextBtnEffect");
    }
    */
}
