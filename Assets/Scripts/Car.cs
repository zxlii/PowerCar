using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Car : MonoBehaviour
{
    static Vector3 POS = Vector3.zero;
    private Rigidbody rgd;
    private bool isSetup = false;
    private bool isMoving = false;
    private bool isRoundOver = false;
    private GameObject dest;
    private UI ui;
    public Transform cam;
    private float maxLength = 0f;
    Vector3 camOrigin;
    void Awake()
    {
        OnCreate();
    }
    private void OnCreate()
    {
        camOrigin = cam.position;
        rgd = GetComponent<Rigidbody>();
    }
    public void OnInit(UI u, GameObject d)
    {
        ui = u;
        dest = d;

        isRoundOver = false;
        isMoving = false;
        isSetup = false;

        rgd.velocity = Vector3.zero;
        transform.localPosition = new Vector3(150, 0, 0);
        
        cam.position = camOrigin;

        maxLength = GetLengthToDest();
    }
    public void AddForceByTime(float holdTime)
    {
        var scale = holdTime * holdTime * 15f;
        var force = Vector3.forward * scale;
        rgd.AddForce(force);
        isSetup = true;
    }

    private void AddForce(float val)
    {
        rgd.AddForce(Vector3.forward * val);
    }

    private Vector3 cameraStay = Vector3.zero;
    void Update()
    {
        if (cam.position.z >= dest.transform.position.z - 10)
        {
            if (cameraStay == Vector3.zero)
                cameraStay = cam.position;
            cam.position = cameraStay;
        }

        if (!isSetup) return;
        if (isRoundOver) return;

        // Debug.Log("aaaaaaaa");

        // 停车逻辑
        var moving = rgd.velocity.magnitude > 0.00001f;
        if (moving)
        {
            // Debug.Log("bbbbbbbbb");
            ui.UpdateCarInfo();
        }
        else if (isMoving)
        {
            isRoundOver = true;
            ui.OnCarStop();
        }
        isMoving = moving;

        // 出界逻辑
        if (transform.position.y < -2)
        {
            isRoundOver = true;
            ui.OnCarOut();
        }
    }

    public float GetMaxLength()
    {
        return maxLength;
    }
    public float GetLengthToDest()
    {
        return dest.transform.position.z - transform.position.z;
    }

}
