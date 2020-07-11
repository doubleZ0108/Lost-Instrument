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
    private int current = 0;
    string[] textArray = {
        "\n",
        "\n琴杆是二胡的支柱，亦是躯干。不仅起着上连下接的支撑作用，而且对整体振动发音也有一定的影响。制作琴杆的材料有些檀木、乌木或红木。一般用乌木较多。乌木价廉物美，具有一定的抗拉性。是支撑琴弦、供按弦操作的重要支柱。琴杆全长81厘米，直径约为0.55寸（1.83厘米)。顶端为琴头，上部装有两个弦轴，下端插入琴筒。琴头呈弯脖形，也有雕刻成龙头或其他形状的。衡量一把二胡的发音纯净与否与琴杆材料的选择有很大关系，通常把红木视为上品，乌木的也不错，其它木材的就要逊色一等了。选择时除了要仔细辨别琴杆的制作材料外，还要兼顾到质地紧密、木射线细密而均匀、无节、无疤,无明显裂痕,有一定光亮度等。",
        "\n千斤又称千金，对二胡琴弦起固定和切弦作用。对音准也有一定的影响。它是用棉线、丝线、有机玻璃、塑料等材料制成。其形式多种多样，有固定千斤、线绕千斤、金属千斤等。较常用的是线绕千斤。",
        "\n琴弓（俗称弓子）由弓杆和弓毛构成，一把好弓首先要长而直；其次，弓毛以白色马尾毛为上品，且弓毛要多而齐，与鱼尾连接处捆扎要牢固；最后要注意弓杆的节应少而小，粗细适中。全长76~85 厘米，弓杆是支撑弓毛的支架，长度2.4尺（80厘米），用江苇竹（又名幼竹）制作，两端烘烤出弯来，系上马尾，竹子粗的一端在弓的尾部，马尾夹置于两弦之间，用以摩擦琴弦发音。弓毛多为马尾，也有用尼龙丝仿制的。有些简易二胡是用尼龙线来代替弓毛，这种音响效果较差。衡量弓毛能否经久耐磨，主要看弓毛是否排列得整齐平展，长度一致，粗细均匀。好的弓毛要求无断头、无纤柔、无蓬乱缠绞等。",
        "\n琴筒是二胡的重要部分，这通过弓的推拉运动，擦弦后振动琴皮发音的共鸣体。琴筒的质地和形状对音量和音质有直接影响。一般用紫檀木或红木制作。形状有六角形、八角形、圆形、前八角后圆形等，常用的是六角形。琴筒后面镶嵌着一个音窗（一般为雕木花窗），不仅对琴筒起了装饰作用，而且对发音、传音和滤音有一定的好处。琴筒是二胡的共鸣筒，一般用乌木，红木制成（紫檀木很少），也有用花梨木或竹子做的，七十年代并开始使用低发泡（ABS）材料模压成型。其形状主要为六方形，长13厘米，前口直径（对边)8.8厘米。有些地区则制成圆形或八方形。筒腰略细，筒后口敞口或装置开有各种式样花孔的音窗。琴筒起扩大和渲染琴弦振动的作用。",
        "\n琴托是琴身的底托，起着装饰、稳定琴身的作用。有的二胡琴托还装有可调底托，用尼龙扣调节，演奏时更为方便。"
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

        FaceOutGameObject(0);
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
        SwipeCallBack();
    }

    public void rightSwipeCallBack(){
        // -1
        int save_current = current;
        current = current == 0 ? 0 : current - 1;
        if(current != save_current){
            if(current != 0){
                SwipeCallBack();
            } else {
                FaceOutGameObject(fadingSpeed);
            }
        }
    }

    void SwipeCallBack(){
        FaceOutGameObject(fadingSpeed);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn(){
        yield return new WaitForSeconds(fadeDuration);
        textObject.text = textArray[current];
        updateAutoLayout();
        FadeInGameObject(fadingSpeed);
    }

    void FaceOutGameObject(float duration){
        textObject.CrossFadeAlpha(0, duration, true);      // fade out
        imageObject.CrossFadeAlpha(0, duration, true);
        scrollbarImageObject.CrossFadeAlpha(0, duration, true);
        handleObject.CrossFadeAlpha(0, duration, true);
    }

    void FadeInGameObject(float duration){
        textObject.CrossFadeAlpha(1, duration, true);      // fade in
        imageObject.CrossFadeAlpha(1, duration, true);
        scrollbarImageObject.CrossFadeAlpha(1, duration, true);
        handleObject.CrossFadeAlpha(1, duration, true);
    }
}
