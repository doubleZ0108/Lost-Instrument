using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 拆分二胡
public class GuZhengApart : MonoBehaviour
{
    public GameObject ZhengWei_1;
    public GameObject ZhengXian_2;
    public GameObject ZhengJia_3;
    public GameObject ZhengShou_4;
    public GameObject DiBan_5;
    public GameObject MianBan_6;
    private bool apart = false;
    private bool moveOut = true;
    private bool move = false;

    private float distance1 = 0f;
    private float distance2 = 0f;
    private float distance3 = 0f;
    private float distance4 = 0f;
    private float distance5 = 0f;
    private float distance6 = 0f;
    public float distanceMax1 = 0f;
    public float distanceMax2 = 0f;
    public float distanceMax3 = 0f;
    public float distanceMax4 = 0f;
    public float distanceMax5 = 0f;
    public float distanceMax6 = 0f;

    public float moveTime;

    public Vector3 direction1;
    public Vector3 direction2;
    public Vector3 direction3;
    public Vector3 direction4;
    public Vector3 direction5;
    public Vector3 direction6;

    // Start is called before the first frame update
    void Start()
    {
        apart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Debug.Log("Start move allocation.");
            // 最终目的：移动时间相同（moveRate和distanceMax成正比）
            if (moveOut)
            {
                distance1 = moveComponent(ZhengWei_1, direction1, distanceMax1 / moveTime, distance1, distanceMax1);
                distance2 = moveComponent(ZhengXian_2, direction2, distanceMax2 / moveTime, distance2, distanceMax2);
                distance3 = moveComponent(ZhengJia_3, direction3, distanceMax3 / moveTime, distance3, distanceMax3);
                distance4 = moveComponent(ZhengShou_4, direction4, distanceMax4 / moveTime, distance4, distanceMax4);
                distance5 = moveComponent(DiBan_5, direction5, distanceMax5 / moveTime, distance5, distanceMax5);
                distance6 = moveComponent(MianBan_6, direction6, distanceMax6 / moveTime, distance6, distanceMax6);
                if (distance1 == 0 && distance2 == 0)
                {
                    GameObject.Find("ApartButton").GetComponent<ClickApartButton>().reverseApart();
                }
            }
            else
            {
                distance1 = moveComponent(ZhengWei_1, -direction1, distanceMax1 / moveTime, distance1, distanceMax1);
                distance2 = moveComponent(ZhengXian_2, -direction2, distanceMax2 / moveTime, distance2, distanceMax2);
                distance3 = moveComponent(ZhengJia_3, -direction3, distanceMax3 / moveTime, distance3, distanceMax3);
                distance4 = moveComponent(ZhengShou_4, -direction4, distanceMax4 / moveTime, distance4, distanceMax4);
                distance5 = moveComponent(DiBan_5, -direction5, distanceMax5 / moveTime, distance5, distanceMax5);
                distance6 = moveComponent(MianBan_6, -direction6, distanceMax6 / moveTime, distance6, distanceMax6);
                if (distance1 == 0 && distance2 == 0) {
                    GameObject.Find("ApartButton").GetComponent<ClickApartButton>().reverseApart();
                }
            }

        }
    }

    float moveComponent(GameObject obj, Vector3 direction, float moveRate, float distance, float distanceMax)
    {
        //Debug.Log("move");
        obj.transform.Translate(direction * moveRate * Time.deltaTime);
        distance += Mathf.Abs(moveRate * Time.deltaTime);
        Debug.Log("---GuZhengmoveDistance=" + distance.ToString());
        Debug.Log("---GuZhengMAXmoveDistance=" + distanceMax.ToString());
        if (distance - distanceMax > 0 || !move)
        {
            Debug.Log("stop move.");
            transform.Translate(-direction * (distance - distanceMax));
            //Debug.Log("STOP!!");
            move = false;
            moveOut = !moveOut;
            apart = !apart;
            //GameObject.Find("ApartButton").GetComponent<ClickApartButton>().reverseApart();
            distance = 0;
        }
        return distance;
    }

    public void moveOutApart()
    {
        move = true;
        moveOut = true;
    }
    public void moveInApart()
    {
        Debug.Log("Move In Apart.");
        move = true;
        moveOut = false;
    }
}
