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

    void Awake()
    {

    }

    void Start()
    {
        ui.onPowerEnd = OnPowerEnd;
    }

    void OnPowerEnd(float powerTime)
    {
        car.AddForceByTime(powerTime);
    }

    void Update()
    {
        if (car.IsMoving())
        {
            var len = dest.transform.position.z - car.transform.position.z;
            ui.UpdateLength(Mathf.RoundToInt(len * 10));
        }
    }
}