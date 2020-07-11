using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpButtonClick : MonoBehaviour
{
    public bool isUp;
    public Sprite[] imgs;
    public GameObject aim;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().sprite = imgs[0];
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
            aim.GetComponent<ErHuTotalMove>().moveUpStart();
            //text.text = "Down";
            this.GetComponent<Image>().sprite = imgs[1];
        }
        else
        {
            aim.GetComponent<ErHuTotalMove>().moveDownStart();
            //text.text = "Up";
            this.GetComponent<Image>().sprite = imgs[0];
        }
        isUp = !isUp;

        GameObject.Find("Main Camera").GetComponent<ScrollLeftAndRightForCameraMove>().changeCanScroll();
    }

    public void showAndHide(bool show)
    {
        Debug.Log("Show&Hide");
        this.GetComponent<CanvasGroup>().alpha = show ? 1 : 0;
        this.GetComponent<CanvasGroup>().interactable = show;
        this.GetComponent<CanvasGroup>().blocksRaycasts = show;

        GameObject.Find("BackButton").GetComponent<CanvasGroup>().alpha = show ? 1 : 0;
        GameObject.Find("BackButton").GetComponent<CanvasGroup>().interactable = show;
        GameObject.Find("BackButton").GetComponent<CanvasGroup>().blocksRaycasts = show;

        GameObject.Find("PeopleButton").GetComponent<CanvasGroup>().alpha = show ? 1 : 0;
        GameObject.Find("PeopleButton").GetComponent<CanvasGroup>().interactable = show;
        GameObject.Find("PeopleButton").GetComponent<CanvasGroup>().blocksRaycasts = show;
    }
}
