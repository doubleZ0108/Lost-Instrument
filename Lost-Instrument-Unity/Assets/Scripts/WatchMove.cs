using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchMove : MonoBehaviour
{
    public int moveRate = 30;
    public bool movingOut = false;
    public bool move = false;

    public float distanceMax = 10f;
    private float distance = 0f;

    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
        distanceMax = 10;
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
}
