using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 模型在轮播台中的旋转
public class SelectRotate : MonoBehaviour
{
    public GameObject center;
    public int position;
    public float rotateSpeed;
    public int direction = 0;  //0向右，1向左
    public bool rotate;  //是否开始旋转
    private float angle = 30.0f;
    private float achieveAngle = 0f;
    private float actualApeed;


    // Start is called before the first frame update
    void Start()
    {
        //rotate = false;
        //if (direction == 0)
        //{
        //    rotateSpeed = -rotateSpeed;
        //}
        //if (position == 0 || position == 3)
        //{
        //    rotateSpeed *= 3.85f;
        //}
    }



    // Update is called once per frame
    void Update()
    {
        //if (direction == 0)
        //{
        //    rotateSpeed = -Mathf.Abs(rotateSpeed);
        //}
        //else
        //{
        //    rotateSpeed = Mathf.Abs(rotateSpeed);
        //}

        if (angle <= achieveAngle)
        {
            Debug.Log("angle" + position.ToString() + ":" + angle.ToString());
            Debug.Log("achieveAngle" + position.ToString() + ":" + achieveAngle.ToString());

            rotate = false;
            GameObject.Find("Main Camera").SendMessage("stopRotate");

            if (direction == 0)
            {
                position = (position + 1) % 4;
                transform.RotateAround(center.transform.position, Vector3.up, achieveAngle - angle);
            }
            else
            {
                position = (position + 3) % 4;
                transform.RotateAround(center.transform.position, Vector3.up, angle - achieveAngle);
            }
            angle = 30.0f;
            achieveAngle = 0f;
        }

        if (rotate)
        {
            if (direction == 0)
            {
                switch (position)
                {
                    case 0:
                        angle = 90.0f + Mathf.Asin(0.8f) * 180 / Mathf.PI;
                        actualApeed = 3.85f * rotateSpeed;
                        break;
                    case 1:
                        angle = Mathf.Asin(0.6f) * 180 / Mathf.PI;
                        actualApeed = rotateSpeed;
                        break;
                    case 2:
                        angle = Mathf.Asin(0.6f) * 180 / Mathf.PI;
                        actualApeed = rotateSpeed;
                        break;
                    case 3:
                        angle = 90.0f + Mathf.Asin(0.8f) * 180 / Mathf.PI;
                        actualApeed = 3.85f * rotateSpeed;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (position)
                {
                    case 0:
                        angle = 90.0f + Mathf.Asin(0.8f) * 180 / Mathf.PI;
                        actualApeed = 3.85f * rotateSpeed;
                        break;
                    case 1:
                        angle = 90.0f + Mathf.Asin(0.8f) * 180 / Mathf.PI;
                        actualApeed = 3.85f * rotateSpeed;
                        break;
                    case 2:
                        angle = Mathf.Asin(0.6f) * 180 / Mathf.PI;
                        actualApeed = rotateSpeed;
                        break;
                    case 3:
                        angle = Mathf.Asin(0.6f) * 180 / Mathf.PI;
                        actualApeed = rotateSpeed;
                        break;
                    default:
                        break;
                }
            }
            
            achieveAngle += Mathf.Abs(actualApeed) * Time.deltaTime;
            transform.RotateAround(center.transform.position, Vector3.up, actualApeed * Time.deltaTime);
        }
    }


    public void RightButton()
    {
        if (!rotate)
        {
            rotate = true;
            direction = 0;
            rotateSpeed = -Mathf.Abs(rotateSpeed);
        }
    }

    public void LeftButton()
    {
        if (!rotate)
        {
            rotate = true;
            direction = 1;
            rotateSpeed = Mathf.Abs(rotateSpeed);
        }
    }
}
