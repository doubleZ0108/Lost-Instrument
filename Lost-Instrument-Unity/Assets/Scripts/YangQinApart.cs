using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YangQinApart : MonoBehaviour
{
    public GameObject GaiBan_1;
    public GameObject GongMingXiang_2;
    public GameObject GunZhouBan_3;
    public GameObject MianBan_4;
    public GameObject QinJia_5;
    public GameObject QinMa_6;
    public GameObject QinXian_7;
    public GameObject WeiTiaoGunZhou_8;
    public GameObject XianDing_9;
    private bool apart = false;
    private bool moveOut = true;
    private bool move = false;

    private float distance1 = 0f;
    private float distance2 = 0f;
    private float distance3 = 0f;
    private float distance4 = 0f;
    private float distance5 = 0f;
    private float distance6 = 0f;
    private float distance7 = 0f;
    private float distance8 = 0f;
    private float distance9 = 0f;
    public float distanceMax1 = 0f;
    public float distanceMax2 = 0f;
    public float distanceMax3 = 0f;
    public float distanceMax4 = 0f;
    public float distanceMax5 = 0f;
    public float distanceMax6 = 0f;
    public float distanceMax7 = 0f;
    public float distanceMax8 = 0f;
    public float distanceMax9 = 0f;

    public float moveTime;

    public Vector3 direction1;
    public Vector3 direction2;
    public Vector3 direction3;
    public Vector3 direction4;
    public Vector3 direction5;
    public Vector3 direction6;
    public Vector3 direction7;
    public Vector3 direction8;
    public Vector3 direction9;



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
            if (moveOut)
            {
                distance1 = moveComponent(GaiBan_1, direction1, distanceMax1 / moveTime, distance1, distanceMax1);
                distance2 = moveComponent(GongMingXiang_2, direction2, distanceMax2 / moveTime, distance2, distanceMax2);
                distance3 = moveComponent(GunZhouBan_3, direction3, distanceMax3 / moveTime, distance3, distanceMax3);
                distance4 = moveComponent(MianBan_4, direction4, distanceMax4 / moveTime, distance4, distanceMax4);
                distance5 = moveComponent(QinJia_5, direction5, distanceMax5 / moveTime, distance5, distanceMax5);
                distance6 = moveComponent(QinMa_6, direction6, distanceMax6 / moveTime, distance6, distanceMax6);
                distance7 = moveComponent(QinXian_7, direction7, distanceMax7 / moveTime, distance7, distanceMax7);
                distance8 = moveComponent(WeiTiaoGunZhou_8, direction8, distanceMax8 / moveTime, distance8, distanceMax8);
                distance9 = moveComponent(XianDing_9, direction9, distanceMax9 / moveTime, distance9, distanceMax9);
                if (distance1 == 0 && distance2 == 0)
                {
                    GameObject.Find("ApartButton").GetComponent<ClickApartButton>().reverseApart();
                }
            }
            else
            {
                distance1 = moveComponent(GaiBan_1, -direction1, distanceMax1 / moveTime, distance1, distanceMax1);
                distance2 = moveComponent(GongMingXiang_2, -direction2, distanceMax2 / moveTime, distance2, distanceMax2);
                distance3 = moveComponent(GunZhouBan_3, -direction3, distanceMax3 / moveTime, distance3, distanceMax3);
                distance4 = moveComponent(MianBan_4, -direction4, distanceMax4 / moveTime, distance4, distanceMax4);
                distance5 = moveComponent(QinJia_5, -direction5, distanceMax5 / moveTime, distance5, distanceMax5);
                distance6 = moveComponent(QinMa_6, -direction6, distanceMax6 / moveTime, distance6, distanceMax6);
                distance7 = moveComponent(QinXian_7, -direction7, distanceMax7 / moveTime, distance7, distanceMax7);
                distance8 = moveComponent(WeiTiaoGunZhou_8, -direction8, distanceMax8 / moveTime, distance8, distanceMax8);
                distance9 = moveComponent(XianDing_9, -direction9, distanceMax9 / moveTime, distance9, distanceMax9);
                if (distance1 == 0 && distance2 == 0)
                {
                    GameObject.Find("ApartButton").GetComponent<ClickApartButton>().reverseApart();
                }
            }
        }
    }

    float moveComponent(GameObject obj, Vector3 direction, float moveRate, float distance, float distanceMax)
    {
        Debug.Log("move");
        obj.transform.Translate(direction * moveRate * Time.deltaTime);
        distance += Mathf.Abs(moveRate * Time.deltaTime);
        //Debug.Log("moveDistance=" + distance.ToString());
        //Debug.Log(distance - distanceMax > 0);
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
        move = true;
        moveOut = false;
    }
}
