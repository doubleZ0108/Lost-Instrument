using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public InputField inputField;
    public PageView pageView;
    //public Toggle toggle0;    //代码动态添加监听方法，无需在界面指定
    //public Toggle toggle1;
    //public Toggle toggle2;
    //public Toggle toggle3;

    private void Start()
    {
        //代码动态添加监听方法，无需在界面指定
        //toggle0.onValueChanged.AddListener(OnValueChanged0);
        //toggle1.onValueChanged.AddListener(OnValueChanged1);
        //toggle2.onValueChanged.AddListener(OnValueChanged2);
        //toggle3.onValueChanged.AddListener(OnValueChanged3);
    }

    public void OnButtonDown()
    {
        int index = int.Parse(inputField.text);
        pageView.pageTo(index);
    }



    public void OnValueChanged0(bool check)
    {
        Debug.Log("OnValueChanged0");
        if (check)
        {
            Debug.Log("Page to 0");
            pageView.pageTo(0);
        }
        else
        {
            Debug.Log("Toggle0 isOn false");
        }
    }

    public void OnValueChanged1(bool check)
    {
        Debug.Log("OnValueChanged1");
        if (check)
        {
            Debug.Log("Page to 1");
            pageView.pageTo(1);
        }
        else
        {
            Debug.Log("Toggle1 isOn false");
        }
    }

    public void OnValueChanged2(bool check)
    {
        Debug.Log("OnValueChanged2");
        if (check)
        {
            Debug.Log("Page to 2");
            pageView.pageTo(2);
        }
        else
        {
            Debug.Log("Toggle2 isOn false");
        }
    }

    public void OnValueChanged3(bool check)
    {
        Debug.Log("OnValueChanged3");
        if (check)
        {
            Debug.Log("Page to 3");
            pageView.pageTo(3);
        }
        else
        {
            Debug.Log("Toggle0 isOn false");
        }
    }
}
