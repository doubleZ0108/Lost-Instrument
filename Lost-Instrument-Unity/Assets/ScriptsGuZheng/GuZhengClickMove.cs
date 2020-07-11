using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuZhengClickMove : MonoBehaviour
{
    public float movementSpeed = 10.0F;
    private float journeyLength;
    private float startTime;

    // 一套动作对应一组数据
    private bool movement1 = false;
    private Vector3 originalPos;
    public Vector3 aimPos1;
    
    // Time when the movement started.

    // Total distance between the markers.

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (movement1)
        {
            float distCovered = (Time.time - startTime) * movementSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(originalPos, aimPos1, fractionOfJourney);
            if (transform.position == aimPos1)
            {
                movement1 = false;
                //GameObject.Find("ShowImage").GetComponent<UIShowAndHide>().Show();
            }
        }
    }

    public void movement()
    {
        movement1 = true;
        startTime = Time.time;
        journeyLength = Vector3.Distance(originalPos, aimPos1);
    }
}
