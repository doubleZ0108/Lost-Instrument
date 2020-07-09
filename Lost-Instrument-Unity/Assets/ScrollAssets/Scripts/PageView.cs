using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PageView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private ScrollRect rect;
    private float targethorizontal = 0;
    private List<float> posList = new List<float>();//存四张图片的位置(0, 0.333, 0.666, 1) 
    private bool isDrag = true;
    private float startTime = 0;
    private float startDragHorizontal;
    private int curIndex = 0;

    public float speed = 4;      //滑动速度  
    public float sensitivity = 0; 
    //public Toggle[] toggleArray;  //toggle开关
    //public Text curPage;


    void Start()
    {
        rect = GetComponent<ScrollRect>();
        float horizontalLength = rect.content.rect.width - GetComponent<RectTransform>().rect.width;
        var _rectWidth = GetComponent<RectTransform>().rect.width;
        for (int i = 0; i < rect.content.transform.childCount; i++)
        {
            posList.Add(_rectWidth * i / horizontalLength);
        }
        curIndex = 0;
        //toggleArray[0].isOn = true;
        //curPage.text = String.Format("当前页码：0");

    }

    void Update()
    {
        if (!isDrag)
        {
            startTime += Time.deltaTime;
            float t = startTime * speed;
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targethorizontal, t);  //加速滑动效果
            //rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targethorizontal, Time.deltaTime * speed); //缓慢匀速滑动效果

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        //开始拖动
        startDragHorizontal = rect.horizontalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        float posX = rect.horizontalNormalizedPosition;
        int index = 0;
        float offset = Mathf.Abs(posList[index] - posX);  //计算当前位置与第一页的偏移量
        for (int i = 1; i < posList.Count; i++)
        {    //遍历页签，选取偏移量最小的那个页面
            float temp = Mathf.Abs(posList[i] - posX);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }
        }
        curIndex = index;
        targethorizontal = posList[curIndex]; //设置当前坐标，更新函数进行插值  
        isDrag = false;
        startTime = 0;
        //toggleArray[curIndex].isOn = true;
        //curPage.text = String.Format("当前页码：{0}", curIndex.ToString());
    }

    public void pageTo(int index)
    {
        Debug.Log("pageTo......");
        curIndex = index;
        targethorizontal = posList[curIndex]; //设置当前坐标，更新函数进行插值  
        isDrag = false;
        startTime = 0;
        //toggleArray[curIndex].isOn = true;
        //curPage.text = String.Format("当前页码：{0}", curIndex.ToString());
    }
}
