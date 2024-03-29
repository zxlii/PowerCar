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
        car.OnInit(ui, dest);
        ui.OnInit(car);

        Main.Instance.PlayBGM();
    }

    public void Restart()
    {
        OnInit();
    }

}