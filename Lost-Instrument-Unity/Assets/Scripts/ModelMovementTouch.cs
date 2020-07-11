using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;

// 进入观察界面后旋转缩放模型
public class ModelMovementTouch : MonoBehaviour
{
    public bool isWatching = false;
    public bool exitWatching = false;
    public GameObject aim;

    private Transform trans;
    private Quaternion targetAngels;

    private float ori_x;
    private float ori_y;
    private float ori_z;

    private Touch oldTouch1;  //上次触摸点1(手指1)
    private Touch oldTouch2;  //上次触摸点2(手指2)

    public float speed = 2;

    private bool _mouseDown = false;

    private Vector3 rotateBack;
    private float rotateUpDown;
    private float rotateLeftRight;


    private float touchTime;
    private bool newTouch = false;
    //public Text text;

    public bool canApart = false;

    void Update()
    {
        if (isWatching)
        {
            //    //没有触摸，就是触摸点为0
            //    if (Input.touchCount <= 0)
            //    {
            //        return;
            //    }

            //    //单点触摸， 水平上下旋转
            //    if (1 == Input.touchCount)
            //    {
            //        Touch touch = Input.GetTouch(0);
            //        Vector2 deltaPos = touch.deltaPosition;
            //        Debug.Log("Rotate");
            //        aim.transform.Rotate(Vector3.down * deltaPos.x, Space.World);//绕Y轴进行旋转
            //                                                                 //transform.Rotate(Vector3.right * deltaPos.y, Space.World);//绕X轴进行旋转，下面我们还可以写绕Z轴进行旋转
            //    }

            //    //多点触摸, 放大缩小
            //    Touch newTouch1 = Input.GetTouch(0);
            //    Touch newTouch2 = Input.GetTouch(1);
            //    //第2点刚开始接触屏幕, 只记录，不做处理
            //    if (newTouch2.phase == TouchPhase.Began)
            //    {
            //        oldTouch2 = newTouch2;
            //        oldTouch1 = newTouch1;
            //        return;
            //    }

            //    //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型
            //    float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            //    float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
            //    //两个距离之差，为正表示放大手势， 为负表示缩小手势
            //    float offset = newDistance - oldDistance;
            //    //放大因子， 一个像素按 0.01倍来算(100可调整)
            //    float scaleFactor = offset / 100f;
            //    Vector3 localScale = aim.transform.localScale;
            //    Vector3 scale = new Vector3(localScale.x + scaleFactor,
            //                                localScale.y + scaleFactor,
            //                                localScale.z + scaleFactor);

            //    //在什么情况下进行缩放
            //    if (scale.x >= 0.05f && scale.y >= 0.05f && scale.z >= 0.05f)
            //    {
            //        Debug.Log("大小");
            //        aim.transform.localScale = scale;
            //    }

            //    //记住最新的触摸点，下次使用
            //    oldTouch1 = newTouch1;
            //    oldTouch2 = newTouch2;
            //}
            //Debug.Log("rotate!!!!!");
            if (Input.GetMouseButtonDown(0))
                _mouseDown = true;
            else if (Input.GetMouseButtonUp(0))
                _mouseDown = false;


            if (_mouseDown)
            {
                float fMouseX = Input.GetAxis("Mouse X");
                float fMouseY = Input.GetAxis("Mouse Y");
                transform.Rotate(Vector3.up, -fMouseX * speed, Space.World);
                transform.Rotate(Vector3.right, fMouseY * speed, Space.World);
                //rotateUpDown += -fMouseX * speed;
                //rotateLeftRight += fMouseY * speed;
                
                rotateBack += Vector3.up * (-fMouseX) * speed + Vector3.right * fMouseY * speed;
                //Debug.Log("========_mouseDown:rotateBack:" + rotateBack.x.ToString() + ";" + rotateBack.y.ToString() + ";" + rotateBack.z.ToString());


                // 双击 && 长按
                //从相机发出一条射线
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    //if (hitInfo.collider.gameObject.name == "AiXi(Clone)")
                    //{
                    //    if (Input.GetMouseButtonDown(0))
                    //    {
                    //        newTouch = true;
                    //        touchTime = Time.time;
                    //        Debug.Log(newTouch);
                    //    }
                    //     if(Input.GetMouseButtonUp(0))
                    //    {
                    //        newTouch = false;
                    //        Debug.Log(newTouch);
                    //    }
                    //}

                    if (hitInfo.collider.gameObject.name == "ErHuBox" || hitInfo.collider.gameObject.name == "PiPaBox" || hitInfo.collider.gameObject.name == "GuZhengBox" || hitInfo.collider.gameObject.name == "YangQinBox")
                    {
                        //Debug.Log("双击or长按");
                        //双击模型使模型销毁
                        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)//几个手指碰到屏幕&&刚点击屏幕
                        {
                            if (Input.GetTouch(0).tapCount == 2)
                            {
                                //Destroy(hitInfo.collider.gameObject);
                                //Debug.Log("双击");
                                GameObject.Find("Main Camera").GetComponent<DemoScript>().GoFade();
                                canApart = false;
                            }

                        }
                        //长按
                        Debug.Log("Input.touchCount="+ Input.touchCount);
                        if (Input.touchCount == 1)
                        {
                            //Debug.Log("长按=");
                            Touch touch = Input.GetTouch(0);
                            if (touch.phase == TouchPhase.Began)
                            {
                                newTouch = true;
                                touchTime = Time.time;
                            }
                            else if (touch.phase == TouchPhase.Stationary)//手指触摸屏幕但没有移动
                            {
                                if (newTouch == true && Time.time - touchTime > 1f)
                                {
                                    newTouch = false;
                                    //Destroy(hitInfo.collider.gameObject);
                                    //Debug.Log("长按");
                                    GameObject.Find("ApartButton").GetComponent<ClickApartButton>().ButtonOnClick();
                                    canApart = false;
                                }
                            }
                            else
                            {
                                newTouch = false;
                            }
                        }
                    }


                }
            }

        }



        if (exitWatching)
        {
            //transform.Rotate(Vector3.down, rotateUpDown, Space.World);
            //transform.Rotate(Vector3.left, rotateLeftRight, Space.World);
            //  用 slerp 进行插值平滑的旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngels, speed * Time.deltaTime);
            // 当初始角度跟目标角度小于1,将目标角度赋值给初始角度,让旋转角度是我们需要的角度
            if (Quaternion.Angle(targetAngels, transform.rotation) < 1)
            {
                transform.rotation = targetAngels;
                GameObject.Find("Main Camera").GetComponent<TouchMovement>().ExitWatchingMove();
                exitWatching = false;
            }
            
        }
    }

    public void ExitWatching()
    {
        isWatching = false;
        exitWatching = true;
        //float x = ori_x - transform.rotation.x;
        //Debug.Log("rota_x:" + transform.rotation.x.ToString());
        //float y = ori_y - transform.localRotation.y;
        //float z = ori_z - transform.localRotation.z;
        //rotateBack = new Vector3(x, y, z);
        Debug.Log("rotateBack" + rotateBack.x.ToString() + ";" + rotateBack.y.ToString() + ";" + rotateBack.z.ToString());

    }
    public void EnterWatching()
    {
        isWatching = true;
        rotateBack = new Vector3(0, 0, 0);
        trans = transform;
        targetAngels = trans.rotation;
        //ori_x = transform.localRotation.x;
        //ori_y = transform.localRotation.y;
        //ori_z = transform.localRotation.z;

        //rotateUpDown = 0f;
        //rotateLeftRight = 0f;

        //Debug.Log("ori_x:" + ori_x.ToString());
    }


    public void startApartMode(bool mode)
    {
        canApart = mode;
    }
}
