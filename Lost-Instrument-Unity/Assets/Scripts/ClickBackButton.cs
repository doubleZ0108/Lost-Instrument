using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBackButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonOnClick()
    {
        GameObject.Find("Main Camera").GetComponent<TouchMovement>().ExitWatching();
    }
}
