using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineRotationMove :Machine
{
    // Start is called before the first frame update
    public float rospeed;
    Coroutine rotation;
    Vector3 pos;
    private void Awake()
    {
        rotation = null;

    }
    public void SetPos(Vector3 nowPos) { pos = nowPos; }
    public void ro()
    {

        if (rotation == null) { Debug.Log(1); rotation = StartCoroutine(startRotation(pos)); }

    }
    IEnumerator startRotation(Vector3 position)
    {
        float x = 90;
        while (x > 0)
        {
            x -= Mathf.Abs(rospeed);
            //rb.rotation += rospeed;
            transform.RotateAround(pos, Vector3.forward, rospeed);
            yield return new WaitForSeconds(0.1f);
        }
        rotation = null;
    }
    public override void Run()
    {
        ro();
    }
}
