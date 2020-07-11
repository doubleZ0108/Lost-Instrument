using UnityEngine;
using System.Collections;

public class DemoScript : MonoBehaviour {
    //name of the scene you want to load
    public string scene;
	public Color loadToColor = Color.white;
	
	public void GoFade(string nameScene = "")
    {
        if (nameScene == "")
        {
            Initiate.Fade(scene, loadToColor, 1.0f);
        }
        else
        {
            Initiate.Fade(nameScene, loadToColor, 1.0f);
        }
    }
}
