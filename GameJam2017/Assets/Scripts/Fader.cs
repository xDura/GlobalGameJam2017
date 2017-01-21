using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fader : MonoBehaviour {

    public Texture2D fadeOutTexture;
    static Color currentColor;
    public Color startColor;
    Rect rect;

	void Awake () {
        currentColor = startColor;
        DOTween.Init(false, true, LogBehaviour.Default);
        FadeIn();
    }
	
	void Update () {        
    }

    public static void FadeOut()
    {
        DOTween.To(() => currentColor, x => currentColor = x, new Color(1, 1, 1, 1.0f), 2).SetOptions(true).SetEase(Ease.InOutCubic);
    }

    public static void FadeIn()
    {
        DOTween.To(() => currentColor, x => currentColor = x, new Color(1, 1, 1, 0.0f), 2).SetOptions(true).SetEase(Ease.InOutCubic);
    }

    void OnGUI()
    {
        GUI.color = currentColor;
        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), fadeOutTexture);
    }

    //public Color GetUIColor()
    //{
    //    return GUI.color;
    //}

    //public void SetColorAlpha(Color color)
    //{
    //    GUI.color = color;
    //    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    //}
}
