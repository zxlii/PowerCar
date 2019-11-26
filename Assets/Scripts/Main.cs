using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Main : MonoBehaviour
{
    public static Main Instance;

    public Game game;

    void Awake()
    {
        Instance = this;
    }

}
