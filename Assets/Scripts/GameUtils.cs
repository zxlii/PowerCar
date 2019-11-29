using System;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Globalization;
public static class GameUtils
{
    static Vector3[] s_Corners = new Vector3[4];
    static Vector3 s_Min = Vector3.zero;
    static Vector3 s_Max = Vector3.zero;
    public static void TweenScaleOneByOne(Transform[] trans, Vector3 start, Vector3 end, float duration, Ease ease = DG.Tweening.Ease.Linear, Action onComplete = null)
    {
        Sequence seq = DOTween.Sequence();
        foreach (var tran in trans)
        {
            tran.localScale = start;
            seq.Append(tran.DOScale(end, duration).SetEase(ease));
        }
        seq.Play().OnComplete(() =>
        {
            seq.Kill(true);
            if (onComplete != null)
                onComplete();
        });
    }
    public static void TweenScaleOneByOne(Transform[] trans, Action onComplete = null)
    {
        TweenScaleOneByOne(trans, Vector3.zero, Vector3.one, .3f, DG.Tweening.Ease.OutBack, onComplete);
    }

    public static void TweenImageAlpha(Image image, float[] alphas, float singleTime, bool isLoop, float delay = 0f, Action onComplete = null)
    {
        Sequence seq = DOTween.Sequence();
        seq.SetId(image.gameObject);
        foreach (var alpha in alphas)
            seq.Append(image.DOFade(alpha, singleTime));

        if (isLoop)
        {
            seq.Play().SetLoops(int.MaxValue, LoopType.Yoyo).SetDelay(delay);
        }
        else
        {
            seq.Play().SetDelay(delay).OnComplete(() =>
            {
                seq.Kill(true);
                if (onComplete != null)
                    onComplete();
            });
        }
    }

    public static bool Vector2Equals(Vector2 a, Vector2 b)
    {
        return Mathf.RoundToInt(a.x) == Mathf.RoundToInt(b.x) && Mathf.RoundToInt(a.y) == Mathf.RoundToInt(b.y);
    }

    private static void ClearMinMax()
    {
        for (int i = 0; i < 3; i++)
        {
            s_Min[i] = float.MaxValue;
            s_Max[i] = float.MinValue;
        }
    }
    public static Bounds GetRectWordBounds(RectTransform rect)
    {
        ClearMinMax();
        rect.GetWorldCorners(s_Corners);
        for (int j = 0; j < 4; j++)
        {
            Vector3 v = s_Corners[j];
            s_Min = Vector3.Min(v, s_Min);
            s_Max = Vector3.Max(v, s_Max);
        }
        var bounds = new Bounds(s_Min, Vector3.zero);
        bounds.Encapsulate(s_Max);
        return bounds;
    }
    public static Bounds GetBounds(RectTransform rootRect, RectTransform childRect)
    {
        ClearMinMax();
        childRect.GetWorldCorners(s_Corners);
        for (int j = 0; j < 4; j++)
        {
            Vector3 v = rootRect.worldToLocalMatrix.MultiplyPoint3x4(s_Corners[j]);
            s_Min = Vector3.Min(v, s_Min);
            s_Max = Vector3.Max(v, s_Max);
        }

        var bounds = new Bounds(s_Min, Vector3.zero);
        bounds.Encapsulate(s_Max);
        return bounds;
    }


    // 让bounds不超出maxBounds指定的范围
    public static void ClamMaxBounds(Bounds maxBounds, ref Bounds bounds)
    {
        var min = bounds.min;
        var max = bounds.max;
        for (int i = 0; i < 2; i++)
        {
            if (min[i] > maxBounds.min[i])
                min[i] = maxBounds.min[i];
            if (max[i] < maxBounds.max[i])
                max[i] = maxBounds.max[i];
        }
        bounds.SetMinMax(min, max);
    }

    public static DateTime GetTimeFromString(string time)
    {
        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo
        {
            ShortDatePattern = "yyyy/MM/dd"
        };
        string defaultTime = DateTime.Now.ToString("yyyy/MM/dd");
        if (string.IsNullOrEmpty(time) || time == "0")
        {
            return Convert.ToDateTime(defaultTime, dtFormat);
        }
        DateTime dateTime = Convert.ToDateTime(time, dtFormat);
        return dateTime;
    }

    public static string GetTimeNow()
    {
        return DateTime.Now.ToString("yyyy/MM/dd");
    }

    //指定目标的特效，从指定位置产生金币飞向右上角金币图标位置
   
}