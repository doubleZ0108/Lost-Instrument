using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //摄像机距离
    public float distance = 0f;
    //缩放系数
    public float scaleFactor = 1f;


    public float maxDistance = 30f;
    public float minDistance = 5f;


    //记录上一次手机触摸位置判断用户是在左放大还是缩小手势
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;


    private Vector2 lastSingleTouchPosition;

    private Vector3 m_CameraOffset;
    private Camera m_Camera;

    public bool useMouse = true;

    //定义摄像机可以活动的范围
    public float xMin = -100;
    public float xMax = 100;
    public float zMin = -100;
    public float zMax = 100;

    //这个变量用来记录单指双指的变换
    private bool m_IsSingleFinger;

    public Vector3 originalCameraPos;
    private Vector3 tmpCameraPos;


    public bool startBackPos = false;
    public bool startWatch = false;

    public float startTime;
    public float moveSpeed = 5f;
    private float journeyLength;



    //初始化游戏信息设置
    void Start()
    {
        m_Camera = this.GetComponent<Camera>();
        m_CameraOffset = m_Camera.transform.position;

        originalCameraPos = this.transform.position;
    }

    void Update()
    {
        //判断触摸数量为单点触摸
        //if (Input.touchCount == 1)
        //{
        //    if (Input.GetTouch(0).phase == TouchPhase.Began || !m_IsSingleFinger)
        //    {
        //        //在开始触摸或者从两字手指放开回来的时候记录一下触摸的位置
        //        lastSingleTouchPosition = Input.GetTouch(0).position;
        //    }
        //    if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //    {
        //        MoveCamera(Input.GetTouch(0).position);
        //    }
        //    m_IsSingleFinger = true;

        //}
        //else

        if (startWatch && Input.touchCount > 1)
        {
            //当从单指触摸进入多指触摸的时候,记录一下触摸的位置
            //保证计算缩放都是从两指手指触碰开始的
            if (m_IsSingleFinger)
            {
                oldPosition1 = Input.GetTouch(0).position;
                oldPosition2 = Input.GetTouch(1).position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                ScaleCamera();
            }

            m_IsSingleFinger = false;
        }


        //用鼠标的
        //if (useMouse)
        //{
        //    distance -= Input.GetAxis("Mouse ScrollWheel") * scaleFactor;
        //    distance = Mathf.Clamp(distance, minDistance, maxDistance);
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        lastSingleTouchPosition = Input.mousePosition;
        //        Debug.Log("GetMouseButtonDown:" + lastSingleTouchPosition);
        //    }
        //    if (Input.GetMouseButton(0))
        //    {
        //        MoveCamera(Input.mousePosition);
        //    }
        //}

        if (startBackPos)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(tmpCameraPos, originalCameraPos, fractionOfJourney);
            if (transform.position == originalCameraPos)
            {
                startBackPos = false;
                //GameObject.Find("ShowImage").GetComponent<UIShowAndHide>().Show();
            }
        }
    }

    /// <summary>
    /// 触摸缩放摄像头
    /// </summary>
    private void ScaleCamera()
    {
        //计算出当前两点触摸点的位置
        var tempPosition1 = Input.GetTouch(0).position;
        var tempPosition2 = Input.GetTouch(1).position;


        float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
        float lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);

        //计算上次和这次双指触摸之间的距离差距
        //然后去更改摄像机的距离
        distance -= (currentTouchDistance - lastTouchDistance) * scaleFactor * Time.deltaTime;


        //把距离限制住在min和max之间
        distance = Mathf.Clamp(distance, minDistance, maxDistance);


        //备份上一次触摸点的位置，用于对比
        oldPosition1 = tempPosition1;
        oldPosition2 = tempPosition2;
    }


    //Update方法一旦调用结束以后进入这里算出重置摄像机的位置
    private void LateUpdate()
    {
        if (startWatch)
        {
            Vector3 position = m_CameraOffset + m_Camera.transform.forward * -distance;
            m_Camera.transform.position = position;
        }
        
    }


    private void MoveCamera(Vector3 scenePos)
    {
        Vector3 lastTouchPostion = m_Camera.ScreenToWorldPoint(new Vector3(lastSingleTouchPosition.x, lastSingleTouchPosition.y, -1));
        Vector3 currentTouchPosition = m_Camera.ScreenToWorldPoint(new Vector3(scenePos.x, scenePos.y, -1));

        Vector3 v = currentTouchPosition - lastTouchPostion;
        m_CameraOffset += new Vector3(v.x, 0, v.z) * m_Camera.transform.position.y;

        //把摄像机的位置控制在范围内
        m_CameraOffset = new Vector3(Mathf.Clamp(m_CameraOffset.x, xMin, xMax), m_CameraOffset.y, Mathf.Clamp(m_CameraOffset.z, zMin, zMax));
        //Debug.Log(lastTouchPostion + "|" + currentTouchPosition + "|" + v);
        lastSingleTouchPosition = scenePos;
    }

    public void startBack()
    {
        startBackPos = true;
        startTime = Time.time;
        tmpCameraPos = m_Camera.transform.position;
        journeyLength = Vector3.Distance(tmpCameraPos, originalCameraPos);
    }

    public void startWatchMode(bool watch)
    {
        startWatch = watch;
    }
}