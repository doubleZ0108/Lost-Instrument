using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentScriptGuZheng : MonoBehaviour
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
        "\n一台古筝的音质取决于面板和琴弦，面板以阳面中段为最佳，利于音质的传导。专业演奏古筝的面板以通纹为最佳，中音区纹理间距5-9厘米，高音与低音区间距1.5-2.5厘米为最佳，面板厚度高中低音的厚度一般为9mm、11mm、10mm，眼下面板大多数采用弦切工艺。",
        "\n将古筝平稳的放于古筝支架上面，方便人们坐着弹奏古筝。"
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
        if (textObject.text.Length > lengthOfOnePage)
        {
            textContentSizeFilter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
        else
        {
            textContentSizeFilter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        }
    }

    public void leftSwipeCallBack()
    {
        // +1
        int save_current = current;
        current = current == textArray.Length - 1 ? current : current + 1;
        SwipeCallBack();
    }

    public void rightSwipeCallBack()
    {
        // -1
        int save_current = current;
        current = current == 0 ? 0 : current - 1;
        if (current != save_current)
        {
            if (current != 0)
            {
                SwipeCallBack();
            }
            else
            {
                FaceOutGameObject(fadingSpeed);
            }
        }
    }

    void SwipeCallBack()
    {
        FaceOutGameObject(fadingSpeed);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(fadeDuration);
        textObject.text = textArray[current];
        updateAutoLayout();
        FadeInGameObject(fadingSpeed);
    }

    void FaceOutGameObject(float duration)
    {
        textObject.CrossFadeAlpha(0, duration, true);      // fade out
        imageObject.CrossFadeAlpha(0, duration, true);
        scrollbarImageObject.CrossFadeAlpha(0, duration, true);
        handleObject.CrossFadeAlpha(0, duration, true);
    }

    void FadeInGameObject(float duration)
    {
        textObject.CrossFadeAlpha(1, duration, true);      // fade in
        imageObject.CrossFadeAlpha(1, duration, true);
        scrollbarImageObject.CrossFadeAlpha(1, duration, true);
        handleObject.CrossFadeAlpha(1, duration, true);
    }
}
