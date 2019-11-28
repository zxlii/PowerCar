using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Car : MonoBehaviour
{
    static Vector3 POS = Vector3.zero;
    private Rigidbody rgd;
    private Action onCarStop;
    private Action onCarOut;
    private Action onUpdateCarInfo;
    private bool isCarOut = false;
    private bool isMoving = false;
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    public void OnInit(Action carStop, Action carOut, Action updateCarInfo)
    {
        onCarStop = carStop;
        onCarOut = carOut;
        onUpdateCarInfo = updateCarInfo;
    }
    public void AddForceByTime(float holdTime)
    {
        AddForce(holdTime * holdTime * 15f);
    }

    public void AddForce(float val)
    {
        rgd.AddForce(Vector3.forward * val);
    }

    public bool IsMoving()
    {
        return rgd.velocity.magnitude > 0.01f;
    }

    void Update()
    {
        if (isCarOut) return;
        var pos = transform.position;
        isCarOut = pos.y < -2;
        if (isCarOut && onCarOut != null)
            onCarOut();

        var moving = IsMoving();
        if (moving && onUpdateCarInfo != null)
            onUpdateCarInfo();

        if (isMoving && !moving && onCarStop != null)
            onCarStop();

        isMoving = moving;
    }
}
