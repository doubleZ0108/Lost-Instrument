using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchMove : MonoBehaviour
{
    public int moveRate = 50;
    public int rotateRate = 100;
    //public bool movingOut = false;
    public bool move = false;
    public bool rotateIn = false;
    public bool rotateOut = false;

    public float distanceMax = 10f;
    public float angleMax = 180f;
    private float distance = 0f;
    private float angle = 0f;

    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
        distanceMax = 10;
        angle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            //if (movingOut)
            //{
            transform.Translate(direction * moveRate * Time.deltaTime);
            distance += Mathf.Abs(moveRate * Time.deltaTime);
            //Debug.Log("moveDistance=" + distance.ToString());
            //Debug.Log(distance - distanceMax > 0);
            if (distance - distanceMax > 0)
            {
                transform.Translate(-direction * (distance - distanceMax));
                //Debug.Log("STOP!!");
                move = false;
                distance = 0;
            }
            //}
            //else
            //{
                //transform.Translate(direction * moveRate * Time.deltaTime);
                //if (distance >= distanceMax)
                //{
                //    transform.Translate(-direction * (distance - distanceMax));
                //    distance = 0;
                //}
            //}
            
        }
        if (rotateIn)
        {
            transform.Rotate(Vector3.up, rotateRate * Time.deltaTime, Space.Self);
            angle += Mathf.Abs(rotateRate * Time.deltaTime);
            //Debug.Log("angle=" + angle.ToString());
            //Debug.Log(angle >= angleMax);
            if (angle >= angleMax)
            {
                Debug.Log("STOP!!");
                transform.Rotate(Vector3.down, angle - angleMax, Space.Self);
                rotateIn = false;
                angle = 0;
            }
        }
        if (rotateOut)
        {
            transform.Rotate(Vector3.down, rotateRate * Time.deltaTime, Space.Self);
            angle += Mathf.Abs(rotateRate * Time.deltaTime);
            if (angle >= angleMax)
            {
                transform.Rotate(Vector3.up, angle - angleMax, Space.Self);
                rotateOut = false;
                angle = 0;
            }
        }

    }

    //public void moveOut(Vector3 direction)
    //{
    //    this.direction = direction;
    //    move = true;
    //    movingOut = true;
    //}
    //public void moveIn(Vector3 direction)
    //{
    //    this.direction = direction;
    //    move = true;
    //    movingOut = false;
    //}
    public void moving(Vector3 direction)
    {
        this.direction = direction;
        move = true;
        //movingOut = false;
    }

    public void rotate()
    {
        rotateIn = true;
    }
    public void rotateBack()
    {
        rotateOut = true;
    }
}
