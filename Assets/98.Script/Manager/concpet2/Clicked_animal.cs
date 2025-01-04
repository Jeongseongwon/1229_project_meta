using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicked_animal : MonoBehaviour,IPointerClickHandler
{
    public int Number_animal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLECKED");
        Manager_Seq_2.instance.animal_button(Number_animal);
        //해당하는 동물이 무슨 동물인지 같이 넘김
        //그리고 seq 스크립트에서 해당하는 함수도 호출함
    }
}
