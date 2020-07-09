using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentScript : MonoBehaviour
{
    public int lengthOfOnePage = 200;       //一面最大的字数
    public float fadingSpeed = 1.5f;        // 多长时间之后 in / out
    public float fadeDuration = 3.0f;       // xs之后显示

    Text textObject;
    Image imageObject;
    Image scrollbarImageObject;
    Image handleObject;
    ContentSizeFitter textContentSizeFilter;
    private int current;
    string[] textArray = {
        
        "\n琴杆是二胡的支柱，亦是躯干。不仅起着上连下接的支撑作用，而且对整体振动发音也有一定的影响。",
        "\n琴杆是二胡的支柱，亦是躯干。不仅起着上连下接的支撑作用，而且对整体振动发音也有一定的影响。制作琴杆的材料有些檀木、乌木或红木。一般用乌木较多。乌木价廉物美，具有一定的抗拉性。是支撑琴弦、供按弦操作的重要支柱。琴杆全长81厘米，直径约为0.55寸（1.83厘米)。顶端为琴头，上部装有两个弦轴，下端插入琴筒。琴头呈弯脖形，也有雕刻成龙头或其他形状的。衡量一把二胡的发音纯净与否与琴杆材料的选择有很大关系，通常把红木视为上品，乌木的也不错，其它木材的就要逊色一等了。选择时除了要仔细辨别琴杆的制作材料外，还要兼顾到质地紧密、木射线细密而均匀、无节、无疤,无明显裂痕,有一定光亮度等。琴杆是二胡的支柱，亦是躯干。不仅起着上连下接的支撑作用，而且对整体振动发音也有一定的影响。制作琴杆的材料有些檀木、乌木或红木。一般用乌木较多。乌木价廉物美，具有一定的抗拉性。是支撑琴弦、供按弦操作的重要支柱。琴杆全长81厘米，直径约为0.55寸（1.83厘米)。顶端为琴头，上部装有两个弦轴，下端插入琴筒。琴头呈弯脖形，也有雕刻成龙头或其他形状的。衡量一把二胡的发音纯净与否与琴杆材料的选择有很大关系，通常把红木视为上品，乌木的也不错，其它木材的就要逊色一等了。选择时除了要仔细辨别琴杆的制作材料外，还要兼顾到质地紧密、木射线细密而均匀、无节、无疤,无明显裂痕,有一定光亮度等。",
        "\n123234",
        "\n99999999999999999"
    };

    void Start()
    {
        imageObject = this.transform.GetChild(0).gameObject.GetComponent<Image>();
        scrollbarImageObject = this.transform.GetChild(1).gameObject.GetComponent<Image>();
        handleObject = this.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Image>();

        GameObject contentObject = this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        textObject = contentObject.GetComponent<Text>();
        textContentSizeFilter = contentObject.GetComponent<ContentSizeFitter>();
        updateAutoLayout();

        textObject.CrossFadeAlpha(0, 0, true);      // fade out
        imageObject.CrossFadeAlpha(0, 0, true);
        scrollbarImageObject.CrossFadeAlpha(0, 0, true);
        handleObject.CrossFadeAlpha(0, 0, true);
    }


    void updateAutoLayout()
    {
        if(textObject.text.Length > lengthOfOnePage){
            textContentSizeFilter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        } else {
            textContentSizeFilter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        }
    }

    public void leftSwipeCallBack(){
        // +1
        int save_current = current;
        current = current == textArray.Length - 1 ? current : current + 1;
        if(current != save_current){
            SwipeCallBack();
        }
    }

    public void rightSwipeCallBack(){
        // -1
        int save_current = current;
        current = current == 0 ? 0 : current - 1;
        if(current != save_current){
            SwipeCallBack();
        }
    }

    void SwipeCallBack(){
        textObject.CrossFadeAlpha(0, fadingSpeed, true);      // fade out
        imageObject.CrossFadeAlpha(0, fadingSpeed, true);
        scrollbarImageObject.CrossFadeAlpha(0, fadingSpeed, true);
        handleObject.CrossFadeAlpha(0, fadingSpeed, true);

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn(){
        yield return new WaitForSeconds(fadeDuration);
        textObject.text = textArray[current];
        updateAutoLayout();
        textObject.CrossFadeAlpha(1, fadingSpeed, true);      // fade in
        imageObject.CrossFadeAlpha(1, fadingSpeed, true);
        scrollbarImageObject.CrossFadeAlpha(1, fadingSpeed, true);
        handleObject.CrossFadeAlpha(1, fadingSpeed, true);
    }
}
