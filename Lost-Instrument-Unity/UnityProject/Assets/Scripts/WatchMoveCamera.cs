using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchMoveCamera : MonoBehaviour
{
    public float moveRate = 0.01f;
    public bool movingOut = false;
    public bool move = false;

    public float distanceMax = 1.5f;
    private float distance = 0f;

    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
        distanceMax = 2f;
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
        }
    }

    public void moving(Vector3 direction)
    {
        this.direction = direction;
        move = true;
        //movingOut = false;
    }
}
