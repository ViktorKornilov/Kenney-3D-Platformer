using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Unity Singleton

public class Transition : Singleton<Transition>
{
    Image image;

    private void Awake()
    {
        var canvas = FindAnyObjectByType<Canvas>();
        transform.SetParent(canvas.transform, false);

        image = gameObject.AddComponent<Image>();
        image.color = Color.black.Alpha(0);

        // full screen
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
    }

    public static async Task FadeIn(float duration = 0.5f)
    {
        Instance.image.color = Instance.image.color.Alpha(0);
        await Instance.image.DOFade(1, duration).AsyncWaitForCompletion();
    }

    public static async Task FadeOut(float duration = 0.5f)
    {
        Instance.image.color = Instance.image.color.Alpha(1);
        await Instance.image.DOFade(0, duration).AsyncWaitForCompletion();
    }

    public static async Task FlipIn(float duration = 0.5f)
    {
        Instance.image.color = Instance.image.color.Alpha(1);

        // image motion pan from right to left with anchor and start value
        await Instance.image.rectTransform.DOAnchorMax(Vector2.one, duration)
            .ChangeStartValue(new Vector2(0,1))
            .SetEase( Ease.OutExpo)
            .AsyncWaitForCompletion();
    }

    public static async Task FlipOut(float duration = 0.5f)
    {
        Instance.image.color = Instance.image.color.Alpha(1);
        // image motion pan from right to left with anchor and start value
        await Instance.image.rectTransform.DOAnchorMax(new Vector2(0,1), duration)
            .ChangeStartValue(Vector2.one)
            .SetEase( Ease.InExpo )
            .AsyncWaitForCompletion();
    }

    public static Color Color
    {
        get => Instance.image.color;
        set => Instance.image.color = value;
    }
}