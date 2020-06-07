using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRightClick : MonoBehaviour
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
            GameObject.Find("ErHuBox").SendMessage("RightButton");
            GameObject.Find("YangQinBox").SendMessage("RightButton");
            GameObject.Find("PiPaBox").SendMessage("RightButton");
            GameObject.Find("GuZhengBox").SendMessage("RightButton");
            position = (position + 1) % 4;
            Text text = GameObject.Find("NameChoice").GetComponent<Text>();
            text.text = names[position].ToString();
        }
    }

    public void LeftButtonClick()
    {
        if (!rotate)
        {
            rotate = true;
            position = (position + 3) % 4;
            Text text = GameObject.Find("NameChoice").GetComponent<Text>();
            text.text = names[position].ToString();
        }
    }

    public void stopRotate()
    {
        rotate = false;
    }
}
