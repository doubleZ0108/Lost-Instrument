using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowTargetGuZheng : MonoBehaviour
{

    public Transform[] charactor;//目标点的参考点
    public Transform aim;
    public float smoothTime = 2.0f;//移动所需的时间，值越小越快移动到目标处
    private Vector3 currentVelocity = Vector3.zero;//当前速度，这个值每次调用SmoothDamp这个函数时被修改
    private Camera mainCamera;//挂载的相机

    public int index = 0;
    public bool move = false;

    void Awake()
    {
        mainCamera = Camera.main;//初始化
    }

    void Start()
    {

    }

    void Update()
    {
        if (move)
        {
            //SmoothDamp方法的调用：在smoothTime时间内从当前点以当前速度移动到目标点
            transform.position = Vector3.SmoothDamp(transform.position, aim.position, ref currentVelocity, smoothTime);
            if (Vector3.Distance(transform.position, aim.position) < 0.01f)
            {
                move = false;
            }
        }
        
    }

    public void startMove(int indexNow)
    {
        
        index = indexNow;
        move = true;
        aim = charactor[index];
        Debug.Log("index:" + index);
    }
}