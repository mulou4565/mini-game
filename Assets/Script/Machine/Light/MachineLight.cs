using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class MachineLight : Machine
{
    public GameObject Light;
    public GameObject MainCamera;
    public GameObject LightCamera;


    // Update is called once per frame
    public override void Run()
    {
        if (!Light.activeSelf)
        {
            Light.SetActive(true);
            CameraMove();
            Invoke("CameraBack", 5);
            Invoke("StopLight", 15);
        }
    }

    public void CameraMove()
    {
        MainCamera.SetActive(false);
        LightCamera.SetActive(true);
    }

    public void CameraBack()
    {
        MainCamera.SetActive(true);
        LightCamera.SetActive(false);
    }

    public void StopLight()
    {
        Light.SetActive(false);
    }
}
