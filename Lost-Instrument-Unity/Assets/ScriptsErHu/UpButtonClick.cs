using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpButtonClick : MonoBehaviour
{
    public bool isUp;

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
        Text text = this.transform.Find("Text").GetComponent<Text>();

        if (!isUp)
        {
            GameObject.Find("ErHuShow").GetComponent<ErHuTotalMove>().moveUpStart();
            text.text = "Down";
        }
        else
        {
            GameObject.Find("ErHuShow").GetComponent<ErHuTotalMove>().moveDownStart();
            text.text = "Up";
        }
        isUp = !isUp;

    }
}
