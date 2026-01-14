using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class FadeCameraToBlack : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    [Range(0f, 1f)]
    [SerializeField] private float startAlpha = 0f;

    public enum StartFadeMode { None, ToBlack, FromBlack }

    [SerializeField] private StartFadeMode startFadeMode = StartFadeMode.None;
    [SerializeField] private float startDelaySeconds = 0f;
    [SerializeField] private float startFadeDurationSeconds = 2f;
    [SerializeField] private bool useUnscaledTime = true;

    private Canvas _canvas;
    private Image _fadeImage;
    private Coroutine _fadeRoutine;

    private void Reset()
    {
        targetCamera = GetComponentInParent<Camera>();
        if (targetCamera == null) targetCamera = Camera.main;
    }

    private void Awake()
    {
        if (targetCamera == null)
        {
            Debug.LogError($"{nameof(FadeCameraToBlack)}: target Camera not assigned.", this);
            enabled = false;
            return;
        }

        BuildOverlay();
        SetAlpha(startAlpha);
    }

    private void Start()
    {
        if (startFadeMode != StartFadeMode.None)
        {
            if (_fadeRoutine != null) StopCoroutine(_fadeRoutine);
            _fadeRoutine = StartCoroutine(AutoStartFadeRoutine());
        }
    }


    public void FadeToBlack(float duration)
    {
        StartFade(target: 1f, duration: duration);
    }

    public void FadeFromBlack(float duration)
    {
        StartFade(target: 0f, duration: duration);
    }

    public void SetFade(float alpha01)
    {
        if (_fadeRoutine != null) StopCoroutine(_fadeRoutine);
        SetAlpha(alpha01);
        _fadeRoutine = null;
    }

    private IEnumerator AutoStartFadeRoutine()
    {
        float d = Mathf.Max(0f, startDelaySeconds);

        if (d > 0f)
        {
            if (useUnscaledTime) yield return new WaitForSecondsRealtime(d);
            else yield return new WaitForSeconds(d);
        }

        float duration = Mathf.Max(0.0001f, startFadeDurationSeconds);

        if (startFadeMode == StartFadeMode.ToBlack)
            yield return FadeRoutine(target: 1f, duration: duration, unscaled: useUnscaledTime);
        else if (startFadeMode == StartFadeMode.FromBlack)
            yield return FadeRoutine(target: 0f, duration: duration, unscaled: useUnscaledTime);

        _fadeRoutine = null;
    }

    private void StartFade(float target, float duration)
    {
        if (_fadeRoutine != null) StopCoroutine(_fadeRoutine);
        _fadeRoutine = StartCoroutine(FadeRoutine(target, Mathf.Max(0.0001f, duration), useUnscaledTime));
    }

    private IEnumerator FadeRoutine(float target, float duration, bool unscaled)
    {
        float start = _fadeImage.color.a;
        float t = 0f;

        while (t < duration)
        {
            t += unscaled ? Time.unscaledDeltaTime : Time.deltaTime; 
            float a = Mathf.Lerp(start, target, t / duration);
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(target);
    }

    private void SetAlpha(float a)
    {
        if (_fadeImage == null) return;
        Color c = _fadeImage.color;
        c.a = Mathf.Clamp01(a);
        _fadeImage.color = c;
    }

    private void BuildOverlay()
    {

        GameObject root = new GameObject($"FadeOverlay_{targetCamera.name}");
        root.transform.SetParent(transform, false);


        _canvas = root.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = targetCamera;
        _canvas.planeDistance = Mathf.Max(0.1f, targetCamera.nearClipPlane + 0.5f);


        _canvas.sortingOrder = short.MaxValue;


        CanvasScaler scaler = root.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;

        root.AddComponent<GraphicRaycaster>();


        GameObject imgGO = new GameObject("FadeImage");
        imgGO.transform.SetParent(root.transform, false);

        _fadeImage = imgGO.AddComponent<Image>();
        _fadeImage.color = new Color(0f, 0f, 0f, 0f);
        _fadeImage.raycastTarget = false;

        RectTransform rt = _fadeImage.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}
