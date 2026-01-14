using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIImageFadeOut : MonoBehaviour
{

    public float delayBeforeFade = 1f;   
    public float fadeDuration = 2f;      

    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        if (delayBeforeFade > 0f)
            yield return new WaitForSeconds(delayBeforeFade);

        Color startColor = img.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            img.color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        img.color = endColor;
    }
}