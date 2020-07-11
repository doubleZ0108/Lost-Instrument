using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PageControllerGuZheng : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    // Use this for initialization

    private ScrollRect rect;
    private float tergetHorizontal = 0;  //滑动的起始坐标
    private bool isDrag = false;
    private List<float> posList = new List<float>();//每页的开始PosX值 第0页就是0开始
    private int currentPageIndex = -1;
    public Action<int> OnPageChanged;
    public RectTransform Content;

    private bool stopMOVE = true;

    public float Smooting = 4;  //滑动速度
    public float Sensitivity = 0;

    private float startTime;

    private float startHorizontal = 0;
    public ToggleGroup ToggleParent;
    public HorizontalLayoutGroup Horizontal;

    int newIndex = -1;
    void Start()
    {
        rect = GetComponent<ScrollRect>();
        RectTransform _rectTras = GetComponent<RectTransform>();
        float tempWidth = (float)Content.childCount * _rectTras.sizeDelta.x + (Content.childCount - 1) * Horizontal.spacing;  //考虑到我们子物体会有间隔
        Content.sizeDelta = new Vector2(tempWidth, _rectTras.rect.height);  //动态改变Content得范围
        //剩余显示的长度  总长度减去 Scrollwell的 长
        float horizontalLrngth = Content.rect.width - _rectTras.rect.width;

        for (int i = 0; i < Content.childCount; i++)
        {
            Toggle smallToggle = Content.GetChild(i).GetComponent<Toggle>();////这里有个坑 就是 每次new 一个  不然闭包 得到得一直是最后一个实例
            smallToggle.group = ToggleParent;
            // smallToggle.onValueChanged.RemoveAllListeners();
            smallToggle.onValueChanged.AddListener(ifselect =>
            { if (ifselect) toggleEvent(smallToggle); }

           );
            if (i == 0)
            {
                //然后开始保存每一个子物体得开始位置  因为要翻页
                posList.Add(0); continue;
            }
            posList.Add(((_rectTras.rect.width + Horizontal.spacing) * i) / horizontalLrngth);
            // print(posList[i]);
        }
    }


    void Update()
    {
        if (!isDrag && !stopMOVE)
        {
            startTime += Time.deltaTime;
            float t = startTime * Smooting;
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, tergetHorizontal, t);
            if (t >= 1)
            {
                stopMOVE = true;
            }
        }
    }

    public void PageTo(int index)
    {
        if (index >= 0 && index < posList.Count)
        {
            rect.horizontalNormalizedPosition = posList[index];
            setPageIndex(index);
            // GetIndex(index);
        }
    }
    public void GetIndex(int index)
    {
        // var toggle = ToggleParent.GetChild(index).GetComponent<Toggle>();
        // toggle.isOn = true;
    }
    void setPageIndex(int index)
    {
        if (currentPageIndex != index)
        {
            currentPageIndex = index;
            if (OnPageChanged != null)
            {
                OnPageChanged(index);
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        isDrag = true;
        //开始拖拽    rect.horizontalNormalizedPosition为滑动值 API可以去看官方
        startHorizontal = rect.horizontalNormalizedPosition;
        //print(rect.horizontalNormalizedPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //  throw new System.NotImplementedException();
        float posX = rect.horizontalNormalizedPosition;//记录放手的 位置

        posX += ((posX - startHorizontal) * Sensitivity);

        posX = posX < 1 ? posX : 1;
        posX = posX > 0 ? posX : 0;

        int index = 0;

        float offset = Mathf.Abs(posList[index] - posX);

        for (int i = 1; i < posList.Count; i++)
        {
            float temp = Mathf.Abs(posList[i] - posX);
            // print("offset =" + offset + " and" + "temp=" + temp);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }


        }

        setPageIndex(index);
        tergetHorizontal = posList[index];
        isDrag = false;
        startTime = 0;
        stopMOVE = false;
    }

    private void toggleEvent(Toggle item)
    {
        switch (item.name)
        {

            case "1":
                print("1");
                break;
            case "2":
                print("2");
                break;
            case "3":
                print("3");
                break;
            case "4":
                print("4");
                break;
            default:
                break;
        }
    }


}
