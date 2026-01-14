using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeIn2 : MonoBehaviour
{


    public Color targetColor = Color.white;
    public AnimationCurve fadeCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.6f, 0.7f, -1.8f, -1.2f), new Keyframe(1, 0));

    private float alpha = 0f;
    private Texture2D texture;
    private bool isDone;
    private float time;

    public void Reset()
    {
        isDone = false;
        alpha = 1f;
        time = 0f;
    }

    private void Awake()
    {
        Reset();
    }

    private void OnGUI()
    {

        if (texture == null) texture = new Texture2D(1, 1);

        Color currentColor = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
        texture.SetPixel(0, 0, currentColor);
        texture.Apply();

        time += Time.deltaTime;
        alpha = fadeCurve.Evaluate(time);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

        if (alpha >= 1f) isDone = true;
    }
}




