using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI : MonoBehaviour
{
    public Text txtStartTip;
    public Text txtTime;
    public Text txtLength;
    public Image imgProgress;
    public UIHoldButton btnHold;
    public GameObject objFail;
    public GameObject objSuccess;

    public Vector2 timeOrigin;

    private float holdStartTimePoint = 0;
    private float holdTimeDelta = 0;
    private int result = 0;
    private Car car;

    public Transform[] transforms;

    void Start()
    {
        OnCreate();
    }

    private void OnCreate()
    {
        timeOrigin = txtTime.rectTransform.anchoredPosition;
        btnHold.RegisterEvent(OnHoldStart, OnHoldEnd);
    }

    public void OnInit(Car c)
    {
        result = 0;

        car = c;

        objFail.SetActive(false);
        objSuccess.SetActive(false);
        btnHold.gameObject.SetActive(true);
        txtStartTip.gameObject.SetActive(true);
        txtStartTip.transform.localScale = Vector3.one;
        txtStartTip.transform.DOScale(1.1f, 1f).SetLoops(-1, LoopType.Yoyo);
        UpdateCarInfo();
    }



    public void UpdateCarInfo()
    {
        var cur = car.GetLengthToDest();
        var max = car.GetMaxLength();
        txtLength.text = cur.ToString("0.0");

        var per = cur / max;
        imgProgress.fillAmount = per;


        Color col = Color.red;
        ColorUtility.TryParseHtmlString("#BF4C25", out col);
        if (cur > 0)
        {
            col = Color.white;

            var colors = new string[] { "#FFFFFF", "#9BEF8F", "#597EDE", "#913FEA" };
            var levels = new float[] { 0.5f, 0.1f, 0.03f, 0 };
            for (int i = 0; i <= 3; i++)
            {
                if (per > levels[i])
                {
                    result = i;
                    break;
                }
            }
            ColorUtility.TryParseHtmlString(colors[result], out col);
        }
        txtLength.color = col;
    }
    void OnHoldStart()
    {
        txtStartTip.transform.DOKill();
        txtStartTip.gameObject.SetActive(false);

        txtTime.color = Color.white;
        txtTime.fontSize = 100;
        txtTime.rectTransform.anchoredPosition = txtStartTip.rectTransform.anchoredPosition;
        holdStartTimePoint = Time.time;
    }
    void OnHoldEnd()
    {
        var dt = 1f;
        var startTime = Time.time;
        var endTime = startTime + dt;
        var size = txtStartTip.fontSize;
        txtTime.rectTransform.DOAnchorPos(timeOrigin, dt).OnUpdate(() =>
        {
            var now = Time.time;
            var cur = now = startTime;
            var p = cur / dt;
            var r = Mathf.Lerp(size, 68, p);
            txtTime.fontSize = Mathf.RoundToInt(r);
        });

        Color yel;
        ColorUtility.TryParseHtmlString("#FFD049", out yel);
        txtTime.color = yel;

        btnHold.gameObject.SetActive(false);

        if (holdTimeDelta < .3f) holdTimeDelta = .3f;
        car.AddForceByTime(holdTimeDelta);
    }

    void Update()
    {
        if (btnHold.IsPointerDown)
        {
            holdTimeDelta = Time.time - holdStartTimePoint;
            txtTime.text = holdTimeDelta.ToString("0.00");
        }
    }

    public void OnCarStop()
    {
        Main.Instance.StopBGM();
        var tips = new string[] { "跑了还没一半！", "恭喜过关！", "距离目标不远了！", "太棒了！犹如神算！" };
        var tip = tips[result];
        if (result == 0)
        {
            objFail.SetActive(true);
            var txtTip = objFail.transform.Find("txtFail").GetComponent<Text>();
            txtTip.text = tip;
        }
        else if (result > 0)
        {
            Main.Instance.PlaySound("Win");
            objSuccess.SetActive(true);
            var txtTip = objSuccess.transform.Find("txtTip").GetComponent<Text>();
            txtTip.text = tip;

            var txtScore = objSuccess.transform.Find("txtScore").GetComponent<Text>();
            txtScore.text = string.Format("获得金币 {0}", result * 100);

            GameUtils.TweenScaleOneByOne(transforms);

        }
    }

    public void OnCarOut()
    {
        Main.Instance.StopBGM();
        objFail.SetActive(true);
        var txtTip = objFail.transform.Find("txtFail").GetComponent<Text>();
        txtTip.text = "用力过猛！";
    }

    public void OnClickOnmore()
    {
        Main.Instance.PlaySound("ButtonClick");
        Main.Instance.game.Restart();
    }

    public void OnResult(bool isOut, float distance, float total)
    {
        var t1 = "用力过猛！";
        var t2 = "在开玩笑吗？";
        var t3 = "恭喜过关！";
        var t4 = "距离目标不远了！";
        var t5 = "太棒了！犹如神算！";
    }
}
