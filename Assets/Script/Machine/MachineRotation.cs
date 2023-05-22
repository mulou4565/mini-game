using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MachineRotation : Machine
{
    // Start is called before the first frame update
    public float rospeed;
    Coroutine rotation;
    public CinemachineVirtualCamera cameras;
    private CinemachineBasicMultiChannelPerlin noise;
    private void Awake()
    {
        rotation = null;
        noise = cameras.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ro()
    {
       
        if (rotation == null) { Debug.Log(1); rotation = StartCoroutine(startRotation()); }

    }
    IEnumerator startRotation()
    {
        float x = 90;
        noise.m_AmplitudeGain = 5;
        while (x > 0)
        {
            x -= Mathf.Abs(rospeed);
            //rb.rotation += rospeed;
            Vector3 nowro = transform.rotation.eulerAngles;
            nowro += new Vector3(0, 0, rospeed);
            transform.rotation = Quaternion.Euler(nowro);
            yield return new WaitForSeconds(0.1f);
        }
        noise.m_AmplitudeGain = 0;
        rotation = null;
    }
    public override void Run()
    {
        ro();
    }
}
