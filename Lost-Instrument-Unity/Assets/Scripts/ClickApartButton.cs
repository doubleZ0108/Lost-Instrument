using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickApartButton : MonoBehaviour
{
    private int position;
    public bool apart = false;
    public Text texture;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonOnClick()
    {
        position = GameObject.Find("Main Camera").GetComponent<TouchMovement>().getPosition();
        if (position == 0)
        {
            //Debug.Log("Start find erhu!");
            if (apart)
            {
                GameObject.Find("ErHu_total").GetComponent<ErHuApart>().moveInApart();
            }
            else
            {
                GameObject.Find("ErHu_total").GetComponent<ErHuApart>().moveOutApart();
            }

        }
        else if (position == 1)
        {
            //Debug.Log("Start find pipa!");
            if (apart)
            {
                GameObject.Find("PiPa_total").GetComponent<PiPaApart>().moveInApart();
            }
            else
            {
                GameObject.Find("PiPa_total").GetComponent<PiPaApart>().moveOutApart();
            }
        }
        else if (position == 2)
        {
            Debug.Log("Start find guzheng!");
            if (apart)
            {
                GameObject.Find("GuZheng_total").GetComponent<GuZhengApart>().moveInApart();
            }
            else
            {
                GameObject.Find("GuZheng_total").GetComponent<GuZhengApart>().moveOutApart();
            }
        }
        else
        {
            Debug.Log("Start find yangqin!");
            if (apart)
            {
                GameObject.Find("YangQin_total").GetComponent<YangQinApart>().moveInApart();
            }
            else
            {
                GameObject.Find("YangQin_total").GetComponent<YangQinApart>().moveOutApart();
            }
        }
    }

    public void reverseApart()
    {
        apart = !apart;
        
        if (apart)
        {

            texture.text = "Comb";
        }
        else
        {

            texture.text = "Apart";
        }
    }

    public void backApart()
    {
        apart = false;
        texture.text = "Apart";
    }
}
