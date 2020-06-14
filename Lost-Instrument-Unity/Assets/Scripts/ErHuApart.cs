using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 拆分二胡
public class ErHuApart : MonoBehaviour
{
    public GameObject node4;
    public GameObject node6;
    public GameObject node8;
    public GameObject node10;
    public GameObject node12;
    public GameObject node14;
    public GameObject node16;
    private bool apart = false;
    private bool moveOut = true;
    private bool move = false;

    private float distance4 = 0f;
    private float distance6 = 0f;
    private float distance8 = 0f;
    private float distance10 = 0f;
    private float distance12 = 0f;
    private float distance14 = 0f;
    private float distance16 = 0f;
    public float distanceMax4;
    public float distanceMax6;
    public float distanceMax8;
    public float distanceMax10;
    public float distanceMax12;
    public float distanceMax14;
    public float distanceMax16;

    public float moveTime;

    public Vector3 direction4;
    public Vector3 direction6;
    public Vector3 direction8;
    public Vector3 direction10;
    public Vector3 direction12;
    public Vector3 direction14;
    public Vector3 direction16;

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
                distance4 = moveComponent(node4, direction4, distanceMax4 / moveTime, distance4, distanceMax4);
                distance6 = moveComponent(node6, direction6, distanceMax6 / moveTime, distance6, distanceMax6);
                distance8 = moveComponent(node8, direction8, distanceMax8 / moveTime, distance8, distanceMax8);
                distance10 = moveComponent(node10, direction10, distanceMax10 / moveTime, distance10, distanceMax10);
                distance12 = moveComponent(node12, direction12, distanceMax12 / moveTime, distance12, distanceMax12);
                distance14 = moveComponent(node14, direction14, distanceMax14 / moveTime, distance14, distanceMax14);
                distance16 = moveComponent(node16, direction16, distanceMax16 / moveTime, distance16, distanceMax16);
            }
            else
            {
                distance4 = moveComponent(node4, -direction4, distanceMax4 / moveTime, distance4, distanceMax4);
                distance6 = moveComponent(node6, -direction6, distanceMax6 / moveTime, distance6, distanceMax6);
                distance8 = moveComponent(node8, -direction8, distanceMax8 / moveTime, distance8, distanceMax8);
                distance10 = moveComponent(node10, -direction10, distanceMax10 / moveTime, distance10, distanceMax10);
                distance12 = moveComponent(node12, -direction12, distanceMax12 / moveTime, distance12, distanceMax12);
                distance14 = moveComponent(node14, -direction14, distanceMax14 / moveTime, distance14, distanceMax14);
                distance16 = moveComponent(node16, -direction16, distanceMax16 / moveTime, distance16, distanceMax16);
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
            GameObject.Find("ApartButton").GetComponent<ClickApartButton>().reverseApart();
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
