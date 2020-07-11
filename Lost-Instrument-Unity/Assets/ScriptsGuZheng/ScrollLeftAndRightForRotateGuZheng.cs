using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollLeftAndRightForRotateGuZheng : MonoBehaviour
{

    private float origionY;                //声明初始的Y轴旋转值
    private Quaternion targetRotation;    //声明旋转目标角度


    public float[] RotateAngles;  // 每个index相对于前一个的旋转角度！！！
    private float RotateAngleNow;

    private int count;                  //声明一个量记录到目标角度需要进行旋转RotateAngle度的个数
                                        // 由于每次旋转转  60(RotateAngle）度，所以从（0，0，0）到（0，120，0）需要旋转两个 60(RotateAngle) 度

    public int index = 0;
    public float time = 3f;
    public float rotateSpeed = 5f;

    public bool rotate = false;


    private void Start()
    {
        origionY = transform.rotation.y;    //获取当前Y轴旋转值赋给origionY
    }

    void Update()
    {


        if (rotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);//利用Slerp插值让物体进行旋转  2是旋转速度 越大旋转越快

            //Debug.Log("targetRotation:" + targetRotation + ";transform.rotation:" + transform.rotation);
            if (Quaternion.Angle(targetRotation, transform.rotation) < 0.001)
            {
                transform.rotation = targetRotation;                        //当物体当前角度与目标角度差值小于1度直接将目标角度赋予物体 让旋转角度精确到我们想要的度数
                rotate = false;
                Debug.Log(Quaternion.Angle(Quaternion.Euler(0,origionY,0) * Quaternion.identity, transform.rotation));
            }
        }
        
        
    }


    public void startRotateLeft(bool toLeft)
    {
        if ((toLeft && index + 1 < RotateAngles.Length) || (!toLeft && index - 1 >= 0))
        {
            index += toLeft ? 1 : -1;
            RotateAngleNow = RotateAngles[index];
            //给旋转目标值赋值，由于只有Y轴动，所以目标值应是  (旋转角(RotateAngle)*需要旋转的个数(count)+origionY(物体初始Y轴旋转角))*Quarternion.identity(四元数的初始值,记住写就行！)

            // 这个东西到底是什么？？？
            //Debug.Log("RotateAngleNow" + RotateAngleNow);
            
            targetRotation = Quaternion.Euler(0, RotateAngleNow + origionY, 0) * Quaternion.identity;

            //Debug.Log("targetRotation" + targetRotation);
            rotate = true;
        }

        Debug.Log("index=" + index);

        if (index == 0)
        {
            GameObject.Find("UpButton").GetComponent<UpButtonClickGuZheng>().showAndHide(true);
        }
        else
        {
            GameObject.Find("UpButton").GetComponent<UpButtonClickGuZheng>().showAndHide(false);
        }
    }
}
