using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickDescriptionButtonGuZheng : MonoBehaviour
{
    public bool showDes = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonClick()
    {
        GameObject aim = GameObject.Find("DescriptionScrollView");
        aim.GetComponent<CanvasGroup>().alpha = showDes? 0 : 1;
        aim.GetComponent<CanvasGroup>().interactable = !showDes;
        aim.GetComponent<CanvasGroup>().blocksRaycasts = !showDes;

        Debug.Log("showDes:" + showDes);

        Text text = this.transform.Find("Text").GetComponent<Text>();
        Debug.Log("text:" + text.text);
        text.text = showDes ? "x" : "+";

        showDes = !showDes;
    }
}
