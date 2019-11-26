using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    static Vector3 POS = Vector3.zero;
    private Rigidbody rgd;
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
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
        // return !rgd.IsSleeping();
        return rgd.velocity.magnitude > 0.01f;
    }
}
