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
            Debug.Log("Start find erhu!");
            if (apart)
            {
                GameObject.Find("ErHu_total").GetComponent<ErHuApart>().moveInApart();
            }
            else
            {
                GameObject.Find("ErHu_total").GetComponent<ErHuApart>().moveOutApart();
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
}
