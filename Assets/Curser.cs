using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curser : MonoBehaviour
{
    CameraRayCast camRay;
    // Start is called before the first frame update
    void Start()
    {
        camRay = GetComponent<CameraRayCast>();
    }

    // Update is called once per frame
    void Update()
    {
        print(camRay.m_LayerHit);
        print(camRay.m_hit);
    }
}
