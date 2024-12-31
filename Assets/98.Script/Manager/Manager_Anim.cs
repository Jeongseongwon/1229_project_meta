using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Anim : MonoBehaviour
{
    //Common
    public int Content_Seq = 0;

    public GameObject Camera;


    [Header("[ COMPONENT CHECK ]")]
    public GameObject[] Camera_array;
    // Start is called before the first frame update
    //카메라 어레이로 전부 저장해놓고
    //순서대로 저장한 다음에 해당하는 번호에 해당하는 카메라를 활성화 비활성화 하는 걸로
    //한대로 해야하므로 그냥 position, rotation 값을 고대로 복사 붙여넣기로 전환하는게 가장 맞을 것 같음

    //해당하는 시퀀스 번호 시작하게 되면 그거 해당하는 애니메이션 바로 실행하고 보는 걸로 하기
    //특히 우리는 구도도 중요하므로 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
