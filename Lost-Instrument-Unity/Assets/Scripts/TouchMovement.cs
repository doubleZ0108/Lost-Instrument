using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 轮博台旋转操作，开启、退出单独观察界面逻辑
public class TouchMovement : MonoBehaviour
{
    private int position = 0;
    private ArrayList names = new ArrayList();
    private bool rotate = false;
    private ArrayList objects = new ArrayList();
    public bool isWatching = false;
    public int moveRate = 30;

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

    // Use this for initialization
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

        names.Add("二胡");
        objects.Add(GameObject.Find("ErHuBox"));
        names.Add("琵琶");
        objects.Add(GameObject.Find("PiPaBox"));
        names.Add("古筝");
        objects.Add(GameObject.Find("GuZhengBox"));
        names.Add("杨琴");
        objects.Add(GameObject.Find("YangQinBox"));
    }
    // Update is called once per frame
    void Update()
    {
        if (!isWatching)
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
        }

        
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
                LeftRotate();
                
            }
            else
            {
                Debug.Log("left");
                RightRotate();
            }
        }

    }


    public void LeftRotate()
    {
        if (!rotate)
        {
            rotate = true;
            GameObject.Find("ErHuBox").SendMessage("RightButton");
            GameObject.Find("YangQinBox").SendMessage("RightButton");
            GameObject.Find("PiPaBox").SendMessage("RightButton");
            GameObject.Find("GuZhengBox").SendMessage("RightButton");
            position = (position + 3) % 4;
            GameObject.Find("ShowingRotate").GetComponent<ClickInstru>().changePos(position);
            Text text = GameObject.Find("NameChoice").GetComponent<Text>();
            text.text = names[position].ToString();

        }

    }

    public void RightRotate()
    {
        if (!rotate)
        {
            rotate = true;
            GameObject.Find("ErHuBox").SendMessage("LeftButton");
            GameObject.Find("YangQinBox").SendMessage("LeftButton");
            GameObject.Find("PiPaBox").SendMessage("LeftButton");
            GameObject.Find("GuZhengBox").SendMessage("LeftButton");
            position = (position + 1) % 4;
            GameObject.Find("ShowingRotate").GetComponent<ClickInstru>().changePos(position);
            Text text = GameObject.Find("NameChoice").GetComponent<Text>();
            text.text = names[position].ToString();

        }

    }


    public void stopRotate()
    {
        rotate = false;
    }
    public void ExitWatching()
    { 
        isWatching = false;
        // 进行三个物体，和相机、UI 的移回操作
        this.GetComponent<CameraMove>().startWatchMode(false);

        bool apart = GameObject.Find("ApartButton").GetComponent<ClickApartButton>().apart;
        if (apart)
        {
            if (position == 0)
            {
                GameObject.Find("ErHu_total").GetComponent<ErHuApart>().moveInApart();
            }
            else if (position == 1)
            {
                GameObject.Find("PiPa_total").GetComponent<PiPaApart>().moveInApart();
            }
            else if (position == 2)
            {
                GameObject.Find("GuZheng_total").GetComponent<GuZhengApart>().moveInApart();
            }
            else
            {
                GameObject.Find("YangQin_total").GetComponent<YangQinApart>().moveInApart();
            }

            //GameObject.Find("ApartButton").GetComponent<ClickApartButton>().reverseApart();
        }

        GameObject aimOne = (GameObject)objects[position];
        aimOne.GetComponent<ModelMovementTouch>().ExitWatching();

        aimOne.GetComponent<ModelMovementTouch>().startApartMode(false);
    }
    public void ExitWatchingMove()
    {
        Debug.Log("ExitWatchingMove");
        GameObject aimOne = (GameObject)objects[position];
        aimOne.GetComponent<WatchMove>().rotateBack();

        GameObject rightOne = (GameObject)objects[(position + 1) % 4];
        rightOne.GetComponent<WatchMove>().moving(Vector3.left);
        GameObject backOne = (GameObject)objects[(position + 2) % 4];
        backOne.GetComponent<WatchMove>().moving(Vector3.back);
        GameObject leftOne = (GameObject)objects[(position + 3) % 4];
        leftOne.GetComponent<WatchMove>().moving(Vector3.right);

        // UI消失
        changeButtonShow(false);

        this.GetComponent<WatchMoveCamera>().moving(Vector3.back);
        GameObject.Find("ShowingRotate").GetComponent<ClickInstru>().ExitWatching();
    }


    public void EnterWatching()
    {
        isWatching = true;

        this.GetComponent<CameraMove>().startWatchMode(true);
        // 进行四个物体的移动操作
        // 三个物体的移出相机范围，相机和ui前移
        Debug.Log("isWatching! position=" + position);

        GameObject aimOne = (GameObject)objects[position];
        aimOne.GetComponent<WatchMove>().rotate();

        aimOne.GetComponent<ModelMovementTouch>().startApartMode(true);
        
    }

    public void EnterWatchingMove()
    {
        GameObject rightOne = (GameObject)objects[(position + 1) % 4];
        rightOne.GetComponent<WatchMove>().moving(Vector3.right);
        //rightOne.transform.Translate(Vector3.right * moveRate * Time.deltaTime);
        GameObject backOne = (GameObject)objects[(position + 2) % 4];
        backOne.GetComponent<WatchMove>().moving(Vector3.forward);
        //backOne.transform.Translate(Vector3.back * moveRate * Time.deltaTime);
        GameObject leftOne = (GameObject)objects[(position + 3) % 4];
        leftOne.GetComponent<WatchMove>().moving(Vector3.left);
        //leftOne.transform.Translate(Vector3.back * moveRate * Time.deltaTime);

        // UI显示
        changeButtonShow(true);


        //GameObject.Find("ErHuBox").GetComponent<ModelMovementTouch>().aim = (GameObject)objects[position];
        GameObject aimOne = (GameObject)objects[position];
        aimOne.GetComponent<ModelMovementTouch>().EnterWatching();
        this.GetComponent<WatchMoveCamera>().moving(Vector3.forward);

        
    }


    public int getPosition()
    {
        return position;
    }

    public void changeButtonShow(bool show)
    {
        GameObject backButton = GameObject.Find("BackButton");
        GameObject apartButton = GameObject.Find("ApartButton");
        backButton.GetComponent<CanvasGroup>().alpha = show? 1 : 0;
        backButton.GetComponent<CanvasGroup>().interactable = show;
        backButton.GetComponent<CanvasGroup>().blocksRaycasts = show;
        //apartButton.GetComponent<CanvasGroup>().alpha = show ? 1 : 0;
        //apartButton.GetComponent<CanvasGroup>().interactable = show;
        //apartButton.GetComponent<CanvasGroup>().blocksRaycasts = show;
    }
}
