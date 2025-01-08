using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicked_fruit : MonoBehaviour,IPointerClickHandler
{
    public int Number_fruit;
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
        //Debug.Log("CLECKED");
        Manager_Seq_3.instance.Click(Number_fruit, this.gameObject);
    }
}
