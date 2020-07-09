using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBackButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonOnClick()
    {
        //GameObject.Find("ApartButton").GetComponent<ClickApartButton>().backApart();
        GameObject.Find("Main Camera").GetComponent<TouchMovement>().ExitWatching();
        
    }
}
