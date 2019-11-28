using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UI : MonoBehaviour
{
    public Text txtStartTip;
    public Text txtTime;
    public Text txtLength;
    public UIHoldButton btnHold;
    private float holdStartTimePoint = 0;
    private double holdTimeDelta = 0;
    private Action<float> onPowerEnd;

    void Start()
    {
        btnHold.RegisterEvent(OnHoldStart, OnHoldEnd);
        txtStartTip.transform.DOScale(1.1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
    public void OnInit(Action<float> powerEnd)
    {
        onPowerEnd = powerEnd;
    }
    void OnHoldStart()
    {
        txtStartTip.transform.DOKill();
        txtStartTip.gameObject.SetActive(false);


        txtTime.color = Color.white;
        txtTime.fontSize = 72;
        txtTime.rectTransform.anchoredPosition = new Vector2(-269.05f, -324);
        holdStartTimePoint = Time.time;
    }
    void OnHoldEnd()
    {
        var dt = 1f;
        var startTime = Time.time;
        var endTime = startTime + dt;
        txtTime.rectTransform.DOAnchorPos(Vector2.zero, dt).OnUpdate(() =>
        {
            var now = Time.time;
            var cur = now = startTime;
            var p = cur / dt;
            var r = Mathf.Lerp(txtStartTip.fontSize, 46, p);
            txtTime.fontSize = Mathf.RoundToInt(r);
        });

        Color yel;
        ColorUtility.TryParseHtmlString("#FFD049", out yel);
        txtTime.color = yel;

        btnHold.gameObject.SetActive(false);

        if (onPowerEnd != null)
            onPowerEnd((float)holdTimeDelta);
    }

    void Update()
    {
        if (btnHold.IsPointerDown)
        {
            holdTimeDelta = Time.time - holdStartTimePoint;
            txtTime.text = Convert.ToDouble(holdTimeDelta).ToString("0.00");
        }
    }

    public void UpdateLength(int m)
    {
        txtLength.text = m.ToString();
    }

    public void OnResult(bool isOut, float distance, float total)
    {
        var t1 = "用力过猛！";
        var t2 = "在开玩笑吗？";
        var t3 = "朋友，差距有点大啊！";
        var t4 = "不错，举例目标不远了！";
        var t5 = "太棒了！犹如神算！";
    }
}
