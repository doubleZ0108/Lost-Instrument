using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickInstru : MonoBehaviour
{
    public bool isWatching = false;
    private RaycastHit ObjHit;
    private Ray CustomRay;
    private int position = 0;

    // Start is called before the first frame update
    void Start()
    {
        position = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isWatching && Input.GetMouseButtonDown(0))
        {
            #if UNITY_ANDROID || UNITY_IPHONE
            //移动端判断如下
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            #else
            //PC端判断如下
            if (EventSystem.current.IsPointerOverGameObject())
            #endif
            {
                //Debug.Log("当前点击在UI上" + EventSystem.current.currentSelectedGameObject);
            }
            else
            {
                //从摄像机发出一条射线,到点击的坐标
                CustomRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                //显示一条射线，只有在scene视图中才能看到
                Debug.DrawLine(CustomRay.origin, ObjHit.point, Color.red, 2);
                if (Physics.Raycast(CustomRay, out ObjHit, 100))
                {
                    if (ObjHit.collider.gameObject != null)
                    {
                        //Debug.Log("Click Object:" + ObjHit.collider.gameObject.name);
                        if("ErHuBox" == ObjHit.collider.gameObject.name && position == 0)
                        {
                            Debug.Log("ErHuJump!");
                            //SceneManager.LoadScene("ErHuScene");

                            // 访问单个模型
                            WatchInstru("Erhu");
                        }
                        if ("PiPaBox" == ObjHit.collider.gameObject.name && position == 1)
                        {
                            Debug.Log("PiPaJump!");
                            //SceneManager.LoadScene("ErHuScene");

                            // 访问单个模型
                            WatchInstru("PiPa");
                        }
                        if ("GuZhengBox" == ObjHit.collider.gameObject.name && position == 2)
                        {
                            Debug.Log("GuZhengJump!");
                            //SceneManager.LoadScene("ErHuScene");

                            // 访问单个模型
                            WatchInstru("GuZheng");
                        }
                        if ("YangQinBox" == ObjHit.collider.gameObject.name && position == 3)
                        {
                            Debug.Log("YangQinJump!");
                            //SceneManager.LoadScene("ErHuScene");

                            // 访问单个模型
                            WatchInstru("YangQin");
                        }

                    }
                }
                else
                {
                    //Debug.Log("Click Null");
                }
            }
        }
    }

    //void onClick() {
    //    SceneManager.LoadScene("ErHuScene");
    //}
    public void ExitWatching()
    {
        isWatching = false;
    }

    public void WatchInstru(string name)
    {
        isWatching = true;
        //Debug.Log("Watch" + name + "!");
        //this.GetComponent<TouchMovement>().EnterWatching();
        //Debug.Log("WatchErHu!!!");
        GameObject.Find("Main Camera").GetComponent<TouchMovement>().EnterWatching();
        
    }

    public void changePos(int pos)
    {
        position = pos;
    }
}
