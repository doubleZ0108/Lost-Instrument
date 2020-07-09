using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollLeftAndRightForCameraMove : MonoBehaviour
{
    private float fingerActionSensitivity = Screen.width * 0.05f; //手指动作的敏感度，这里设定为 二十分之一的屏幕宽度.
                                                                  //
    private float fingerBeginX;
    private float fingerBeginY;
    private float fingerCurrentX;
    private float fingerCurrentY;
    private float fingerSegmentX;
    private float fingerSegmentY;
    //
    private int fingerTouchState;
    //
    private int FINGER_STATE_NULL = 0;
    private int FINGER_STATE_TOUCH = 1;
    private int FINGER_STATE_ADD = 2;

    private Vector3 originalPos;   // 相机初始位置
    public Vector3[] aimPositions;  // 在第几个旋转角度下的位置
    public int tmpIndex = 0;  // 目前滑动到的位置
    public float time = 1.5f;
    // Time when the movement started.
    private float startTime;
    


    private bool startMoveCamera = false;
    private bool startRotateCamera = false;



    // move detail
    private Vector3 aimPositionLast;  // 上一次操作移动的位置
    private Vector3 aimPositionNow;  // 这一次操作的目的位置
    private float journeyLength;
    //private float journeyLengthBack; // 回退过程的距离
    //private float journeyLengthFront; // 前进过程的距离

    //private bool moveBack = true; 


    //public GameObject centerObject;
    //private Vector3 centerPosition;
    //public float[] rotateAngles; // 相对于上一个位置移动的角度
    //public Vector3[] rotateAxises; // 旋转轴
    //public float rotateAngleNow;
    //public Vector3 rotateAxisNow;
    //private float angle;


    // Start is called before the first frame update
    void Start()
    {
        fingerActionSensitivity = Screen.width * 0.05f;
        fingerBeginX = 0;
        fingerBeginY = 0;
        fingerCurrentX = 0;
        fingerCurrentY = 0;
        fingerSegmentX = 0;
        fingerSegmentY = 0;

        fingerTouchState = FINGER_STATE_NULL;

        originalPos = transform.position;
        aimPositionLast = aimPositions[tmpIndex];
        aimPositionNow = aimPositions[tmpIndex];

        //centerPosition = centerObject.transform.position;
        //aimPosition = aimObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (fingerTouchState == FINGER_STATE_NULL)
            {
                fingerTouchState = FINGER_STATE_TOUCH;
                fingerBeginX = Input.mousePosition.x;
                fingerBeginY = Input.mousePosition.y;
            }

        }

        if (fingerTouchState == FINGER_STATE_TOUCH)
        {
            fingerCurrentX = Input.mousePosition.x;
            fingerCurrentY = Input.mousePosition.y;
            fingerSegmentX = fingerCurrentX - fingerBeginX;
            fingerSegmentY = fingerCurrentY - fingerBeginY;

        }


        if (fingerTouchState == FINGER_STATE_TOUCH)
        {
            float fingerDistance = fingerSegmentX * fingerSegmentX + fingerSegmentY * fingerSegmentY;

            if (fingerDistance > (fingerActionSensitivity * fingerActionSensitivity))
            {
                toAddFingerAction();
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            fingerTouchState = FINGER_STATE_NULL;
        }



        // move function
        if (startMoveCamera)
        {
            //if (moveBack)
            //{
                //Vector3 aimPosition = aimPositions[tmpIndex-1];
            float distCovered = (Time.time - startTime) * journeyLength / time;
            //float fractionOfJourneyBack = distCovered / journeyLengthBack;
            Debug.Log("journeyLength:" + journeyLength);
            transform.position = Vector3.Lerp(aimPositionLast, aimPositionNow, distCovered);
            if (Vector3.Distance(aimPositionNow, transform.position) < 0.001f)
            {
                //startTime = Time.time;
                transform.position = aimPositionNow;
                //moveBack = false;
            }
            //}
            //else
            //{
            //    //Vector3 aimPosition = aimPositions[tmpIndex];
            //    float distCovered = (Time.time - startTime) * journeyLengthFront / time;
            //    //float fractionOfJourneyFront = distCovered / journeyLengthFront;
            //    transform.position = Vector3.Lerp(originalPos, aimPositionNow, distCovered);
            //    if (transform.position == aimPositionNow)
            //    {
            //        startMoveCamera = false;
            //        //startRotateCamera = true;
            //    }
            //}
        }

        //if (startRotateCamera)
        //{
        //    transform.RotateAround(centerPosition, rotateAxisNow, rotateAngleNow / time * Time.deltaTime);
        //    angle += rotateAngleNow / time * Time.deltaTime;
        //    if ((angle >= rotateAngleNow && rotateAngleNow >= 0) || (angle <= rotateAngleNow && rotateAngleNow < 0))
        //    {
        //        transform.RotateAround(centerPosition, rotateAxisNow, rotateAngleNow - angle);
        //        startRotateCamera = false;

        //    }
        //}

    }


    private void toAddFingerAction()
    {

        fingerTouchState = FINGER_STATE_ADD;

        if (Mathf.Abs(fingerSegmentX) > Mathf.Abs(fingerSegmentY))
        {
            fingerSegmentY = 0;
        }
        else
        {
            fingerSegmentX = 0;
        }

        if (fingerSegmentX == 0)
        {
            if (fingerSegmentY > 0)
            {
                Debug.Log("up");
            }
            else
            {
                Debug.Log("down");
            }
        }
        else if (fingerSegmentY == 0)
        {
            if (fingerSegmentX > 0)
            {
                Debug.Log("right");
                ScrollLeft(false);
                
            }
            else
            {
                Debug.Log("left");
                ScrollLeft(true);
                
                //RightRotate();
            }
        }

    }

    public void ScrollLeft(bool toLeft)
    {

        if ((toLeft && tmpIndex + 1 < aimPositions.Length) || (!toLeft && tmpIndex - 1 >= 0))
        {
            //moveBack = true;
            tmpIndex += toLeft ? 1 : -1;
            startMoveCamera = true;
            aimPositionLast = aimPositionNow;
            aimPositionNow = aimPositions[tmpIndex];
            journeyLength = Vector3.Distance(aimPositionLast, aimPositionNow);
            //journeyLengthFront = Vector3.Distance(originalPos, aimPositionNow);
            startTime = Time.time;

            //rotateAngleNow = toRight ?  rotateAngles[tmpIndex] : -rotateAngleNow;
            //rotateAxisNow = toRight ? rotateAxises[tmpIndex] : rotateAxises[tmpIndex + 1];
            //angle = 0f;
            GameObject.Find("ErHuBox").GetComponent<ScrollLeftAndRightForRotate>().startRotateLeft(toLeft);


        }

        if (!toLeft)
        {
            GameObject.Find("ContentGroup").GetComponent<ContentScript>().rightSwipeCallBack();
        }
        else
        {
            GameObject.Find("ContentGroup").GetComponent<ContentScript>().leftSwipeCallBack();
        }

        //if ((toLeft && tmpIndex + 1 < aimPositions.Length) || (!toLeft && tmpIndex - 1 >= 0))
        //{
        //    tmpIndex += toLeft ? 1 : -1;
        //    this.GetComponent<FollowTarget>().startMove(tmpIndex);
        //}

    }
}
