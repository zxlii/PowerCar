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

    public void AddForce(float val)
    {
        rgd.AddForce(Vector3.forward * val);
    }

    void Update()
    {
        POS = transform.position;
        POS.x = 0;
        transform.position = POS;
    }
}
