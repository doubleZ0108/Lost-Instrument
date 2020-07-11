using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchTap : MonoBehaviour
{

    private float touchTime;
    private bool newTouch = false;
    public Text text;

    public bool canApart = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canApart)
        {
            if (Input.GetMouseButtonDown(0))
            {
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

                    if (hitInfo.collider.gameObject.name == "ErHuBox")
                    {
                        //双击模型使模型销毁
                        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)//几个手指碰到屏幕&&刚点击屏幕
                        {
                            if (Input.GetTouch(0).tapCount == 2)
                            {
                                //Destroy(hitInfo.collider.gameObject);
                                Debug.Log("双击");
                                canApart = false;
                            }

                        }
                        //长按
                        if (Input.touchCount == 1)
                        {
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
                                    Debug.Log("长按");
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
        
    }

    public void startApartMode(bool mode)
    {
        canApart = mode;
    }
}