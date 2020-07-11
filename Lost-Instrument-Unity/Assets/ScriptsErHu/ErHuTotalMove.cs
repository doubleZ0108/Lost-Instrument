using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErHuTotalMove : MonoBehaviour
{
    private bool moveUp = false;
    private bool moveDown = false;
    private Vector3 originalPos;

    public Vector3 upButtonAimPos;
    public float upButtonSpeed = 10.0F;
    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveUp)
        {
            float distCovered = (Time.time - startTime) * upButtonSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(originalPos, upButtonAimPos, fractionOfJourney);
            if (transform.position == upButtonAimPos)
            {
                moveUp = false;
                GameObject.Find("ShowImage").GetComponent<UIShowAndHide>().Show();
            }
        }
        if(moveDown)
        {
            float distCovered = (Time.time - startTime) * upButtonSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(upButtonAimPos, originalPos, fractionOfJourney);
            if (transform.position == originalPos)
            {
                moveDown = false;
            }
        }
    }

    public void moveUpStart()
    {
        moveUp = true;
        startTime = Time.time;
        journeyLength = Vector3.Distance(originalPos, upButtonAimPos);
        moveDown = false;
    }
    public void moveDownStart()
    {
        GameObject.Find("ShowImage").GetComponent<UIShowAndHide>().Hide();
        moveDown = true;
        startTime = Time.time;
        journeyLength = Vector3.Distance(upButtonAimPos, originalPos);
        moveUp = false;
    }
}
