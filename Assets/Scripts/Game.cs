using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Game : MonoBehaviour
{
    public Car car;
    public UI ui;
    public GameObject dest;

    void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        car.OnInit(OnCarStop, OnCarOut, UpdateCarInfo);
        ui.OnInit(OnPowerEnd);

        UpdateCarInfo();
    }

    void OnPowerEnd(float powerTime)
    {
        car.AddForceByTime(powerTime);
    }

    void UpdateCarInfo()
    {
        var len = dest.transform.position.z - car.transform.position.z;
        ui.UpdateLength(Mathf.RoundToInt(len * 10));
    }

    void OnCarStop()
    {
        OnResult(false);
    }

    void OnCarOut()
    {
        OnResult(true);
    }

    void OnResult(bool isOut)
    {

    }
}