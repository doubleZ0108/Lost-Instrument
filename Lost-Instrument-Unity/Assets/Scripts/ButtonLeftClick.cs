using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLeftClick : MonoBehaviour
{
    private int position = 0;
    private ArrayList names = new ArrayList();
    private bool rotate = false;
    // Start is called before the first frame update
    void Start()
    {
        names.Add("二胡");
        names.Add("琵琶");
        names.Add("古筝");
        names.Add("杨琴");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonOnClick()
    {
        if (!rotate)
        {
            rotate = true;
            GameObject.Find("ErHuBox").SendMessage("LeftButton");
            GameObject.Find("YangQinBox").SendMessage("LeftButton");
            GameObject.Find("PiPaBox").SendMessage("LeftButton");
            GameObject.Find("GuZhengBox").SendMessage("LeftButton");
            position = (position + 3) % 4;
            Text text = GameObject.Find("NameChoice").GetComponent<Text>();
            text.text = names[position].ToString();
            
        }
        
    }

    public void RightButtonClick()
    {
        if (!rotate)
        {
            rotate = true;
            position = (position + 1) % 4;
            Text text = GameObject.Find("NameChoice").GetComponent<Text>();
            text.text = names[position].ToString();
            //rotate = true;
        }
    }

    public void stopRotate()
    {
        rotate = false;
    }
}
